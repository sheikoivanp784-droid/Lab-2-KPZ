using System;
using System.Collections.Generic;
using Shapes3DLibrary;

namespace Shapes3DApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "3D Shapes Palette — Factory Method";

            List<ShapeCreator> canvasHistory = new List<ShapeCreator>();

            while (true)
            {
                Console.Clear();
                PrintHeader();

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("  [1] Створити Куб (Cube)");
                Console.WriteLine("  [2] Створити Циліндр (Cylinder)");
                Console.WriteLine("  [3] Створити Піраміду (Pyramid)");
                Console.WriteLine("  [4] Показати всі створені фігури на полотні");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("  [0] Вихід");
                Console.ResetColor();

                Console.Write("\n  Оберіть інструмент палітри: ");
                string choice = Console.ReadLine();

                if (choice == "0") break;

                ShapeCreator creator = null;

                switch (choice)
                {
                    case "1":
                        double side = ReadPositiveDouble("  Введіть довжину сторони куба: ");
                        string cubeColor = ChooseColor();
                        creator = new CubeCreator(side, cubeColor);
                        break;

                    case "2":
                        double radius = ReadPositiveDouble("  Введіть радіус основи циліндра: ");
                        double cylHeight = ReadPositiveDouble("  Введіть висоту циліндра: ");
                        string cylColor = ChooseColor();
                        creator = new CylinderCreator(radius, cylHeight, cylColor);
                        break;

                    case "3":
                        double baseArea = ReadPositiveDouble("  Введіть площу основи піраміди: ");
                        double pyrHeight = ReadPositiveDouble("  Введіть висоту піраміди: ");
                        string pyrColor = ChooseColor();
                        creator = new PyramidCreator(baseArea, pyrHeight, pyrColor);
                        break;

                    case "4":
                        ShowAllCanvasShapes(canvasHistory);
                        continue;

                   

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n  Невірний вибір! Спробуйте ще раз.");
                        Console.ResetColor();
                        Pause();
                        continue;
                }

                if (creator != null)
                {
                    canvasHistory.Add(creator);
                    Console.WriteLine("\n  --------------------------------------------------");
                    ApplyConsoleColor(creator);
                    PaletteClient client = new PaletteClient(creator);
                    client.AddShapeToCanvas();
                    Console.ResetColor();
                    Console.WriteLine("  --------------------------------------------------");
                    Pause();
                }
            }
        }

        static void PrintHeader()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════════════════════╗");
            Console.WriteLine("║        ПАЛІТРА 3D-ФІГУР (FACTORY METHOD)           ║");
            Console.WriteLine("╚════════════════════════════════════════════════════╝");
            Console.ResetColor();
        }

        static double ReadPositiveDouble(string prompt)
        {
            double value;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine()?.Replace('.', ',');
                if (double.TryParse(input, out value) && value > 0)
                    return value;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  Помилка! Введіть коректне число більше нуля.");
                Console.ResetColor();
            }
        }

        static string ChooseColor()
        {
            Console.WriteLine("\n  Оберіть колір заливки:");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("  [1] Синій  ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[2] Зелений  ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("[3] Жовтий  ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[4] Червоний  ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("[5] Фіолетовий");
            Console.ResetColor();

            Console.Write("  Ваш вибір (за замовчуванням Білий): ");
            string c = Console.ReadLine();

            return c switch
            {
                "1" => "Синій",
                "2" => "Зелений",
                "3" => "Жовтий",
                "4" => "Червоний",
                "5" => "Фіолетовий",
                _ => "Білий"
            };
        }

        static void ApplyConsoleColor(ShapeCreator creator)
        {
            Shape3D temp = creator.CreateShape();
            Console.ForegroundColor = temp.Color switch
            {
                "Синій" => ConsoleColor.Blue,
                "Зелений" => ConsoleColor.Green,
                "Жовтий" => ConsoleColor.Yellow,
                "Червоний" => ConsoleColor.Red,
                "Фіолетовий" => ConsoleColor.Magenta,
                _ => ConsoleColor.White
            };
        }

        static void ShowAllCanvasShapes(List<ShapeCreator> history)
        {
            Console.Clear();
            PrintHeader();

            if (history.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n  Полотно поки порожнє. Спочатку створіть хоча б одну фігуру.");
                Console.ResetColor();
                Pause();
                return;
            }

            Console.WriteLine($"\n  Усього фігур на полотні: {history.Count}\n");
            for (int i = 0; i < history.Count; i++)
            {
                Console.WriteLine($"  === Об'єкт #{i + 1} ({history[i].ToolName}) ===");
                ApplyConsoleColor(history[i]);
                PaletteClient client = new PaletteClient(history[i]);
                client.AddShapeToCanvas();
                Console.ResetColor();
                Console.WriteLine();
            }

            Pause();
        }

       

        static void Pause()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("\n  Натисніть будь-яку клавішу для продовження...");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}