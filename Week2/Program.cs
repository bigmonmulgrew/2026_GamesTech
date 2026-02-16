

Unit marine = new Unit();       // { Name = "Marine",Health = 45, Damage = 6 };     // This allows you to initialize the data of the object at the time of creation.
                                                                                    // This is called "Object Initializer Syntax" and it is a feature of C# that
                                                                                    // allows you to set properties or fields of an object in a more concise way.
marine.UnitType = "Marine";
marine.Health = 45;
marine.Damage = 6;

Unit zergling = new Unit();     // { Name = "Zergling", Health = 35, Damage = 5 };
zergling.UnitType = "Zergling";
zergling.Health = 35;
zergling.Damage = 5;

Unit ultralisk = new Unit();    // { Name = "Ultralisk", Health = 500, Damage = 35 };
ultralisk.UnitType = "Ultralisk";
ultralisk.Health = 500;
ultralisk.Damage = 35;

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

