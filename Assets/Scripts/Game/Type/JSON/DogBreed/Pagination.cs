using System;

namespace Game.Type.JSON
{
    [Serializable]
    public class Pagination
    {
        public int current;
        public int next;
        public int last;
        public int records;
    }
}