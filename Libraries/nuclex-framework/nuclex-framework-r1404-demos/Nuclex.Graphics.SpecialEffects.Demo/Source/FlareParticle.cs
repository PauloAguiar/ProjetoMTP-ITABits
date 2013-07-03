using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Nuclex.Graphics;

namespace Nuclex.Graphics.SpecialEffects.Demo {

  /// <summary>
  ///   Stores the properties of a particle and doubles as vertex
  /// </summary>
  [StructLayout(LayoutKind.Sequential)]
  public struct FlareParticle {

    /// <summary>
    ///   Description of this vertex structure for creating a vertex declaration
    /// </summary>
    public static readonly VertexElement[] VertexElements =
      VertexDeclarationHelper.BuildElementList<FlareParticle>();

    /// <summary>Offset, in bytes, from one particle to the next</summary>
    public static int SizeInBytes = VertexDeclarationHelper.GetStride<FlareParticle>();

    /// <summary>Initializes a new simple particle</summary>
    /// <param name="position">Initial position of the particle</param>
    /// <param name="velocity">Velocity the particle is moving at</param>
    /// <param name="color">Color of the particle</param>
    public FlareParticle(Vector3 position, Vector3 velocity, Color color) {
      this.Position = position;
      this.Velocity = velocity;
      this.Color = color;
      this.Power = 100.0f;
    }

    /// <summary>Current position of the particle in space</summary>
    [VertexElement(VertexElementUsage.Position)]
    public Vector3 Position;

    /// <summary>Velocity the particle is moving at</summary>
    public Vector3 Velocity;

    /// <summary>Power of the flare</summary>
    [VertexElement(VertexElementUsage.PointSize)]
    public float Power;
    
    /// <summary>Color if this particle</summary>
    [VertexElement(VertexElementUsage.Color)]
    public Color Color;
    
  
  }
  
  

} // namespace Nuclex.Graphics.SpecialEffects.Demo
