namespace Narcissistic;


class NarcissisticNumbers
{
    static int CountNarcissisticNumbersTill(int n)
    {
        int currentNumber, j, k, multipliedDigit, numberOfNarcissistic, numberOfDigits;
        double s;

        currentNumber = 0; numberOfNarcissistic = 0;
        while (currentNumber < n)
        {
            j = currentNumber; numberOfDigits = 0;
            // this loop counts the number of digits
            while (j != 0)//
            {
                numberOfDigits++;
                j /= 10;
            }

            j = currentNumber; s = 0;
            while (j != 0)//
            {
                k = 0; multipliedDigit = 1;
                // this loop multiply last digit
                // as many times as digits in number
                while (k < numberOfDigits)//
                {
                    multipliedDigit = multipliedDigit * (j % 10);
                    k++;
                }

                s += multipliedDigit;
                j /= 10;
            }

            numberOfNarcissistic = numberOfNarcissistic + (s == currentNumber ? 1 : 0);
            currentNumber++;
        }

        return numberOfNarcissistic;
    }

    static bool IsNarcissisticNumber(int n)
    {
        int j, k, multipliedDigit, numberOfDigits;
        double s;
        int currentNumber = n;

        j = currentNumber; numberOfDigits = 0;
        // this loop counts the number of digits
        while (j != 0)//
        {
            numberOfDigits++;
            j /= 10;
        }

        j = currentNumber; s = 0;
        while (j != 0)//
        {
            k = 0; multipliedDigit = 1;
            // this loop multiply last digit
            // as many times as digits in number
            while (k < numberOfDigits)//
            {
                multipliedDigit = multipliedDigit * (j % 10);
                k++;
            }

            s += multipliedDigit;
            j /= 10;
        }

        return s == currentNumber;
    }

    static void Main(string[] args)
    {
        // Give X and get the number of
        // narcissistic numbers till X
        int num = CountNarcissisticNumbersTill(120);
        Console.WriteLine(num);

        // Give X and get true if the given number is narcissistic
        bool res = IsNarcissisticNumber(123);
        Console.WriteLine(res);
    }
}
