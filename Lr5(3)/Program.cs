using System;
using System.Threading;

class MatrixMultiplication
{
    static int[,] matrix1 = new int[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
    static int[,] matrix2 = new int[3, 3] { { 9, 8, 7 }, { 6, 5, 4 }, { 3, 2, 1 } };
    static int[,] resultMatrix = new int[3, 3];

    static Semaphore semaphore = new Semaphore(2, 2);
    static object lockObj = new object();

    static void Main()
    {
        Thread[] threads = new Thread[3];
        for (int i = 0; i < 3; i++)
        {
            threads[i] = new Thread(new ParameterizedThreadStart(CalculateRow));
            threads[i].Start(i);
        }

        for (int i = 0; i < 3; i++)
        {
            threads[i].Join();
        }

        Console.WriteLine("res:");
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Console.Write("{0}\t", resultMatrix[i, j]);
            }
            Console.WriteLine();
        }

        Console.ReadLine();
    }

    static void CalculateRow(object rowObj)
    {
        int row = (int)rowObj;
        semaphore.WaitOne();

        for (int i = 0; i < 3; i++)
        {
            int sum = 0;
            for (int j = 0; j < 3; j++)
            {
                sum += matrix1[row, j] * matrix2[j, i];
            }

            lock (lockObj)
            {
                resultMatrix[row, i] = sum;
            }
        }

        semaphore.Release();
    }
}