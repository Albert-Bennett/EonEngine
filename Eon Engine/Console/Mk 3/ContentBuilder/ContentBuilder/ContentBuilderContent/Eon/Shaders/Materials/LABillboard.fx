#include "Common.fxh"

float4x4 World;
float4x4 View;
float4x4 Proj;

float3 Up;

float Scale;
float3 Pos;

float3 Axis;

float MinDepth;
float MaxDepth;

float2 HalfPixel;
float AlphaBias = 1.0f;

texture Texture;
sampler TextureMapSampler = sampler_state
{
    Texture = <Texture>;
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
	float3 Pos : POSITION;
	float3 TexCoord : TEXCOORD0;
};

struct VSOutput
{
    float4 Pos : POSITION;
    float2 TexCoord : TEXCOORD0;
	float Depth : TEXCOORD1;
	float2 UV : TEXCOORD2;
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

	float3 pos = input.Pos;

	float3 side = cross(Axis, Up);
	side = normalize(side);
	 
	pos += (input.TexCoord.x - 0.5f) * side * Scale;
	pos += (0.5f - input.TexCoord.y) * Up * Scale;
	pos += Pos;

	output.Pos = mul(float4(pos, 1), mul(View, Proj));
	
	float3 p = output.Pos.xyz/ output.Pos.w;

	output.UV = 0.5f * (float2(p.x, -p.y) + 1) - HalfPixel;

	output.Depth = p.z;
	output.TexCoord = input.TexCoord;

	return output;
}

PSOutput PXFunct(VSOutput input)
{
	PSOutput output;

	float inDepth = tex2D(DepthSampler, input.UV).r;

	if((input.Depth >= MinDepth && input.Depth < MaxDepth) &&
		(input.Depth < inDepth || inDepth == 0.0f))
	{
		float4 colour = tex2D(TextureMapSampler, input.TexCoord);

		if(colour.a < AlphaBias)
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

technique Render
{
    pass P0
    {
        VertexShader = compile vs_2_0 VSFunct();
        PixelShader = compile ps_2_0 PXFunct();
    }
}