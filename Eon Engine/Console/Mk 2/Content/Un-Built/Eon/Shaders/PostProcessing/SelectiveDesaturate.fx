#include "GeneralCalculations.fxh"

float3 Min = float3(0, 0, 0);
float3 Max = float3(1, 1, 1);

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

	float amount = ToGreyScale(colour);
	float3 final = float3(amount, amount, amount);

	if(Max.x > 0)
		if(colour.x >= Min.x && colour.x <= Max.x)
			final.x = colour.x;

	if(Max.y > 0)
		if(colour.y >= Min.y && colour.y <= Max.y)
			final.y = colour.y;

	if(Max.z > 0)
		if(colour.z >= Min.z && colour.z <= Max.z)
			final.z = colour.z;

	return float4(final, 1);
}

technique Negitive
{
	pass P0
	{
		PixelShader = compile ps_2_0 PXFunct();
	}
}