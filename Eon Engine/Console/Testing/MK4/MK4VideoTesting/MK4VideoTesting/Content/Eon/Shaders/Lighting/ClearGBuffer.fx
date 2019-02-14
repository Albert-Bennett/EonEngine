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
	float4 Opaque : COLOR0;
	float4 Material : COLOR1;
	float4 Colour : COLOR2;
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

	output.Opaque = 0.0f;
	output.Material = 0.0f;
	output.Colour = 0.0f;

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
