using System.Collections.Generic;

namespace Version_Viewer
{
    class GameList
    {
        public class RootObject
        {
            public string code { get; set; }
            public string name { get; set; }
            public string about { get; set; }
            public bool visible { get; set; }
        }

        public List<RootObject> Games { get; set; }
    }
}
