#include "Common.fxh"

float4x4 World;
float4x4 InvWorld;

float4x4 ViewProj;
float4x4 IViewProj;

float2 HalfPixel;

texture Texture;
sampler TextureSampler = sampler_state
{
	Texture = <Texture>;
	AddressU = CLAMP;
	AddressV = CLAMP;
	MinFilter = ANISOTROPIC;
	MagFilter = LINEAR;
	MipFilter = LINEAR;
	MaxAnisotropy = 16;
};

texture NormalMap;
sampler NormalSampler = sampler_state
{
	Texture = <NormalMap>;
	AddressU = CLAMP;
	AddressV = CLAMP;
	MinFilter = ANISOTROPIC;
	MagFilter = LINEAR;
	MipFilter = LINEAR;
	MaxAnisotropy = 16;
};

texture SpecularMap;
sampler SpecularSampler = sampler_state
{
	Texture = <SpecularMap>;
	AddressU = CLAMP;
	AddressV = CLAMP;
	MinFilter = ANISOTROPIC;
	MagFilter = LINEAR;
	MipFilter = LINEAR;
	MaxAnisotropy = 16;
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

struct VSOutput
{
	float4 Pos : SV_POSITION;
	float4 ScreenPos : TEXCOORD0;
};

struct PSOutput
{
	float4 Colour : COLOR0;
	float4 Opaque : COLOR1;
};

VSOutput VSFunct(float4 Pos : POSITION0)
{
	VSOutput output;

	float4 worldPos = mul(Pos, World);
	output.Pos = mul(worldPos, ViewProj);

	output.ScreenPos = output.Pos;

	return output;
}

PSOutput PSFunct(VSOutput input)
{
	PSOutput output;

	input.ScreenPos = input.ScreenPos/ input.ScreenPos.w;

	float2 uv = 0.5f * (float2(input.ScreenPos.x, 
		-input.ScreenPos.y) + 1.0f) - HalfPixel;

	float depth = tex2D(DepthSampler, uv).r;
	clip(-depth + 0.9999f);

	float4 pos = float4(input.ScreenPos.xy, depth, 1.0f);

	pos = mul(pos, IViewProj);
	pos /= pos.w;

	pos = mul(pos, InvWorld);

	clip(0.5f - abs(pos.xyz));

	float2 coord = pos.xy + 0.5f;

	output.Colour = tex2D(TextureSampler, coord);
	output.Opaque = 0.0f;

	return output;
}

technique SimpleRender
{
	pass P0
	{
		VertexShader = compile vs_2_0 VSFunct();
		PixelShader = compile ps_2_0 PSFunct();
	}
}

float3x3 CalculateCotangent(float3 pos, float2 uv)
{
	float3 norm = cross(normalize(ddx(pos)),
		normalize(ddy(pos)));

	float3 dp1 = ddx(pos);
	float3 dp2 = ddy(pos);

	float2 duv1 = ddx(uv);
	float2 duv2 = ddy(uv);

	float3 perp1 = cross(dp2, norm);
	float3 perp2 = cross(norm, dp1);

	float3 tan = perp2 * duv1.x + perp1 * duv2.x;
    float3 bitan = perp2 * duv1.y + perp1 * duv2.y;
 
    float inv = sqrt(max(dot(tan, tan), dot(bitan, bitan)));

    return float3x3(tan * inv, bitan * inv, norm);
}

PSOutput PSAdvFunct(VSOutput input)
{
	PSOutput output;

	input.ScreenPos = input.ScreenPos/ input.ScreenPos.w;

	float2 uv = 0.5f * (float2(input.ScreenPos.x, 
		-input.ScreenPos.y) + 1.0f) - HalfPixel;

	float depth = tex2D(DepthSampler, uv).r;
	clip(-depth + 0.9999f);

	float4 pos = float4(input.ScreenPos.xy, depth, 1.0f);

	pos = mul(pos, IViewProj);
	pos /= pos.w;

	pos = mul(pos, InvWorld);

	clip(0.5f - abs(pos.xyz));

	float2 coord = pos.xy + 0.5f;

	float4 colour = tex2D(TextureSampler, coord);

	clip(colour.a - 0.999f);

	output.Colour = colour;

	float3x3 tbn = CalculateCotangent(pos, uv);

	output.Opaque.rgb = CalculateNormal(tex2D(NormalSampler, coord).xyz, tbn);
	output.Opaque.a = 0.0f;

	return output;
}

technique AdvancedRender
{
	pass P0
	{
		VertexShader = compile vs_2_0 VSFunct();
		PixelShader = compile ps_3_0 PSAdvFunct();
	}
}


