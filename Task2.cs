struct ComplexNumber
{
    public double RealPart;
    public double ImaginaryPart;

    public ComplexNumber(double RealPart=0, double ImaginaryPart=0) 
    {
        this.RealPart = RealPart;
        this.ImaginaryPart = ImaginaryPart;
    }

    public void ChangeComlexNumber()
    {
        Console.Write("\nEnter the Real part of complex number: ");
        this.RealPart = Convert.ToDouble(Console.ReadLine());

        Console.Write("\nEnter the Imaginary part of complex number: ");
        this.ImaginaryPart = Convert.ToDouble(Console.ReadLine());
    }

    public void PrintComplexNumber()
    {
        Console.WriteLine($"\nReal part:{this.RealPart} Imaginary part:{this.ImaginaryPart}");
    }
}

partial class Program
{
    public static void SumComplexNumbers(ref ComplexNumber complexNumber)
    {
        ComplexNumber newNumber = new ComplexNumber();
        newNumber.ChangeComlexNumber();

        complexNumber.RealPart += newNumber.RealPart;
        complexNumber.ImaginaryPart += newNumber.ImaginaryPart;
    }

    public static void DifComplexNumbers(ref ComplexNumber complexNumber)
    {
        ComplexNumber newNumber = new ComplexNumber();
        newNumber.ChangeComlexNumber();

        complexNumber.RealPart -= newNumber.RealPart;
        complexNumber.ImaginaryPart -= newNumber.ImaginaryPart;
    }

    public static void MultComplexNumbers(ref ComplexNumber complexNumber)
    {
        ComplexNumber newNumber = new ComplexNumber();
        newNumber.ChangeComlexNumber();

        double resultReal, resultImaginary;

        resultReal = (complexNumber.RealPart * newNumber.RealPart + -1 * complexNumber.ImaginaryPart * newNumber.ImaginaryPart);
        resultImaginary = (complexNumber.ImaginaryPart * newNumber.RealPart + complexNumber.RealPart * newNumber.ImaginaryPart));
    }

    public static void Main(string[] args)
    { 
        ComplexNumber complexNumber = new ComplexNumber();

        bool ExitFromWhile = false;
        while (!ExitFromWhile)
        {
            Console.WriteLine("Enter command: ");
            var command = Console.ReadKey().KeyChar;

            switch (command)
            {
                case 'a':
                    complexNumber.ChangeComlexNumber();
                    break;

                case 'b':
                    Program.SumComplexNumbers(ref complexNumber);
                    break;

                case 'c':
                    Program.DifComplexNumbers(ref complexNumber);
                    break;

                case 'd':
                    break;

                case 'e':
                    break;

                case 'f':
                    break;

                case 'g':
                    break;

                case 'h':
                    complexNumber.PrintComplexNumber();
                    break;

                case 'Q':
                    ExitFromWhile = true;
                    break;

                case 'q':
                    ExitFromWhile = true;
                    break;

                default:
                    Console.WriteLine("\nIvalid Command");
                    break;
            }
            Console.WriteLine();
        }
    }
}

