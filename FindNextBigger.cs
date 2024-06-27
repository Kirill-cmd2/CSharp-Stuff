// to improve: finding greater number for negatives
namespace NextBigger;

public static class BigNumber
{
    public static void Main(string[] args)
    {
        int result;

        result = NextBiggerThan(1234321);
        Console.WriteLine(result);

        result = NextBiggerThan(10);
        Console.WriteLine(result);
    }

    /// <summary>
    /// Finds the nearest largest integer consisting of the digits of the given positive integer number; return -1 if no such number exists.
    /// </summary>
    /// <param name="number">Source number.</param>
    /// <returns>
    /// The nearest largest integer consisting of the digits  of the given positive integer; return -1 if no such number exists.
    /// </returns>
    /// <exception cref="ArgumentException">Thrown when source number is less than 0.</exception>
    public static int NextBiggerThan(int number)
    {
        if (number < 0)
        {
            throw new ArgumentException($"Value of {nameof(number)} cannot be less zero.");
        }

        // if given number is equal to maximum of Int32 type
        // there can't be number bigger than given number
        if (number == int.MaxValue)
        {
            return -1;
        }

        // getting number of digits of the given number
        int numberOfDigits = NumberLength(number);

        // compare two consequetive numbers (pair)
        // starting from the end of given number
        // find index of first digit less than its pair and break out of loop
        int i;
        for (i = numberOfDigits - 2; i >= 0; i--)
        {
            int first = GetDigitByIndex(number, i);
            int second = GetDigitByIndex(number, i + 1);

            if (first < second)
            {
                break;
            }
        }

        // i eqauls -1 when numberOfDigits is 1
        // (given number consist of only one number)
        // there can't be greater number than given
        if (i == -1)
        {
            return -1;
        }

        // initially set smallestLargerIndex to next of i
        // because digit next to i is larger than it as found on 51 line
        // then start loop with 2 digits away to right from i
        // if this (index=j) digit is
        // greater than digit at index i and
        // less than digit at index smallestLargerIndex
        // re-assign smallestLargerIndex to j (which corresponds to smallest digit in number after i)
        int smallestLargerIndex = i + 1;
        for (int j = i + 2; j < numberOfDigits; j++)
        {
            int digitIndexj = GetDigitByIndex(number, j);
            int digitIndexi = GetDigitByIndex(number, i);
            int digitIndexsmall = GetDigitByIndex(number, smallestLargerIndex);

            if (digitIndexj > digitIndexi && digitIndexj < digitIndexsmall)
            {
                smallestLargerIndex = j;
            }
        }

        int swappedNumber = SwapDigits(number, i, smallestLargerIndex, numberOfDigits);

        // get number after first changed digit
        // e.g. 1243321 => 3321
        int rest = swappedNumber % PowerOfTen(numberOfDigits - i - 1);
        int leftSide = swappedNumber - rest;

        rest = SortDigitsAscending(rest);

        int result = leftSide + rest;
        return result;
    }

    /// <summary>
    /// Finds the number of digits for given integer.
    /// </summary>
    /// <param name="number">Source number.</param>
    /// <returns>
    /// Number of digits.
    /// </returns>
    public static int NumberLength(int number)
    {
        int numberOfDigits = 0;

        // increment numberOfDigits until source number become zero
        for (int a = number; a != 0; a /= 10)
        {
            numberOfDigits++;
        }

        return numberOfDigits;
    }

    /// <summary>
    /// Finds appropriate digit of number which stays at given index.
    /// </summary>
    /// <param name="number">Source number.</param>
    /// <param name="index">Index of digit for search.</param>
    /// <returns>
    /// Digit (int) which corresponds to given index in number.
    /// </returns>
    public static int GetDigitByIndex(int number, int index)
    {
        int result, digitsNum;

        // in order to get digit which "stands" in given index in number
        // need to return last digit of number which has "index + 1" number of digits
        // using do-while if digit to search is last one in given number
        int b = number;
        do
        {
            result = b % 10;
            digitsNum = NumberLength(b);
            b /= 10;
        }
        while (digitsNum != index + 1);

        return result;
    }

    /// <summary>
    /// Finds exponent of ten raised to given number.
    /// </summary>
    /// <param name="exponent">Exponent of number to be raised.</param>
    /// <returns>
    /// Result (int) of raising 10 to given number.
    /// </returns>
    public static int PowerOfTen(int exponent)
    {
        int baseNumber = 10;
        int result = 1;

        for (int i = 0; i < exponent; i++)
        {
            result *= baseNumber;
        }

        return result;
    }

    /// <summary>
    /// Swap two digits of number by their index.
    /// </summary>
    /// <param name="givenNumber">Number digits of which should be swapped.</param>
    /// <param name="firstIndex">Index of first digit to be swapped.</param>
    /// <param name="secondIndex">Index of second digit to be swapped.</param>
    /// <param name="numberOfDigits">Number of digits in givenNumber.</param>
    /// <returns>
    /// Number with two swapped digits.
    /// </returns>
    public static int SwapDigits(int givenNumber, int firstIndex, int secondIndex, int numberOfDigits)
    {
        int temporaryNum = 0;
        int iPower = 0, smallPower = 0;

        // start from left side of number
        // if current index is equal to index of first or second digit to be swapped
        // just save corresponding power number (for raising 10 later on)
        // else add each digit multiplied to corresponding power of ten to temporaryNum
        for (int power = numberOfDigits - 1; power >= 0; power--)
        {
            int currentIndex = numberOfDigits - power - 1;

            if (currentIndex == firstIndex)
            {
                iPower = power;
            }
            else if (currentIndex == secondIndex)
            {
                smallPower = power;
            }
            else
            {
                temporaryNum += GetDigitByIndex(givenNumber, currentIndex) * PowerOfTen(power);
            }
        }

        int iDigit = GetDigitByIndex(givenNumber, firstIndex);
        int iSmall = GetDigitByIndex(givenNumber, secondIndex);

        // add up temporaryNum and digits multiplied to
        // 10 raised into the power of other digit's corresponding power
        // e.g. 1234321 => 1200321 + 4 * 10^4 + 3 * 10^3
        temporaryNum = temporaryNum + (iDigit * PowerOfTen(smallPower)) + (iSmall * PowerOfTen(iPower));
        return temporaryNum;
    }

    /// <summary>
    /// Sort digits of given number in ascending order.
    /// </summary>
    /// <param name="number">Number digits of which should be sorted out.</param>
    /// <returns>
    /// Number (int) which consist of digits of the given number sorted in ascending order.
    /// </returns>
    public static int SortDigitsAscending(int number)
    {
        int sortedNumber = 0;
        int place = 1;

        // start from highest digit in order to put it in the lowest place
        for (int digit = 9; digit >= 0; digit--)
        {
            int tempNumber = number;
            int position = 0;

            while (tempNumber > 0)
            {
                int currentDigit = tempNumber % 10;
                if (currentDigit == digit)
                {
                    sortedNumber += currentDigit * place;
                    place *= 10; // increase place of digit to the next rank
                }

                tempNumber /= 10;
                position++;
            }
        }

        return sortedNumber;
    }
}
