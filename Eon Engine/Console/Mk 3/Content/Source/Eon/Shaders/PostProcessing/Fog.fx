float4x4 IViewProj;

float3 CamPos;
float2 HalfPixel;

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

	output.ScreenPos = 0.5f * (float2(output.ScreenPos.x, 
		-output.ScreenPos.y) + 1) + HalfPixel;

	return output;
}

float4 PXFunct(VSOutput input) : COLOR0
{
	float depth = tex2D(DepthSampler, input.ScreenPos).r;
	float3 colour = tex2D(SceneSampler, input.ScreenPos).rgb; 

	if(depth != 0.0f)
	{
		float4 pos = 1.0f;
		pos.xy = input.ScreenPos.xy;
		pos.z = depth;

		pos = mul(pos, IViewProj);
		pos /= pos.w;

		float dist = length(pos - CamPos);
		float factor = saturate((dist - FogStart) / (FogEnd - FogStart));

		return float4(lerp(colour, FogColour, factor), 1);
	}

	return float4(colour, 1.0f);
}

technique Quick
{
    pass Pass1
    {
        VertexShader = compile vs_2_0 VSFunct();
        PixelShader = compile ps_2_0 PXFunct();
    }
}

float4 PXFunctEXP(VSOutput input) : COLOR0
{
	float depth = tex2D(DepthSampler, input.ScreenPos).r;
	float3 colour = tex2D(SceneSampler, input.ScreenPos).rgb; 

	if(depth != 0.0f)
	{
		float4 pos = 1.0f;
		pos.xy = input.ScreenPos.xy;
		pos.z = depth;

		pos = mul(pos, IViewProj);
		pos /= pos.w;

		float dist = length(pos - CamPos);

		float factor = exp(-dist * FogThickness);
		factor = saturate(1 - factor);

		return float4(lerp(colour, FogColour, factor), 1);
	}

	return float4(colour, 1.0f); 
}

technique Thick
{
    pass Pass1
    {
        VertexShader = compile vs_2_0 VSFunct();
        PixelShader = compile ps_2_0 PXFunctEXP();
    }
}

float4 PXFunctEXP2(VSOutput input) : COLOR0
{
	float depth = tex2D(DepthSampler, input.ScreenPos).r;
	float3 colour = tex2D(SceneSampler, input.ScreenPos).rgb; 

	if(depth != 0.0f)
	{
		float4 pos = 1.0f;
		pos.xy = input.ScreenPos.xy;
		pos.z = depth;

		pos = mul(pos, IViewProj);
		pos /= pos.w;

		float dist = length(pos - CamPos);

		float factor = exp(-pow(-dist * FogThickness, 2));
		factor = saturate(1 - factor);

		return float4(lerp(colour, FogColour, factor), 1);
	}

	return float4(colour, 1.0f);
}

technique Thickest
{
    pass Pass1
    {
        VertexShader = compile vs_2_0 VSFunct();
        PixelShader = compile ps_2_0 PXFunctEXP2();
    }
}
