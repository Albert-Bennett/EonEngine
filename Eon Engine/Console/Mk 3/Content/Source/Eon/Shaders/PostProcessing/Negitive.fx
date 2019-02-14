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
	float3 colour = tex2D(SceneSampler, texCoord).rgb;
	float3 negitive = float3(1, 1, 1) - colour;

	return float4(negitive, 1);
}

technique Negitive
{
	pass P0
	{
		PixelShader = compile ps_2_0 PXFunct();
	}
}