#define SAMPLES  15

float Weights[SAMPLES];
float2 Offsets[SAMPLES]; 

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

half4 PXFunct(float2 texCoord : TEXCOORD0) : COLOR0
{
	half3 colour = 0;

	[unroll(SAMPLES)]
	for(half i = 0; i < SAMPLES; i++)
		colour += tex2D(SceneSampler, texCoord + Offsets[i]).rgb * Weights[i];

	return half4(colour, 1.0f);
}

technique Blur
{
	Pass P0
	{
		PixelShader = compile ps_2_0 PXFunct();
	}
}

texture Mask;
sampler MaskSampler = sampler_state
{
	Texture = <Mask>;
	AddressU = CLAMP;
	AddressV = CLAMP;
	magfilter = LINEAR;
	minfilter = LINEAR;
	mipfilter = LINEAR;
};

half4 PXMaskFunct(float2 texCoord : TEXCOORD0) : COLOR0
{
	float mask = tex2D(MaskSampler, texCoord).r;

	half3 colour = 0;

	if(mask > 0.0f)
	{
		[unroll(SAMPLES)]
		for(half i = 0; i < SAMPLES; i++)
			colour += tex2D(SceneSampler, texCoord + Offsets[i]).rgb * Weights[i];
	}
	else
		colour = tex2D(SceneSampler, texCoord).rgb;	

	return half4(colour, 1.0f);
}

technique BlurMask
{
	Pass P0
	{
		PixelShader = compile ps_2_0 PXMaskFunct();
	}
}

