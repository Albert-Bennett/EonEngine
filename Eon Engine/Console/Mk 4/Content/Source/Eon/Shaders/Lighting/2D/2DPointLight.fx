float2 GDSize;
float SpecPow;

float Intensity;
float Radius;

float3 Pos;
float3 Colour;

texture NormalMap;
sampler NormalMapSampler = sampler_state
{
	Texture = <NormalMap>;
	AddressU = CLAMP;
	AddressV = CLAMP;
	MagFilter = POINT;
	MinFilter = POINT;
	MipFilter = POINT;
};

struct VertexPixel
{
	float4 Pos : POSITION;
	float2 TexCoord : TEXCOORD0;
	float4 Colour : COLOR0; 
};

VertexPixel VSLight(float4 pos : POSITION0, float2 texCoord : TEXCOORD0)
{
	VertexPixel output = (VertexPixel)0;

	output.Pos = pos;
	output.TexCoord = texCoord;

	return output;
}

float4 PSLight(VertexPixel input) : COLOR0
{
	float3 normal = 2.0f * (tex2D(NormalMapSampler, input.TexCoord)).rgb - 1.0f;

	float3 pos;
	pos.x = GDSize.x * input.TexCoord.y;
	pos.y = GDSize.y * input.TexCoord.x;
	pos.z = 0;

	float3 lightVec = Pos - pos;

	float attenuation = saturate(1.0f - length(lightVec)/ Radius);

	if(attenuation > 0.001f)
	{
		float3 lightNorm = normalize(lightVec);
		float3 halfVec = float3(0, 0, 1);

		float nl = max(0, dot(normal, lightNorm));

		float3 r = normalize(2 * nl * normal - lightVec);
		float specular = min(pow(saturate(dot(r, halfVec)), 10), nl);
		specular *= Intensity;

		return attenuation * float4(Colour.rgb, 1) * Intensity + (specular * SpecPow);
	}
	else
		discard;

	return float4(0,0,0,0);
}

technique Render
{
	pass P0
	{
		VertexShader = compile vs_2_0 VSLight();
		PixelShader = compile ps_2_0 PSLight();
	}
}


