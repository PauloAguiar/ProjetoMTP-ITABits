// --------------------------------------------------------------------------------------------- //
// Flare Particle Effect
// --------------------------------------------------------------------------------------------- //
//
// Renders flare particles from point sprites
//

/// <summary>Concatenated world, view and projection matrix</summary>
float4x4 WorldViewProjection : VIEWPROJECTION;

Texture FlareTexture;

// --------------------------------------------------------------------------------------------- //
// Texture Samplers
// --------------------------------------------------------------------------------------------- //

sampler FlareSampler = sampler_state {
  texture = <FlareTexture>;
  magfilter = LINEAR;
  minfilter = LINEAR;
  mipfilter= LINEAR;
  AddressU = mirror;
  AddressV = mirror;
};

// --------------------------------------------------------------------------------------------- //
// Supporting Structures
// --------------------------------------------------------------------------------------------- //

/// <summary>Vertex shader inputs, taken from the vertices themselves.</summary>
struct VertexShaderInput {

  /// <summary>Position of the particle in world space</summary>
  float4 Position : POSITION0;
  
  /// <summary>The flare's intensity, controlling the point size</summary>
  float Power : PSIZE0;
  
  /// <summary>Color of the flare</summary>
  float4 Color : COLOR0;

};

/// <summary>Vertex shader output values. These are sent to the pixel shader.</summary>
struct VertexShaderOutput {

  /// <summary>Position of the particle in screen space</summary>
  float4 Position : POSITION0;

  /// <summary>Point size of the particles</summary>
  float Size : PSIZE0;

  /// <summary>Color of the flare</summary>
  float4 Color : COLOR0;

};

// --------------------------------------------------------------------------------------------- //
// Vertex Shader
// --------------------------------------------------------------------------------------------- //

VertexShaderOutput VertexShaderFunction(VertexShaderInput input) {
  VertexShaderOutput output;

  output.Position = mul(input.Position, WorldViewProjection);
  output.Size = input.Power / 4.0f;
  output.Color = input.Color;

  return output;
}

// --------------------------------------------------------------------------------------------- //
// Supporting Structures
// --------------------------------------------------------------------------------------------- //

/// <summary>Vertex shader inputs, taken from the vertices themselves.</summary>
struct PixelShaderInput {

  /// <summary>Color of the flare</summary>
  float4 Color : COLOR0;

  /// <summary>Texture coordinates at the vertex</summary>
  /// <remarks>
  ///   These are auto-generated by the GPU for point sprites (the GPU internally
  ///   creates billboards with texture coordinates). If this shader fails for you,
  ///   that means your GPU does not support point sprites.
  /// </remarks>
#ifdef XBOX
  float4 TextureCoordinates : SPRITETEXCOORD;
#else
  float2 TextureCoordinates : TEXCOORD0;
#endif

};

// --------------------------------------------------------------------------------------------- //
// Pixel Shader
// --------------------------------------------------------------------------------------------- //

float4 PixelShaderFunction(PixelShaderInput input) : COLOR0 {
  float2 textureCoordinates;

#ifdef XBOX
  textureCoordinates = abs(input.TextureCoordinates.zw);
#else
  textureCoordinates = input.TextureCoordinates.xy;
#endif
        
  return tex2D(FlareSampler, textureCoordinates) * input.Color;
  //return float4(1, 0, 0, 1);
}

// --------------------------------------------------------------------------------------------- //
// Techniques
// --------------------------------------------------------------------------------------------- //

technique FlareTechnique {
  pass BlendPass {
    PointSpriteEnable = true;

    ZEnable = false;
    ZWriteEnable = false;

    AlphaBlendEnable = true;
    SrcBlend = SrcAlpha;
    DestBlend = One; // InvSrcAlpha;

    VertexShader = compile vs_1_1 VertexShaderFunction();
    PixelShader = compile ps_1_1 PixelShaderFunction();
  }
}
