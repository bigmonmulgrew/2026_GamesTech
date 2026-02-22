public class Pirate : Unit
{
    string pirateShip;
    bool hasEyePatch;   
    bool hasPegLeg;
    bool hasHookHand;
    bool hasParrot;
    bool isCaptain;

    string treasureClue;

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
