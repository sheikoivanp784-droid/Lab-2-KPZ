using System;

namespace Shapes3DLibrary
{
    public abstract class Shape3D
    {
        public string Name { get; set; }
        public string Color { get; set; }

        public abstract void Draw();
        public abstract double GetVolume();
    }

    public class Cube : Shape3D
    {
        public double Side { get; set; }

        public Cube(double side = 5.0, string color = "Синій")
        {
            Name = "Куб";
            Side = side;
            Color = color;
        }

        public override void Draw()
        {
            Console.WriteLine($"  Фігура: {Name} | Колір: {Color} | Сторона: {Side}");
            Console.WriteLine(@"
      +------+
     /      /|
    +------+ |
    |      | +
    |      |/
    +------+");
        }

        public override double GetVolume() => Math.Pow(Side, 3);
    }

    public class Cylinder : Shape3D
    {
        public double Radius { get; set; }
        public double Height { get; set; }

        public Cylinder(double radius = 3.0, double height = 7.0, string color = "Зелений")
        {
            Name = "Циліндр";
            Radius = radius;
            Height = height;
            Color = color;
        }

        public override void Draw()
        {
            Console.WriteLine($"  Фігура: {Name} | Колір: {Color} | Радіус: {Radius} | Висота: {Height}");
            Console.WriteLine(@"
      .----.
     (      )
     |`----'|
     |      |
     |      |
      `----'");
        }

        public override double GetVolume() => Math.PI * Math.Pow(Radius, 2) * Height;
    }

    public class Pyramid : Shape3D
    {
        public double BaseArea { get; set; }
        public double Height { get; set; }

        public Pyramid(double baseArea = 12.0, double height = 6.0, string color = "Жовтий")
        {
            Name = "Піраміда";
            BaseArea = baseArea;
            Height = height;
            Color = color;
        }

        public override void Draw()
        {
            Console.WriteLine($"  Фігура: {Name} | Колір: {Color} | Площа основи: {BaseArea} | Висота: {Height}");
            Console.WriteLine(@"
        /\
       /  \
      /    \
     /      \
    /________\");
        }

        public override double GetVolume() => (1.0 / 3.0) * BaseArea * Height;
    }

    public abstract class ShapeCreator
    {
        public string ToolName { get; set; }

        public abstract Shape3D CreateShape();

        public void RenderOnCanvas()
        {
            Shape3D shape = CreateShape();
            shape.Draw();
            Console.WriteLine($"\n  Розрахунковий об'єм: {shape.GetVolume():F2} куб. од.");
        }
    }

    public class CubeCreator : ShapeCreator
    {
        private readonly double _side;
        private readonly string _color;

        public CubeCreator(double side = 5.0, string color = "Синій")
        {
            ToolName = "Інструмент: Куб";
            _side = side;
            _color = color;
        }

        public override Shape3D CreateShape() => new Cube(_side, _color);
    }

    public class CylinderCreator : ShapeCreator
    {
        private readonly double _radius;
        private readonly double _height;
        private readonly string _color;

        public CylinderCreator(double radius = 3.0, double height = 7.0, string color = "Зелений")
        {
            ToolName = "Інструмент: Циліндр";
            _radius = radius;
            _height = height;
            _color = color;
        }

        public override Shape3D CreateShape() => new Cylinder(_radius, _height, _color);
    }

    public class PyramidCreator : ShapeCreator
    {
        private readonly double _baseArea;
        private readonly double _height;
        private readonly string _color;

        public PyramidCreator(double baseArea = 12.0, double height = 6.0, string color = "Жовтий")
        {
            ToolName = "Інструмент: Піраміда";
            _baseArea = baseArea;
            _height = height;
            _color = color;
        }

        public override Shape3D CreateShape() => new Pyramid(_baseArea, _height, _color);
    }

    public class PaletteClient
    {
        private readonly ShapeCreator _creator;

        public PaletteClient(ShapeCreator creator)
        {
            _creator = creator;
        }

        public void AddShapeToCanvas()
        {
            _creator.RenderOnCanvas();
        }
    }
}