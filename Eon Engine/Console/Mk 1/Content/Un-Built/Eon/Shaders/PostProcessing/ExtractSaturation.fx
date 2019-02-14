float Threshold;

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

float4 PXFunct(float2 texCoord : TEXCOORD0) : COLOR0
{
	float4 colour = tex2D(SceneSampler, texCoord);

	return saturate((colour - Threshold) / (1 - Threshold)); 
}

technique Extract
{
	pass P0
	{
		PixelShader = compile ps_2_0 PXFunct();
	}
}
