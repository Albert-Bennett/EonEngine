float4x4 IViewProj;
float4x4 PrevViewProj;

float2 MaxVelocity;
float2 GDSize;

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

texture DepthMap;
sampler DepthSampler = sampler_state
{
	Texture = DepthMap;
	AddressU = CLAMP;
	AddressV = CLAMP;
	magfilter = LINEAR;
	minfilter = LINEAR;
	mipfilter = LINEAR;
};

texture DistortionMap;
sampler DistortSampler = sampler_state
{
	Texture = DistortionMap;
	AddressU = CLAMP;
	AddressV = CLAMP;
	magfilter = LINEAR;
	minfilter = LINEAR;
	mipfilter = LINEAR;
};

struct VSInput
{
	float3 Pos: POSITION0;
};

struct VSOutput
{
	float4 Pos : POSITION0;
	float4 ScreenPos : TEXCOORD0;
};

VSOutput VSFunct(VSInput input)
{
	VSOutput output;

	output.Pos = float4(input.Pos, 1);
	output.ScreenPos = output.Pos;

	return output;
}

float4 PXFunct(VSOutput input) : COLOR0
{
    input.ScreenPos.xy /= input.ScreenPos.w;

	float2 uv = 0.5f * (float2(input.ScreenPos.x, -input.ScreenPos.y) + 1)-
		float2(1.0 / GDSize);

	float4 colour = tex2D(SceneSampler, uv); 
	float mask = tex2D(DistortSampler, uv).b;

	if(mask == 0.0f)
	{
		float depth = tex2D(DepthSampler, uv).r;

		float4 pos = 1.0f;
		pos.xy = input.ScreenPos.xy;
		pos.z = depth;

		float4 currPos = mul(pos, IViewProj);
		currPos /= currPos.w;

		float4 prevPos = mul(currPos, PrevViewProj);
		prevPos /= prevPos.w;

		float2 velocity = (pos - prevPos) / 2.0f;
		velocity = clamp(velocity, -MaxVelocity, MaxVelocity);

		int samples = 12;

		[unroll(12)] for(int i = 0; i < samples; i++)
		{
			colour += tex2D(SceneSampler, uv);
			uv += velocity;
		}

		colour /= samples;
	
		return colour;
	}
	else
		return colour;
}

technique MotionBlur
{
    pass P0
    {
        PixelShader = compile ps_2_0 PXFunct();
    }
}
