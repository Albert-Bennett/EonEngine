float4x4 IViewProj;

float3 CamPos;
float2 GDSize;

float FogStart;
float FogThickness;
float FogEnd;

float3 FogColour;

texture Scene;
sampler SceneSampler = sampler_state
{
	Texture = Scene;
	AddressU = CLAMP;
	AddressV = CLAMP;
	magfilter = LINEAR;
	minfilter = LINEAR;
	mipfilter = LINEAR;
};

texture DepthMap;
sampler DepthSampler = sampler_state
{
	Texture = DepthMap;
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
	float4 ScreenPos : TEXCOORD0;
};

VSOutput VSFunct(VSInput input)
{
	VSOutput output;

	output.Pos = float4(input.Pos, 1);
	output.ScreenPos = output.Pos;

	return output;
}

float4 PXFunct(VSOutput input) : COLOR0
{
    input.ScreenPos.xy /= input.ScreenPos.w;

	float2 uv = 0.5f * (float2(input.ScreenPos.x, -input.ScreenPos.y) + 1)-
		float2(1.0 / GDSize);

	float3 colour = tex2D(SceneSampler, uv).rgb; 

	float depth = tex2D(DepthSampler, uv).r;

	float4 pos = 1.0f;
	pos.xy = input.ScreenPos.xy;
	pos.z = depth;

	pos = mul(pos, IViewProj);
	pos /= pos.w;

	float dist = length(pos - CamPos);
	float factor = saturate((dist - FogStart) / (FogEnd - FogStart));

    return float4(lerp(colour, FogColour, factor), 1);
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
    input.ScreenPos.xy /= input.ScreenPos.w;

	float2 uv = 0.5f * (float2(input.ScreenPos.x, -input.ScreenPos.y) + 1)-
		float2(1.0 / GDSize);

	float depth = tex2D(DepthSampler, uv).r;
	float3 colour = tex2D(SceneSampler, uv).rgb; 

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
    input.ScreenPos.xy /= input.ScreenPos.w;

	float2 uv = 0.5f * (float2(input.ScreenPos.x, -input.ScreenPos.y) + 1)-
		float2(1.0 / GDSize);

	float depth = tex2D(DepthSampler, uv).r;
	float3 colour = tex2D(SceneSampler, uv).rgb; 

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

technique Thickest
{
    pass Pass1
    {
        VertexShader = compile vs_2_0 VSFunct();
        PixelShader = compile ps_2_0 PXFunctEXP2();
    }
}
