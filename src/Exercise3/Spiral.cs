using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RallyExercises.Exercise3
{
  public class Spiral
  {
    private int _maxValue;
    private int _maxDigits;
    private int[,] _data;

    /// <summary>
    /// Creates a Spiral object with the specified maximum value
    /// </summary>
    /// <param name="maxValue">Maximum number value of the spiral</param>
    public Spiral(int maxValue)
    {
      if (maxValue < 0 || maxValue >= int.MaxValue)
      {
        throw new ArgumentException("input must be greater than or equal to zero and less than or equal to int.MaxValue - 1.");
      }

      _maxValue = maxValue;

      // Calculate the maximum number of digits needed to display a number in the spiral
      // This is used to pad smaller numbers in the spiral
      _maxDigits = 1;

      while (maxValue >= 10)
      {
        maxValue /= 10;
        _maxDigits++;
      }
    }
    
    /// <summary>
    /// Gets the previously generated spiral data if it exists.
    /// If it doesn't exist, then it will be calculated and stored for future use.
    /// </summary>
    /// <returns>Returns a matrix representing a spiral of numbers</returns>
    private int[,] GetSpiral()
    {
      if (_data == null)
      {
        // Memoize value
        _data = GenerateSpiral();
      }

      return _data;
    }

    /// <summary>
    /// Generates a matrix representing a clockwise spiral of numbers 
    /// with zero at the center
    /// </summary>
    /// <returns>Returns a matrix representing a spiral of numbers</returns>
    private int[,] GenerateSpiral()
    {
      // Determine size of matrix required to store the spiral
      var squareRootInput = Math.Sqrt(_maxValue + 1);
      var width = (int)Math.Ceiling(squareRootInput);
      var height = (int)Math.Ceiling(Math.Floor(squareRootInput + 0.5));

      var output = new int[width, height];

      for (var i = 0; i < width; i++)
      {
        for (var j = 0; j < height; j++)
        {
          output[i, j] = -1;
        }
      }
      
      // Start at the center of the grid
      int x = (int)((width - 1) / 2);
      int y = (int)((height - 1) / 2);

      // Start by moving right
      int dX = 1;
      int dY = 0;

      // The first segment is 1 item long:
      //  E.g.: go right 1 cell, then change direction
      int segmentIndex = 0;
      int segmentLength = 1;
      
      for (var i = 0; i <= _maxValue; i++)
      {
        output[x, y] = i;

        // Move to the next cell
        x += dX;
        y += dY;

        segmentIndex++;

        // If this is the end of a segment, then change directions
        // and start a new segment
        if (segmentIndex >= segmentLength)
        {
          segmentIndex = 0;
          
          // Update dX and dY to change directions according to the cycle:
          // ... Right -> Down -> Left -> Up -> Right ...
          //
          // dX  dY   Direction
          //-------------------
          //  1   0   Right
          //  0   1   Down
          // -1   0   Left
          //  0  -1   Up

          var oldDX = dX;
          dX = -dY;
          dY = oldDX;

          // When changing from vertical to horizontal movement,
          // Increase the length of the segment by 1
          if (dX != 0)
          {
            segmentLength++;
          }
        }
      }

      return output;
    }

    /// <summary>
    /// Gets a string representation of the Spiral
    /// </summary>
    /// <returns>Returns a string representation of the Spiral</returns>
    public override string ToString()
    {
      var spiral = GetSpiral();

      int width = spiral.GetLength(0);
      int height = spiral.GetLength(1);

      var output = new StringBuilder();

      for (var y = 0; y < height; y++)
      {
        if (y > 0)
        {
          output.AppendLine();
        }

        for (var x = 0; x < width; x++)
        {
          if (x > 0)
          {
            output.Append(" ");
          }

          // Output an empty string if there is no valid number at the current position
          string val = spiral[x, y] >= 0 ? spiral[x, y].ToString() : "";

          // Pad value with spaces, if required, to make it the same size as the largest number
          output.Append(val.PadLeft(_maxDigits));
        }
      }

      return output.ToString();
    }
  }
}
