// See https://aka.ms/new-console-template for more information
using ConsoleApp1;

Class1 a = new();
Class2 b = new();
Class3 c = new();

Console.WriteLine($"Typeof a:{a.GetType()}");
Console.WriteLine($"Typeof b:{b.GetType()}");
Console.WriteLine($"Typeof c:{c.GetType()}");
Console.WriteLine($"Typeof b as Class1:{((Class1)b).GetType()}");

