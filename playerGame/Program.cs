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
enum ComponentId
{
    rank,
    name,
    strength,
    points
}

using Type = std::vector<ComponentId>;

struct Archetype 
{
    Type type;
    // this gives us a O(1). However, not helpful because extra space
    // unordered_set<ComponenetId> type_set;
    
    // We always have less ComponentId than Archetype so having map is important
    ArchetypeId id; 
};

using ArchetypeSet = unordered_set<ArchetypeId>;
// gives us all the archtype ids that the component is in
unordered_map<ComponentId, ArchetypeSet> component_index;

// keeps track of the associated archtype with the entityId
unordered_map<EntityId, Archetype&> entity_index;

// find archetype for a list of components given
unordered_map<Type, Archetype> archetype_index;

bool has_component(EntityId entity, ComponenetId component)
{
    Archetype& archetype = entity_index[entity];
    ArchetypeSet& archetype_set = component_index[component];
    return archetype_set.count(archetype.id) != 0;
}



