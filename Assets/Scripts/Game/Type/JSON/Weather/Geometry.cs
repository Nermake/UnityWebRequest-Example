using System;
using System.Collections.Generic;

namespace Game.Type.JSON
{
    [Serializable]
    public class Geometry
    {
        public string type;
        public List<List<List<double>>> coordinates;
    }
}