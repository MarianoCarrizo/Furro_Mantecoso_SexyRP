
using System.Text.RegularExpressions;

namespace Application.Utilities.Validations
{
    public class InputValidations
    {
        public class OutOfRangeException : Exception { }

        public static string StringInput(string text)
        {
            Console.Write(text);
            string textOutput = Console.ReadLine();
            return textOutput;
        }
        public static string DniInput(string text)
        {
            Console.Write(text);
            string textOutput = Console.ReadLine();
           if (textOutput.Length == 0)
           {
                return "error,No puede ingresarse un DNI Vacio.";
           }
            return textOutput;
        }
        public static string YesOrNoInput(string text)
        {
            Console.Write(text);
            string textOutput = Console.ReadLine()
                      .ToLower();
            textOutput = Regex.Replace(textOutput, @"[^\w\d\s]", "");
            textOutput = Regex.Replace(textOutput, "ó", "o");
            return textOutput;
        }
        public static bool IsValid(int select = 6)
        {
            return select == 6;

        }

        public static int IntInput(string text, int min, int max)
        {
            while (true)
            {
                try
                {
                    Console.Write(text);
                    int num = int.Parse(Console.ReadLine());
                    if (num < min | num > max)
                    {
                        throw new OutOfRangeException();
                    }
                    return num;
                }
                catch (FormatException a) { Console.WriteLine("Entrada inválida: " + a.Message); }
                catch (OutOfRangeException) { Console.WriteLine("Entrada inválida: ingresar un numero entre " + min + " y " + max); }
            }
        }


    }
}