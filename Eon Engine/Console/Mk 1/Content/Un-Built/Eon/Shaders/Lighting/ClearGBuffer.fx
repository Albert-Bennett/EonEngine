struct VSInput
{
	float3 Pos : POSITION0;
};

struct VSOutput
{
	float4 Pos : POSITION0;
};

struct PSOutput
{
	float4 Colour : COLOR0;
	float4 Normals : COLOR1;
	float4 Depth : COLOR2;
};

VSOutput VSFunct(VSInput input)
{
	VSOutput output;
	output.Pos = float4(input.Pos, 1);

	return output;
}

PSOutput PSFunct(VSOutput input)
{
	PSOutput output;

	output.Colour = 0.0f;

	output.Normals.rgb = 0.5f;
	output.Normals.a = 0.0f;

	output.Depth = 0.0f;
	output.Depth.r = 1.0f;

	return output;
}

technique Clear
{
    pass Pass1
    {
        VertexShader = compile vs_2_0 VSFunct();
        PixelShader = compile ps_2_0 PSFunct();
    }
}
