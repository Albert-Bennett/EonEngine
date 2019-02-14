#include "Common.fxh"

float4x4 IViewProj;

float2 GDSize;

float4 Colour;
float Intensity;

float3 Direction;

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
	float3 Pos: POSITION0;
};

struct VSOutput
{
	float4 Pos : POSITION0;
	float4 ScreenPos : TEXCOORD0;
};

VSOutput VSFunct(VSInput input)
{
	VSOutput output;

	output.Pos = float4(input.Pos, 1);
	output.ScreenPos = output.Pos;

	return output;
}

float4 PXFunct(VSOutput input) : COLOR0
{
	input.ScreenPos.xy /= input.ScreenPos.w;

	float2 uv = 0.5f * (float2(input.ScreenPos.x, -input.ScreenPos.y) + 1)-
		float2(1.0 / GDSize);

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

		float4 opaqueSample = tex2D(OpaqueSampler, uv);

		half3 norm = DecodeNormal(opaqueSample.rg);

		float specPow = opaqueSample.b * 255.0f;
		float shininess = opaqueSample.a * 255.0f;

		float3 lightVec = -Direction;

		float nl = max(0, dot(norm, lightVec));
		float3 diffuse = nl * (Colour * Intensity);

		float3 h = normalize(reflect(lightVec, norm));
		float spec = nl * pow(saturate(dot(normalize(pos), h)), specPow);

		return float4(diffuse, spec * shininess);
	}

	return 0.0f;
}

technique Lighting
{
    pass P0
    {
        VertexShader = compile vs_2_0 VSFunct();
        PixelShader = compile ps_2_0 PXFunct();
    }
}