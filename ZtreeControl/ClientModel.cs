using PaceCommon;

namespace ZtreeControl
{
    class ClientModel : IClientModel
    {
        public static string Server = "127.0.0.1";
        public static string Name = "localhost";
        public static int X = -1;
        public static int Y = -1;
        public static int W = -1;
        public static int H = -1;

        public static string BuildCommandLineOptionsZLeaf(string server = "", string name = "", int sizex = -1, int sizey = -1, int posx = -1, int posy = -1)
        {
            var ret = "";

            if (server != "") { ret += " /server " + server; }
            if (name != "") { ret += " /name " + name; }
            if (sizex > -1 && sizey > -1) { ret += " /size " + sizex + "x" + sizey; }
            if (posx > -1 && posy > -1) { ret += " /position " + posx + "," + posy; }
            TraceOps.Out("ZLeaf Arguments: " + ret);
            return ret;
        }

        public static string BuildCommandLineOptionsZTree(string dir = "")
        {
            var ret = "";

            if (dir != "") { ret += " /dir " + dir; }

            return ret;
        }
    }
}
