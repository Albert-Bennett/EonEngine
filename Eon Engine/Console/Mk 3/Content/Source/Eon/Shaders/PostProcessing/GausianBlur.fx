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

float4 PXFunct(float2 texCoord : TEXCOORD0) : COLOR0
{
	float4 colour = 0;

	[unroll(SAMPLES)]
	for(int i = 0; i < SAMPLES; i++)
		colour += tex2D(SceneSampler, texCoord + Offsets[i]) * Weights[i];

	return colour;
}

technique Blur
{
	Pass P0
	{
		PixelShader = compile ps_2_0 PXFunct();
	}
}