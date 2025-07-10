using System.Numerics;
using System.Security.Cryptography;

namespace Core;

public class MyRandomNumberGenerator(RandomNumberGenerator rng)
{
    public BigInteger GenerateOddNumberWithMsbSetToOne(int numberOfBytes)
    {
        var randomBytes = new byte[numberOfBytes];
        rng.GetBytes(randomBytes);
        randomBytes[numberOfBytes - 1] |= 0x80;
        randomBytes[0] |= 0x01;

        return new BigInteger(randomBytes, true);
    }

    public BigInteger GenerateNumberWithMsbSetToZero(int numberOfBytes)
    {
        var randomBytes = new byte[numberOfBytes];
        rng.GetBytes(randomBytes);
        randomBytes[numberOfBytes - 1] &= 0x7F;

        return new BigInteger(randomBytes, true);
    }

    public BigInteger GenerateProbablyPrimeNumber(int numberOfBytes, int numberOfTry)
    {
        var n = GenerateOddNumberWithMsbSetToOne(numberOfBytes);
        while (!MillerRabinTestWithWitness(n, numberOfTry)) n = GenerateOddNumberWithMsbSetToOne(numberOfBytes);

        return n;
    }

    public bool MillerRabinTestWithWitness(BigInteger n, int numberOfTry)
    {
        var count = 0;
        while (count < numberOfTry)
            if (MillerRabinTest(n))
                count++;
            else
                return false;

        return true;
    }

    public bool MillerRabinTest(BigInteger n)
    {
        var a = GenerateNumberWithMsbSetToZero(n.GetByteCount(true));
        var d = n - 1;
        var s = 0;

        while (d % 2 == 0)
        {
            d >>= 1;
            s++;
        }

        if (BigInteger.ModPow(a, d, n) != BigInteger.One) return false;

        for (var i = 1; i < s; i++)
            if (BigInteger.ModPow(a, BigInteger.Pow(2, i) * d, n) == -1)
                return false;

        return true;
    }
}