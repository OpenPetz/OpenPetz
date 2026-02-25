using Godot;
using System;
using System.Collections.Generic;

namespace OpenPetz.Graphics.SoA 
{
	public class Buffer
	{
		public enum Type {
			Ball = 0,
			Line = 1
		}
		
		public int Size {get; private set;} = 0;
		
		public int[] ColorAndOutlineColor {get; private set;} //upper 23 bits: outline color(s) index, lower 8 bits: color index
		public Vector2[] Position {get; private set;}
		public Vector2[] AtlasPosition {get; private set;}
		public Vector2[] AtlasSize {get; private set;}

		public float[] Diameter {get; private set;}
		
		public Buffer(int _size)
		{
			ColorAndOutlineColor = new int[_size];
			Position = new Vector2[_size];
			AtlasPosition = new Vector2[_size];
			AtlasSize = new Vector2[_size];
			Diameter = new float[_size];
			
			Size = _size;
		}
	}
}