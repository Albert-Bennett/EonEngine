#include "Common.fxh"

float4x4 World;
float4x4 View;
float4x4 Proj;

float3 CamPos;

float3 Up;

float Scale;
float3 Pos;
float4x4 Rot;

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
	float4 ScreenPos : TEXCOORD2;
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

	float3 side = cross(CamPos, Up);
	side = normalize(side);
	 
	pos += (input.TexCoord.x - 0.5f) * side * Scale;
	pos += (0.5f - input.TexCoord.y) * Up * Scale;
	pos = mul(pos, Rot);
	pos += Pos;

	output.Pos = mul(float4(pos, 1), mul(View, Proj));
	output.ScreenPos = output.Pos;
	
	output.Depth = output.Pos.z/ output.Pos.w;
	output.TexCoord = input.TexCoord;

	return output;
}

PSOutput PXFunct(VSOutput input)
{
	PSOutput output;

	input.ScreenPos /= input.ScreenPos.w;

	float2 uv = 0.5f * (float2(input.ScreenPos.x, 
		-input.ScreenPos.y) + 1) - HalfPixel;

	float inDepth = tex2D(DepthSampler, uv).r;

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

			output.Opaque.rg = CalculateBasicNormal(World);
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