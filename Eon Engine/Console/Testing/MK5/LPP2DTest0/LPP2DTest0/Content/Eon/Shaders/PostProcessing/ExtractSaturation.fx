float Threshold;

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

half4 PXFunct(float2 texCoord : TEXCOORD0) : COLOR0
{
	half4 colour = tex2D(SceneSampler, texCoord);

	return saturate((colour - Threshold) / (1 - Threshold)); 
}

technique Extract
{
	pass P0
	{
		PixelShader = compile ps_2_0 PXFunct();
	}
}
