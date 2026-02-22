using System.Collections.Generic; // We need this, but do we actually need to type it, and why?

public class Pirate : Unit
{
    string pirateShip;
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
    public string PirateShip        { get { return pirateShip; } set { pirateShip = value; } }
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

    public Pirate(string unitType, int health, float damage, string pirateShip) 
        : base(unitType, health, damage)
    {
        this.pirateShip = pirateShip;

        // Hard coded clue for now
        treasureClue = "My treasure is under a tree!";
    }

    public override string ToString()
    {
            return $"Arr! I be {unitType} of the {pirateShip}!";
    }
}
