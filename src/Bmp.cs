using Godot;
using System;


//BLINDLY CODED, NEEDS TO BE TESTED FIRST

public class BMP {
	
	private bool _loaded = false;
	private uint _bitCount = 0; //8 for 256 color index, 24 for 24 bit (RGB)
	private uint _width = 0;
	private uint _height = 0;
	
	private Image _palette = new Image();
	private Image _raster = new Image();
	
	public uint BitCount => _bitCount;
	public bool Loaded => _loaded;
	
	public void LoadFile(string path){
		
		try {
			using var file = FileAccess.Open(path, FileAccess.ModeFlags.Read);
			
			int junk = 0;
			//Ignore first 14 bytes
			for (junk = 0; junk < 7; junk++)
				file.Get16();
			
			uint headerSize = file.Get32();
			
			if (headerSize != 40)
				return;
			
			//Read width and height;
			
			_width = file.Get32();
			_height = file.Get32();
			
			//Between 1 and 256 (inclusive) only
			
			if (_width <= 0 || _height <= 0)
				return;
			
			if (_width > 256 || _height > 256)
				return;
			
			//Ignore these two bytes too
			file.Get16();
			
			_bitCount = file.Get16();
			
			if (_bitCount != 8 && _bitCount != 24)
				return;
			
			uint compression = file.Get32();
			
			if (compression != 0)
				return;
				
			//Ignore 20 more bytes
			for (junk = 0; junk < 5; junk++)
				file.Get32();
			
			//define buffer containers
			byte[] palette = null;
			byte[] raster = null;
			
			//if 8 bit, then there must be the index palette, in BGR0 format 
			if (_bitCount == 8){
				//_palette = new byte[256 * 4];
				palette = file.GetBuffer(1024);
				//generate the palette
				_palette = Image.CreateFromData((int)_width, (int)_height, false, Image.Format.Rgba8, palette);
			}
			
			//Now for the raster part
			
			int bytesPerTexel = _bitCount == 24 ? 3 : 1;
			
			raster = file.GetBuffer(_width * _height * bytesPerTexel);
			
			if (bytesPerTexel == 3){
				_raster = Image.CreateFromData((int)_width, (int)_height, false, Image.Format.Rgb8, raster);
			} else {
				_raster = Image.CreateFromData((int)_width, (int)_height, false, Image.Format.R8, raster);
			}
			
			//All good? then it means successfully loaded
			_loaded = true;
			
		}catch (Exception e){
			
		}
	}
	
	public Texture2D GetPalette() {
		if (_loaded && _bitCount == 8){
			
			var textureImg = ImageTexture.CreateFromImage(_raster);
			Texture2D texture = textureImg as Texture2D;
			
			return texture;
		}
		else
		{
			return new Texture2D();
		}
	}
	
	public Texture2D GetData() {
		if (_loaded){
			
			var textureImg = ImageTexture.CreateFromImage(_raster);
			Texture2D texture = textureImg as Texture2D;
			
			return texture;
		}
		else
		{
			return new Texture2D();
		}
	}
	
}
