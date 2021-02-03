using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson6
{
    // Ученик - Марачковский Андрей
    class Program
    {

        #region Task01
        public delegate double MathFunc(double a, double b, out string name);
        public static double Func1(double a1, double x1, out string nameFunc)
        {
            nameFunc = "a * x^2";
            return a1 * (x1 * x1);
        }
        public static double Func2(double a2, double x2, out string nameFunc)
        {
            nameFunc = "a * sin(x)";
            return a2 * Math.Sin(x2);
        }
        static void Save(string fileName, double a, double x, MathFunc func)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);
            string name;
            bw.Write(func(a, x, out name));
            bw.Write(name);
            bw.Close();
            fs.Close();
        }
        static void Load(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            double a = br.ReadDouble();
            string name = br.ReadString();
            Console.WriteLine($"Результат: {a}");
            Console.WriteLine($"Функция: {name}");
            br.Close();
            fs.Close();
        }
        static void Task01()
        {
            Console.WriteLine("a = 3, x = 5");
            Save("data.bin", 3, 5, Func1);
            Load("data.bin");
            Console.WriteLine();
            Console.WriteLine("a = 5, x = 8");
            Save("data.bin", 5, 8, Func2);
            Load("data.bin");
            Console.ReadKey();
        }
        #endregion

        #region Task02
        public delegate double Function(double x);
        public static double F1(double x)
        {
            return x * x - 50 * x + 10;
        }
        public static double F2(double x)
        {
            return x * Math.Sin(x);
        }
        public static double F3(double x)
        {
            return x + (x * 5);
        }
        public static double F(double x)
        {
            return x * x - 50 * x + 10;
        }
        public static void SaveFunc(string fileName, double a, double b, double h, Function Func)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);
            double x = a;
            while (x <= b)
            {
                bw.Write(Func(x));
                x += h;
            }
            bw.Close();
            fs.Close();
        }
        public static double LoadFunc(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader bw = new BinaryReader(fs);
            double min = double.MaxValue;
            double d;
            for (int i = 0; i < fs.Length / sizeof(double); i++)
            {
                // Считываем значение и переходим к следующему
                d = bw.ReadDouble();
                if (d < min) min = d;
            }
            bw.Close();
            fs.Close();
            return min;
        }
        public static double[] LoadFunc(string fileName, out double min)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader bw = new BinaryReader(fs);
            double[] mas = new double[fs.Length / sizeof(double)];
            min = double.MaxValue;
            double d;
            for (int i = 0; i < fs.Length / sizeof(double); i++)
            {
                // Считываем значение и переходим к следующему
                d = bw.ReadDouble();
                mas[i] = d;
                if (d < min) min = d;
            }
            bw.Close();
            fs.Close();
            return mas;
        }
        static void Task02()
        {
            bool isExit = false;
            do
            {
                int num;
                Console.Clear();
                Console.Write("Выбирите пункт (1-4) или 0 для выхода: ");
                if (!int.TryParse(Console.ReadLine(), out num))
                {
                    num = 0;
                }
                switch (num)
                {
                case 0:
                    isExit = true;
                    break;
                case 1:
                    Console.Clear();
                    Console.WriteLine("Функция №1");
                    Console.Write("Введите начало промежутка: ");
                    string xF1 = Console.ReadLine();
                    int x1 = Convert.ToInt32(xF1);
                    Console.Write("Введите окончание промежутка: ");
                    string yF1 = Console.ReadLine();
                    int y1 = Convert.ToInt32(yF1);
                    Console.Write("Введите шаг: ");
                    string strStep1 = Console.ReadLine();
                    int step1 = Convert.ToInt32(strStep1);
                    SaveFunc("data1.bin", x1, y1, step1, F1);
                    Console.WriteLine(LoadFunc("data1.bin"));
                    Console.ReadKey();
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Функция №2");
                    Console.Write("Введите начало промежутка: ");
                    string xF2 = Console.ReadLine();
                    int x2 = Convert.ToInt32(xF2);
                    Console.Write("Введите окончание промежутка: ");
                    string yF2 = Console.ReadLine();
                    int y2 = Convert.ToInt32(yF2);
                    Console.Write("Введите шаг: ");
                    string strStep2 = Console.ReadLine();
                    int step2 = Convert.ToInt32(strStep2);
                    SaveFunc("data2.bin", x2, y2, step2, F2);
                    Console.WriteLine(LoadFunc("data2.bin"));
                    Console.ReadKey();
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("Функция №3");
                    Console.Write("Введите начало промежутка: ");
                    string xF3 = Console.ReadLine();
                    int x3 = Convert.ToInt32(xF3);
                    Console.Write("Введите окончание промежутка: ");
                    string yF3 = Console.ReadLine();
                    int y3 = Convert.ToInt32(yF3);
                    Console.Write("Введите шаг: ");
                    string strStep3 = Console.ReadLine();
                    int step3 = Convert.ToInt32(strStep3);
                    SaveFunc("data3.bin", x3, y3, step3, F3);
                    Console.WriteLine(LoadFunc("data3.bin"));
                    Console.ReadKey();
                    break;
                case 4:
                    Console.Clear();
                    Console.WriteLine("Функция №4");
                    Console.Write("Введите начало промежутка: ");
                    string xF4 = Console.ReadLine();
                    int x4 = Convert.ToInt32(xF4);
                    Console.Write("Введите окончание промежутка: ");
                    string yF4 = Console.ReadLine();
                    int y4 = Convert.ToInt32(yF4);
                    Console.Write("Введите шаг: ");
                    string strStep4 = Console.ReadLine();
                    int step4 = Convert.ToInt32(strStep4);
                    double minimum;
                    SaveFunc("data4.bin", x4, y4, step4, F1);
                    var massiv = LoadFunc("data4.bin", out minimum);
                    Console.WriteLine($"Минимум - {minimum}");
                    Console.WriteLine("Массив значений из файла по функции №1");
                    for (int i = 0; i < massiv.Length; i++)
                    {
                        Console.Write(massiv[i] + " ");
                    }
                    Console.ReadKey();
                    break;
                }
            }
            while (!isExit);
            Console.ReadKey();
        }
        #endregion

        static void Main(string[] args)
        {
            bool isExit = false;
            do
            {
                int number;
                Console.Clear();
                Console.Write("Введите номер задания (1-2), либо число 0 для выхода: ");
                if (!int.TryParse(Console.ReadLine(), out number))
                {
                    number = 0;
                }
                switch (number)
                {
                    case 0:
                        isExit = true;
                        break;
                    case 1:
                        Console.Clear();
                        Task01();
                        break;
                    case 2:
                        Console.Clear();
                        Task02();
                        break;
                }
            }
            while (!isExit);
        }
    }
}
