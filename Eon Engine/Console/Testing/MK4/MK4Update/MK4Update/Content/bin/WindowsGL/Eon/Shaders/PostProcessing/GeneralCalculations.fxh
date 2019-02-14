float ToGreyScale(float4 colour)
{
	return (colour.r + colour.g + colour.b) /3;
}

float4 AdjustSaturation(float4 colour, float saturation)
{
	float grey = ToGreyScale(colour); 
	return lerp(grey, colour, saturation);
}
