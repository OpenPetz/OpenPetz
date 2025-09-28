using Godot;
using System;
using System.Collections.Generic;
using System.Drawing;

using OpenPetz;

//To Do: manage the background rendering a better way

public partial class PlayScene : Node2D
{
	private Texture2D Background = null;
	
	public override void _Ready()
	{
		//string bgPath = "./Resource/Area/Arabia/arabia.png";
		string bgPath = "./Resource/Wallpaper/Orange Catz.BMP";
		
		Image img = Image.LoadFromFile(bgPath);
		Background = ImageTexture.CreateFromImage(img);
		
		var bg = new MeshInstance2D();
		bg.Mesh = MeshManager.FetchNormalMesh();
		
		var mat = ShaderManager.FetchShaderMaterial("background");
		mat.SetShaderParameter(StringManager.S("tex"), Background);
		mat.SetShaderParameter(StringManager.S("size"), new Vector2(1024f, 768f));
		
		bg.Material = mat;
		
		bg.ZIndex = -999;
		
		AddChild(bg);
		
		var pet = new Pet();
		
		pet.Position = new Vector2(512f,512f);
		
		AddChild(pet);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

}
