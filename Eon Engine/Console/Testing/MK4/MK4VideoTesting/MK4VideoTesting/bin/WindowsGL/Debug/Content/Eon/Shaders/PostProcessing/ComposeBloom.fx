#include "Common.fxh"

float BloomIntensity;
float SceneIntensity;

float BloomSaturation;
float SceneSaturation;

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

texture PreBloom;
sampler PreBloomSampler = sampler_state
{
	Texture = <PreBloom>;
	AddressU = CLAMP;
	AddressV = CLAMP;
	magfilter = LINEAR;
	minfilter = LINEAR;
	mipfilter = LINEAR;
};

half4 PXFunct(float2 texCoord : TEXCOORD0) : COLOR0
{
	half4 scene = tex2D(SceneSampler, texCoord);
	half4 bloom = tex2D(PreBloomSampler, texCoord);

	scene = AdjustSaturation(scene, SceneSaturation) * SceneIntensity;
	bloom = AdjustSaturation(bloom, BloomSaturation) * BloomIntensity;

	scene *= 1 - saturate(bloom);

	return scene + bloom;
}

technique Compose
{
	pass P0
	{
		PixelShader = compile ps_2_0 PXFunct();
	}
}
