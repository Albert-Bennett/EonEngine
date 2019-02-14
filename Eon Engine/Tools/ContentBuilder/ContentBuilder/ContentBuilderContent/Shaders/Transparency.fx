float4x4 worldViewProj;
float4x4 world;
float4 lightDirection;
float4 viewPos;
float4 lightColour;
float4 specularColour;
float4 ambientColour;

texture ColourMap;
sampler ColourMapSampler = sampler_state
{
   Texture = <ColorMap>;
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
	float3 Normal : NORMAL;
	float3 TexCoord : TEXCOORD0;
};

struct VSOutput
{
    float4 Pos : POSITION;
    float2 TexCoord : TEXCOORD0;
    float3 LightDirect : TEXCOORD1;
    float3 Normal : TEXCOORD2;
    float3 ViewDirect : TEXCOORD3;
};

VSOutput VSFunct(VSInput input)
{
    VSOutput output = (VSOutput)0;

	output.Pos = mul(input.Pos, worldViewProj);
	output.Normal = mul(input.Normal, world);

	output.LightDirect = lightDirect;

	float4 worldPos = mul(input.Pos, world);

	output.ViewDirect = viewPos - worldPos;
	output.TexCoord = input.TexCoord;

	return output;
}

float4 PXFunct(VSOutput input) : COLOR0
{
    float3 gloss = tex2D(TransparencyMapSampler, input.TexCoord).rgb;
	float4 colour = tex2D(ColourMapSampler, input.TexCoord);
	float3 normal = normalize(input.Normal);
	float3 lightDirect = normalize(input.LightDirect);
	float3 viewDirect = normalize(input.ViewDirect);

	float diff = saturate(dot(normal, lightDirect));
	float3 reflect = normalize(2 * diff * normal - lightDirect);
	float spec = pow(saturate(dot(reflect, viewDirect)), 20);

	spec = spec * gloss.g;

	return ambient + lightColour * colour * diff + SpecularColour * spec;
}

technique Transparency
{
    pass P0
    {
        // TODO: set renderstates here.

        VertexShader = compile vs_2_0 VSFunct();
        PixelShader = compile ps_2_0 PXFunct();
    }
}
