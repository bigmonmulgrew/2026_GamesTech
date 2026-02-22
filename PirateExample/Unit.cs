class Unit
{
    private string unitType;
    private int health;
    private float damage;

    // Data that belongs to ONE unit
    public string UnitType { get { return unitType; } set { unitType = value; } }
    public float Health { get { return health; }  set { health = (int)value; } }
    public float Damage { get { return damage; } set { damage = value; } }
    public Unit(string unitType, int health, float damage)
    {
        this.unitType = unitType;
        this.health = health;
        this.damage = damage;
    }

    public void PrintStats() { Console.WriteLine($"Name: {UnitType}, Health: {Health}, Damage: {Damage}"); }

   
    public override string ToString() { return $"Name: {UnitType}, Health: {Health}, Damage: {Damage}"; }
}
