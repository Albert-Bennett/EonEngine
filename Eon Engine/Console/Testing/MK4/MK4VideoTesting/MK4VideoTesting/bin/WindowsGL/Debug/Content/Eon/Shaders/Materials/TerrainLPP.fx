#include "Common.fxh"

float4x4 World;
float4x4 View;
float4x4 Proj;

float CamPos;

float TextureRepeats;

texture WeightMap;
sampler WeightSampler = sampler_state
{
	texture = <WeightMap>;
	MinFilter = ANISOTROPIC;
	MagFilter = LINEAR;
	MipFilter = LINEAR;
	AddressU = WRAP;
	AddressV = WRAP;
	MaxAnisotropy = 16;
};

texture R;
sampler RSampler = sampler_state
{
	texture = <R>;
	MinFilter = ANISOTROPIC;
	MagFilter = LINEAR;
	MipFilter = LINEAR;
	AddressU = WRAP;
	AddressV = WRAP;
	MaxAnisotropy = 16;
};

texture G;
sampler GSampler = sampler_state
{
	texture = <G>;
	MinFilter = ANISOTROPIC;
	MagFilter = LINEAR;
	MipFilter = LINEAR;
	AddressU = WRAP;
	AddressV = WRAP;
	MaxAnisotropy = 16;
};

texture B;
sampler BSampler = sampler_state
{
	texture = <B>;
	MinFilter = ANISOTROPIC;
	MagFilter = LINEAR;
	MipFilter = LINEAR;
	AddressU = WRAP;
	AddressV = WRAP;
	MaxAnisotropy = 16;
};

texture RN;
sampler RNSampler = sampler_state
{
	texture = <RN>;
	MinFilter = ANISOTROPIC;
	MagFilter = LINEAR;
	MipFilter = LINEAR;
	AddressU = WRAP;
	AddressV = WRAP;
	MaxAnisotropy = 16;
};

texture GN;
sampler GNSampler = sampler_state
{
	texture = <GN>;
	MinFilter = ANISOTROPIC;
	MagFilter = LINEAR;
	MipFilter = LINEAR;
	AddressU = WRAP;
	AddressV = WRAP;
	MaxAnisotropy = 16;
};

texture BN;
sampler BNSampler = sampler_state
{
	texture = <BN>;
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
	float3 Tan : TANGENT0;
	float3 BiTan : BINORMAL0;
	float3 Norm : NORMAL0;
};

struct VSOutput
{
	float4 Pos : POSITION0;
	float2 TexCoord : TEXCOORD0;
	float Depth : TEXCOORD1;
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
	float4 viewPos = mul(worldPos, View);
	output.Pos = mul(viewPos, Proj);

	output.Depth = output.Pos.z/ output.Pos.w;
	output.TexCoord = input.TexCoord;

	return output;
}

PSOutput PSFunct(VSOutput input)
{
	PSOutput output;

	float3 weight = tex2D(WeightSampler, input.TexCoord).rgb;
	float repeats = TextureRepeats;

	float2 texCoord = input.TexCoord * repeats;

	float3 r = tex2D(RSampler, texCoord).rgb;
	float3 g = tex2D(GSampler, texCoord).rgb;
	float3 b = tex2D(BSampler, texCoord).rgb;

	float3 colour = weight.r * r + weight.g * g + weight.b * b;

	output.Colour.rgb = colour;
	output.Colour.a = 1.0f;

	half3 rNorm = tex2D(RNSampler, texCoord).xyz;
	half3 gNorm = tex2D(GNSampler, texCoord).xyz;
	half3 bNorm = tex2D(BNSampler, texCoord).xyz;

	half3 normal = weight.r * rNorm + weight.g * gNorm + weight.b * bNorm;

	output.Opaque.rgb = normal;
	output.Opaque.a = 0.0f;

	output.Depth = 0.0f;
	output.Depth.rg = PackDepth(input.Depth);

	return output;
}

technique Opaque
{
    pass P0
    {
        VertexShader = compile vs_3_0 VSFunct();
        PixelShader = compile ps_3_0 PSFunct();
    }
}

