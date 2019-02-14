#include "Common.fxh"

float4x4 World;
float4x4 View;
float4x4 Proj;

float FarPlane;
float CamPos;

int TextureRepeats;

texture WeightMap;
sampler WeightSampler= sampler_state
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
	float3 Norm : NORMAL0;
	float2 TexCoord : TEXCOORD0;
	float3 Tan : TANGENT0;
	float3 BiTan : BINORMAL0;
};

struct VSOutput
{
	float4 Pos : POSITION0;
	float2 TexCoord : TEXCOORD0;
	float Depth : TEXCOORD1;
	float3x3 BiTanNorm : TEXCOORD2;
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
	float4 viewPos = mul(worldPos, View);
	output.Pos = mul(viewPos, Proj);

	output.Depth = output.Pos.z/ output.Pos.w;

	float3x3 WorldView = mul(World, View);

	output.BiTanNorm[0] = normalize(mul(input.Tan, WorldView));
	output.BiTanNorm[1] = normalize(mul(input.BiTan, WorldView));
	output.BiTanNorm[2] = normalize(mul(input.Norm, WorldView));

	output.TexCoord = input.TexCoord;

	return output;
}

PSOutput PSFunct(VSOutput input)
{
	PSOutput output;

	output.Depth = float4(input.Depth, 0, 0, 1.0f);

	float3 weight = tex2D(WeightSampler, input.TexCoord).rgb;
	int repeats = TextureRepeats;

	float2 texCoord = input.TexCoord * repeats;

	half3 rNorm = tex2D(RNSampler, texCoord).xyz;
	half3 gNorm = tex2D(GNSampler, texCoord).xyz;
	half3 bNorm = tex2D(BNSampler, texCoord).xyz;

	float3 normal = clamp(1.0f - weight.r - weight.g - weight.b, 0, 1);
	normal += weight.r * rNorm + weight.g * gNorm + weight.b * bNorm;
	normal = normal * 2.0f - 1.0f;
	normal = normalize(mul(normal, input.BiTanNorm));
	normal = 0.5f * (normal + 1.0f);

	output.Norm.rg = EncodeNormal(normal);
	output.Norm.ba = 0.0f;

	float3 r = tex2D(RSampler, texCoord).rgb;
	float3 g = tex2D(GSampler, texCoord).rgb;
	float3 b = tex2D(BSampler, texCoord).rgb;

	float3 colour = clamp(1.0f - weight.r - weight.g - weight.b, 0, 1);
	colour += weight.r * r + weight.g * g + weight.b * b;

	output.Colour = float4(colour, 1.0f);

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

