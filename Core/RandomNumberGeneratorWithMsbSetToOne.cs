using System.Numerics;
using System.Security.Cryptography;

namespace Core;

public sealed record RandomNumberGeneratorWithMsbSetToOne : IGenerateRandomOddNumber
{
    public BigInteger GenerateOddNumber(int numberOfBytes, RandomNumberGenerator rng)
    {
        var randomBytes = new byte[numberOfBytes];
        rng.GetBytes(randomBytes);
        randomBytes[numberOfBytes - 1] |= 0x80;
        randomBytes[0] |= 0x01;

        return new BigInteger(randomBytes, true);
    }
}