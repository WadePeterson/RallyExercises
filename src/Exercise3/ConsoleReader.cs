using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RallyExercises
{
  public class ConsoleReader
  {
    /// <summary>
    /// Get or set the text displayed when prompting a user for a value
    /// </summary>
    public string Label { get; set; }

    public ConsoleReader()
    {
      this.Label = "Input";
    }

    /// <summary>
    /// Read an integer from the Console with the specified min and max values
    /// </summary>
    /// <param name="min">Optional inclusive minimum value</param>
    /// <param name="max">Optional inclusive maximum value</param>
    /// <returns>Returns an integer in the specified range</returns>
    public int ReadInt(int min = int.MinValue, int max = int.MaxValue)
    {
      if (min > max)
      {
        throw new ArgumentException("max must be greater than or equal to min");
      }

      int input;
      bool valid;

      do
      {
        string error = null;
        valid = false;

        Console.Write("{0}: ", this.Label);

        var line = Console.ReadLine();

        if (int.TryParse(line, out input))
        {
          if (input < min)
          {
            error = String.Format("Must be greater than or equal to {0}.", min);
          }
          else if (input > max)
          {
            error = String.Format("Must be less than or equal to {0}.", max);
          }
          else
          {
            valid = true;
          }
        }
        else
        {
          error = String.Format("Must be a valid integer in the range of [{0} - {1}]", min, max);
        }

        if (!valid)
        {
          Console.WriteLine("Invalid input. {0}", error);
        }

        Console.WriteLine();
      } while (!valid);

      return input;
    }
  }
}
