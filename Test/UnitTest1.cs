using System.Numerics;
using System.Security.Cryptography;
using Core;

namespace Test;

public class MyRandomNumberGeneratorWithMsbSetToOneTest
{
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void GenerateProbablyPrimeNumber_DifferentNumberOfBytesIn5Try_ReturnsOddNumber(int numberOfBytes)
    {
        var myRng = new MyRandomNumberGenerator(RandomNumberGenerator.Create());

        var p = myRng.GenerateProbablyPrimeNumber(numberOfBytes, 5);

        Assert.Equal(1, p % 2);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void GenerateProbablyPrimeNumber_DifferentNumberOfBytesIn5Try_ReturnsNumberWithMsbSetToOne(int numberOfBytes)
    {
        var myRng = new MyRandomNumberGenerator(RandomNumberGenerator.Create());

        var p = myRng.GenerateProbablyPrimeNumber(numberOfBytes, 5);

        Assert.True(p > BigInteger.Pow(2, numberOfBytes * 8 - 1));
    }

    [Theory]
    [InlineData(77)]
    [InlineData(2141)]
    [InlineData(97)]
    [InlineData(41211)]
    [InlineData(5555555)]
    public void MillerRabinTestWithWitness_OddNotPrimaryNumbers_ReturnsFalse(BigInteger number)
    {
        var myRng = new MyRandomNumberGenerator(RandomNumberGenerator.Create());

        var isPrime = myRng.MillerRabinTestWithWitness(number, 1_000);

        Assert.False(isPrime);
    }
}