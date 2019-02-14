#define DOUBLEFILTER 14
#define SPLITS 4

float4x4 World;
float4x4 InvView;

float4x4 SplitViewProj[4];
float3 FrustumCorners[4];
float2 ClipPlanes[4];

int FilterSize;

float DepthBias;
float MinShadow;

float2 ShadowMapSize;

texture ShadowMap;
sampler ShadowMapSampler = sampler_state
{
	Texture = <ShadowMap>;
	AddressU = CLAMP;
	AddressV = CLAMP;
	MinFilter = POINT;
	MagFilter = POINT;
	MipFilter = POINT;
};

texture DepthMap;
sampler DepthSampler = sampler_state
{
	Texture = <DepthMap>;
	AddressU = CLAMP;
	AddressV = CLAMP;
	MinFilter = POINT;
	MagFilter = POINT;
	MipFilter = POINT;
};

struct VSInput
{
	float3 Pos : POSITION0;
	float3 TexCoord : TEXCOORD0;
};

struct VSOutput
{
	float4 Pos : POSITION0;
	float2 TexCoord : TEXCOORD0;
	float3 FrustumRay : TEXCOORD1;
};

VSOutput VSFunct(VSInput input)
{
	VSOutput output;

	output.Pos = 1.0f;
	output.Pos.x = input.Pos.x - (1.0f / ShadowMapSize.x);
	output.Pos.y = input.Pos.y + (1.0f / ShadowMapSize.y);
	output.Pos.z = input.Pos.z;

	output.TexCoord = input.TexCoord.xy;
	output.FrustumRay = FrustumCorners[input.TexCoord.z];

	return output;
}

float CalcPCFTerm(float depth, float2 texCoord)
{
	float term = 0.0f;

	if(FilterSize > 3)
	{
		int radius = (FilterSize - 1.0f) * 0.5f;

		[unroll(DOUBLEFILTER)] 
		for(int x = radius; x >= -radius; x--)
			[unroll(DOUBLEFILTER)] 
			for(int y = radius; y >= -radius; y--)
			{
				float2 uv = texCoord + float2(x, y);

				float shadow = tex2D(ShadowMapSampler, uv).r;	
				clip(-shadow + 0.9999f);

				float sample = depth > shadow + DepthBias ? MinShadow : 1.0f;

				float weightX = 1.0f;
				float weightY = 1.0f;

				if(x == radius)
					weightX = frac(texCoord.x * ShadowMapSize.x);	
				else if(x == -radius) 
					weightX = 1 - frac(texCoord.x * ShadowMapSize.x);

				if(y == radius)
					weightY = frac(texCoord.y * ShadowMapSize.y);			
				else if(y == -radius) 
					weightY = 1 - frac(texCoord.y * ShadowMapSize.y);

				term += sample * weightX * weightY;
			}

		term /= FilterSize * FilterSize;
		term *= 1.55f;
	}
	else
	{
		float shadow = tex2D(ShadowMapSampler, texCoord).r;
		clip(-shadow + 0.9999f);

		return depth > shadow + DepthBias ? MinShadow : 1.0f;
	}

	return term;
}

float4 PSFunct(VSOutput input) : COLOR0
{
	float depth = tex2D(DepthSampler, input.TexCoord).r;
	clip(-depth + 0.9999f);

	float4 pos = float4(depth * input.FrustumRay, 1.0f);

	int idx = 0;
	float offset = 0;

	[unroll(SPLITS - 1)]
	for(int i = 1; i < SPLITS; i++)
		if(pos.z <= ClipPlanes[i].x && pos.z > ClipPlanes[i].y)
		{
			idx = i;
			offset = (float)i/ (float)SPLITS;
		}

	float4 lightPos = mul(pos, mul(InvView, SplitViewProj[idx]));
	float lightDepth = lightPos.z / lightPos.w;

	if(lightDepth > 0.0f) 
	{
		//float2 uv = 0.5f * (float2(lightPos.x,
		//	-lightPos.y) + 1.0f) - (1.0f / ShadowMapSize.x);

		//float2 uv = 0.5f * lightPos.xy + float2(0.5f, 0.5f);
		//uv.y = 1.0f - uv.y;
		//uv += HalfPixel/ 2;

		float2 uv = 0.5f * lightPos.xy / lightPos.w + float2(0.5f, 0.5f);
		uv.x = uv.x / SPLITS + offset;
		uv.y = 1.0f - uv.y; 
		uv += 1.0f / ShadowMapSize;

		return float4(CalcPCFTerm(lightDepth, uv), 0, 0, 1);
	}

	return float4(0, 0, 0, 1);
}

technique Occlude
{
	pass P0
	{
		VertexShader = compile vs_3_0 VSFunct();
		PixelShader = compile ps_3_0 PSFunct();
	}
}
