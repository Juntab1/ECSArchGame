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