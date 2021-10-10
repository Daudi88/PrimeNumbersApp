using System;
using System.Collections.Generic;
using System.Threading;

namespace PrimeNumbersApp
{
    public class PrimeNumbers
    {
        /// <summary>
        /// A list to hold all prime numbers.
        /// </summary>
        private static List<int> primeNumbers = new();

        /// <summary>
        /// The time to pause the thread in milliseconds.
        /// </summary>
        const int TimeToSleep = 2000;

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
                if (int.TryParse(Console.ReadLine(), out var choice))
                {
                    switch (choice)
                    {
                        case 1:
                            AddPrimeNumber();
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
                            Console.Write("\tInvalid choice. Try a " +
                                "number between 1-4.");
                            Thread.Sleep(TimeToSleep);
                            break;
                    }
                }
                else
                {
                    ErrorMessage();
                }
            }
        }

        /// <summary>
        /// Lets the user enter a number. If the number is a prime number 
        /// and not already in the data structure it will be added to the data 
        /// structure. If the user enters wrong input an error message will 
        /// be printed to the screen.
        /// </summary>
        private void AddPrimeNumber()
        {
            while (true)
            {
                Console.Write("\n\tEnter a number: ");
                if (int.TryParse(Console.ReadLine(), out var num))
                {
                    if (IsPrimeNumber(num))
                    {
                        if (primeNumbers.Contains(num))
                        {
                            Console.Write($"\tThe number {num} is already in " +
                                $"the data structure...");
                        }
                        else
                        {
                            primeNumbers.Add(num);
                            Console.Write($"\tYey! The number {num} is a prime " +
                                "number and is added to the data structure!");
                            primeNumbers.Sort();
                        }
                    }
                    else
                    {
                        Console.Write($"\tSorry! The number {num} is not a " +
                            $"prime number...");
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
        /// <returns><see langword="true"/> if the number is a prime number, 
        /// otherwise <see langword="false"/>.</returns>
        public static bool IsPrimeNumber(int num)
        {
            // This algorithm I found on stack overflow but
            // I have broken the steps down to understand it.
            // https://stackoverflow.com/questions/62150130/algorithm-of-checking-if-the-number-is-prime

            // All numbers below 2 are not prime numbers.
            if (num <= 1) return false;

            // 2 and 3 are the two first prime numbers. 
            if (num <= 3) return true;

            // Every other number that is evenly divisible by 2 or 3 is not a
            // prime number.
            if (num % 2 == 0 || num % 3 == 0) return false;

            // To find if the number is a prime number we try to divide the
            // number with other prime numbers. We have checked against 2 and 3
            // so next number is 5. Any prime number > 3 is of the form 6k±1 
            // for instance 5 (6*1-1) or 7 (6*1+1). If we apply this pattern we
            // can check against two prime numbers every time around the for-loop.
            // We can also check 'i * i' against 'num' since at least one factor
            // of the number needs to be smaller or equal to the square root of
            // 'num'. This could be written Math.Sqrt(num) but multiplication
            // is supposed to be faster.
            for (int i = 5; i * i <= num; i += 6)
            {
                // First time around 'i' is 5 and will cancel out numbers like
                // 25, 35 and 55 but we also check against 'i + 2' which the first
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
        /// Prints the whole data structure to the screen if there are any 
        /// numbers, or prints that the data structure is empty to the screen.
        /// </summary>
        private void PrintStructure()
        {
            if (primeNumbers.Count > 0)
            {
                Console.WriteLine("\n\t" + string.Join(", ", primeNumbers));
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
            var largest = GetLargestNumber(primeNumbers);
            var nextPrime = NextPrimeNumber(largest);

            // If nothing went wrong the next prime
            // number is added to the data structure.
            if (nextPrime != -1)
            {
                primeNumbers.Add(nextPrime);
                Console.Write($"\n\tThe number {nextPrime} was added to the " +
                    $"data structure!");
            }
            else
            {
                Console.Write("Something went wrong. No number was added to " +
                    "the data structure...");
            }

            Thread.Sleep(TimeToSleep);
        }

        /// <summary>
        /// Finds the next prime number based on <paramref name="num"/>.
        /// </summary>
        /// <param name="num">A prime number.</param>
        /// <returns>The next prime number.</returns>
        public static int NextPrimeNumber(int num)
        {
            // If there are no previous prime numbers
            // 2 is the next prime number.
            if (num < 2) return 2;

            // If 'num > 1' there is always at least one prime
            // number p within this range 'num < p < 2num' but since
            // we don't want to check if 'num' is prime we add 1 to
            // 'num' in the for loop.
            for (int i = (int)num + 1; i < 2 * num; i++)
            {
                if (IsPrimeNumber(i))
                {
                    return i;
                }
            }

            // If something went wrong or the number is
            // outside the scope of an integer we return -1.
            return -1;
        }

        /// <summary>
        /// Finds the largest number in a list of integers.
        /// </summary>
        /// <param name="list">A list of integers.</param>
        /// <returns>The largest number or 0.</returns>
        public static int GetLargestNumber(List<int> list)
        {
            var largest = 0;

            if (list?.Count > 0)
            {
                // Checks the whole list and compares
                // each number against 'largest'.
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i] > largest)
                    {
                        largest = list[i];
                    }
                }
            }

            return largest;
        }

        /// <summary>
        /// Prints an error message to the screen and erases it afterwards.
        /// </summary>
        private void ErrorMessage()
        {
            Console.Write("\tWrong type of input, requires a " +
                            "number, please try again.");
            Thread.Sleep(TimeToSleep);

            // Erases previous written text.
            Console.SetCursorPosition(0, Console.CursorTop);
            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine(new string(' ', 80));
                Console.SetCursorPosition(0, Console.CursorTop - 2);
            }
        }
    }
}
