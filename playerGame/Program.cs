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
// need to implement type eraser for Column since we don't know the type to start out with.
// So, the program will not consider the type at runtime
struct Column{
    // holds a pointer to the type and its value
    void *elements;
    // the chosen size of each element
    size_t element_size;
    // how many elements are in the column
    size_t count;
}


struct ArchetypeEdge
{
    Archetype& add;
    Archetype& remove;
}
struct Archetype 
{
    // what components are present, no data value
    Type type;
    // this gives us a O(1). However, not helpful because extra space
    // unordered_set<ComponenetId> type_set;
    
    // We always have less ComponentId than Archetype so having a way to map is important
    ArchetypeId id; 
    // holds the data value of each of the entities that exist for the Archetype, continious data to make
    // the CPU cache predictable
    std::vector<Column> components;

    unordered_map<ComponentId, ArchetypeEdge> edges;
};

using ArchetypeSet = unordered_set<ArchetypeId>;

struct ArchetypeRecord 
{
    size_t column;
}

// keeping track of what column containing the given component
using ArchetypeMap = unordered_map<ArchetypeId, ArchetypeRecord>;

// gives us all the archtype ids that the component is in
unordered_map<ComponentId, ArchetypeMap> component_index;

struct Record
{
    Archetype& archetype;
    // tells us what row to look at index wise from the column of components within the archetype
    size_t row;
}
// keeps track of the associated archtype with the entityId
unordered_map<EntityId, Record> entity_index;

// find archetype for a list of components given
unordered_map<Type, Archetype> archetype_index;

bool has_component(EntityId entity, ComponenetId component)
{
    Archetype& archetype = entity_index[entity];
    ArchetypeSet& archetype_set = component_index[component];
    return archetype_set.count(archetype.id) != 0;
}

// void* is to point to any data type, handle raw memory addresses 
// like a normal get function this would be O(1), constant time
void* get_componenet(EntityId entity, ComponentId component) {
    // Remember, Record contains an archetype and row 
    Record& record = entity_index[entity];
    // get the archetype of the given entity
    Archetype& archetype = record.archetype;

    // get all archetypes that are associated with the componenet alone
    ArchetypeMap archetypes = component_index[component];
    // the archetype does not exist of the earlier inputed entity
    if (archetypes.count(archetype.id) == 0)
    {
        return nullptr;
    }

    ArchetypeRecord& a_record = archetypes[archetype.id];
    return archetype.columns[a_record.column][record.row];
}

void add_component(EntityId entity, ComponentId component)
{
    // find what row 
    Record& record = entity_index[entity];
    // we want to know the current archetype
    Archetype& archetype = Record.archetype;
    // we say the "next_archetype" is the key to the component
    Archetype& next_archetype = archetype.add_archetype[component];
    // a function that puts the new information into the row we want
    move_entity(archetype, record.row, next_archetpye);
}

// problem when we want to add componenets to an entity! We have to get rid of the current archetype

// archetype we are trying to find, and id is the new componentId we are adding to so we can create that new "column" in our arch map
Archetype& add_to_archetype(Archetype& src, ComponentId id);




// way to look up all the archetypes that have a certain entity
void which_arch(ComponentId componentId)
{
    ArchetypeSet& allArchContains = component_index[componentId];

    for (Archetype& curr : allArchContains)
    {
        Console.WriteLine(curr);
    }
}

// use arrays, it is predictable for the CPU to prefetch data from the RAM
// OOP random memory access causing CPU cache to slow greatly down cause it can't prefetch well from
// RAM, RAM just contains junk

// Vectorized code means done at once, but the data needs to be continouis 

// The ABC problem
// if we have different entities with components of similar how can we make it continouis to make it predictable?
// keeping one array for each component does not work
/*
    0: [  B  ]
    1: [  B C]
    2: [A B C]
    3: [A B  ]
    4: [A    ]
    5: [A   C]
    6: [    C]
*/

// Creating array per type would solve this problem

// so that is why you need to keep the unique combination of components inside your archtpye


