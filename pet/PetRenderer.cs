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

    //To Do: Move these two to a singleton

    private Texture2D texture;


    private Texture2D palette;

    //Methods

    public override void _Ready()
    {

        //Prepare the Textures
        texture = GD.Load<Texture2D>("res://pet/data/textures/hair11.bmp");

        palette = GD.Load<Texture2D>("res://pet/data/textures/petzpalette.png");

        //Create dummy ballz for now.
        for (int i = 1; i <= 5; i++)
        {

            Ball dummyBall = new Ball(texture, palette, 15, i * 10, 2, 1, 39);

            Vector2 dummyCoord = new Vector2((i - 3) * 20, (i - 3) * 20);

            coordArray.Add(new Vector3(dummyCoord.X, dummyCoord.Y, 0));
            dummyBall.Position = dummyCoord;

            dummyBall.ZIndex = 0;

            //add them to the lists
            this.ballz.Add(dummyBall);
            AddChild(dummyBall);
        }

    }

    public override void _Process(double delta)
    {
        rotation.Y += (float)0.05;
        UpdateGeometries();
    }

    // CUSTOM Methods

    //To Do: implement the rotation vector math for x and z rotation
    private void UpdateGeometries()
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

}
