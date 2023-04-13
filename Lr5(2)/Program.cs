using System;
using System.Threading;

namespace TrafficIntersectionExample
{
    class Program
    {
        static object locker = new object();
        static Semaphore semaphore = new Semaphore(2, 2);

        static void Main(string[] args)
        {
            Thread thread1 = new Thread(TrafficLight1);
            Thread thread2 = new Thread(TrafficLight2);
            Thread thread3 = new Thread(TrafficLight3);
            Thread thread4 = new Thread(TrafficLight4);

            thread1.Start();
            thread2.Start();
            thread3.Start();
            thread4.Start();

            thread1.Join();
            thread2.Join();
            thread3.Join();
            thread4.Join();
        }

        static void TrafficLight1()
        {
            while (true)
            {
                lock (locker)
                {
                    Console.WriteLine("Світлофор 1 зелений");
                    Thread.Sleep(5000);
                    Console.WriteLine("Світлофор 2 червоний");
                }
                semaphore.WaitOne();
                Console.WriteLine("Автомобіль проїхав світлофор 1");
                Thread.Sleep(2000);
                semaphore.Release();
            }
        }

        static void TrafficLight2()
        {
            while (true)
            {
                lock (locker)
                {
                    Console.WriteLine("Світлофор 2 зелений");
                    Thread.Sleep(5000);
                    Console.WriteLine("Світлофор 2 червоний");
                }
                semaphore.WaitOne();
                Console.WriteLine("Автомобіль проїхав через світлофор 2");
                Thread.Sleep(2000);
                semaphore.Release();
            }
        }

        static void TrafficLight3()
        {
            while (true)
            {
                lock (locker)
                {
                    Console.WriteLine("Світлофор 3 зелений");
                    Thread.Sleep(5000);
                    Console.WriteLine("Світлофор 3 червоний");
                }
                semaphore.WaitOne();
                Console.WriteLine("Автомобіль проїхав через світлофор 3");
                Thread.Sleep(2000);
                semaphore.Release();
            }
        }

        static void TrafficLight4()
        {
            while (true)
            {
                lock (locker)
                {
                    Console.WriteLine("Світлофор 4 зелений");
                    Thread.Sleep(5000);
                    Console.WriteLine("Світлофор 4 червоний");
                }
                semaphore.WaitOne();
                Console.WriteLine("Автомобіль проїхав світлофор 4");
                Thread.Sleep(2000);
                semaphore.Release();
            }
        }
    }
}