using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RallyExercises.Exercise3
{
  class Program
  {
    private static void Main(string[] args)
    {
      var consoleReader = new ConsoleReader();
      var input = consoleReader.ReadInt(min: 0);
      var spiral = new Spiral(input);

      Console.WriteLine("Output:");
      Console.WriteLine();
      Console.WriteLine(spiral.ToString());
    }
  }
}
