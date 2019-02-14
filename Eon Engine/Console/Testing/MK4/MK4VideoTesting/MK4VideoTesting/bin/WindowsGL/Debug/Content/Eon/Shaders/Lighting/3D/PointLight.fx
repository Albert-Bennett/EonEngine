#include "Common.fxh"

float4x4 World;
float4x4 ViewProj;

float4x4 IViewProj;

float2 HalfPixel;

float3 Pos;
float3 Colour;
float Intensity;
float Radius;

texture Opaque;
sampler OpaqueSampler = sampler_state
{
	Texture = <Opaque>;
	AddressU = CLAMP;
	AddressV = CLAMP;
	MagFilter = LINEAR;
	MinFilter = LINEAR;
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
	input.ScreenPos /= input.ScreenPos.w;

	float2 uv = 0.5f * (float2(input.ScreenPos.x, 
		-input.ScreenPos.y) + 1.0f) + HalfPixel;

	float4 depthSample = tex2D(DepthSampler, uv);

	float depth = UnPackDepth(depthSample.rg);
	clip(-depth + 0.99999);

	float4 pos = float4(input.ScreenPos.xy, depth, 1.0f);
	pos = mul(pos, IViewProj);
	pos /= pos.w;

	float3 lightVec = Pos - pos.xyz;
	lightVec = normalize(lightVec);

	float attenuation = saturate(1.0f - length(lightVec) / (Radius/ 2));
	clip(attenuation - 0.0001f); 

	float3 norm = tex2D(OpaqueSampler, uv).rgb;

	float2 specular = depthSample.ba;
	specular.y *= 0.5f;

	float nl = max(0, dot(norm, lightVec));
	clip(nl - 0.0001f);

	float3 diffuse = nl * Colour.rgb;

	float3 h = normalize(reflect(lightVec, norm));
	float spec = nl * pow(saturate(dot(normalize(pos.xyz), h)), specular.x);

	return attenuation * Intensity * float4(diffuse, spec * specular.y);
}

technique Lighting
{
    pass P0
    {
        VertexShader = compile vs_2_0 VSFunct();
        PixelShader = compile ps_2_0 PXFunct();
    }
}
