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

texture C0;
sampler C0Sampler = sampler_state
{
	texture = <C0>;
	AddressU = CLAMP;
	AddressV = CLAMP;
	MagFilter = POINT;
	MinFilter = POINT;
	MipFilter = POINT;
};

texture C1;
sampler C1Sampler = sampler_state
{
	texture = <C1>;
	AddressU = CLAMP;
	AddressV = CLAMP;
	MagFilter = POINT;
	MinFilter = POINT;
	MipFilter = POINT;
};

texture C2;
sampler C2Sampler = sampler_state
{
	texture = <C2>;
	AddressU = CLAMP;
	AddressV = CLAMP;
	MagFilter = POINT;
	MinFilter = POINT;
	MipFilter = POINT;
};

texture C3;
sampler C3Sampler = sampler_state
{
	texture = <C3>;
	AddressU = CLAMP;
	AddressV = CLAMP;
	MagFilter = POINT;
	MinFilter = POINT;
	MipFilter = POINT;
};

texture DepthMap;
sampler DepthSampler = sampler_state
{
	texture = <DepthMap>;
	AddressU = CLAMP;
	AddressV = CLAMP;
	MagFilter = POINT;
	MinFilter = POINT;
	MipFilter = POINT;
};

texture D0;
sampler D0Sampler = sampler_state
{
	texture = <D0>;
	AddressU = CLAMP;
	AddressV = CLAMP;
	MagFilter = POINT;
	MinFilter = POINT;
	MipFilter = POINT;
};

texture D1;
sampler D1Sampler = sampler_state
{
	texture = <D1>;
	AddressU = CLAMP;
	AddressV = CLAMP;
	MagFilter = POINT;
	MinFilter = POINT;
	MipFilter = POINT;
};

texture D2;
sampler D2Sampler = sampler_state
{
	texture = <D2>;
	AddressU = CLAMP;
	AddressV = CLAMP;
	MagFilter = POINT;
	MinFilter = POINT;
	MipFilter = POINT;
};

texture D3;
sampler D3Sampler = sampler_state
{
	texture = <D3>;
	AddressU = CLAMP;
	AddressV = CLAMP;
	MagFilter = POINT;
	MinFilter = POINT;
	MipFilter = POINT;
};

texture OpaqueMap;
sampler OpaqueSampler = sampler_state
{
	texture = <OpaqueMap>;
	AddressU = CLAMP;
	AddressV = CLAMP;
	MagFilter = POINT;
	MinFilter = POINT;
	MipFilter = POINT;
};

texture O0;
sampler O0Sampler = sampler_state
{
	texture = <O0>;
	AddressU = CLAMP;
	AddressV = CLAMP;
	MagFilter = POINT;
	MinFilter = POINT;
	MipFilter = POINT;
};

texture O1;
sampler O1Sampler = sampler_state
{
	texture = <O1>;
	AddressU = CLAMP;
	AddressV = CLAMP;
	MagFilter = POINT;
	MinFilter = POINT;
	MipFilter = POINT;
};

texture O2;
sampler O2Sampler = sampler_state
{
	texture = <O2>;
	AddressU = CLAMP;
	AddressV = CLAMP;
	MagFilter = POINT;
	MinFilter = POINT;
	MipFilter = POINT;
};

texture O3;
sampler O3Sampler = sampler_state
{
	texture = <O3>;
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
	float2 ScreenPos : TEXCOORD0;
};

struct PSOutput
{
	float4 DepthMap : COLOR0;
	float4 OpaqueMap : COLOR1;
	float4 Colour : COLOR2; 
};

VSOutput VSFunct(VSInput input)
{
	VSOutput output;

	output.Pos = float4(input.Pos, 1.0f);
	output.ScreenPos = input.TexCoord - HalfPixel;

	return output;
}

float3 RectifyOpaqueData(float4 sceneOpaque, float4 opaque, float alpha)
{
	half3 sceneNorm = sceneOpaque.rgb;
	half3 norm = opaque.rgb;

	sceneNorm += norm * alpha;
	sceneNorm = normalize(sceneNorm);

	float3 output = sceneNorm;

	return output;
}

void FindPeelData(float3 colour, float4 opaque,
	 float4 peelColour, float4 peelOpaque,
	 out float4 outColour, out float4 outOpaque)
{
	[branch]
	if(peelColour.a < 1.0f)
	{
		outOpaque = float4(RectifyOpaqueData(opaque, peelOpaque, peelColour.a), 0.0f);
		outColour = float4(colour + (peelColour.rgb * peelColour.a), 1.0f);
	}
	else
	{
		outOpaque = peelOpaque;
		outColour = peelColour;
	}
}

PSOutput PSFunct(VSOutput input)
{
	PSOutput output;

	output.DepthMap = tex2D(DepthSampler, input.ScreenPos);
	output.OpaqueMap = tex2D(OpaqueSampler, input.ScreenPos);
	output.Colour = tex2D(SceneSampler, input.ScreenPos);

	[unroll(4)]
	for(int i = 0; i < 4; i++)
	{
		[branch]
		if(i == 0)
		{
			float4 peelDepth = tex2D(D0Sampler, input.ScreenPos);

			if(peelDepth.r > 0.0f)
			{
				output.DepthMap = peelDepth;

				FindPeelData(output.Colour.rgb, output.OpaqueMap,
					tex2D(C0Sampler, input.ScreenPos),
					tex2D(O0Sampler, input.ScreenPos),
					output.Colour, output.OpaqueMap);
			}
		}
		else if(i == 1)
		{
			float4 peelDepth = tex2D(D1Sampler, input.ScreenPos);

			if(peelDepth.r > 0.0f)
			{
				output.DepthMap = peelDepth;

				FindPeelData(output.Colour.rgb, output.OpaqueMap,
					tex2D(C1Sampler, input.ScreenPos),
					tex2D(O1Sampler, input.ScreenPos),
					output.Colour, output.OpaqueMap);
			}
		}
		else if(i == 2)
		{
			float4 peelDepth = tex2D(D2Sampler, input.ScreenPos);

			if(peelDepth.r > 0.0f)
			{
				output.DepthMap = peelDepth;

				FindPeelData(output.Colour.rgb, output.OpaqueMap,
					tex2D(C2Sampler, input.ScreenPos),
					tex2D(O2Sampler, input.ScreenPos),
					output.Colour, output.OpaqueMap);
			}
		}
		else
		{
			float4 peelDepth = tex2D(D3Sampler, input.ScreenPos);

			if(peelDepth.r > 0.0f)
			{
				output.DepthMap = peelDepth;

				FindPeelData(output.Colour.rgb, output.OpaqueMap,
					tex2D(C3Sampler, input.ScreenPos),
					tex2D(O3Sampler, input.ScreenPos),
					output.Colour, output.OpaqueMap);
			}
		}
	}

	return output;
}

technique Merge
{
	pass P0
	{
		VertexShader = compile vs_2_0 VSFunct();
		PixelShader = compile ps_3_0 PSFunct();
	}
}
