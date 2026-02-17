 class Dice
{
    private int faces;

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
}

