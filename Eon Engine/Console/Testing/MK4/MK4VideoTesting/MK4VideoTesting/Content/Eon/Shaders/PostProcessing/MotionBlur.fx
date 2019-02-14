#include "Common.fxh"

#define SAMPLES 14

float4x4 IViewProj;
float4x4 PrevViewProj;

float2 MaxVelocity;
float2 HalfPixel;

texture Scene;
sampler SceneSampler = sampler_state
{
	Texture = <Scene>;
	AddressU = CLAMP;
	AddressV = CLAMP;
	magfilter = LINEAR;
	minfilter = LINEAR;
	mipfilter = LINEAR;
};

texture DepthMap;
sampler DepthSampler = sampler_state
{
	Texture = <DepthMap>;
	AddressU = CLAMP;
	AddressV = CLAMP;
	magfilter = LINEAR;
	minfilter = LINEAR;
	mipfilter = LINEAR;
};

struct VSInput
{
	float3 Pos: POSITION0;
};

struct VSOutput
{
	float4 Pos : POSITION0;
	float2 ScreenPos : TEXCOORD0;
};

VSOutput VSFunct(VSInput input)
{
	VSOutput output;

	output.Pos = float4(input.Pos, 1);
	output.ScreenPos = output.Pos.xy/ output.Pos.w;

	return output;
}

half4 PXFunct(VSOutput input) : COLOR0
{
	float2 uv = 0.5f * (float2(input.ScreenPos.x, 
		-input.ScreenPos.y) + 1) - HalfPixel;

	float depth = UnPackDepth(tex2D(DepthSampler, uv).rg);

	if(depth > 0.0f)
	{
		half4 pos = float4(input.ScreenPos, depth, 1.0f);
		half4 prevPos = mul(pos, PrevViewProj);
		prevPos /= prevPos.w;

		pos = mul(pos, IViewProj);
		pos /= pos.w;

		half2 velocity = pos - prevPos;
		velocity = clamp(velocity, -MaxVelocity, MaxVelocity);

		float4 c = 0.0f;

		[unroll(SAMPLES)] 
		for(int i = 0; i < SAMPLES; i++)
		{
			c += tex2D(SceneSampler, input.ScreenPos);
			input.ScreenPos += velocity;
		}

		return c / SAMPLES;
	}

	return 0;
}

technique MotionBlur
{
    pass P0
    {
	    VertexShader = compile vs_2_0 VSFunct();
        PixelShader = compile ps_2_0 PXFunct();
    }
}
