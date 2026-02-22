
public class Ship
{
    string name;
    // Size is arbitrary, but let's say it's the number of crew members it can hold
    int size;       
    Vector2 location;

    public string Name { get { return name; } }
    public int Size { get { return size; } }
    public Vector2 Location { get { return location; } }

    public Ship(string name, int size, Vector2 location)
    {
        this.name = name;
        this.size = size;
        this.location = location;
    }

    public void Sail(Vector2 movement)
    {
        location.X += movement.X;
        location.Y += movement.Y;
    }
    public void SailTo(Vector2 destination)
    { 
        location = destination;
    }
}
