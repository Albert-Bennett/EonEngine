#include "Common.fxh"

float4x4 IViewProj;

float3 CamPos;

float FogStart;
float FogThickness;
float FogEnd;

float3 FogColour;

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

float4 PXFunct(VSOutput input) : COLOR0
{
	float2 uv = 0.5f * (float2(input.ScreenPos.x, 
		-input.ScreenPos.y) + 1);

	float depth = UnPackDepth(tex2D(DepthSampler, uv).rg);
	half3 colour = tex2D(SceneSampler, uv).rgb; 

	if(depth > 0.0f)
	{
		float4 pos = float4(uv, depth, 1.0f);
		pos = mul(pos, IViewProj);
		pos /= pos.w;

		half dist = length(pos - CamPos);
		half factor = saturate((dist - FogStart) / (FogEnd - FogStart));

		return half4(lerp(colour, FogColour, factor), 1);
	}

	return half4(colour, 1.0f);
}

technique Quick
{
    pass P0
    {
        VertexShader = compile vs_2_0 VSFunct();
        PixelShader = compile ps_2_0 PXFunct();
    }
}

half4 PXFunctEXP(VSOutput input) : COLOR0
{
	float2 uv = 0.5f * (float2(input.ScreenPos.x, 
		-input.ScreenPos.y) + 1);

	float depth = UnPackDepth(tex2D(DepthSampler, uv).rg);
	half3 colour = tex2D(SceneSampler, uv).rgb; 

	if(depth > 0.0f)
	{
		float4 pos = 1.0f;
		pos.xy = input.ScreenPos.xy;
		pos.z = depth;

		pos = mul(pos, IViewProj);
		pos /= pos.w;

		half dist = length(pos - CamPos);

		half factor = exp(-dist * FogThickness);
		factor = saturate(1 - factor);

		return half4(lerp(colour, FogColour, factor), 1);
	}

	return half4(colour, 1.0f); 
}

technique Thick
{
    pass P0
    {
        VertexShader = compile vs_2_0 VSFunct();
        PixelShader = compile ps_2_0 PXFunctEXP();
    }
}

half4 PXFunctEXP2(VSOutput input) : COLOR0
{
	float2 uv = 0.5f * (float2(input.ScreenPos.x, 
		-input.ScreenPos.y) + 1);

	float depth = UnPackDepth(tex2D(DepthSampler, uv).rg);
	half3 colour = tex2D(SceneSampler, uv).rgb; 

	if(depth > 0.0f)
	{
		float4 pos = 1.0f;
		pos.xy = input.ScreenPos.xy;
		pos.z = depth;

		pos = mul(pos, IViewProj);
		pos /= pos.w;

		half dist = length(pos - CamPos);

		half factor = exp(-pow(-dist * FogThickness, 2));
		factor = saturate(1 - factor);

		return half4(lerp(colour, FogColour, factor), 1);
	}

	return half4(colour, 1.0f);
}

technique Thickest
{
    pass P0
    {
        VertexShader = compile vs_2_0 VSFunct();
        PixelShader = compile ps_2_0 PXFunctEXP2();
    }
}
