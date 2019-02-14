#include "Common.fxh"

float4x4 World;
float4x4 View;
float4x4 Proj;

float2 Speed;

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
	float4 ScreenPos : TEXCOORD2;
	float3x3 TBN : TEXCOORD3;
};

struct PSOutput
{
	float4 Opaque : COLOR0;
	float4 Depth : COLOR1;
	float4 Colour : COLOR2;
};

VSOutput VSFunct(VSInput input)
{
    VSOutput output = (VSOutput)0;

	float4 pos = mul(input.Pos, World);
	output.Pos = mul(pos, mul(View, Proj));
	output.ScreenPos = output.Pos;
	
	output.Depth = output.Pos.z/ output.Pos.w;
	output.TexCoord = input.TexCoord;

	output.TBN = mul(float3x3(input.Tan,
		input.BiTan, input.Norm), World);

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
		float2 texCoord = input.TexCoord;
		texCoord += Speed;

		if(texCoord.x >= 1.0f)
			texCoord.x = 0.0f;
		else if(texCoord.x <= 0.0f)
			texCoord.x = 1.0f;

		if(texCoord.y >= 1.0f)
			texCoord.y = 0.0f;
		else if(texCoord.y <= 0.0f)
			texCoord.y = 1.0f;

		float4 colour = tex2D(ColourMapSampler, texCoord);

		if(colour.a == 0.0f)
			discard;
		else
		{
			output.Colour = colour;

			output.Depth = 0.0f;
			output.Depth.rg = PackDepth(input.Depth);

			output.Opaque.rgb = CalculateNormal(tex2D(NormalMapSampler,
				input.TexCoord).xyz, input.TBN);

			output.Opaque.a = 0.0f;
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
        VertexShader = compile vs_3_0 VSFunct();
        PixelShader = compile ps_3_0 PXFunct();
    }
}
