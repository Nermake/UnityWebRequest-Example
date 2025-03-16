using System;

namespace Game.Type.JSON
{
    [Serializable]
    public class Links
    {
        public string self;
        public string current;
        public string next;
        public string last;
    }
}