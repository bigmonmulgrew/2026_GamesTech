
Pirate captain = new Pirate("Blackbeard", 150, 50, "Sea++");    

Pirate crew1 = new Pirate("Will", 90, 25, "Sea++");  // We are repeating data, consider using a constant
Pirate crew2 = new Pirate("Anne", 95, 30, "Sea++");

// Add the crew members to the captain's crew list
captain.Crew.Add(crew1);
captain.Crew.Add(crew2);

// Take a prisoner and add them to the captain's prisoners list
captain.Prisoners.Add(new Unit("Captured Soldier", 50, 5));

// Add some destinations to the captain's destinations queue
captain.Destinations.Enqueue("Tortuga");
captain.Destinations.Enqueue("Skull Island");

Console.WriteLine("Next destination: " + captain.Destinations.Dequeue());
// Output: Next destination: Tortuga
