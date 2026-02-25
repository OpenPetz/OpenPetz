using Godot;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenPetz.Parser
{
	
	public class Film 
	{
		public class Animation 
		{
			public string Name;
			
			public Vector2I MaxSizePadded = Vector2I.Zero;
			
			public List<Frame> Frames = new List<Frame>();
		}
		
		public class Frame
		{
			public Vector2I Position = Vector2I.Zero;
			public Vector2I Position2 = Vector2I.Zero;
			
			public uint Format; //??
			
			public uint Flags; //??
			public uint Offset; //pointer to the data on .FLM I believe?
		}
		
		public uint FrameCount { get; private set; } = 0;
		
		//TO DO: Question if these should be rounded to a PoT number
		public Vector2I MaxSize = Vector2I.Zero;
		public Vector2I MaxSizePadded = Vector2I.Zero;
		
		public int Padding; //??
		
		public List<Animation> Animations = new List<Animation>();
		
		private byte[] FilmData;
		
		public Film ()
		{
			
		}
		
		public void ReadFileStream (Stream _flh, Stream _flm, long _flmLength)
		{
			using var flhFile = new BinaryReader(_flh, Encoding.UTF8);
			using var flmFile = new BinaryReader(_flm, Encoding.UTF8);
			
			flhFile.ReadUInt32();
			
			FrameCount = flhFile.ReadUInt16();
			
			MaxSize.X = flhFile.ReadUInt16();
			MaxSize.Y = flhFile.ReadUInt16();
			
			MaxSizePadded.X = ((MaxSize.X >> 2) << 2) + 4;
			MaxSizePadded.Y = ((MaxSize.Y >> 2) << 2) + 4;
			
			flhFile.ReadUInt16();
			
			//Animations.Add(new Animation());
			int animIndex = -1;
			
			for (uint i = 0; i < FrameCount; i++)
			{
				var frame = new Frame();
				
				frame.Position.X = flhFile.ReadInt16();
				frame.Position.Y = flhFile.ReadInt16();
				
				frame.Position2.X = flhFile.ReadInt16();
				frame.Position2.Y = flhFile.ReadInt16();
				
				flhFile.ReadUInt32();
				
				frame.Format = flhFile.ReadUInt32();
				
				byte[] str = flhFile.ReadBytes(16);
				
				if (str[0] != 0x0)
				{
					Animations.Add(new Animation());
					animIndex++;
					Animations[animIndex].Name = System.Text.Encoding.GetEncoding("UTF-8").GetString(str);
					var size = frame.Position2 - frame.Position;
					Animations[animIndex].MaxSizePadded = new Vector2I(((size.X >> 2) << 2) + 4, size.Y);
				} 
				else if (animIndex == -1) //FLH is misconfigured
				{
					GD.Print("Film Parser Error: The first frame must have a name.");
					break;
				}
				
				frame.Flags = flhFile.ReadUInt32();
				frame.Offset = flhFile.ReadUInt32();
				
				Animations[animIndex].Frames.Add(frame);
			}
			FilmData = flmFile.ReadBytes((int)_flmLength);
		}
		
		//Horrifying, just Horrifying
		public OpenPetz.Graphics.Image.Static GetImage ()
		{
			var anim = Animations[1];
			
			int maxx = MaxSizePadded.X * MaxSizePadded.Y;
			
			byte[] data = new byte[maxx * anim.Frames.Count];
			
			var stripSize = new Vector2I(MaxSizePadded.X, MaxSizePadded.Y * anim.Frames.Count);
			
			Array.Fill(data, (byte)253);
			
			for (int j = 0; j < anim.Frames.Count; j++)
			{
				var frame = anim.Frames[j];
				var size = frame.Position2 - frame.Position;
				var widthPadded = (size.X % 4 != 0) ? ((size.X >> 2) << 2) + 4 : size.X;
				var heightPadded = ((size.Y >> 2) << 2) + 4;
				
				for (int i = 0; i < size.Y; i++)
				{
					Array.Copy(FilmData, (int)(frame.Offset + (i) * widthPadded), data, (int)(j * maxx + i * MaxSizePadded.X), size.X);
				}
			}
			
			var img = Image.CreateFromData((int)MaxSizePadded.X, (int)MaxSizePadded.Y * anim.Frames.Count, false, Image.Format.R8, data);
			
			var textureImg = ImageTexture.CreateFromImage(img);
            Texture2D texture = textureImg as Texture2D;
            
			var stat = new OpenPetz.Graphics.Image.Static();
			stat.CreateFromTexture(texture, OpenPetz.Graphics.Image.Type.Indexed, stripSize, PaletteManager.FetchPalette("petz"));		
			
			return stat;
		}
	}
}