using Godot;
using System;
using System.Collections.Generic;

namespace OpenPetz.Graphics.Geometry 
{
	public class Ball 
	{
		public struct Params 
		{
			public int ColorAndOutlineColor {get; private set;} = 0;
			
			public int Fuzz {get; set;} = 0;
			public int Diameter {get; set;} = 1;
			public int OutlineType {get; set;} = 0;
			public int TextureIndex {get; set;} = -1;
			
			public int ColorIndex {
				get { return ColorAndOutlineColor & (int)0x000000ff; } //256
				set { ColorAndOutlineColor = (ColorAndOutlineColor & (int)0x7fffff00) + (value & (int)0xff); }
			}
			public int OutlineColor {
				get { return (ColorAndOutlineColor & (int)0x0000ff00) >> 8; }
				set { ColorAndOutlineColor = (ColorAndOutlineColor & (int)0x7fff00ff) + ((value & 0xff) << 8); }
			}
			
			public Params(){}
		}
		
		private List<PaintBallGroup> paintBallGroups = null;

		//public TextureAtlas atlas {get; private set;} = null;
		
		public Params Info {get; protected set;} = new Params();
		
		public Vector3 Rotation = new Vector3(0.0f, 0.0f, 0.0f);
		public Vector3 Position = new Vector3(0.0f, 0.0f, 0.0f);

		public TextureAtlas textureAtlas {get; private set;} = null;
		private SubTextureCoordinations atlasCoords = new SubTextureCoordinations(0.0f, 0.0f, 1.0f, 1.0f); 
		
		public Ball(TextureAtlas _atlas, Params _params)
		{
			Info = _params;
			
			textureAtlas = _atlas;
			
			if (textureAtlas.TextureData != null)
			{
				atlasCoords = textureAtlas.GetSubTextureCoords(Info.TextureIndex, Info.ColorIndex);
			}
		}
		
		public void PassData(SoA.Buffer _buffer, int _index)
		{
			_buffer.ColorAndOutlineColor[_index] = Info.ColorAndOutlineColor;
			_buffer.Diameter[_index] = Info.Diameter;
			_buffer.Position[_index] = new Vector2(Position.X, Position.Y);
			_buffer.AtlasPosition[_index] = atlasCoords.Position;
			_buffer.AtlasSize[_index] = atlasCoords.Size;
		}
		
		public void PassDataOnce(SoA.Buffer _buffer, int _index)
		{
			PassData(_buffer, _index);
		}
	}
}