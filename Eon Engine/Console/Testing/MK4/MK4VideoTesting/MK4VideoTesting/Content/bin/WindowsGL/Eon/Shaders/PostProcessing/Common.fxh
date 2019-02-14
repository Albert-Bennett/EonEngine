inline half ToGreyScale(half4 colour)
{
	return (colour.r + colour.g + colour.b) /3;
}

inline half4 AdjustSaturation(float4 colour, float saturation)
{
	half grey = ToGreyScale(colour); 
	return lerp(grey, colour, saturation);
}

inline float UnPackDepth(half2 packed)
{
	return packed.x + packed.y * (1.0f/ 255.0f);
}
