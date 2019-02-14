#include "Common.fxh"

float4x4 World;
float4x4 ViewProj;

float4x4 IViewProj;

float2 HalfPixel;

float3 Pos;
float4 Colour;
float Intensity;

float3 Direction;
float FallOff;
float OuterConeAngle;
float InnerConeAngle;

texture Opaque;
sampler OpaqueSampler = sampler_state
{
	Texture = <Opaque>;
	AddressU = CLAMP;
	AddressV = CLAMP;
	MagFilter = POINT;
	MinFilter = POINT;
};

texture DepthMap;
sampler DepthSampler = sampler_state
{
	Texture = <DepthMap>;
	AddressU = CLAMP;
	AddressV = CLAMP;
	MagFilter = POINT;
	MinFilter = POINT;
};

struct VSInput
{
	float4 Pos: POSITION0;
};

struct VSOutput
{
	float4 Pos : POSITION0;
	float4 ScreenPos : TEXCOORD0;
};

VSOutput VSFunct(VSInput input)
{
	VSOutput output;

	float4 worldPos = mul(input.Pos, World);
	output.Pos = mul(worldPos, ViewProj);

	output.ScreenPos = output.Pos;

	return output;
}

float4 PXFunct(VSOutput input) : COLOR0
{
	input.ScreenPos.xy /= input.ScreenPos.w;

	float2 uv = 0.5f * (float2(input.ScreenPos.x,
		-input.ScreenPos.y) + 1) - HalfPixel;

	float4 depthSample = tex2D(DepthSampler, uv);

	float depth = depthSample.r;
	clip(-depth + 0.9999f);

	if(depth > 0.0f)
	{
		float4 pos = 1.0f;
		pos.xy = input.ScreenPos.xy;
		pos.z = depth;

		pos = mul(pos, IViewProj);
		pos /= pos.w;

		float3 lightVec = Pos - pos;
		lightVec = normalize(lightVec);

		float direct = dot(Direction, -lightVec);
		float outerAngle = cos(OuterConeAngle);
		float innerAngle = cos(InnerConeAngle);

		float attenuation = (direct >= outerAngle) * 
		pow((direct - outerAngle) / (innerAngle - outerAngle), FallOff);

		clip(attenuation - 0.0001f);

		if(attenuation > 0.0f)
		{
			float4 opaqueSample = tex2D(OpaqueSampler, uv);

			half3 norm = DecodeNormal(opaqueSample.rg);

			float specPow = opaqueSample.b * 255.0f;
			float shininess = opaqueSample.a * 255.0f;

			float nl = max(0, dot(norm, lightVec));
			clip(nl - 0.0001f);

			float3 diffuse = nl * (Colour * Intensity);

			float3 h = normalize(reflect(lightVec, norm));
			float spec = nl * pow(saturate(dot(normalize(pos), h)), specPow);

			return attenuation * float4(diffuse, spec * shininess);
		}
	}

	return 0.0f;
}

technique Lighting
{
    pass P0
    {
        VertexShader = compile vs_3_0 VSFunct();
        PixelShader = compile ps_3_0 PXFunct();
    }
}
