#include "Common.fxh"

float4x4 IViewProj;
float3 CamPos;

float2 HalfPixel;

float3 Colour;
float Intensity;

float3 Direction;

texture Opaque;
sampler OpaqueSampler = sampler_state
{
	Texture = <Opaque>;
	AddressU = CLAMP;
	AddressV = CLAMP;
	MagFilter = POINT;
	MinFilter = POINT;
};

texture DepthMap;
sampler DepthSampler = sampler_state
{
	Texture = <DepthMap>;
	AddressU = CLAMP;
	AddressV = CLAMP;
	MagFilter = POINT;
	MinFilter = POINT;
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
	input.ScreenPos /= input.ScreenPos.w;

	float2 uv = 0.5f * (float2(input.ScreenPos.x, 
		-input.ScreenPos.y) + 1.0f) + HalfPixel;

	float4 depthSample = tex2D(DepthSampler, uv);

	float depth = UnPackDepth(depthSample.rg);
	clip(-depth + 0.9999f);

	float4 pos = float4(input.ScreenPos.xy, depth, 1.0f);
	pos = mul(pos, IViewProj);
	pos /= pos.w;

	float3 norm = tex2D(OpaqueSampler, uv).rgb;

	float2 specular = depthSample.ba;
	specular.y *= 0.5f;

	float3 lightVec = -Direction;

	float nl = max(0, dot(norm, lightVec));
	clip(nl - 0.0001f);

	float3 diffuse = nl * Colour;

	float3 h = normalize(reflect(lightVec, norm));
	float spec = nl * pow(saturate(dot(normalize(pos), h)), specular.x);

	return Intensity * float4(diffuse, spec * specular.y);
}

float4 PXSunFunct(VSOutput input) : COLOR0
{
	input.ScreenPos /= input.ScreenPos.w;

	float2 uv = 0.5f * (float2(input.ScreenPos.x, 
		-input.ScreenPos.y) + 1.0f) + HalfPixel;

	float4 opaqueSample = tex2D(OpaqueSampler, uv);

	float depth = opaqueSample.a;
	clip(-depth + 0.9999f);

	float4 pos = float4(input.ScreenPos.xy, depth, 1.0f);
	pos = mul(pos, IViewProj);
	pos /= pos.w;

	half3 norm = opaqueSample.rgb;

	float2 specular = tex2D(DepthSampler, uv).rg;
	specular.y *= 128;

	float3 lightVec = -Direction;

	float nl = max(0, dot(norm, lightVec));
	clip(nl - 0.0001f);

	float3 diffuse = nl * Colour;

	float3 r = normalize(reflect(-lightVec, norm));
	float eye = normalize(CamPos - pos);
	float spec = pow(saturate(dot(r, eye)), specular.x) * specular.y;

	return Intensity * float4(diffuse, spec);
}

technique SunLighting
{
    pass P0
    {
        VertexShader = compile vs_2_0 VSFunct();
        PixelShader = compile ps_2_0 PXSunFunct();
    }
}

technique SpotLighting
{
    pass P0
    {
        VertexShader = compile vs_2_0 VSFunct();
        PixelShader = compile ps_2_0 PXFunct();
    }
}