float4x4 World;
float4x4 View;
float4x4 Proj;

float2 Speed;

texture ColourMap;
sampler ColourMapSampler = sampler_state
{
   Texture = <ColourMap>;
   MinFilter = Linear;
   MagFilter = Linear;
   MipFilter = Linear;   
   AddressU  = Clamp;
   AddressV  = Clamp;
};

texture TransparencyMap;
sampler TransparencyMapSampler = sampler_state
{
   Texture = <TransparencyMap>;
   MinFilter = Linear;
   MagFilter = Linear;
   MipFilter = Linear;   
   AddressU  = Clamp;
   AddressV  = Clamp;
};


struct VSInput
{
	float4 Pos : POSITION;
	float3 TexCoord : TEXCOORD0;
};

struct VSOutput
{
    float4 Pos : POSITION;
    float2 TexCoord : TEXCOORD0;
	float3 Depth : TEXCOORD1;
};

struct PSOutput
{
	float4 Colour : COLOR0;
	float4 Depth : COLOR1;
};

VSOutput VSFunct(VSInput input)
{
    VSOutput output = (VSOutput)0;

	float4 worldPos = mul(input.Pos, World);
	float4 viewPos = mul(worldPos, View);
	 
	output.Pos = mul(viewPos, Proj);

	output.Depth.x = output.Pos.z;
	output.Depth.y = output.Pos.w;
	output.Depth.z = viewPos.z;

	output.TexCoord = input.TexCoord;

	return output;
}

PSOutput PXFunct(VSOutput input)
{
	PSOutput output;

	float2 texCoord = input.TexCoord;
	texCoord += Speed;

	if(texCoord.x >= 1.0f)
		texCoord.x = 0.0f;
	else if(texCoord.x <= 0.0f)
		texCoord.x = 1.0f;

	if(texCoord.y >= 1.0f)
		texCoord.y = 0.0f;
	else if(texCoord.y <= 0.0f)
		texCoord.y = 1.0f;

    float3 gloss = tex2D(TransparencyMapSampler, texCoord).rgb;
	float3 colour = tex2D(ColourMapSampler, texCoord).rgb;

	output.Colour = float4(colour, gloss.r);

	output.Depth = 0.0f;
	output.Depth.r = input.Depth.x / input.Depth.y;
	output.Depth.a = 1.0f;

	clip(-output.Depth + 0.99999f);

	return output;
}

technique Transparency
{
    pass P0
    {
        VertexShader = compile vs_2_0 VSFunct();
        PixelShader = compile ps_2_0 PXFunct();
    }
}
