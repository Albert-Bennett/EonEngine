float SpecPow;
float Intensity;

float3 Direction;
float3 Colour;

texture NormalMap;
sampler NormalMapSampler = sampler_state
{
	Texture = NormalMap;
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

VertexPixel VSLight(float4 pos:POSITION0, float2 texCoord:TEXCOORD0, float4 colour:COLOR0)
{
	VertexPixel output = (VertexPixel)0;

	output.Pos = pos;
	output.TexCoord = texCoord;
	output.Colour = colour;

	return output;
}

float4 PSLight(VertexPixel input):COLOR0
{
	float3 normal = 2.0f * (tex2D(NormalMapSampler, input.TexCoord)).rgb - 1.0f;

	float nl = max(0, dot(normal, Direction));

	float3 r = normalize(2 * nl * normal - Direction);
	float specular = min(pow(saturate(dot(r, SpecPow)), 10), nl);
	specular *= Intensity;

	return nl * float4(Colour, 1) * Intensity + (specular * SpecPow); 
}

technique Render
{
	pass P0
	{
		VertexShader = compile vs_2_0 VSLight();
		PixelShader = compile ps_2_0 PSLight();
	}
}
