texture ColourMap;
sampler ColourSampler = sampler_state
{
	texture = <ColourMap>;
	MinFilter = LINEAR;
	MagFilter = LINEAR;
	MipFilter = LINEAR;
	AddressU = WRAP;
	AddressV = WRAP;
};

texture LightMap;
sampler LightSampler = sampler_state
{
	texture = <LightMap>;
	MinFilter = LINEAR;
	MagFilter = LINEAR;
	MipFilter = LINEAR;
	AddressU = WRAP;
	AddressV = WRAP;
};

texture DepthMap;
sampler DepthSampler = sampler_state
{
	texture = <DepthMap>;
	MinFilter = LINEAR;
	MagFilter = LINEAR;
	MipFilter = LINEAR;
	AddressU = WRAP;
	AddressV = WRAP;
};

texture FRColourMap;
sampler FRColourSampler = sampler_state
{
	texture = <FRColourMap>;
	MinFilter = LINEAR;
	MagFilter = LINEAR;
	MipFilter = LINEAR;
	AddressU = WRAP;
	AddressV = WRAP;
};

texture FRDepthMap;
sampler FRDepthSampler = sampler_state
{
	texture = <FRDepthMap>;
	MinFilter = LINEAR;
	MagFilter = LINEAR;
	MipFilter = LINEAR;
	AddressU = WRAP;
	AddressV = WRAP;
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
	float4 Depth : COLOR1;
};

VSOutput VSFunct(VSInput input)
{
	VSOutput output;

	output.Pos = float4(input.Pos, 1);
	output.TexCoord = input.TexCoord;

	return output;
}

PSOutput PSFunct(VSOutput input)
{
	PSOutput output = (PSOutput)0;

	float frDepth = tex2D(FRDepthSampler, input.TexCoord).r;
	float sDepth = tex2D(DepthSampler, input.TexCoord).r;

	float4 colour = tex2D(ColourSampler, input.TexCoord);

	float4 frColour = 0;
	float depth = 0.0f;

	bool isOpaque = true;

	if(sDepth > 0.0f)
	{
		if(sDepth > frDepth)
		{
			frColour += tex2D(FRColourSampler, input.TexCoord);
			float alpha = frColour.a;

			if(alpha < 0.99f)
			{
				isOpaque = false;
				depth = sDepth + frDepth;
			}
			else
				depth = frDepth;
		}
		else
			depth = sDepth;
	}
	else
	{
		frColour += tex2D(FRColourSampler, input.TexCoord);
		float alpha = frColour.a;

		depth = frDepth;

		if(alpha < 0.99f)
		{
			isOpaque = false;
			depth = sDepth + frDepth;
		}
		else
			depth = frDepth;
	}

	output.Depth = float4(depth, 0, 0, 1);

	if(isOpaque == true)
		output.Colour = frColour;
	else
	{
		float4 light = tex2D(LightSampler, input.TexCoord);
		light += light / 2;

		output.Colour = frColour + colour * light;
	}

	return output;
}

technique Combine
{
	pass P0
	{
		VertexShader = compile vs_3_0 VSFunct();
		PixelShader = compile ps_3_0 PSFunct();
	}
}
