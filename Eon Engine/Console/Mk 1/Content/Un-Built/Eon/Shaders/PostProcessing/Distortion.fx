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

texture Distortion;
sampler DistortionSampler = sampler_state
{
	Texture = Distortion;
	AddressU = WRAP;
	AddressV = WRAP;
	magfilter = LINEAR;
	minfilter = LINEAR;
	mipfilter = LINEAR;
};

float4 PXFunct(float2 texCoord : TEXCOORD0) : COLOR0
{
	float2 texCoordBack = texCoord;
	float2 distortion = tex2D(DistortionSampler, texCoord).rg;

	if(distortion.x > 0 && distortion.y > 0)
	{
		float2 off = distortion * 0.05f;

		float3 colour = tex2D(SceneSampler, texCoordBack + (off - 0.025f));

		return float4((colour * 4) * 0.23f, 1);
	}
	else
		return tex2D(SceneSampler, texCoordBack);
}

technique Distort
{
	pass P0
	{
		PixelShader = compile ps_2_0 PXFunct();
	}
}