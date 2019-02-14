#include "Common.fxh"

float4x4 World;
float4x4 View;
float4x4 Proj;

float FlowOffset0;
float FlowOffset1;

float HalfCycle;
float2 GDSize;

texture Texture;
sampler TextureSampler = sampler_state
{
	texture = <Texture>;
	MinFilter = ANISOTROPIC;
	MagFilter = LINEAR;
	MipFilter = LINEAR;
	MaxAnisotropy = 16;
};

texture NoiseMap;
sampler NoiseSampler = sampler_state
{
	texture = <NoiseMap>;
	MinFilter = ANISOTROPIC;
	MagFilter = LINEAR;
	MipFilter = LINEAR;
	MaxAnisotropy = 16;
};

texture FlowMap;
sampler FlowSampler = sampler_state
{
	texture = <FlowMap>;
	MinFilter = ANISOTROPIC;
	MagFilter = LINEAR;
	MipFilter = LINEAR;
	MaxAnisotropy = 16;
};

texture NormalMap0;
sampler N0Sampler = sampler_state
{
	texture = <NormalMap0>;
	MinFilter = ANISOTROPIC;
	MagFilter = LINEAR;
	MipFilter = LINEAR;
	MaxAnisotropy = 16;
};

texture NormalMap1;
sampler N1Sampler = sampler_state
{
	texture = <NormalMap1>;
	MinFilter = ANISOTROPIC;
	MagFilter = LINEAR;
	MipFilter = LINEAR;
	MaxAnisotropy = 16;
};

texture SpecularMap;
sampler SpecularSampler = sampler_state
{
	texture = <SpecularMap>;
	MinFilter = ANISOTROPIC;
	MagFilter = LINEAR;
	MipFilter = LINEAR;
	MaxAnisotropy = 16;
};

struct VSInput
{
	float4 Pos : POSITION0;
	float2 TexCoord : TEXCOORD0;
	float3 Tan : TANGENT0;
	float3 BiTan : BINORMAL0;
	float3 Norm : NORMAL0;
};

struct VSOutput
{
	float4 Pos : SV_POSITION;
	float2 TexCoord : TEXCOORD0;
	float Depth : TEXCOORD1;
	float3x3 TBN : TEXCOORD2;
};

struct PSOutput
{
	float4 Depth : COLOR0;
	float4 Norm : COLOR1;
	float4 Colour : COLOR2;
};

VSOutput VSFunct(VSInput input)
{
	VSOutput output;

	float4 worldPos = mul(input.Pos, World);
	output.Pos = mul(worldPos, mul(View, Proj));

	output.Depth = output.Pos.z/ output.Pos.w;

	output.TBN = mul(float3x3(input.Tan,
		input.BiTan, input.Norm), World);

	output.TexCoord = input.TexCoord;

	return output;
}

PSOutput PSFunct(VSOutput input)
{
	PSOutput output;

	half2 flow = 2.0f * tex2D(FlowSampler, input.TexCoord).rg - 1.0f;
	half cycle = tex2D(NoiseSampler, input.TexCoord).r;

	float p0 = cycle * 0.5f + FlowOffset0;
	float p1 = cycle * 0.5f + FlowOffset1;

	half3 n0 = tex2D(N0Sampler, (input.TexCoord * GDSize) + flow * p0).rgb;
	half3 n1 = tex2D(N1Sampler, (input.TexCoord * GDSize) + flow * p1).rgb;

	half l = abs(HalfCycle - FlowOffset0) / HalfCycle;

	half3 normal = mul(lerp(n0, n1, l), input.TBN);
	normal = 0.5f * (normal + 1.0f);
	output.Norm.rg = EncodeNormal(normal);

	float4 spec = tex2D(SpecularSampler, input.TexCoord);
	output.Norm.ba = spec.rg;

	output.Depth = float4(input.Depth, spec.b, spec.a, 1.0f);

	output.Colour = tex2D(TextureSampler, input.TexCoord);
	output.Colour.a = 1.0f;

	return output;
}

technique Opaque
{
	pass P0
	{
		VertexShader = compile vs_2_0 VSFunct();
		PixelShader = compile ps_2_0 PSFunct();
	}
}


