using System;
using System.Collections.Generic;
using System.IO;

static class Program
{
    static void Main(string[] args)
    {
        string filePath = "exampleInput.csv";

        Solver.ProcessShapes(filePath);
    }
}

static class Solver
{
    public static void ProcessShapes(string filePath)
    {
        var shapes = new List<IShape>();
        double total = 0;

        string[] lines = File.ReadAllLines(filePath);
        for (int i = 0; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(',');

            if (parts[0] == "cube")
            {
                double side = double.Parse(parts[1]);
                shapes.Add(new Cube(side));
            }
            else if (parts[0] == "cuboid")
            {
                double length = double.Parse(parts[1]);
                double width = double.Parse(parts[2]);
                double height = double.Parse(parts[3]);
                shapes.Add(new Cuboid(length, width, height));
            }
            else if (parts[0] == "prism")
            {
                double baseArea = double.Parse(parts[1]);
                double height = double.Parse(parts[2]);
                shapes.Add(new Prism(baseArea, height));
            }
            else if (parts[0] == "area")
            {
                double scale = double.Parse(parts[1]);
                double sum = 0;

                foreach (var shape in shapes)
                {
                    sum += shape.CalculateArea();
                }

                total += sum * scale;

                shapes.Clear();
            }
        }

        Console.WriteLine($"Total: {total}");
    }
}

interface IShape
{
    double CalculateArea();
}

class Cube : IShape
{
    private double Side;

    public Cube(double side)
    {
        Side = side;
    }

    public double CalculateArea()
    {
        return 6 * Side * Side;
    }
}

class Cuboid : IShape
{
    private double Length, Width, Height;

    public Cuboid(double length, double width, double height)
    {
        Length = length;
        Width = width;
        Height = height;
    }

    public double CalculateArea()
    {
        return 2 * (Length * Width + Width * Height + Height * Length);
    }
}

class Prism : IShape
{
    private double BaseArea, Height;

    public Prism(double baseArea, double height)
    {
        BaseArea = baseArea;
        Height = height;
    }

    public double CalculateArea()
    {
        return 2 * BaseArea + BaseArea * Height;
    }
}