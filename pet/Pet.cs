using Godot;
using System;
using System.Collections.Generic;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

public partial class Pet : Node2D
{
    Texture2D texture = GD.Load<Texture2D>("res://pet/data/textures/hair6.bmp");
    Texture2D palette = GD.Load<Texture2D>("res://pet/data/textures/petzpalette.png");

    ShaderMaterial material = (ShaderMaterial)GD.Load<ShaderMaterial>("res://shaders/ball_shader.tres");


    public override void _Ready()
	{
		World.pets.Add(this);

		var bitmap = new BMP();
		bitmap.LoadFile("res://pet/data/textures/hair6.bmp");

		Texture2D tex = bitmap.GetData();




		//AddChild(ball);





		Ball ball2 = new Ball(texture, palette, 25, 105, 3, 1, 5);
		AddChild(ball2);

		ball2.Position = new Vector2(200, 150);



		//Line line = new Line();
		//line.start = ball2;
		//line.end = ball;
		//AddChild(line);
	}

	public override void _ExitTree()
	{
		World.pets.Remove(this);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		QueueRedraw();
	}

    public override void _Draw()
    {
		
  //      Ball ball = new Ball(texture, palette, 10, 105, 3, 1, 5);
  //      ball.Position = new Vector2(0, 0);
		//ImmediateMesh mesh1 = new ImmediateMesh();
  //      this.DrawMesh(ball.setupMesh(mesh1, material), texture);

		//Material mat = mesh1.SurfaceGetMaterial(0);


  //      Ball ball2 = new Ball(texture, palette, 25, 105, 3, 1, 5);
  //      ball2.Position = new Vector2(200, 150);
	 //   this.DrawMesh(ball2.setupMesh(new ImmediateMesh(), material), texture);
    }

}
