// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


// Entities: unique "things" that are stored in an unique integer value
//              - can have componenets represented as a vector of componenet identifiers 
//              - total set of components is called an archtype
//              - ex: a "Player" enetity can have position and health, entity type: [Positionm Health]
//              - need a way to keep track of componenets for entity and easy way is with map storing vetor of components 
Object[] EntityComponenets;
Dictionary<string, object[]> entity_index;

// problem can be storing an array for each Entity which is costly
// THIS IS WHERE ARCHETYPE COMES INTO PLAY!
// archtype: stores all entities that have the same components, saves memory overhead

using Type = vector<ComponenetId>;

struct Archetype {
    Type type;
};
Dictionary<EntityId, Archetype&> entity_index;

// need someway so the componenets in a vector will be sorted in the same way
// Also to find an archtype for a list of components 

// Type is the list of componenets we are trying to find
Dictionary<Type, Archetype> archetype_index;

// https://ajmmertens.medium.com/building-an-ecs-1-where-are-my-entities-and-components-63d07c7da742
// In the link above there is a way to do faster Archetypes and Vectorization

// The Component Index

// To fix the O(n) look up for if an archetype has a specific component 

struct Archetype {
    Type type;
    unordered_set<ComponenetId> type_set;
}

// now to see if the component exists we can do
bool has_component(EntityId entity, ComponentId component) {
  Archetype& archetype = entity_index[entity];
  return archetype->type_set.count(component) != 0;
}

// THis is a problem though because it adds overweight to have the sets
// We will end up with an archetype for each unique combination of componenets
// EX: Position, Health is diffirent from Position, Velocity, and Health

// TO SOLVE THIS: we will have a set per archtype with component ids
// so each componenet would have an associated array containing the archtype ids

struct Archetype {
  ArchetypeId id; // unique integer identifier for an archetype
  Type type;
};
using ArchetypeSet = unordered_set<ArchetypeId>;
unordered_map<ComponentId, ArchetypeSet> component_index;

// new has_component function

bool has_component(EntityId entity, ComponentId component) {
  Archetype& archetype = entity_index[entity];
  ArchetypeSet& archetype_set = component_index[component];
  return archetype_set.count(archetype.id) != 0;
}

// now we can find all archetpyes for a component too

ArchetypeSet& position_archetypes = component_index[Position];
for (auto& archetype : position_archetypes) {
  // archetype has Position component!
}
