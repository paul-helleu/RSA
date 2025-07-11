using System.Numerics;
using System.Security.Cryptography;

namespace Core;

public interface IGenerateRandomOddNumber
{
    public BigInteger GenerateOddNumber(int numberOfBytes, RandomNumberGenerator rng);
}