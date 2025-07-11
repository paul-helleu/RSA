using System.Numerics;
using System.Security.Cryptography;

namespace Core;

public sealed record RandomNumberGeneratorWithMsbSetToZero : IGenerateRandomOddNumber
{
    public BigInteger GenerateOddNumber(int numberOfBytes, RandomNumberGenerator rng)
    {
        var randomBytes = new byte[numberOfBytes];
        rng.GetBytes(randomBytes);
        randomBytes[numberOfBytes - 1] &= 0x7F;

        return new BigInteger(randomBytes, true);
    }
}