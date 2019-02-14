half3 DecodeNormal(half2 enc)
{
    half2 fenc = enc * 4 - 2;
    half f = dot(fenc, fenc);
    half g = sqrt(1 - f / 4);

    half3 n;
    n.xy = fenc * g;
    n.z = 1 - f / 2;

    return n;
}

half2 EncodeNormal(half3 n)
{
    half p = sqrt(n.z * 8 + 8);
    return n.xy / p + 0.5;
}

half2 CalculateNormal(half3 normal, float3x3 tbn)
{
	normal = 2.0 * normal - 1.0;
	normal = normalize(mul(normal, tbn));
	normal = 0.5 * (normal + 1.0);

	return EncodeNormal(normal);
} 

half2 CalculateBasicNormal(float3x3 worldView)
{
	half3 normal = half3(0.5, 0.5, 1.0);
	normal = normalize(mul(normal, worldView));
	normal = 0.5 * (normal + 1.0);

	return EncodeNormal(normal);
}

float ToGreyScale(float4 colour)
{
	return (colour.r + colour.g + colour.b) /3;
}

float4 AdjustSaturation(float4 colour, float saturation)
{
	float grey = ToGreyScale(colour); 
	return lerp(grey, colour, saturation);
}
