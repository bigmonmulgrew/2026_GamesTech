using System.Collections.Generic; // We need this, but do we actually need to type it, and why?

public class Pirate : Unit
{
    Ship pirateShip;
    Map treasureMap;
    List<Pirate> crew = new();
    List<Unit> prisoners = new();
    Queue<string> destinations = new();

    bool hasEyePatch;   
    bool hasPegLeg;
    bool hasHookHand;
    bool hasParrot;
    bool isCaptain;

    string treasureClue;

    // Collapse to keep things tidy
    #region Properties  
    public Ship PirateShip        { get { return pirateShip; } set { pirateShip = value; } }
    public Map TreasureMap        { get { return treasureMap; } set { treasureMap = value; } }
    public List<Pirate> Crew        { get { return crew; } set { crew = value; } }
    public List<Unit> Prisoners     { get { return prisoners; } set { prisoners = value; } }
    public Queue<string> Destinations { get { return destinations; } set { destinations = value; } }
    public bool HasEyePatch         { get { return hasEyePatch; } set { hasEyePatch = value; } }
    public bool HasPegLeg           { get { return hasPegLeg; } set { hasPegLeg = value; } }
    public bool HasHookHand         { get { return hasHookHand; } set { hasHookHand = value; } }
    public bool HasParrot           { get { return hasParrot; } set { hasParrot = value; } }
    public bool IsCaptain           { get { return isCaptain; } set { isCaptain = value; } }
    public string TreasureClue      { get { return treasureClue; } set { treasureClue = value; } }
    #endregion

    public Pirate(string unitType, int health, float damage, Ship pirateShip) 
        : base(unitType, health, damage)
    {
        this.pirateShip = pirateShip;
        this.treasureMap = new Map("Loot map");

        // Hard coded clue for now
        treasureClue = "My treasure is under a tree!";
    }

    public override string ToString()
    {
            return $"Arr! I be {unitType} of the {pirateShip}!";
    }
    /// <summary>
    /// Deals with all the prisoners. No data needed, the Pirate should already have it
    /// </summary>
    public void ProcessPrisoners()      // note this is plural.
    {
        // Only the pirate captain should be able to process prisoners
        // Or mauybe we could implment a first mate too?

        if (!isCaptain) 
        {
            Console.WriteLine($"{unitType} is not the captain and cannot process prisoners.");
            return; 
        }

        // Next we need to check if we even have prisoners.
        if (prisoners.Count == 0)
        {
            Console.WriteLine($"There are currently no prisoners on the {pirateShip.Name}");
            return;
        }

        // Now we start to actually process the prisoners

        foreach (Unit prisoner in prisoners)
        {
            // Instead of nesting this, lets make a function that focusses on what we need to do to process one prisoner.
            // This will repeat that for each member
            ProcessPrisoner(prisoner);
        }
    }

    // Note this is singular, and needs to be passed a prisoner. It is also private
    // It only exists to abstract functionality from ProcessPrisoners()
    private void ProcessPrisoner(Unit prisoner) 
    {
        // Check if prisoner is null incase of improper usage
        if (prisoner == null) return;

        // Now we need to know what type of prisoner we are dealing with, pirate or civilian.
        // All units that are not pirate are assumed to be a civilian for our purposes.

        // The prisoner is a unit but we need to check it is also a prisoner
        // Thetre are many ways to do this

        Pirate pirate = prisoner as Pirate; 

        if (pirate != null)
        {
            Console.WriteLine($"{pirate.UnitType} will walk the plank tonight! His {prisoner.Health * prisoner.Damage} gold will be added to our treasure chest");
            return;
        }
        else
        {
            Console.WriteLine($"A randsom note for {prisoner.Health * prisoner.Damage} gold has beed dispatched to {prisoner.UnitType}'s family.");
        }

    }
}
