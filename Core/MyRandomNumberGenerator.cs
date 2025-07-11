using System.Numerics;
using System.Security.Cryptography;

namespace Core;

public class MyRandomNumberGenerator(RandomNumberGenerator rng)
{
    public BigInteger GenerateProbablyPrimeNumber(int numberOfBytes, int numberOfTry,
        IGenerateRandomOddNumber oddGenerator, IGenerateRandomOddNumber numberGenerator)
    {
        var n = oddGenerator.GenerateOddNumber(numberOfBytes, rng);
        while (!MillerRabinTestWithWitness(n, numberOfTry, numberGenerator))
            n = oddGenerator.GenerateOddNumber(numberOfBytes, rng);

        return n;
    }

    public bool MillerRabinTestWithWitness(BigInteger n, int numberOfTry, IGenerateRandomOddNumber numberGenerator)
    {
        var count = 0;
        while (count < numberOfTry)
            if (MillerRabinTest(n, numberGenerator))
                count++;
            else
                return false;

        return true;
    }

    public bool MillerRabinTest(BigInteger n, IGenerateRandomOddNumber generator)
    {
        var a = generator.GenerateOddNumber(n.GetByteCount(true), rng);
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