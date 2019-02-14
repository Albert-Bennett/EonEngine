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
	float4 Normals : COLOR0;
	float4 Depth : COLOR1;
	float4 Distortion : COLOR2;
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

	output.Distortion = 0.0f;
	output.Normals = 0.0f;
	output.Depth = 0.0f;

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
