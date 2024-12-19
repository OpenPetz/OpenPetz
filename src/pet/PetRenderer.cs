using Godot;
using System;
using System.Collections.Generic;

//To Do: re-think if this class should inherit from Node2D
public partial class PetRenderer : Node2D
{
	public Vector3 rotation = new Vector3(0, 0, 0);

	//Coordination container (temporary)
	private List<Vector3> coordArray = new List<Vector3> ();

	//Geometry containers
	private List<Ball> ballz = new List<Ball> (); //store ballz
	private List<Line> linez = new List<Line> (); //store ballz

	//To Do: Move these two to a singleton

	private Texture2D texture;


	private Texture2D palette;

	//Methods

	public override void _Ready()
	{

		//Prepare the Textures
		texture = GD.Load<Texture2D>("res://art/textures/hair11.bmp");

		palette = GD.Load<Texture2D>("res://Resource/palettes/oddballz.png");

		//Create dummy ballz for now.
		for (int i = 1; i <= 5; i++)
		{

			int color = i % 2 == 0 ? 35 : 55;
			
			Ball dummyBall = new Ball(texture, palette, 12, color, 4, 1, 39);

			Vector2 dummyCoord = new Vector2((i - 3) * 20, 0);

			coordArray.Add(new Vector3(dummyCoord.X, dummyCoord.Y, 0));
			dummyBall.Position = dummyCoord;

			dummyBall.ZIndex = 0;

			//add them to the lists
			this.ballz.Add(dummyBall);
			AddChild(dummyBall);
		}

		for (int l = 0; l < 4; l++)
		{

			Line dummyLine = new Line(null, null, this.ballz[l], this.ballz[l + 1], -1, 1, 39, 39);

			//add them to the lists
			this.linez.Add(dummyLine);
			AddChild(dummyLine);
		}
	}

	public override void _Process(double delta)
	{
		rotation.Y += (float)0.05;
		UpdateGeometries();
	}

	// CUSTOM Methods

	//NOTE: Order of updating matters!
	private void UpdateGeometries(){
		UpdateMainBallz();
		UpdateLinez();
	}
	
	//To Do: implement the rotation vector math for x and z rotation
	private void UpdateMainBallz()
	{

		float rYSin = (float)Math.Sin(rotation.Y);
		float rYCos = (float)Math.Cos(rotation.Y);
		for (int index = 0; index < this.ballz.Count; index++)
		{

			Vector3 coord = this.coordArray[index];

			float xf = coord.X;

			float zf = coord.Z;
			float zz = zf;

			zf = (zz * rYCos) - (xf * rYSin);
			xf = (xf * rYCos) + (zz * rYSin);

			float z = (float)Math.Round(zf);
			float x = (float)Math.Round(xf);

			Vector2 v = new Vector2(x, ballz[index].Position.Y);

			ballz[index].Position = v;
			//Since Godot renders Nodes with highest Z on top of others unlike original petz l, we set negative of it
			this.ballz[index].ZIndex = (int)-z;
		}
	}
	
	private void UpdateLinez(){
		foreach (Line line in this.linez){
			line.ZIndex = Math.Min(line.start.ZIndex, line.end.ZIndex) - 1;
		}
	}

}
