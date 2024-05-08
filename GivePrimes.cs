namespace Primes;


class GivePrimes
{
    static void Main(string[] args)
    {
        //It will give specified number of prime numbers
        var primeNums = Primes().Take(10);

        // Displaying prime numbers one by one
        // to the console
        foreach (var primenum in primeNums)
            Console.WriteLine(primenum);
    }

    public static IEnumerable<int> Primes()
    {
        int counter = 2;

        while (true)
        {
            if (IsPrimeNumber(counter))
                yield return counter;

            counter++;
        }
    }

    private static bool IsPrimeNumber(int value)
    {
        bool output = true;

        // Count i till the half of given number
        // in order to reduce number of iterations
        for (int i = 2; i <= value / 2; i++)
        {
            // Change output to false
            // if value can be divided to i without a remainder
            // and break out of the loop
            if (value % i == 0)
            {
                output = false;
                break;
            }
        }

        return output;
    }
}