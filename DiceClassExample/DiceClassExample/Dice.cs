 class Dice
{
    private int faces;

    public int NumberOfSides { get; } // No set, this is read only

    public Dice()
    {
        faces = 6;
    }

    public Dice(int faces)
    {
        // If class variable and method variable names match,
        // use "this" to mean the class. Otherwise most local
        // scope takes priority.
        this.faces = faces; 
    }

    public int Roll()
    {
        Random RAND = new Random();
        return RAND.Next(1, faces + 1);
    }

    /// <summary>
    /// Returns the sum of many rolls
    /// </summary>
    /// <param name="times"></param>
    /// <returns></returns>
    public int RollMultipleTimes(int times)
    {
        if (times <= 0) return 0;   // Without this it would go on forever with times being negative
        Random RAND = new Random();
        // Why is capitalising RAND a bad idea?
        return RAND.Next(1, faces + 1) + RollMultipleTimes(times - 1);
    }
}

