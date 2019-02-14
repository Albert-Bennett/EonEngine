float4x4 World;
float4x4 View;
float4x4 Proj;
float4x4 WorldView;

int TextureRepeats = 64;

texture WeightMap;
sampler WeightSampler= sampler_state
{
	texture = <WeightMap>;
	MinFilter = LINEAR;
	MagFilter = LINEAR;
	MipFilter = LINEAR;
	AddressU = WRAP;
	AddressV = WRAP;
};

texture RedTexture;
sampler RedSampler= sampler_state
{
	texture = <RedTexture>;
	MinFilter = LINEAR;
	MagFilter = ANISOTROPIC;
	MipFilter = LINEAR;
	AddressU = WRAP;
	AddressV = WRAP;
};

texture BlueTexture;
sampler BlueSampler= sampler_state
{
	texture = <BlueTexture>;
	MinFilter = LINEAR;
	MagFilter = ANISOTROPIC;
	MipFilter = LINEAR;
	AddressU = WRAP;
	AddressV = WRAP;
};

texture GreenTexture;
sampler GreenSampler= sampler_state
{
	texture = <GreenTexture>;
	MinFilter = LINEAR;
	MagFilter = ANISOTROPIC;
	MipFilter = LINEAR;
	AddressU = WRAP;
	AddressV = WRAP;
};

texture RedNM;
sampler RedNMSampler= sampler_state
{
	texture = <RedTexture>;
	MinFilter = LINEAR;
	MagFilter = ANISOTROPIC;
	MipFilter = LINEAR;
	AddressU = WRAP;
	AddressV = WRAP;
};

texture BlueNM;
sampler BlueNMSampler= sampler_state
{
	texture = <BlueNM>;
	MinFilter = LINEAR;
	MagFilter = ANISOTROPIC;
	MipFilter = LINEAR;
	AddressU = WRAP;
	AddressV = WRAP;
};

texture GreenNM;
sampler GreenNMSampler= sampler_state
{
	texture = <GreenNM>;
	MinFilter = LINEAR;
	MagFilter = ANISOTROPIC;
	MipFilter = LINEAR;
	AddressU = WRAP;
	AddressV = WRAP;
};

struct VSInput
{
	float4 Pos : POSITION0;
	float2 TexCoord : TEXCOORD0;
	float3 Norm : NORMAL0;
	float3 Tan : TANGENT0;
};

struct VSOutput
{
	float4 Pos : POSITION0;
	float2 TexCoord : TEXCOORD0;
	float3 Depth : TEXCOORD1;
	float3x3 BiTanNorm : TEXCOORD2;
};

struct PSOutput
{
	float4 Norm : COLOR0;
	float4 Depth : COLOR1;
};

VSOutput VSFunct(VSInput input)
{
	VSOutput output;

	float4 worldPos = mul(input.Pos, World);
	float4 viewPos = mul(worldPos, View);
	output.Pos = mul(viewPos, Proj);

	output.Depth.x = output.Pos.z;
	output.Depth.y = output.Pos.w;
	output.Depth.z = viewPos.z;

	float biTan = cross(input.Norm, input.Tan);

	output.BiTanNorm[0] = normalize(mul(input.Tan, (float3x3)WorldView));
	output.BiTanNorm[1] = normalize(mul(biTan, (float3x3)WorldView));
	output.BiTanNorm[2] = normalize(mul(input.Norm, (float3x3)WorldView));

	output.TexCoord = input.TexCoord;

	return output;
}

PSOutput PSFunct(VSOutput input)
{
	PSOutput output;

	float3 r = tex2D(RedNMSampler, input.TexCoord * TextureRepeats);
	float3 g = tex2D(GreenNMSampler, input.TexCoord * TextureRepeats);
	float3 b = tex2D(BlueNMSampler, input.TexCoord * TextureRepeats);

	float4 weight = tex2D(WeightSampler, input.TexCoord);

	float3 norm = clamp(1.0f - weight.r - weight.b - weight.b, 0, 1);
	norm += weight.r * r + weight.g * g + weight.b * b;
	norm = norm * 2.0f - 1.0f;
	norm = normalize(norm);
	norm = 0.5f * (norm + 1.0f);

	output.Norm.xyz = norm;
	output.Norm.a = 255 - weight.a;

	output.Depth = input.Depth.x / input.Depth.y;
	output.Depth.y = input.Depth.z;
	output.Depth.a = tex2D(BlueNMSampler, input.TexCoord).a;

	return output;
}

technique Opaque
{
    pass P0
    {
        VertexShader = compile vs_3_0 VSFunct();
        PixelShader = compile ps_3_0 PSFunct();
    }
}

struct RVSOutput
{
	float4 Pos : POSITION0;
	float2 TexCoord : TEXCOORD0;
};

RVSOutput RVSFunct(float2 texCoord : TEXCOORD0, float4 pos : POSITION0)
{
	RVSOutput output;

	float4 worldPos = mul(pos, World);
	float4 viewPos = mul(worldPos, View);
	output.Pos = mul(viewPos, Proj);

	output.TexCoord = texCoord;

	return output;
}

float4 RPSFunct(RVSOutput input) : COLOR0
{
	float3 r = tex2D(RedSampler, input.TexCoord * TextureRepeats);
	float3 g = tex2D(GreenSampler, input.TexCoord * TextureRepeats);
	float3 b = tex2D(BlueSampler, input.TexCoord * TextureRepeats);

	float3 weight = tex2D(WeightSampler, input.TexCoord);

	float3 colour = clamp(1.0f - weight.r - weight.b - weight.b, 0, 1);
	colour += weight.r * r + weight.g * g + weight.b * b;

	return float4(colour, 1);
}

technique Colour
{
	pass P0
	{
		VertexShader = compile vs_2_0 RVSFunct();
        PixelShader = compile ps_2_0 RPSFunct();
	}
}
