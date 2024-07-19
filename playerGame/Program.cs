using System;

namespace playerGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}

// This games goal will be to rank up levels depending on how many times you press a button
// Entity: Player
// Component:
//          - Player: rank, name, strength, points
//          - Enemy: name, strength

// Need to create code:

// create enum where named "ComponentId" that contains constant types like name and level and such
// archtype keeps track of a combination of components
// need one for player and one for enemy
//      - create a type which is a vector or list or something of the ComponenetId
            // using Type = std::vector<ComponentId>;

            // struct Archetype {
            //     Type type;
            // };

// create structs defining the constant components created above



