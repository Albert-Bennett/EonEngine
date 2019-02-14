inline half2 PackDepth(float depth)
{
	return half2(floor(depth * 255.0f) / 255.0f,
		frac(depth * 255.0f));
}

inline float UnPackDepth(half2 packed)
{
	return packed.x + packed.y * (1.0f/ 255.0f);
}
