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

        Console.Write("Enter the Imaginary part of complex number: ");
        this.ImaginaryPart = Convert.ToDouble(Console.ReadLine());
    }

    public void PrintComplexNumber()
    {
        Console.WriteLine($"\nReal part:{this.RealPart} Imaginary part:{this.ImaginaryPart}");
    }

    public void ModuleComplexNumber()
    {
        double module = Math.Sqrt(Math.Pow(this.RealPart, 2) + Math.Pow(this.ImaginaryPart, 2));
        Console.WriteLine($"\nModule of complex number:{module}");
    }

    public void ArgComplexNumber()
    {
        double arg = this.RealPart >= 0 ? (Math.Atan(this.ImaginaryPart / this.RealPart)) : (Math.Atan(this.ImaginaryPart / this.RealPart) + Math.PI);
        Console.WriteLine($"\nArgiment of complex number:{arg}");
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

        resultReal = (complexNumber.RealPart * newNumber.RealPart - complexNumber.ImaginaryPart * newNumber.ImaginaryPart);
        resultImaginary = (complexNumber.ImaginaryPart * newNumber.RealPart + complexNumber.RealPart * newNumber.ImaginaryPart);

        complexNumber.RealPart = resultReal;
        complexNumber.ImaginaryPart = resultImaginary;
    }

    public static void DivComplexNumbers(ref ComplexNumber complexNumber)
    {
        ComplexNumber newNumber = new ComplexNumber();
        newNumber.ChangeComlexNumber();

        double resultReal, resultImaginary;

        resultReal = (complexNumber.RealPart * newNumber.RealPart + complexNumber.ImaginaryPart * newNumber.ImaginaryPart) / (Math.Pow(newNumber.RealPart, 2) + Math.Pow(newNumber.ImaginaryPart, 2));

        resultImaginary = (complexNumber.ImaginaryPart*newNumber.RealPart - complexNumber.RealPart * newNumber.ImaginaryPart) / (Math.Pow(newNumber.RealPart, 2) + Math.Pow(newNumber.ImaginaryPart, 2));

        complexNumber.RealPart = resultReal;
        complexNumber.ImaginaryPart = resultImaginary;
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
                    SumComplexNumbers(ref complexNumber);
                    break;

                case 'c':
                    DifComplexNumbers(ref complexNumber);
                    break;

                case 'd':
                    MultComplexNumbers(ref complexNumber);
                    break;

                case 'e':
                    DivComplexNumbers(ref complexNumber);
                    break;

                case 'f':
                    complexNumber.ModuleComplexNumber();
                    break;

                case 'g':
                    complexNumber.ArgComplexNumber();
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

