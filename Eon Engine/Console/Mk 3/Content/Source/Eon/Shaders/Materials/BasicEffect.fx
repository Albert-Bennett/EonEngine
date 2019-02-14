#include "Common.fxh"

float4x4 World;
float4x4 View;
float4x4 Proj;

float MinDepth;
float MaxDepth;

float2 HalfPixel;

texture ColourMap;
sampler ColourMapSampler = sampler_state
{
    Texture = <ColourMap>;
	MinFilter = ANISOTROPIC;
	MagFilter = LINEAR;
	MipFilter = LINEAR;
	AddressU = WRAP;
	AddressV = WRAP;
	MaxAnisotropy = 16;
};

texture NormalMap;
sampler NormalMapSampler = sampler_state
{
    Texture = <NormalMap>;
	MinFilter = ANISOTROPIC;
	MagFilter = LINEAR;
	MipFilter = LINEAR;
	AddressU = WRAP;
	AddressV = WRAP;
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
	MipFilter = POINT;
};

struct VSInput
{
	float4 Pos : POSITION;
	float3 TexCoord : TEXCOORD0;
	float3 Norm : NORMAL0;
	float3 Tan : TANGENT0;
	float3 BiTan : BINORMAL0;
};

struct VSOutput
{
    float4 Pos : POSITION;
    float2 TexCoord : TEXCOORD0;
	float Depth : TEXCOORD1;
	float2 UV : TEXCOORD2;
	float3x3 BiTanNorm : TEXCOORD3;
};

struct PSOutput
{
	float4 Depth : COLOR0;
	float4 Opaque : COLOR1;
	float4 Colour : COLOR2;
};

VSOutput VSFunct(VSInput input)
{
    VSOutput output;

	float4 worldPos = mul(input.Pos, World);
	output.Pos = mul(worldPos, mul(View, Proj));

	float3 p = output.Pos.xyz/ output.Pos.w;

	output.UV = 0.5f * (float2(p.x, -p.y) + 1) - HalfPixel;
	output.Depth = p.z;
	output.TexCoord = input.TexCoord;

	float3x3 WorldView = mul(World, View);

	output.BiTanNorm[0] = normalize(mul(input.Tan, WorldView));
	output.BiTanNorm[1] = normalize(mul(input.BiTan, WorldView));
	output.BiTanNorm[2] = normalize(mul(input.Norm, WorldView));

	return output;
}

PSOutput PXFunct(VSOutput input)
{
	PSOutput output;

	float inDepth = tex2D(DepthSampler, input.UV).r;

	if((input.Depth >= MinDepth && input.Depth < MaxDepth) &&
		(input.Depth < inDepth || inDepth == 0.0f))
	{
		float4 colour = tex2D(ColourMapSampler, input.TexCoord);

		if(colour.a == 0.0f)
			discard;
		else
		{
			output.Colour = colour;
			output.Depth = float4(input.Depth, 0, 0, 1.0f);

			half3 normal = tex2D(NormalMapSampler, input.TexCoord).xyz * 2.0f - 1.0f;
			normal = normalize(mul(normal, input.BiTanNorm));
			normal = 0.5f * (normal + 1.0f);

			output.Opaque.rg = EncodeNormal(normal);
			output.Opaque.ba = 0.0f;
		}
	}
	else
		discard;

	return output;
}

technique Render
{
    pass P0
    {
        VertexShader = compile vs_2_0 VSFunct();
        PixelShader = compile ps_2_0 PXFunct();
    }
}

struct VSInputSimple
{
	float4 Pos : POSITION;
	float3 TexCoord : TEXCOORD0;
};

struct VSOutputSimple
{
    float4 Pos : POSITION;
    float2 TexCoord : TEXCOORD0;
	float Depth : TEXCOORD1;
	float2 UV : TEXCOORD2;
};

VSOutputSimple VSFunctSimple(VSInputSimple input)
{
    VSOutputSimple output;

	float4 worldPos = mul(input.Pos, World);
	output.Pos = mul(worldPos, mul(View, Proj));

	float3 p = output.Pos.xyz/ output.Pos.w;

	output.UV = 0.5f * (float2(p.x, -p.y) + 1) - HalfPixel;
	output.Depth = p.z;
	output.TexCoord = input.TexCoord;

	return output;
}

PSOutput PXFunctSimple(VSOutputSimple input)
{
	PSOutput output;

	float inDepth = tex2D(DepthSampler, input.UV).r;

	if((input.Depth >= MinDepth && input.Depth < MaxDepth) &&
		(input.Depth < inDepth || inDepth == 0.0f))
	{
		float4 colour = tex2D(ColourMapSampler, input.TexCoord);

		if(colour.a == 0.0f)
			discard;
		else
		{
			output.Colour = colour;
			output.Depth = float4(input.Depth, 0, 0, 1.0f);

			half3 norm = half3(1, 0.5f, 0.5f) * 2.0f - 1.0f;
			norm = normalize(mul(norm, mul(World, View)));
			norm = 0.5f * (norm + 1.0f);

			output.Opaque.rg = EncodeNormal(norm);
			output.Opaque.ba = 0.0f;
		}
	}
	else
		discard;

	return output;
}

technique Simple
{
    pass P0
    {
        VertexShader = compile vs_2_0 VSFunctSimple();
        PixelShader = compile ps_2_0 PXFunctSimple();
    }
}