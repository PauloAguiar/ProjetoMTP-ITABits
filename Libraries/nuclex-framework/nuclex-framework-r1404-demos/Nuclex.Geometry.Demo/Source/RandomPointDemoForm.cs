using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Microsoft.Xna.Framework;

using Nuclex.Geometry.Areas;

namespace Nuclex.Geometry.Demo {

  /// <summary>Form that demonstrates the random point generation algorithms</summary>
  public partial class RandomPointDemoForm : Form {

    #region class Area2Painter
#if false
    /// <summary>Paints any IArea2 based shape into a graphics context</summary>
    private class Area2Painter : IArea2Visitor {

      /// <summary>Initializes a new IArea2 painter</summary>
      /// <param name="graphics">Graphics context the painter will paint into</param>
      /// <param name="size">Size of the drawing region</param>
      public Area2Painter(Graphics graphics, SizeF size) {
        this.size = size;
        this.graphics = graphics;
      }

      /// <summary>Paints the provided shape</summary>
      /// <param name="shape">Shape that will be painted</param>
      public void Paint(IArea2 shape) {
        shape.Accept(this);
      }

      /// <summary>Visits an axis aligned rectangle to paint it</summary>
      /// <param name="rectangle">Rectangle that will be painted</param>
      void IArea2Visitor.Visit(AxisAlignedRectangle2 rectangle) {
        this.graphics.DrawRectangle(
          Pens.Black,
          rectangle.Min.X * this.size.Width,
          rectangle.Min.Y * this.size.Height,
          (rectangle.Max.X - rectangle.Min.X) * this.size.Width,
          (rectangle.Max.Y - rectangle.Min.Y) * this.size.Height
        );
      }

      /// <summary>Visits an axis aligned rectangle to paint it</summary>
      /// <param name="rectangle">Rectangle that will be painted</param>
      void IArea2Visitor.Visit(Rectangle2 rectangle) {
        // Not supported yet...
      }

      /// <summary>Visits a triangle to paint it</summary>
      /// <param name="triangle">Triangle that will be painted</param>
      void IArea2Visitor.Visit(Triangle2 triangle) {
        this.graphics.DrawLines(
          Pens.Black,
          new PointF[] {
            new PointF(
              triangle.A.X * this.size.Width,
              triangle.A.Y * this.size.Height
            ),
            new PointF(
              triangle.B.X * this.size.Width,
              triangle.B.Y * this.size.Height
            ),
            new PointF(
              triangle.C.X * this.size.Width,
              triangle.C.Y * this.size.Height
            ),
            new PointF(
              triangle.A.X * this.size.Width,
              triangle.A.Y * this.size.Height
            )
          }
        );
      }

      /// <summary>Visits a disc to paint it</summary>
      /// <param name="disc">Disc that will be painted</param>
      void IArea2Visitor.Visit(Disc2 disc) {
        float radiusX = disc.Radius * this.size.Width;
        float radiusY = disc.Radius * this.size.Height;

        this.graphics.DrawEllipse(
          Pens.Black,
          disc.Center.X * this.size.Width - radiusX,
          disc.Center.Y * this.size.Height - radiusY,
          radiusX * 2.0f,
          radiusY * 2.0f
        );
      }

      /// <summary>Graphics context drawing will take place in</summary>
      private Graphics graphics;
      /// <summary>Size of the drawing area</summary>
      private SizeF size;

    }
#endif
    #endregion // class Area2Painter

    /// <summary>Initializes a new random point demonstration form</summary>
    public RandomPointDemoForm() {
      InitializeComponent();
    }

    /// <summary>Switches to the rectangle shape when its option is selected</summary>
    /// <param name="sender">Option that has been selected</param>
    /// <param name="e">Not used</param>
    private void rectangleSelected(object sender, EventArgs e) {
      this.activeShape = new AxisAlignedRectangle2(
        new Vector2(0.1f, 0.1f), new Vector2(0.9f, 0.9f)
      );
      this.demoPicture.Refresh();
    }

    /// <summary>Switches to the triangle shape when its option is selected</summary>
    /// <param name="sender">Option that has been selected</param>
    /// <param name="e">Not used</param>
    private void triangleSelected(object sender, EventArgs e) {
      this.activeShape = new Triangle2(
        new Vector2(0.1f, 0.1f), new Vector2(0.9f, 0.1f), new Vector2(0.5f, 0.9f)
      );
      this.demoPicture.Refresh();
    }

    /// <summary>Switches to the disc shape when its option is selected</summary>
    /// <param name="sender">Option that has been selected</param>
    /// <param name="e">Not used</param>
    private void discSelected(object sender, EventArgs e) {
      this.activeShape = new Disc2(
        new Vector2(0.5f, 0.5f), 0.4f
      );
      this.demoPicture.Refresh();
    }

    /// <summary>Called when the demo picture is painting itself</summary>
    /// <param name="sender">Picture that is painting itself</param>
    /// <param name="e">Contains the graphics context to paint in</param>
    private void demoPicturePainting(object sender, PaintEventArgs e) {
#if false
      // Store the size of the drawing area in floating point coordinates
      SizeF size = new SizeF(
        (float)this.demoPicture.Width, (float)this.demoPicture.Height
      );

      // If a shape has been selected, draw it into the drawing area
      if(this.activeShape != null)
        new Area2Painter(e.Graphics, size).Paint(this.activeShape);

      // If points have been generated, draw the points as well
      if(this.points != null) {
        foreach(Vector2 point in this.points) {
          e.Graphics.FillEllipse(
            Brushes.DarkGray,
            point.X * size.Width - 1.0f,
            point.Y * size.Height - 1.0f,
            3.0f,
            3.0f
          );
        }
      }
#endif
    }

    /// <summary>Generates a number of random points inside or on the shape</summary>
    /// <param name="sender">Button that has been clicked</param>
    /// <param name="e">Not used</param>
    private void generateClicked(object sender, EventArgs e) {
      if(this.activeShape != null) {
        DefaultRandom rng = new DefaultRandom();
        const int PointCount = 1000;

        this.points = new Vector2[PointCount];

        if(this.perimeterOption.Checked) {
          for(int point = 0; point < PointCount; ++point)
            this.points[point] = this.activeShape.RandomPointOnPerimeter(rng);
        } else {
          for(int point = 0; point < PointCount; ++point)
            this.points[point] = this.activeShape.RandomPointWithin(rng);
        }

        this.demoPicture.Refresh();
      }
    }

    /// <summary>The currently active shape, selected by the user</summary>
    private IArea2 activeShape;
    /// <summary>
    ///   Points that have been generated when the 'Generate' button was clicked
    /// </summary>
    private Vector2[] points;

  }

} // namespace Nuclex.Geometry.Demo