using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RallyExercises.Exercise3;

namespace Exercise3.Tests
{
  [TestClass]
  public class SpiralTests
  {
    private void TestCase(Spiral spiral, string[] expected)
    {
      TestCase(spiral, String.Join(Environment.NewLine, expected));
    }

    private void TestCase(Spiral spiral, string expected)
    {
      var actual = spiral.ToString();
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void CreateWithNegativeInput_ThrowsArgumentException()
    {
      try
      {
        var spiral = new Spiral(-1);
        Assert.Fail("ArgumentException was not thrown");
      }
      catch (ArgumentException)
      {
        // expected
      }
    }

    [TestMethod]
    public void StringWithZeroInput_SingleCharacter()
    {
      var spiral = new Spiral(0);

      TestCase(spiral, "0");
    }

    [TestMethod]
    public void StringWithOneInput_SingleLine()
    {
      var spiral = new Spiral(1);

      TestCase(spiral, "0 1");
    }

    [TestMethod]
    public void StringWithTwoInput_TwoLines()
    {
      var spiral = new Spiral(2);

      TestCase(spiral, new[] {
        "0 1",
        "  2"
      });
    }

    [TestMethod]
    public void StringWithPerfectSquare_NoEmptySpaces()
    {
      var spiral = new Spiral(24);

      TestCase(spiral, new[]
      {
        "20 21 22 23 24",
        "19  6  7  8  9",
        "18  5  0  1 10",
        "17  4  3  2 11",
        "16 15 14 13 12"
      });
    }

    [TestMethod]
    public void StringWithSingleValueInFirstColumn()
    {
      var spiral = new Spiral(16);

      TestCase(spiral, new[]
      {
        "    6  7  8  9",
        "    5  0  1 10",
        "    4  3  2 11",
        "16 15 14 13 12"
      });
    }

    [TestMethod]
    public void StringWithSingleValueInLastColumn()
    {
      var spiral = new Spiral(25);

      TestCase(spiral, new[]
      {
        "20 21 22 23 24 25",
        "19  6  7  8  9   ",
        "18  5  0  1 10   ",
        "17  4  3  2 11   ",
        "16 15 14 13 12   "
      });
    }

    [TestMethod]
    public void StringWithSingleValueInFirstRow()
    {
      var spiral = new Spiral(20);

      TestCase(spiral, new[]
      {
        "20            ",
        "19  6  7  8  9",
        "18  5  0  1 10",
        "17  4  3  2 11",
        "16 15 14 13 12"
      });
    }

    [TestMethod]
    public void StringWithSingleValueInLastRow()
    {
      var spiral = new Spiral(12);

      TestCase(spiral, new[]
      {
        " 6  7  8  9",
        " 5  0  1 10",
        " 4  3  2 11",
        "         12"
      });
    }

  }
}
