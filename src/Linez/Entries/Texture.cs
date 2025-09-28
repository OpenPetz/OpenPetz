using System;

namespace OpenPetz.Linez.Entries {
    public struct Texture {
        public int Index {get; set;} = 0;
		public string Path {get; set;} = "";
		public int Transparency {get; set;} = 0;
        
        public Texture(string _path, int _transparency, int _unk1 = 256, int _unk2 = 256)
        {
            Path = _path;
            Transparency = _transparency;
        }
    }
}