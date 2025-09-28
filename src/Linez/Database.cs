using Godot;
using System;
using System.Collections.Generic;

namespace OpenPetz.Linez {
    public class Database {
        public List<Entries.BallInfo> BallzInfo {get; private set;} = new List<Entries.BallInfo>();
		public int[] DefaultScales {get; private set;} = {110, 115};
		public List<Entries.Texture> TextureList {get; private set;} = new List<Entries.Texture>();
        
		public void Parse(string _lnz) // place holder: implement actual parser later
		{
			//temp
			TextureList.Add(new Entries.Texture("./art/textures/hair10.bmp", 1));
			TextureList.Add(new Entries.Texture("./art/textures/hair4.bmp", 1));
			TextureList.Add(new Entries.Texture("./art/textures/hair10.bmp", 1));
			
			BallzInfo.Add(new Entries.BallInfo(45,	244,	0,	1,	-1,	-3,	3,	1));
			BallzInfo.Add(new Entries.BallInfo(45,	244,	0,	1,	-1,	-3,	3,	1));
			BallzInfo.Add(new Entries.BallInfo(45,	244,	0,	2,	-1,	-5,	1,	1));
			BallzInfo.Add(new Entries.BallInfo(45,	244,	0,	2,	-1,	-12,	3,	1));
			BallzInfo.Add(new Entries.BallInfo(45,	244,	0,	2,	-2,	-16,	0,	1));
			BallzInfo.Add(new Entries.BallInfo(45,	244,	0,	2,	0,	-16,	0,	1));
			BallzInfo.Add(new Entries.BallInfo(45,	244,	0,	3,	-1,	3,	1,	1));
			BallzInfo.Add(new Entries.BallInfo(45,	244,	0,	0,	1,	-2,	0,	1));
			BallzInfo.Add(new Entries.BallInfo(45,	244,	0,	1,	-1,	-22,	0,	1));
			BallzInfo.Add(new Entries.BallInfo(35,	244,	0,	1,	-1,	15,	2,	0));
			BallzInfo.Add(new Entries.BallInfo(45,	244,	0,	1,	-1,	-22,	0,	1));
			BallzInfo.Add(new Entries.BallInfo(35,	244,	0,	1,	-1,	15,	2,	0));
			BallzInfo.Add(new Entries.BallInfo(45,	244,	0,	1,	-1,	-8,	1,	1));
			BallzInfo.Add(new Entries.BallInfo(45,	244,	0,	1,	-1,	-8,	1,	1));
			BallzInfo.Add(new Entries.BallInfo(201,	244,	0,	0,	1,	3,	-1,	-1));
			BallzInfo.Add(new Entries.BallInfo(201,	244,	0,	0,	1,	3,	-1,	-1));
			BallzInfo.Add(new Entries.BallInfo(35,	244,	0,	0,	-2,	7,	2,	0));
			BallzInfo.Add(new Entries.BallInfo(35,	244,	0,	0,	0,	7,	2,	0));
			BallzInfo.Add(new Entries.BallInfo(35,	244,	0,	0,	-1,	7,	2,	0));
			BallzInfo.Add(new Entries.BallInfo(35,	244,	0,	0,	0,	7,	2,	0));
			BallzInfo.Add(new Entries.BallInfo(35,	244,	0,	0,	-2,	7,	2,	0));
			BallzInfo.Add(new Entries.BallInfo(35,	244,	0,	0,	-1,	7,	2,	0));
			BallzInfo.Add(new Entries.BallInfo(35,	244,	0,	0,	0,	4,	2,	0));
			BallzInfo.Add(new Entries.BallInfo(35,	244,	0,	0,	0,	4,	2,	0));
			BallzInfo.Add(new Entries.BallInfo(45,	244,	0,	1,	-1,	-9,	0,	1));
			BallzInfo.Add(new Entries.BallInfo(45,	244,	0,	1,	-1,	-21,	3,	1));
			BallzInfo.Add(new Entries.BallInfo(45,	244,	0,	1,	-1,	-21,	3,	1));
			BallzInfo.Add(new Entries.BallInfo(244,	203,	0,	0,	3,	9,	-1,	-1));
			BallzInfo.Add(new Entries.BallInfo(244,	203,	0,	0,	3,	9,	-1,	-1));
			BallzInfo.Add(new Entries.BallInfo(45,	244,	0,	0,	-1,	19,	0,	1));
			BallzInfo.Add(new Entries.BallInfo(35,	244,	0,	1,	1,	-9,	2,	0));
			BallzInfo.Add(new Entries.BallInfo(35,	244,	0,	1,	1,	-9,	2,	0));
			BallzInfo.Add(new Entries.BallInfo(45,	244,	0,	1,	0,	-14,	3,	1));
			BallzInfo.Add(new Entries.BallInfo(45,	244,	0,	1,	-2,	-14,	3,	1));
			BallzInfo.Add(new Entries.BallInfo(35,	244,	0,	0,	0,	-2,	2,	0));
			BallzInfo.Add(new Entries.BallInfo(35,	244,	0,	0,	0,	-2,	2,	0));
			BallzInfo.Add(new Entries.BallInfo(45,	244,	0,	1,	-1,	8,	0,	1));
			BallzInfo.Add(new Entries.BallInfo(244,	244,	0,	0,	-1,	-5,	-1,	0));
			BallzInfo.Add(new Entries.BallInfo(45,	244,	0,	1,	-1,	-2,	1,	1));
			BallzInfo.Add(new Entries.BallInfo(45,	244,	0,	1,	-1,	-2,	1,	1));
			BallzInfo.Add(new Entries.BallInfo(45,	244,	0,	0,	-1,	-12,	0,	1));
			BallzInfo.Add(new Entries.BallInfo(35,	244,	0,	0,	0,	-5,	2,	0));
			BallzInfo.Add(new Entries.BallInfo(35,	244,	0,	0,	0,	-5,	2,	0));
			BallzInfo.Add(new Entries.BallInfo(45,	244,	0,	1,	-1,	-7,	4,	1));
			BallzInfo.Add(new Entries.BallInfo(35,	244,	0,	1,	-1,	-9,	2,	1));
			BallzInfo.Add(new Entries.BallInfo(35,	244,	0,	1,	-1,	-11,	2,	1));
			BallzInfo.Add(new Entries.BallInfo(35,	244,	0,	1,	-1,	-9,	2,	1));
			BallzInfo.Add(new Entries.BallInfo(35,	244,	0,	1,	-1,	-8,	2,	1));
			BallzInfo.Add(new Entries.BallInfo(35,	244,	0,	1,	-1,	-6,	2,	1));
			BallzInfo.Add(new Entries.BallInfo(35,	244,	0,	0,	-2,	5,	2,	0));
			BallzInfo.Add(new Entries.BallInfo(35,	244,	0,	0,	0,	5,	2,	0));
			BallzInfo.Add(new Entries.BallInfo(35,	244,	0,	0,	-1,	5,	2,	0));
			BallzInfo.Add(new Entries.BallInfo(35,	244,	0,	0,	0,	5,	2,	0));
			BallzInfo.Add(new Entries.BallInfo(35,	244,	0,	0,	-2,	5,	2,	0));
			BallzInfo.Add(new Entries.BallInfo(35,	244,	0,	0,	-1,	5,	2,	0));
			BallzInfo.Add(new Entries.BallInfo(75,	244,	0,	0,	-1,	5,	-1,	0));
			BallzInfo.Add(new Entries.BallInfo(75,	244,	0,	0,	-1,	5,	-1,	0));
			BallzInfo.Add(new Entries.BallInfo(25,	244,	0,	0,	0,	-15,	-1,	1));
			BallzInfo.Add(new Entries.BallInfo(25,	244,	0,	0,	0,	-15,	-1,	1));
			BallzInfo.Add(new Entries.BallInfo(25,	244,	0,	0,	0,	-15,	-1,	1));
			BallzInfo.Add(new Entries.BallInfo(25,	244,	0,	0,	0,	-15,	-1,	1));
			BallzInfo.Add(new Entries.BallInfo(25,	244,	0,	0,	0,	-15,	-1,	1));
			BallzInfo.Add(new Entries.BallInfo(25,	244,	0,	0,	0,	-15,	-1,	1));
			BallzInfo.Add(new Entries.BallInfo(45,	244,	0,	0,	-1,	-8,	1,	1));
			BallzInfo.Add(new Entries.BallInfo(45,	244,	0,	0,	-1,	-8,	1,	1));
			BallzInfo.Add(new Entries.BallInfo(7,	244,	0,	0,	0,	5,	-1,	-1));
			BallzInfo.Add(new Entries.BallInfo(7,	244,	0,	0,	0,	5,	-1,	-1));
		}
		
        public Database()
        {
			
        }
    }
}