float2 HalfPixel;

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

texture Blur;
sampler BlurSampler = sampler_state
{
	Texture = <Blur>;
	AddressU = CLAMP;
	AddressV = CLAMP;
	MagFilter = POINT;
	MinFilter = POINT;
	MipFilter = POINT;
};

texture Distortion;
sampler DistortionSampler = sampler_state
{
	Texture = <Distortion>;
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

	float3 visable = tex2D(DistortionSampler, uv).gba;

	if(visable.z < 1.0f)
		if(visable.x > 0 && visable.y > 0)
		{
			visable.xy *= 0.05f;
			visable.xy -= 0.025f;

			half3 colour = tex2D(SceneSampler, input.ScreenPos + visable.xy).rgb;

			return half4(((colour * 4) * 0.23f), 1);
		}

	return tex2D(BlurSampler, input.ScreenPos);
}

technique Distort
{
	pass P0
	{
		PixelShader = compile ps_2_0 PXFunct();
	}
}