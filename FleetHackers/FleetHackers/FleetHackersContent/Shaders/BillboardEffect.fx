float4x4 View;
float4x4 Projection;

bool AlphaTest = true;
bool AlphaTestGreater = true;
float AlphaTestValue = 0.5f;

texture ParticleTexture;
sampler2D texSampler = sampler_state {
	texture = < ParticleTexture > ;
};

float2 Size;
float3 Up;
float3 Side;

struct VertexShaderInput
{
    float4 Position : POSITION0;
	float2 UV : TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
	float2 UV : TEXCOORD0;
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

	float3 position = input.Position;
	float2 offset = float2((input.UV.x - 0.5f) * 2.0f, -(input.UV.y - 0.5f) * 2.0f);

	position += offset.x * Size.x * Side + offset.y * Size.y * Up;

    output.Position = mul(float4(position, 1), mul(View, Projection));
	output.UV = input.UV;

    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
	float4 color = tex2D(texSampler, input.UV);

	if (AlphaTest)
	{
		clip((color.a - AlphaTestValue) * (AlphaTestGreater ? 1 : -1));
	}

	return color;
}

technique Technique1
{
    pass Pass1
    {
        // TODO: set renderstates here.

        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
