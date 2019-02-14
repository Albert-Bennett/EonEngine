float4x4 ViewProj;

float3 Up;

float Scale;
float3 Pos;
float4x4 Rot;

float Width;
float Height;

int Row;
int Col;

int Cols;
int Rows;

texture Texture;
sampler TextureMapSampler = sampler_state
{
   Texture = <Texture>;
   MinFilter = Linear;
   MagFilter = Linear;
   MipFilter = Linear;   
   AddressU  = Clamp;
   AddressV  = Clamp;
};

struct VSInput
{
	float3 Pos : POSITION;
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

	float3 pos = input.Pos;
	float3 eye = ViewProj._m02_m12_m22;

	float3 side = cross(eye, Up);
	side = normalize(side);
	 
	pos += (input.TexCoord.x - 0.5f) * side * Scale;
	pos += (0.5f - input.TexCoord.y) * Up * Scale;
	pos = mul(pos, Rot);
	pos += Pos;

	output.Pos = mul(float4(pos, 1), ViewProj);

	output.Depth.x = output.Pos.z;
	output.Depth.y = output.Pos.w;
	output.Depth.z = 1;

	output.TexCoord = input.TexCoord;

	return output;
}

PSOutput PXFunct(VSOutput input)
{
	PSOutput output;

	float2 uv = input.TexCoord;
	uv.x = (uv.x / Cols) + (Col * Width);
	uv.y = (uv.y / Rows) + (Row * Height); 

	output.Colour = tex2D(TextureMapSampler, uv);

	if(output.Colour.a < 0.2f)
		output.Depth = 0;
	else
	{
		output.Depth = 0.0f;
		output.Depth.r = input.Depth.x / input.Depth.y;
		output.Depth.a = 1.0f;

		clip(-output.Depth.r + 0.999f);
	}

	return output;
}

technique ForwardRender
{
    pass P0
    {
        VertexShader = compile vs_2_0 VSFunct();
        PixelShader = compile ps_2_0 PXFunct();
    }
}