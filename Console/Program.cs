// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Security.Cryptography;
using Core;

var myRng = new MyRandomNumberGenerator(RandomNumberGenerator.Create());

var stopwatch = Stopwatch.StartNew();
var p = myRng.GenerateProbablyPrimeNumber(8, 10);
stopwatch.Stop();

Console.WriteLine($"p: {p} is probably prime, generated in {stopwatch.ElapsedTicks} ticks!");

stopwatch.Restart();
var q = myRng.GenerateProbablyPrimeNumber(8, 10);
stopwatch.Stop();

Console.WriteLine($"q: {q} is probably prime, generated in {stopwatch.ElapsedTicks} ticks!");