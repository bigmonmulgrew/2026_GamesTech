
if (FileManager.LoadText(FileManager.LogFilePath) != null)
{
    Console.WriteLine("Previous log content:");
    Console.WriteLine(FileManager.LoadText(FileManager.LogFilePath));
    // Not a good idea to load the log file multiple times, but this is just for demonstration purposes.
    // How would you improve this code to avoid loading the log file multiple times?
}


Ship ship = new Ship("Sea++", 100, new Vector2(0, 0));

Pirate captain = new Pirate("Blackbeard", 150, 50, ship);    

Pirate crew1 = new Pirate("Will", 90, 25, ship); 
Pirate crew2 = new Pirate("Anne", 95, 30, ship);

// Add the crew members to the captain's crew list
captain.Crew.Add(crew1);
captain.Crew.Add(crew2);

// Take a prisoner and add them to the captain's prisoners list
captain.Prisoners.Add(new Unit("Captured Soldier", 50, 5));

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