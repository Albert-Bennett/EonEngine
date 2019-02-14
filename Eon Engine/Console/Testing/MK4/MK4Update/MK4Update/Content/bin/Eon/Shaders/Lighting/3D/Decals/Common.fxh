half2 EncodeNormal(half3 n)
{
    half2 enc = normalize(n.xy) * (sqrt(-n.z * 0.5 + 0.5));
    enc = enc * 0.5 + 0.5;

    return enc;
}

half2 CalculateNormal(half3 normal, float3x3 tbn)
{
	normal = normalize(2.0f * normal - 1.0f);
	normal = mul(normal, tbn);
	normal = 0.5f * (normal + 1.0f);

	return EncodeNormal(normal);
} 

