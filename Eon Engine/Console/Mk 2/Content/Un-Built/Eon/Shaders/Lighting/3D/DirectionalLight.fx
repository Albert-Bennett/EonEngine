float4x4 IView;
float4x4 IViewProj;

float3 CamPos;
float2 GDSize;

float4 Colour;
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
	MipFilter = POINT;
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

	half4 norm = tex2D(OpaqueSampler, uv);
	half3 normal = mul(2.0f * (norm.xyz) - 1.0f, IView);
	normal = normalize(normal);

	float4 depthSample = tex2D(DepthSampler, uv);

	float specInten = depthSample.b;
	float specPow = depthSample.g;

	float depth = depthSample.r;
	clip(-depth + 0.99999f);

	float4 pos = 1.0f;
	pos.xy = input.ScreenPos.xy;
	pos.z = depth;

	pos = mul(pos, IViewProj);
	pos /= pos.w;

	float3 lightVec = -normalize(Direction);

	float nl = max(0, dot(normal, lightVec));
	float3 diffuse = nl * (Colour * Intensity);

	float3 r = normalize(reflect(-lightVec, normal));
	float eye = normalize(CamPos - pos);
	float spec = specInten * pow(saturate(dot(r, eye)), specPow);

	return float4(diffuse, spec);

	//return shadow * float4(diffuse, spec);
}

technique Lighting
{
    pass P0
    {
        VertexShader = compile vs_2_0 VSFunct();
        PixelShader = compile ps_2_0 PXFunct();
    }
}