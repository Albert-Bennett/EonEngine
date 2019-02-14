Texture ColourMap;
sampler ColourMapSampler = sampler_state
{
	texture = <ColourMap>;
	magfilter = LINEAR;
	minfilter = LINEAR;
	mipfilter = LINEAR;
	AddressU = mirror;
	AddressV = mirror;
};

Texture LightMap;
sampler LightMapSampler = sampler_state
{
	texture = <LightMap>;
	magfilter = LINEAR;
	minfilter = LINEAR;
	mipfilter = LINEAR;
	AddressU = mirror;
	AddressV = mirror;
};

struct PSInput
{
	float4 colour : COLOR0;
	float2 texCoords : TEXCOORD0;
};

float ColourLenght(float4 colour)
{
	return (colour.r + colour.g + colour.b)/ 3;
}

float4 Combine(PSInput input):COLOR0
{
	float4 colour = tex2D(ColourMapSampler, input.texCoords);
	float4 light = tex2D(LightMapSampler, input.texCoords);

	return float4((colour.rgb * colour.rgb/2) * light.rgb, 1);
}

technique Combined
{
	pass P0
	{
		PixelShader = compile ps_2_0 Combine();
	}
}