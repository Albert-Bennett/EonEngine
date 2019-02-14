float4x4 World;
float4x4 View;
float4x4 Proj;
float4x4 WorldView;

int TextureRepeats = 16;

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
	float4 LightDir : TEXCOORD1;
};

VSOutput VSFunct(VSInput input)
{
	VSOutput output;

	float4 worldPos = mul(input.Pos, World);
	float4 viewPos = mul(worldPos, View);
	output.Pos = mul(viewPos, Proj);

	float biTan = cross(input.Norm, input.Tan);
	float3x3 BiTanNorm;

	BiTanNorm[0] = normalize(mul(input.Tan, (float3x3)WorldView));
	BiTanNorm[1] = normalize(mul(biTan, (float3x3)WorldView));
	BiTanNorm[2] = normalize(mul(input.Norm, (float3x3)WorldView));

	output.TexCoord = input.TexCoord;

	output.LightDir.xyz = mul(float3(0,-1,0), BiTanNorm);
	output.LightDir.w = 0;

	return output;
}

float4 PSFunct(VSOutput input) : COLOR0
{
	float4 weight = tex2D(WeightSampler, input.TexCoord);

	float3 r = float3(0,0,0);
	float3 g = float3(0,0,0);
	float3 b = float3(0,0,0);

	float3 rTex = float3(0,0,0);
	float3 gTex = float3(0,0,0);
	float3 bTex = float3(0,0,0);

	if(weight.r > 0)
	{
		r = tex2D(RedNMSampler, input.TexCoord * TextureRepeats);
		rTex = tex2D(RedSampler, input.TexCoord * TextureRepeats);
	}

	if(weight.g > 0)
	{
		g = tex2D(GreenNMSampler, input.TexCoord * TextureRepeats);
		gTex = tex2D(GreenSampler, input.TexCoord * TextureRepeats);
	}

	if(weight.b > 0)
	{
		b = tex2D(BlueNMSampler, input.TexCoord * TextureRepeats);
		bTex = tex2D(BlueSampler, input.TexCoord * TextureRepeats);
	}

	float3 norm = clamp(1.0f - weight.r - weight.b - weight.b, 0, 1);
	norm += weight.r * r + weight.g * g + weight.b * b;
	norm = norm * 2.0f - 1.0f;
	norm = normalize(norm);
	norm = 0.5f * (norm + 1.0f);

	float3 colour = clamp(1.0f - weight.r - weight.g - weight.b, 0, 1);
	 colour += weight.r * rTex + weight.g * gTex + weight.b * bTex;

	return float4(saturate(dot(norm, input.LightDir)) * colour, 1);
}

technique Texture
{
    pass P0
    {
        VertexShader = compile vs_3_0 VSFunct();
        PixelShader = compile ps_3_0 PSFunct();
    }
}
