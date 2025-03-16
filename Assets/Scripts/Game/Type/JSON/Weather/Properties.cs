using System;
using System.Collections.Generic;

namespace Game.Type.JSON
{
    [Serializable]
    public class Properties
    {
        public string units;
        public string forecastGenerator;
        public string generatedAt;
        public string updateTime;
        public string validTimes;
        public Elevation elevation;
        public List<Period> periods;
    }
}