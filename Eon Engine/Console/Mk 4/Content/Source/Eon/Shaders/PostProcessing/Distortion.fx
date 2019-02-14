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

	half2 distortion = tex2D(DistortionSampler, uv).gb;

	if(distortion.x > 0 && distortion.y > 0)
	{
		float2 off = distortion * 0.05f;

		half3 colour = tex2D(SceneSampler, input.ScreenPos + (off - 0.025f)).rgb;

		return half4((colour * 4) * 0.23f, 1);
	}
	else
		return tex2D(SceneSampler, input.ScreenPos);
}

technique Distort
{
	pass P0
	{
		PixelShader = compile ps_2_0 PXFunct();
	}
}