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
    /// Initialize the grid required to hold the spiral values.
    /// </summary>
    /// <returns>Returns an initialize matrix</returns>
    private int[,] InitializeGrid()
    {
      int width;
      int height;
            
      CalculateGridDimensions(out width, out height);

      // This used to be calculated via the following:
      //   var squareRootInput = Math.Sqrt(_maxValue + 1);
      //   var width = (int)Math.Ceiling(squareRootInput);
      //   var height = (int)Math.Ceiling(Math.Floor(squareRootInput + 0.5));

      var grid = new int[width, height];

      // Initialize grid to invalid values (-1)
      // After filling the grid, any remaining invalid values 
      // will display as blank cells
      for (var x = 0; x < width; x++)
      {
        for (var y = 0; y < height; y++)
        {
          grid[x, y] = -1;
        }
      }

      return grid;
    }

    /// <summary>
    /// Calculates the dimensions required for the spiral data matrix
    /// by simulating a run through the spiral.
    /// </summary>
    /// <param name="width">Output: width of matrix</param>
    /// <param name="height">Output: height of matrix</param>
    private void CalculateGridDimensions(out int width, out int height)
    {
      int minX = 0;
      int minY = 0;
      int maxX = 0;
      int maxY = 0;

      var location = new Vector2D(0, 0);
      var segment = new SpiralSegment(1, UnitVectors.Right);

      int index = 0;

      do
      {
        int remainingCells = _maxValue - index;
        Vector2D distanceToTravel;

        if (remainingCells >= segment.Length)
        {
          // travel the full segment and then get the next segment
          index += segment.Length;
          distanceToTravel = segment.Direction.Multiply(segment.Length);

          segment = segment.Next();
        }
        else
        {
          // travel the remaining cells
          index += remainingCells;
          distanceToTravel = segment.Direction.Multiply(remainingCells);
        }

        location = location.Add(distanceToTravel);

        // Update the observed bounds
        minX = (int)Math.Min(minX, location.X);
        minY = (int)Math.Min(minY, location.Y);
        maxX = (int)Math.Max(maxX, location.X);
        maxY = (int)Math.Max(maxY, location.Y);
      } while (index < _maxValue);

      width = maxX - minX + 1;
      height = maxY - minY + 1;
    }

    /// <summary>
    /// Generates a matrix representing a clockwise spiral of numbers 
    /// with zero at the center
    /// </summary>
    /// <returns>Returns a matrix representing a spiral of numbers</returns>
    private int[,] GenerateSpiral()
    {
      // initialize the grid
      var output = InitializeGrid();

      int width = output.GetLength(0);
      int height = output.GetLength(1);

      // Start at the center of the grid
      var location = new Vector2D(
        x: (int)((width - 1) / 2),
        y: (int)((height - 1) / 2));

      // Set the initial segment (move 1 cell to the right)
      var segment = new SpiralSegment(1, UnitVectors.Right);
      int segmentIndex = 0;

      for (var i = 0; i <= _maxValue; i++)
      {
        output[location.X, location.Y] = i;

        // Move to the next cell
        segmentIndex++;
        location = location.Add(segment.Direction);

        // If this is the end of a segment, then change directions
        // and start a new segment
        if (segmentIndex >= segment.Length)
        {
          segmentIndex = 0;
          segment = segment.Next();
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
