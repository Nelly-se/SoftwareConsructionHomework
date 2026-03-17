
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{

    public abstract class Shape
    {
        public abstract double CalculateArea();
        public abstract bool IsValid();
    }

    // 长方形类：继承自Shape
    public class Rectangle : Shape
    {
        public double Length { get; }
        public double Width { get; }

        public Rectangle(double length, double width)
        {
            Length = length;
            Width = width;
        }
        // 实现面积计算
        public override double CalculateArea()
        {
            return IsValid() ? Length * Width : 0;
        }

        // 实现合法性判断：长和宽都必须大于0
        public override bool IsValid()
        {
            return Length > 0 && Width > 0;
        }
    }


    // 正方形类：继承自长方形（正方形是特殊的长方形）
    public class Square : Rectangle
    {
        public double Side { get; }

        public Square(double side) : base(side, side)
        {
            Side = side;
        }

        // 重写合法性判断：边长必须大于0
        public override bool IsValid()
        {
            return Side > 0;
        }
    }

    // 圆形类：继承自Shape
    public class Circle : Shape
    {
        public double Radius { get; }
        private const double Pi = Math.PI;

        public Circle(double radius)
        {
            Radius = radius;
        }

        // 实现面积计算：π*r²
        public override double CalculateArea()
        {
            return IsValid() ? Pi * Radius * Radius : 0;
        }

        // 实现合法性判断：半径必须大于0
        public override bool IsValid()
        {
            return Radius > 0;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random=new Random();
            List<Shape> shapes = new List<Shape>();
            double totalArea = 0;
            for(int i = 0; i < 10; i++)
            {
                int shapeType = random.Next(3);
                Shape shape;

                switch (shapeType)
                {
                    case 0:
                        double length = random.NextDouble() * 10;
                        double width = random.NextDouble() * 10;
                        shape = new Rectangle(length, width);
                        break;
                    case 1:
                        double side = random.NextDouble();
                        shape = new Square(side);
                        break;
                    case 2:
                        double radius=random.NextDouble();
                        shape = new Circle(radius);
                        break;
                    default:
                        shape = null;
                        break;
                }
                if (shape != null)
                {
                    shapes.Add(shape);
                    totalArea += shape.CalculateArea();
                    Console.WriteLine($"{shape.GetType().Name} - 合法: {shape.IsValid()}, 面积: {shape.CalculateArea():F2}");


                }

            }
            // 输出总面积
            Console.WriteLine($"\n10个形状的总面积: {totalArea:F2}");

            // 防止控制台一闪而过（Framework版本常用）
            Console.WriteLine("\n按任意键退出...");
            Console.ReadKey();
        }
    }
}
