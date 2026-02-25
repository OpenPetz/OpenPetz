using Godot;
using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;

using OpenPetz;

//To Do: manage the background rendering a better way

public partial class PlayScene : Node2D
{
	private Texture2D Background = null;
	
	public PlayScene()
	{
		var zip = new OpenPetz.Parser.Zip("./Resource/Area/Adoption Center.zip");
		Stream test = zip.ReadFileStream("adoptioncenter.bmp");
		
		var bg = new OpenPetz.Graphics.Image.Static();
		bg.CreateFromStream(test);
		
		bg.ZIndex = -999;
		
		AddChild(bg);
		
		var cheez = new FilmObject("Cheese", "food_c1");
		
		cheez.Position = new Vector2(200, 5);
		
		AddChild(cheez);
		
		var gm = new FilmObject("Diamond", "shel_j1");
		
		gm.Position = new Vector2(400, 50);
		
		AddChild(gm);
		
		var frm = new FilmObject("Brown Picture Frame", "pict_d1");
		
		frm.Position = new Vector2(600, 50);
		
		AddChild(frm);
		
		/*var pet = new Pet();
		var pet2 = new Pet();
		
		pet.Position = new Vector2(333f,512f);
		pet2.Position = new Vector2(666f,512f);
		
		AddChild(pet);
		AddChild(pet2);*/
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

}
