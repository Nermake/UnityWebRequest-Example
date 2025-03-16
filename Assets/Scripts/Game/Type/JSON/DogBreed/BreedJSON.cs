using System;
using System.Collections.Generic;

namespace Game.Type.JSON
{
    [Serializable]
    public class BreedJSON
    {
        public List<BreedData> data;
        public Meta meta;
        public Links links;
    }
}