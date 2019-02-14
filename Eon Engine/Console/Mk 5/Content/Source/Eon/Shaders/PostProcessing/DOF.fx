#include "Common.fxh"

float4x4 IViewProj;

float3 CamPos;
float2 HalfPixel;

float BlurStart;
float BlurEnd;

texture Scene;
sampler SceneSampler = sampler_state
{
	Texture = <Scene>;
	AddressU = CLAMP;
	AddressV = CLAMP;
	MagFilter = POINT;
	MinFilter = POINT;
	MipFilter = POINT;
};

texture BlurredScene;
sampler BlurredSampler = sampler_state
{
	Texture = <BlurredScene>;
	AddressU = CLAMP;
	AddressV = CLAMP;
	MagFilter = POINT;
	MinFilter = POINT;
	MipFilter = POINT;
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

	if(depth > 0)
	{
		float4 pos = float4(uv, depth, 1.0f);
		pos = mul(pos, IViewProj);
		pos /= pos.w;

		half3 colour = tex2D(SceneSampler, uv).rgb; 
		half3 blurred = tex2D(BlurredSampler, uv).rgb;

		half dist = length(pos - CamPos);
		half factor = saturate((dist - BlurStart) / (BlurEnd - BlurStart));

		return half4(lerp(colour, blurred, factor), 1);
	}

	return 0;
}

technique DOF
{
    pass P0
    {
        VertexShader = compile vs_2_0 VSFunct();
        PixelShader = compile ps_2_0 PXFunct();
    }
}
