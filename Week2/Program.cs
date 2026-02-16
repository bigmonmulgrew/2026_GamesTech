

Unit marine = new Unit("Marine", 45, 6);

Unit zergling = new Unit("Zergling", 35, 5);

Unit ultralisk = new Unit("Ultralisk", 500, 35);

Console.WriteLine("Stats of the units using \"PrintStats\" Method:");
marine.PrintStats();
zergling.PrintStats();
ultralisk.PrintStats();

Console.WriteLine("\nStats of the units using \"ToString\" Method:");
// To string is automatically called when we try to print an object as a string.
Console.WriteLine(marine);
Console.WriteLine(zergling);
Console.WriteLine(ultralisk);

marine.Health = 200;

Console.WriteLine($"\nStats of the units after changing the health of the marine: {marine}");

