using Godot;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Bmp {

    public uint Width {get; private set;} = 0;
    public uint Height {get; private set;} = 0;
    
    private Image Palette { get; set; } = null;
    private Image Raster { get; set; } = null;
    
    public uint BitCount { get; private set; } = 0;
    public bool Loaded { get; private set; } = false;
    
    [Flags] public enum LoadType {
        Palette = 1,
        Raster = 2,
        All = 3,
    }
    
	public void LoadFile(string _path, LoadType _type) 
    {
		using var file = File.Open(_path, FileMode.Open);
		LoadFileStream(file, _type);
	}
    
    public void LoadFileStream(Stream _stream, LoadType _type) 
    {
        
        try {
            
			using var file = new BinaryReader(_stream, Encoding.UTF8);
            
            int junk = 0;
            //Ignore first 14 bytes
            for (junk = 0; junk < 7; junk++)
                file.ReadInt16();
            
            uint headerSize = file.ReadUInt32();
            
            if (headerSize != 40)
            {
                PrintError("HeaderSize is not 40");
                return;
            }
            
            //Read width and height;
            if (_type.HasFlag(LoadType.Raster))
            {
                Width = file.ReadUInt32();
                Height = file.ReadUInt32();
            
                //Between 1 and 512 (inclusive) only
                
                if (Width <= 0 || Height <= 0)
                {
                    PrintError("Width or Height is 0 or below.");
                    return;
                }
            
                if (Width > 1024 || Height > 1024)
                {
                    PrintError("Width or Height is above 1024.");
                    return;
                }
            } else {
                file.ReadInt64();
            }
            
            //Ignore these two bytes too
            file.ReadInt16();
            
            BitCount = file.ReadUInt16();
            
            if (BitCount != 8 && BitCount != 24)
            {
                PrintError("Invalid Bit Count (" + BitCount + ")");
                return;
            }
            
            uint compression = file.ReadUInt32();
            
            if (compression != 0)
            {
                PrintError("Compression is not supported.");
                return;
            }
                
            //Ignore 20 more bytes
            for (junk = 0; junk < 5; junk++)
                file.ReadInt32();
            
            //if 8 bit, then there must be the index palette, in BGR0 format 
            if (BitCount == 8){
                
                if (_type.HasFlag(LoadType.Palette))
                {
                    byte[] pal = file.ReadBytes(1024);
                    //generate the palette
                    Palette = Image.CreateFromData(256, 1, false, Image.Format.Rgba8, pal);
                } else {
                    //Bye bye those 1024 bytes 
                    file.ReadBytes(1024);
                }
            }
            
            //Now for the raster part
            
            if (_type.HasFlag(LoadType.Raster))
            {
            
                    uint bytesPerTexel = BitCount == 24 ? 3u : 1u;
                
                    byte[] raster = file.ReadBytes((int)(Width * Height * bytesPerTexel));
                
                    if (bytesPerTexel == 3)
                    {
                        Raster = Image.CreateFromData((int)Width, (int)Height, false, Image.Format.Rgb8, raster);
                    }
                    else 
                    {
                        Raster = Image.CreateFromData((int)Width, (int)Height, false, Image.Format.R8, raster);
                    }
            }
            //All good? then it means successfully loaded
            Loaded = true;
            
        } catch (Exception e) {
            
        }
    }
    
    public Texture2D GetPalette() {
        if (Loaded && Palette != null){
            
            var textureImg = ImageTexture.CreateFromImage(Palette);
            Texture2D texture = textureImg as Texture2D;
            
            return texture;
        }
        else
        {
            return null;
        }
    }
    
    public Texture2D GetData() {
        if (Loaded && Raster != null){
            
            var textureImg = ImageTexture.CreateFromImage(Raster);
            Texture2D texture = textureImg as Texture2D;
            
            return texture;
        }
        else
        {
            return null;
        }
    }
    
    private void PrintError(string message)
    {
        GD.Print("BMP loader error: "+ message);
    }
    
}
