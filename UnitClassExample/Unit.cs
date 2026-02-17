class Unit
{
    private string unitType;
    private int health;
    private float damage;

    

    // Data that belongs to ONE unit
    // C# Does not care about white space (Tabs, Spaces, newlines). All three of theese do the same thing
    // Consistency is key when it comes to white space. Notice this is hard to read due to the inconsistent use of white space.
    // It is a good practice to use the same amount of indentation (Tabs or Spaces) throughout your code.
    // This makes it easier to read and understand.
    public string UnitType
    {
        get { return unitType; }
        set { unitType = value; }
    }
    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            health = (int)value;
        }
    }
    public float Damage { get { return damage; } set { damage = value; } }

    public Unit(string unitType, int health, float damage)
    {
        this.unitType = unitType;
        this.health = health;
        this.damage = damage;
    }

    // This is an example of a bad practice Property.
    // The first character is lowercase. This will work, but can cause issues later.
    // Visual studio will flag this and give a small warning .. under the first character.
    public string myString { get { return unitType; } set { unitType = value; } }

    // Method that prints the units data.
    public void PrintStats()
    {
        Console.WriteLine($"Name: {UnitType}, Health: {Health}, Damage: {Damage}");
    }


    // Allows an object of this class to be printed as a string.
    public override string ToString()
    {
        return $"Name: {UnitType}, Health: {Health}, Damage: {Damage}";
    }
}
