using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RallyExercises.Exercise3
{
  public class SpiralSegment
  {
    public int Length { get; set; }
    public Vector2D Direction { get; set; }

    public SpiralSegment()
    {
      this.Length = 0;
      this.Direction = Vector2D.Empty;
    }

    public SpiralSegment(int length, Vector2D direction)
    {
      this.Length = length;
      this.Direction = direction;
    }

    public SpiralSegment Next()
    {
      var nextSegment = new SpiralSegment();

      // Update X and Y direction according to the cycle:
      // ... Right -> Down -> Left -> Up -> Right ...
      //
      // dX  dY   Direction
      //-------------------
      //  1   0   Right
      //  0   1   Down
      // -1   0   Left
      //  0  -1   Up

      nextSegment.Direction = new Vector2D(
        x: -1 * this.Direction.Y,
        y: this.Direction.X);

      // If changing from vertical to horizontal movement, increase the length of the segment by 1
      nextSegment.Length = nextSegment.Direction.X == 0 ? this.Length : this.Length + 1;

      return nextSegment;
    }
  }
}
