
if (FileManager.LoadText(FileManager.LogFilePath) != null)
{
    Console.WriteLine("Previous log content:");
    Console.WriteLine(FileManager.LoadText(FileManager.LogFilePath));
    // Not a good idea to load the log file multiple times, but this is just for demonstration purposes.
    // How would you improve this code to avoid loading the log file multiple times?
}


Ship ship = new Ship("Ocean's Unity", 100, new Vector2(5, 4));
Ship rivalShip = new Ship("UoD Ureal", 250, new Vector2(4, 5));

Pirate captain = new Pirate("Blackbeard", 150, 50, ship);    
captain.IsCaptain = true;

Pirate crew1 = new Pirate("Will", 90, 25, ship); 
Pirate crew2 = new Pirate("Anne", 95, 30, ship);

// Add the crew members to the captain's crew list
captain.Crew.Add(crew1);
captain.Crew.Add(crew2);

// Take a prisoner and add them to the captain's prisoners list
captain.Prisoners.Add(new Unit("Dave", 50, 5));
captain.Prisoners.Add(new Unit("Jon", 50, 5));
captain.Prisoners.Add(new Pirate("Jeremy", 80, 50, rivalShip));
captain.Prisoners.Add(new Unit("Hayden", 50, 5));
captain.Prisoners.Add(new Unit("Isaac", 50, 5));
captain.Prisoners.Add(new Pirate("Patrick", 80, 15, rivalShip));

// Add some destinations to the captain's destinations queue
captain.Destinations.Enqueue("Tortuga");
captain.Destinations.Enqueue("Skull Island");

Console.WriteLine(
    $"Next destination: {captain.Destinations.Dequeue()} " +
    $"aboard the {captain.PirateShip.Name}, " +
    $"following my trusty map, {captain.TreasureMap.Name}"
    );
// Output: Next destination: Tortuga aboard the Sea++

Console.WriteLine(Directory.GetCurrentDirectory());

FileManager.SaveText($"Captain's log: Set sail for adventure!\n" +
    $"Treasure clue: {captain.TreasureClue}");
Console.WriteLine($"Log saved to: {FileManager.LogFilePath}");

Console.Write("\n\n\n"); // Just some empty lines to create some space.

Console.WriteLine($"Crew 1 processing prisoners");
crew1.ProcessPrisoners();

Console.Write("\n\n\n"); // Just some empty lines to create some space.
Console.WriteLine($"Captain processing prisoners");
captain.ProcessPrisoners();