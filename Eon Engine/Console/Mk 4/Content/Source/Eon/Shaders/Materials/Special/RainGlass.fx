float4x4 World;
float4x4 View;
float4x4 Proj;

texture Distortion;
sampler DistortSampler = sampler_state
{
    Texture = <Distortion>;
	MinFilter = ANISOTROPIC;
	MagFilter = LINEAR;
	MipFilter = LINEAR;
	AddressU = WRAP;
	AddressV = WRAP;
	MaxAnisotropy = 16;
};

texture DepthMap;
sampler DepthSampler = sampler_state
{
    Texture = <DepthMap>;
	AddressU = CLAMP;
	AddressV = CLAMP;
	MagFilter = POINT;
	MinFilter = POINT;
	MipFilter = POINT;
};

struct VSInput
{
	float4 Pos : POSITION0;
	float2 TexCoord : TEXCOORD0;
};

struct VSOutput
{
	float4 Pos : SV_POSITION;
	float2 TexCoord : TEXCOORD0;
	float Depth : TEXCOORD1;
};

struct PSOutput
{
	float4 Colour : COLOR0;
};

VSOutput VSFunct(VSInput input)
{
	VSOutput output;

	float4 worldPos = mul(input.Pos, World);
	output.Pos = mul(worldPos, mul(View, Proj));

	output.Depth = output.Pos.z/ output.Pos.w;

	output.TexCoord = input.TexCoord;

	return output;
}

PSOutput PSFunct(VSOutput input)
{
	PSOutput output;

	float inDepth = tex2D(DepthSampler, input.TexCoord).r;
	
	if(input.Depth == inDepth)
		output.Colour = float4(tex2D(DistortSampler, 
			input.TexCoord).xyz, 1.0f);
	else
		output.Colour = 0.0f;

	return output;
}

technique Opaque
{
	pass P0
	{
		VertexShader = compile vs_2_0 VSFunct();
		PixelShader = compile ps_2_0 PSFunct();
	}
}


