using System;
using System.Collections.Generic;
using System.Threading;

namespace PrimeNumbersApp
{
    class PrimeNumbers
    {
        /// <summary>
        /// A list to hold all prime numbers. I chose a 
        /// list since it's easy to add to it.
        /// </summary>
        public List<int> DataStructure { get; set; }
        const int TimeToSleep = 2000;

        public PrimeNumbers()
        {
            DataStructure = new();
        }

        /// <summary>
        /// A main menu that gives the user 4 choices. 
        /// It runs until the user decides to exit the program.
        /// </summary>
        public void MainMenu()
        {
            var exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("\t   ___      _             _  __           __");
                Console.WriteLine("\t  / _ \\____(_)_ _  ___   / |/ /_ ____ _  / /  ___ _______");
                Console.WriteLine("\t / ___/ __/ /  ' \\/ -_) /    / // /  ' \\/ _ \\/ -_) __(_-<");
                Console.WriteLine("\t/_/  /_/ /_/_/_/_/\\__/ /_/|_/\\_,_/_/_/_/_.__/\\__/_/ /___/\n");
                Console.WriteLine("\t1. Add a number");
                Console.WriteLine("\t2. Print data structure");
                Console.WriteLine("\t3. Add next prime number");
                Console.WriteLine("\t4. Exit program");
                Console.Write("\t> ");
                int.TryParse(Console.ReadLine(), out var choice);

                switch (choice)
                {
                    case 1:
                        AddNumber();
                        break;
                    case 2:
                        PrintStructure();
                        break;
                    case 3:
                        AddNextPrimeNumber();
                        break;
                    case 4:
                        exit = true;
                        break;
                    default:
                        ErrorMessage();
                        break;
                }
            }
        }

        /// <summary>
        /// Lets the user enter a number. If the number is a prime number 
        /// and not already in the data structure it will be added to the data 
        /// structure. If the user enters wrong input an error message will 
        /// be printed to the screen.
        /// </summary>
        private void AddNumber()
        {
            while (true)
            {
                Console.Write("\n\tEnter a number: ");
                if (int.TryParse(Console.ReadLine(), out var num))
                {
                    if (IsPrime(num))
                    {
                        if (DataStructure.Contains(num))
                        {
                            Console.Write($"\tThe number {num} is already in the data structure...");
                        }
                        else
                        {
                            DataStructure.Add(num);
                            DataStructure.Sort();
                            Console.Write($"\tYey! The number {num} is a prime " +
                                "number and is added to the data structure!");
                        }
                    }
                    else
                    {
                        Console.Write($"\tSorry! The number {num} is not a prime number...");
                    }

                    Thread.Sleep(TimeToSleep);
                    break;
                }
                else
                {
                    ErrorMessage();
                }

            }
        }

        /// <summary>
        /// Checks if <paramref name="num"/> is a prime number or not.
        /// </summary>
        /// <param name="num">The number to be checked.</param>
        /// <returns><see langword="true"/> if the number is a prime number, otherwise <see langword="false"/>.</returns>
        public static bool IsPrime(int num)
        {
            // All numbers below 2 are not prime numbers.
            if (num <= 1) return false;

            // 2 and 3 are the two first prime numbers. 
            if (num <= 3) return true;

            // Every other number that is evenly divisible by 2 or 3 is not a
            // prime number.
            if (num % 2 == 0 || num % 3 == 0) return false;

            // The numbers after 3 that aren't divisible by 2 or 3 up until 25
            // (5, 7, 11, 13, 17, 19, 23) are all prime numbers so we dont have
            // to check those. For all remaining numbers (starting at 25) we
            // have to check if they are evenly divisible by any prime number
            // other than 2 or 3 (since we already checked against them). We can
            // check 'i * i' againt 'num' since at least one factor of the
            // number needs to be smaller or equal to the square root of 'num'.
            // I guess I could have used Math.Sqrt(num) but I read that
            // multiplication was faster.
            for (int i = 5; i * i <= num; i += 6)
            {
                // First time around 'i' is 5 and will cancel out numbers like
                // 25, 35 and 55 but we also check againt 'i + 2' which the first
                // time is 7. This will cancel out numbers like 49, 77 and 91.
                // Next time around we move 'i' 6 steps to the next two prime
                // numbers 11 and 13 and so on. 'i' or 'i + 2' will this way always
                // land on at least one prime number.
                if (num % i == 0 || num % (i + 2) == 0)
                {
                    return false;
                }
            }

            // If we haven't canceled out 'num' by now it's a prime number.
            return true;
        }

        /// <summary>
        /// Prints the whole data structure to the screen if there are any numbers, 
        /// or prints that the data structure is empty to the screen.
        /// </summary>
        private void PrintStructure()
        {
            if (DataStructure.Count > 0)
            {
                Console.WriteLine("\n\t" + string.Join(", ", DataStructure));
                Console.Write("\tPress any key to continue...");
                Console.ReadKey();
            }
            else
            {
                Console.Write("\n\tThe data structure is empty...");
                Thread.Sleep(TimeToSleep);
            }
        }

        /// <summary>
        /// Adds the next prime number based on the highest number in the data 
        /// structure.
        /// </summary>
        private void AddNextPrimeNumber()
        {
            if (DataStructure.Count > 0)
            {
                // Isolates the largest prime number in the data structure.
                var largest = DataStructure[^1];

                // There will always be a prime number somewhere between 'largest' and 'largest * 2'.
                for (int i = largest + 1; i < largest * 2; i++)
                {
                    if (IsPrime(i))
                    {
                        DataStructure.Add(i);
                        Console.Write($"\n\tThe number {i} was added to the data structure!");
                        break;
                    }
                }
            }
            else
            {
                // if the data structure is empty the first prime number will be added.
                DataStructure.Add(2);
                Console.Write($"\n\tThe number 2 was added to the data structure!");
            }

            Thread.Sleep(TimeToSleep);
        }

        /// <summary>
        /// Prints an error message to the screen and erases it afterwards.
        /// </summary>
        private void ErrorMessage()
        {
            Console.Write("\tWrong type of input, requires a " +
                            "number, please try again.");
            Thread.Sleep(TimeToSleep);
            Console.SetCursorPosition(0, Console.CursorTop);
            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine(new string(' ', 80));
                Console.SetCursorPosition(0, Console.CursorTop - 2);
            }
        }
    }
}
