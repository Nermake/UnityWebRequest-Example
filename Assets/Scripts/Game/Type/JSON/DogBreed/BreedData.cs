using System;

namespace Game.Type.JSON
{
    [Serializable]
    public class BreedData
    {
        public string id;
        public string type;
        public BreedAttributes attributes;
        public BreedRelationships relationships;
    }
}