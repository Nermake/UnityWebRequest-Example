using System;

namespace Game.Type.JSON
{
    [Serializable]
    public class BreedAttributes
    {
        public string name;
        public string description;
        public LifeSpan life;
        public Weight maleWeight;
        public Weight femaleWeight;
        public bool hypoallergenic;
    }
}