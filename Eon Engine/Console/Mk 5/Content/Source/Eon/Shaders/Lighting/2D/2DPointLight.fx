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
};

VertexPixel VSLight(float4 pos : POSITION0, float2 texCoord : TEXCOORD0)
{
	VertexPixel output;

	output.Pos = pos;
	output.TexCoord = texCoord;

	return output;
}

float4 PSLight(VertexPixel input) : COLOR0
{
	float3 pos;
	pos.x = GDSize.x * input.TexCoord.x;
	pos.y = GDSize.y * input.TexCoord.y;
	pos.z = 0;

	float3 lightVec = Pos - pos;

	float attenuation = saturate(1.0f - length(lightVec) / Radius);

	clip(attenuation - 0.0001f); 

	lightVec = normalize(lightVec);
	float3 halfVec = float3(0, 0, 1);

	float3 normal = 2.0f * (tex2D(NormalMapSampler, input.TexCoord)).rgb - 1.0f;

	float nl = max(0, dot(normal, lightVec));
	clip(nl - 0.0001f);

	float3 diffuse = Colour * nl;

	//float3 r = normalize(2 * nl * normal - lightVec);
	//float specular = min(pow(saturate(dot(r, halfVec)), 5), nl) * (SpecPow * 0.5f);

	float3 r = normalize(reflect(lightVec, normal));
	float specular = nl * pow(saturate(dot(normalize(pos), r)), SpecPow) * (SpecPow * 0.5f);

	return Intensity * attenuation * float4(diffuse, specular);
}

technique Render
{
	pass P0
	{
		VertexShader = compile vs_2_0 VSLight();
		PixelShader = compile ps_2_0 PSLight();
	}
}


