using System;
using System.Collections.Generic;

namespace Game.Type.JSON
{
    [Serializable]
    public class WeatherJSON
    {
        public List<string> @context;
        public string type;
        public Geometry geometry;
        public Properties properties;
    }
}