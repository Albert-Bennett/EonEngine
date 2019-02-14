half2 EncodeNormal(half3 n)
{
    half2 enc = normalize(n.xy) * (sqrt(-n.z * 0.5 + 0.5));
    enc = enc * 0.5 + 0.5;

    return enc;
}

half2 CalculateNormal(half3 normal, float3x3 tbn)
{
	normal = 2.0f * normal - 1.0f;
	normal = normalize(mul(normal, tbn));
	normal = 0.5f * (normal + 1.0f);

	return EncodeNormal(normal);
} 

half2 CalculateBasicNormal(float3x3 worldView)
{
	half3 normal = half3(0.5, 0.5, 1.0);
	normal = normalize(mul(normal, worldView));
	normal = 0.5 * (normal + 1.0);

	return EncodeNormal(normal);
}
