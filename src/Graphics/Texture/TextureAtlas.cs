using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

using OpenPetz;

public partial class TextureAtlas : Node2D { //TO DO: Replace with Node
    
    public static Vector2I MaximumSize => new Vector2I(1024, 1024);
    
    private SubViewport subViewport = null;
    private List<OpenPetz.Linez.Entries.Texture> textureList = null;
	
    private List<SubTextureContainer> subTexList = new List<SubTextureContainer>();
	//private List<SubTextureContainer> subTexListTransparent = new List<SubTextureContainer>();
    
    private Guid guid;
	
    public Texture2D TextureData { get; private set; } = null;
	public Texture2D Palette { get; private set; } = null;
    
    public Vector2I Size { get; private set; } = new Vector2I(512, 512);
    
    public TextureAtlas(Texture2D _palette, Guid _guid, List<OpenPetz.Linez.Entries.Texture> _textureList)
    {
        //First step is checking to see if it is already cached.
        
		guid = _guid;
		Palette = _palette;
		
		textureList = _textureList;
		
		for (var i = 0; i < textureList.Count; i++){
			var tex = textureList[i];
			tex.Index = i;
			textureList[i] = tex;
		}
		
		//
		
        string fileName = "./cache/texture_atlas/raster/"+_guid.ToString()+".png";
        
        /*if (FileAccess.FileExists(fileName))
        {
            // Load the cache instead
            
            Image img = Image.LoadFromFile(fileName);
            img.Convert(Image.Format.R8);
            TextureData = ImageTexture.CreateFromImage(img);
            
        } else */{
            //We are bound to dynamically generating it now using SubViewport.
            subViewport = new SubViewport();
            subViewport.Size = Size;
            subViewport.RenderTargetUpdateMode = SubViewport.UpdateMode.Once;
            AddChild(subViewport);
            
            PackTextures();
        }
    }
    
    public override void _Ready()
    {
        if (subViewport != null)
		{
            RenderingServer.FramePostDraw += SaveGeneratedAtlas;
		}
    }
    
    // CUSTOM METHODS
    
    public SubTextureCoordinations GetSubTextureCoords(int _index, int _color)
    {
		//Non texturable color? Non texturable color!
		if (_color < 10 || _color > 149 || _index == -1){
			Vector2I position = new Vector2I(0, 0);
			position.X = _color % 16;
			position.Y = (_color - position.X) / 16;			
			
			return new SubTextureCoordinations((float)position.X / Size.X, (float)position.Y / Size.Y, 1f / Size.X, 1f / Size.Y, 0);
		}
		
        var subTex = subTexList[_index];

        return new SubTextureCoordinations(subTex.Position.X / Size.X, subTex.Position.Y / Size.Y, subTex.Size.X / Size.X, subTex.Size.Y / Size.Y, subTex.Transparency);
    }
    
    // HEAVILY WIP
    
    private void PackTextures()
    {
		Vector2 posPtr = new Vector2(0f, 0f);
		
		{
			var dummyMesh = new MeshInstance2D();
			
			var material = ShaderManager.FetchShaderMaterial("texture_atlas/solid_colors");
			
			dummyMesh.Mesh = MeshManager.FetchNormalMesh();
			dummyMesh.Material = material;
			subViewport.AddChild(dummyMesh);
			
			posPtr.X += 16f;
		}
		
		List<Texture2D> texList = new List<Texture2D>();
		
		foreach (var texParam in textureList){
			var texture = TextureManager.FetchTexture(texParam.Path);
			texList.Add(texture);
		}
		
		textureList.Sort(delegate(OpenPetz.Linez.Entries.Texture a, OpenPetz.Linez.Entries.Texture b) 
		{
			var sizeA = texList[a.Index].GetSize();
			var sizeB = texList[b.Index].GetSize();
			
			var ai = Math.Max(sizeA.X, sizeA.Y);
			var bi = Math.Max(sizeB.X, sizeB.Y);
			
			return (ai > bi) ? 1 : -1;
		});
		
		foreach (var texParam in textureList){
			var texture = TextureManager.FetchTexture(texParam.Path);
			var texSize = texture.GetSize();
			
			if (posPtr.X + texSize.X > Size.X) //jump down
			{
				posPtr.X = 0;
				posPtr.Y += texSize.Y;
			}
			
			var dummyMesh = new MeshInstance2D();
			
			var material = ShaderManager.FetchShaderMaterial("texture_atlas/texture");
			
			dummyMesh.Mesh = MeshManager.FetchNormalMesh();
			dummyMesh.Material = material;
			subViewport.AddChild(dummyMesh);
			
			material.SetShaderParameter("tex", texture);
			material.SetShaderParameter("position", posPtr);
			material.SetShaderParameter("size", texSize);
			
			var subTex = new SubTextureContainer();
			subTex.Position = posPtr;
			subTex.Size = texSize;
			//
			subTex.Transparency = texParam.Transparency;
			subTex.Index = texParam.Index;
			
			subTexList.Add(subTex);
			
			posPtr.X += texSize.X;
		}
		
		subTexList.Sort(delegate(SubTextureContainer a, SubTextureContainer b) 
		{
			return (a.Index > b.Index) ? 1 : -1;
		});
    }
    
    private void SaveGeneratedAtlas()
    {
        Texture2D tex = subViewport.GetTexture();
        
        Image img = tex.GetImage();
        img.Convert(Image.Format.R8);
        
        img.SavePng("./cache/texture_atlas/raster/"+guid.ToString()+".png");
        //Then unsubscribe
        RenderingServer.FramePostDraw -= SaveGeneratedAtlas;
        //Get rid of the subViewport
		RemoveChild(subViewport); // Godot complains too much
        subViewport = null; 
        
        Image img2 = Image.LoadFromFile("./cache/texture_atlas/raster/"+guid.ToString()+".png");
        img2.Convert(Image.Format.R8);
		TextureData = ImageTexture.CreateFromImage(img2);
    }
}

//

public struct TextureParams {
	public int Index {get; set;} = 0;
	public string Path {get; set;} = "";
    public int Transparency {get; set;} = 0;
	public TextureParams(){}
}

internal struct SubTextureContainer {
	public SubTextureContainer()
	{
		;
	}

	public int Index { get; set; } = 0;
	public Vector2 Position { get; set; } = new Vector2(0.0f, 0.0f);
    public Vector2 Size { get; set; } = new Vector2(1.0f, 1.0f);
    public int Transparency { get; set; } = 0;
}

public struct SubTextureCoordinations {
    
    public Vector2 Position { get; private set; } = new Vector2(0.0f, 0.0f);
    public Vector2 Size { get; private set; } = new Vector2(1.0f, 1.0f);
	public int Transparency = 0;
    
    public SubTextureCoordinations (float _x, float _y, float _width, float _height, int _transparency = 0)
    {
        Position = new Vector2(_x, _y);
        Size = new Vector2(_width, _height);
		Transparency = _transparency;
    }
}

internal class TextureListCache {
    
}
