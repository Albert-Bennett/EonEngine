#include "CommonCalculations.fxh"

float4x4 World;
float4x4 ViewProj;

float3 Up;

float Scale;
float3 Pos;
float4x4 Rot;

texture Texture;
sampler TextureMapSampler = sampler_state
{
   Texture = <Texture>;
   MinFilter = Linear;
   MagFilter = Linear;
   MipFilter = Linear;   
   AddressU  = Clamp;
   AddressV  = Clamp;
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
	float3 Depth : TEXCOORD1;
};

struct PSOutput
{
	float4 Colour : COLOR0;
	float4 Depth : COLOR1;
};

VSOutput VSFunct(VSInput input)
{
    VSOutput output = (VSOutput)0;

	float3 pos = input.Pos;

	pos *= Scale;
	pos = mul(pos, Rot);
	pos += Pos;

	output.Pos = mul(float4(input.Pos, 1), ViewProj);

	output.Depth.x = output.Pos.z;
	output.Depth.y = output.Pos.w;
	output.Depth.z = 1;

	output.TexCoord = input.TexCoord;

	return output;
}

PSOutput PXFunct(VSOutput input)
{
	PSOutput output;

	output.Colour = tex2D(TextureMapSampler, input.TexCoord);
	output.Colour = Clip(output.Colour);

	output.Depth = 0.0f;
	output.Depth.r = input.Depth.x / input.Depth.y;
	output.Depth.a = 1.0f;

	clip(-output.Depth.r + 0.999f);

	return output;
}

technique ForwardRender
{
    pass P0
    {
        VertexShader = compile vs_2_0 VSFunct();
        PixelShader = compile ps_2_0 PXFunct();
    }
}