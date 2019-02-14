#include "Common.fxh"

float4x4 World;
float4x4 View;
float4x4 Proj;

bool MaskBlur = false;

texture Texture;
sampler TextureSampler = sampler_state
{
	texture = <Texture>;
	MinFilter = ANISOTROPIC;
	MagFilter = LINEAR;
	MipFilter = LINEAR;
	AddressU = WRAP;
	AddressV = WRAP;
	MaxAnisotropy = 16;
};

texture Texture2;
sampler Texture2Sampler = sampler_state
{
	texture = <Texture2>;
	MinFilter = ANISOTROPIC;
	MagFilter = LINEAR;
	MipFilter = LINEAR;
	AddressU = WRAP;
	AddressV = WRAP;
	MaxAnisotropy = 16;
};

texture NormalMap;
sampler NormalSampler = sampler_state
{
	texture = <NormalMap>;
	MinFilter = ANISOTROPIC;
	MagFilter = LINEAR;
	MipFilter = LINEAR;
	AddressU = WRAP;
	AddressV = WRAP;
	MaxAnisotropy = 16;
};

texture SpecularMap;
sampler SpecularSampler = sampler_state
{
	texture = <SpecularMap>;
	MinFilter = ANISOTROPIC;
	MagFilter = LINEAR;
	MipFilter = LINEAR;
	AddressU = WRAP;
	AddressV = WRAP;
	MaxAnisotropy = 16;
};

struct VSInput
{
	float4 Pos : POSITION0;
	float2 TexCoord : TEXCOORD0;
	float3 Tan : TANGENT;
	float3 BiTan : BINORMAL;
	float3 Norm : NORMAL;
};

struct VSOutput
{
	float4 Pos : POSITION0;
	float2 TexCoord : TEXCOORD0;
	float Depth : TEXCOORD1;
	float3x3 TBN : TEXCOORD2;
};

struct PSOutput
{
	float4 Opaque : COLOR0;
	float4 Depth : COLOR1;
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

	half3 normal = tex2D(NormalSampler, input.TexCoord).xyz;

	float3 colour = tex2D(TextureSampler, input.TexCoord).rgb;
	float4 colour2 = tex2D(Texture2Sampler, input.TexCoord);

	output.Depth = 0.0f;
	output.Depth.rg = PackDepth(input.Depth);
	output.Depth.ba = tex2D(SpecularSampler, input.TexCoord).rg;

	output.Opaque.rgb = CalculateNormal(normal, input.TBN);
	output.Opaque.a = 0.0f;

	output.Colour = float4(colour + (colour2.rgb * colour2.a), 1.0f);

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


