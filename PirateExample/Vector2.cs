
class Vector2
{
    int x = 0;
    int y = 0;
    public int X { get { return x; } set { x = value; } }
    public int Y { get { return y; } set { y = value; } }

    Vector2() { }   // Empty to allow for default initialization
    public Vector2(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    
    public override string ToString()
    {
        return $"({x}, {y})";
    }
}
