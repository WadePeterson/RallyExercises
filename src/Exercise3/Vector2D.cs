using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RallyExercises.Exercise3
{
  public struct Vector2D
  {
    public static readonly Vector2D Empty = new Vector2D(0, 0);

    private readonly int _x;
    private readonly int _y;

    public int X { get { return _x; } }
    public int Y { get { return _y; } }
    
    public Vector2D(int x, int y)
    {
      _x = x;
      _y = y;
    }

    public Vector2D Add(Vector2D otherVector)
    {
      return new Vector2D(
        x: this.X + otherVector.X,
        y: this.Y + otherVector.Y);
    }

    public Vector2D Multiply(int scalar)
    {
      return new Vector2D(
        x: this.X * scalar,
        y: this.Y * scalar);
    }
  }

  public static class UnitVectors
  {
    public static readonly Vector2D Zero = new Vector2D(0, 0);
    public static readonly Vector2D Right = new Vector2D(1, 0);
    public static readonly Vector2D Down = new Vector2D(0, 1);
    public static readonly Vector2D Left = new Vector2D(-1, 0);
    public static readonly Vector2D Up = new Vector2D(0, -1);
  }
}
