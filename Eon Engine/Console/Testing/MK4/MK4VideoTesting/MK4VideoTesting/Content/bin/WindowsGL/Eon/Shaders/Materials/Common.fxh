inline float3 CalculateNormal(float3 normal, float3x3 tbn)
{
	normal = normal * 2.0f - 1.0f;
	normal = normalize(mul(normal, tbn));

	return normal;
} 

inline float3 CalculateBasicNormal(float3x3 worldView)
{
	half3 normal = half3(0.5, 0.5, 1.0);
	normal = normalize(mul(normal, worldView));
	normal = 0.5 * (normal + 1.0);

	return normal;
}

inline half2 PackDepth(float depth)
{
	return half2(floor(depth * 255.0f) / 255.0f,
		frac(depth * 255.0f));
}
