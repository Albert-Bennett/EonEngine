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

	output.ScreenPos = 0.5f * (float2(output.ScreenPos.x, 
		-output.ScreenPos.y) + 1) - HalfPixel;

	return output;
}

float4 PXFunct(VSOutput input) : COLOR0
{
	float depth = tex2D(DepthSampler, input.ScreenPos).r;

	if(depth > 0)
	{
		float4 pos = 1.0f;
		pos.xy = input.ScreenPos.xy;
		pos.z = depth;

		pos = mul(pos, IViewProj);
		pos /= pos.w;

		float3 colour = tex2D(SceneSampler, input.ScreenPos).rgb; 
		float3 blurred = tex2D(BlurredSampler, input.ScreenPos).rgb;

		float dist = length(pos - CamPos);
		float factor = saturate((dist - BlurStart) / (BlurEnd - BlurStart));

		return float4(lerp(colour, blurred, factor), 1);
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
