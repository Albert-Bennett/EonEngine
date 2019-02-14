float2 HalfPixel;

texture ColourMap;
sampler ColourSampler = sampler_state
{
	texture = <ColourMap>;
	AddressU = CLAMP;
	AddressV = CLAMP;
	MagFilter = POINT;
	MinFilter = POINT;
	MipFilter = POINT;
};

texture LightMap;
sampler LightSampler = sampler_state
{
	texture = <LightMap>;
	AddressU = CLAMP;
	AddressV = CLAMP;
	MagFilter = POINT;
	MinFilter = POINT;
	MipFilter = POINT;
};

texture ShadowMap;
sampler ShadowMapSampler = sampler_state
{
	texture = <ShadowMap>;
	AddressU = CLAMP;
	AddressV = CLAMP;
	MinFilter = POINT;
	MagFilter = POINT;
	MipFilter = POINT;
};

struct VSInput
{
	float3 Pos : POSITION0;
	float2 TexCoord : TEXCOORD0;
};

struct VSOutput
{
	float4 Pos : POSITION0;
	float2 TexCoord : TEXCOORD0;
};

VSOutput VSFunct(VSInput input)
{
	VSOutput output;

	output.Pos = float4(input.Pos, 1);
	output.TexCoord = input.TexCoord + HalfPixel;

	return output;
}

float4 PSFunct(VSOutput input) : COLOR0
{
	float3 colour = tex2D(ColourSampler, input.TexCoord).rgb;

	float4 light = tex2D(LightSampler, input.TexCoord);

	float shadow = tex2D(ShadowMapSampler, input.TexCoord).r;

	if(shadow > 0.0f)
		return float4(shadow * (colour * (light.rgb + light.a)), 1);

	return float4(0, 0, 0, 1);
}

technique Combine
{
	pass P0
	{
		VertexShader = compile vs_2_0 VSFunct();
		PixelShader = compile ps_2_0 PSFunct();
	}
}
