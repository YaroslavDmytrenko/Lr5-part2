using System;
using System.Collections.Generic;
using System.Threading;


    class Program
    {
        static Queue<int> queue = new Queue<int>();
        static object locker = new object();

        static void Main(string[] args)
        {
            Thread producerThread = new Thread(Producer);
            Thread consumerThread = new Thread(Consumer);

            producerThread.Start();
            consumerThread.Start();

            producerThread.Join();
            consumerThread.Join();
        }

        static void Producer()
        {
            Random random = new Random();

            while (true)
            {
                int number = random.Next(100);
                lock (locker)
                {
                    queue.Enqueue(number);
                    Console.WriteLine($"Producer produced {number}");
                }
                Thread.Sleep(1000);
            }
        }

        static void Consumer()
        {
            while (true)
            {
                lock (locker)
                {
                    if (queue.Count > 0)
                    {
                        int number = queue.Dequeue();
                        Console.WriteLine($"Consumer consumed {number}");
                    }
                }
                Thread.Sleep(1000);
            }
        }
    }