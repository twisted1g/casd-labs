void MatrixPrint(int[,] matrix, int dim)
{
    for (int i = 0; i < dim; i++)
    {
        for (int j = 0; j < dim; j++) Console.Write($"{matrix[i, j]} ");
        Console.WriteLine();
    }
    Console.WriteLine();
}

void PrintVector(int[] vector, int dim)
{
    for (int j = 0; j < dim; j++) Console.Write($"{vector[j]} ");
    Console.WriteLine();
}

bool IsSymmetric(int[,] matrix, int dim)
{
    for (int i = 0; i < dim; i++)
        for (int j = 0; j < dim; j++) if (matrix[i, j] != matrix[j, i]) return false;
    return true;
}

double MatrixMult(int[,] matrix, int[] vector, int dim)
{
    int[] new_vector1 = new int[dim];
    for (int i = 0; i < dim; i++)
    {
        int sum = 0;
        for (int j = 0; j < dim; j++)
        {
            sum += vector[j] * matrix[i, j];
        }
        new_vector1[i] = sum;
    }
    double result = 0;
    for (int i = 0; i < dim; i++) result += new_vector1[i] * new_vector1[i];

    result = Math.Sqrt(result);
    return result;
}

try
{
    StreamReader sr = new StreamReader(@"D:\file.txt.txt");
    int dim = Convert.ToInt32(sr.ReadLine());
    Console.WriteLine(dim);


    int[,] matrix = new int[dim, dim];
    for (int i = 0; i < dim; i++)
    {
        string[] nums = sr.ReadLine().Split(" ");
        for (int j = 0; j < dim; j++) matrix[i, j] = Convert.ToInt32(nums[j]);
    }
    MatrixPrint(matrix, dim);

    string line = sr.ReadLine();
    int[] vector = line.Split(' ').Select(x => Convert.ToInt32(x)).ToArray();

    PrintVector(vector, dim);

    sr.Close();

    if (IsSymmetric(matrix, dim))
    {
        Console.WriteLine("True");

        double result = MatrixMult(matrix, vector, dim);
        Console.WriteLine(result);
    }
    else throw new Exception();
}
catch
{
    Console.WriteLine("Error");
}