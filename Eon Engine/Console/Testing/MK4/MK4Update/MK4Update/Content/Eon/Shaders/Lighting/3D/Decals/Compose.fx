float2 HalfPixel;

texture Scene;
sampler SceneSampler = sampler_state
{
	texture = <Scene>;
	AddressU = CLAMP;
	AddressV = CLAMP;
	MagFilter = POINT;
	MinFilter = POINT;
	MipFilter = POINT;
};

texture SceneOpaque;
sampler SceneOpaqueSampler = sampler_state
{
	texture = <SceneOpaque>;
	AddressU = CLAMP;
	AddressV = CLAMP;
	MagFilter = POINT;
	MinFilter = POINT;
	MipFilter = POINT;
};

texture Colour;
sampler ColourSampler = sampler_state
{
	texture = <Colour>;
	AddressU = CLAMP;
	AddressV = CLAMP;
	MagFilter = POINT;
	MinFilter = POINT;
	MipFilter = POINT;
};

texture Opaque;
sampler OpaqueSampler = sampler_state
{
	texture = <Opaque>;
	AddressU = CLAMP;
	AddressV = CLAMP;
	MagFilter = POINT;
	MinFilter = POINT;
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

struct PSOutput
{
	float4 Colour : COLOR0;
	float4 Opaque : COLOR1;
};

VSOutput VSFunct(VSInput input)
{
	VSOutput output;

	output.Pos = float4(input.Pos, 1);
	output.TexCoord = input.TexCoord + HalfPixel;

	return output;
}

PSOutput PSFunct(VSOutput input)
{
	PSOutput output;

	float4 sceneOpaque = tex2D(SceneOpaqueSampler, input.TexCoord);

	float4 colour = tex2D(ColourSampler, input.TexCoord);
	float4 opaque = tex2D(OpaqueSampler, input.TexCoord); 

	if(colour.a == 1.0f)
		output.Colour = colour;
	else
	{
		float3 sceneColour = tex2D(SceneSampler, input.TexCoord).rgb;

		output.Colour = float4(sceneColour.rgb + (colour.rgb * colour.a), 1.0f);
	}

	if(opaque.r == 0.0f && opaque.g == 0.0f)
		output.Opaque = sceneOpaque;
	else
		output.Opaque = opaque;

	return output;
}

technique Combine
{
	pass P0
	{
		VertexShader = compile vs_2_0 VSFunct();
		PixelShader = compile ps_2_0 PSFunct();
	}
}
