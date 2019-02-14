float4x4 World;
float4x4 ViewProj;

struct VSOutput
{
	float4 Pos : POSITION0;
	float2 Depth : TEXCOORD0;
};

VSOutput VSFunct(float4 pos : POSITION0)
{
	VSOutput output;

	float4 worldPos = mul(pos, World);
	output.Pos = mul(worldPos, ViewProj);

	output.Depth = output.Pos.zw;

	return output;
}

float4 PSFunct(VSOutput input) : COLOR0
{
	float depth = input.Depth.x / input.Depth.y;

	return float4(depth, 0, 0, 1);
}

technique Create
{
	pass P0
	{
		VertexShader = compile vs_2_0 VSFunct();
		PixelShader = compile ps_2_0 PSFunct();
	}
}