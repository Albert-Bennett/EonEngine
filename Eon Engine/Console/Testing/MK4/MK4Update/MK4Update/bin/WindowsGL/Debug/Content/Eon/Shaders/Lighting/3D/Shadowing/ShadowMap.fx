float4x4 World;
float4x4 ViewProj;

struct VSOutput
{
	float4 Pos : POSITION0;
	float Depth : TEXCOORD0;
};

VSOutput VSFunct(float4 pos : POSITION0)
{
	VSOutput output;

	float4 worldPos = mul(pos, World);
	output.Pos = mul(worldPos, ViewProj);

	output.Depth = output.Pos.z/ output.Pos.w;

	return output;
}

float4 PSFunct(VSOutput input) : COLOR0
{
	return float4(input.Depth, 0, 0, 1);
}

technique Create
{
	pass P0
	{
		VertexShader = compile vs_2_0 VSFunct();
		PixelShader = compile ps_2_0 PSFunct();
	}
}