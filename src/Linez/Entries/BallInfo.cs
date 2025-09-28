using System;

namespace OpenPetz.Linez.Entries {
    public struct BallInfo {
        public int Color {get; set;}
		public int OutlineColor {get; set;}
		public int SpeckleColor {get; set;}
		public int Fuzz {get; set;}
		public int OutlineType {get; set;}
		public int SizeDifference {get; set;}
		public int Group {get; set;}
        public int Texture {get; set;} = -1;
		public int BallNumber {get; set;} = 0;
        
        public BallInfo(int _1, int _2, int _3, int _4, int _5, int _6, int _7 = 0, int _8 = -1, int _9 = 0)
        {
			Color = _1;
			OutlineColor = _2;
			SpeckleColor = _3;
			Fuzz = _4;
			OutlineType = _5;
			SizeDifference = _6;
			Group = _7;
			Texture = _8;
			BallNumber = _9;
        }
    }
}