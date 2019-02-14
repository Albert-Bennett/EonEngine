inline float3 CalculateNormal(half3 normal, float3x3 tbn)
{
	normal = normal * 2.0f - 1.0f;
	normal = normalize(mul(normal, tbn));
	normal = 0.5f * (normal + 1.0f);

	return normal;
} 
