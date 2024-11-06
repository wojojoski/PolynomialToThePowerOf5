using System;
using System.Collections.Generic;

namespace Algorytmy_WielomianDoPotegi5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double a, b, c, d, e, f;
            Console.WriteLine("Podaj współczynniki a, b, c, d, e, f równania wielomianowego 5 stopnia: ax^5 + bx^4 + cx^3 + dx^2 + ex + f = 0");
            Console.Write("a: ");
            a = Convert.ToDouble(Console.ReadLine());
            Console.Write("b: ");
            b = Convert.ToDouble(Console.ReadLine());
            Console.Write("c: ");
            c = Convert.ToDouble(Console.ReadLine());
            Console.Write("d: ");
            d = Convert.ToDouble(Console.ReadLine());
            Console.Write("e: ");
            e = Convert.ToDouble(Console.ReadLine());
            Console.Write("f: ");
            f = Convert.ToDouble(Console.ReadLine());

            double min = -10;
            double max = 10;
            double step = 0.1;
            int maxIterations = 1000;
            double tolerance = 1e-7;

            FindRoots(a, b, c, d, e, f, min, max, step, maxIterations, tolerance);
        }

        static double Polynomial(double a, double b, double c, double d, double e, double f, double x)
        {
            return a * Math.Pow(x, 5) + b * Math.Pow(x, 4) + c * Math.Pow(x, 3) + d * Math.Pow(x, 2) + e * x + f;
        }

        static double PolynomialDerivative(double a, double b, double c, double d, double e, double x)
        {
            return 5 * a * Math.Pow(x, 4) + 4 * b * Math.Pow(x, 3) + 3 * c * Math.Pow(x, 2) + 2 * d * x + e;
        }

        static double NewtonRaphson(double a, double b, double c, double d, double e, double f, double guess, int maxIterations, double tolerance)
        {
            double x = guess;

            for (int i = 0; i < maxIterations; i++)
            {
                double fx = Polynomial(a, b, c, d, e, f, x);
                double fpx = PolynomialDerivative(a, b, c, d, e, x);

                if (Math.Abs(fpx) < 1e-10)
                {
                    return double.NaN;
                }

                double xNew = x - fx / fpx;

                if (Math.Abs(xNew - x) < tolerance)
                {
                    return xNew;
                }

                x = xNew;
            }

            return double.NaN;
        }

        static void FindRoots(double a, double b, double c, double d, double e, double f, double lowerBound, double upperBound, double step, int maxIterations, double tolerance)
        {
            HashSet<double> roots = new HashSet<double>();
            double x = lowerBound;
            while (x <= upperBound)
            {
                double root = NewtonRaphson(a, b, c, d, e, f, x, maxIterations, tolerance);
                if (!double.IsNaN(root) && Math.Abs(Polynomial(a, b, c, d, e, f, root)) < tolerance)
                {
                    bool isDuplicate = false;
                    foreach (double r in roots)
                    {
                        if (Math.Abs(r - root) < tolerance)
                        {
                            isDuplicate = true;
                            break;
                        }
                    }

                    if (!isDuplicate)
                    {
                        roots.Add(root);
                        Console.WriteLine($"Znaleziono pierwiastek: {root}");
                    }
                }
                x += step;
            }
        }
    }
}
