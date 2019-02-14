float4 Clip(float4 colour)
{
	float4 clipped = colour;

	clip(-clipped.x + 0.95f);
	clip(-clipped.y + 0.95f);
	clip(-clipped.z + 0.95f);
	clip(-clipped.w + 0.95f);

	return clipped;
}
