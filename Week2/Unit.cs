using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Unit
{
    // Data that belongs to ONE unit
    public string Name;
    public int Health;
    public int Damage;

    // Method that prints the units data.
    public void PrintStats()
    {
        Console.WriteLine($"Name: {Name}, Health: {Health}, Damage: {Damage}");
    }


    // Allows an object of this class to be printed as a string.
    public override string ToString()
    {
        return $"Name: {Name}, Health: {Health}, Damage: {Damage}";
    }
}
