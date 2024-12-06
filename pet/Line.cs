using Godot;
using Godot.NativeInterop;
using System;
using System.Collections.Generic;
using System.Threading;

public partial class Line : Node2D
{
	private MeshInstance2D meshInstance;
	public ImmediateMesh immediateMesh;
	public ShaderMaterial material;

	List<Vector3> pnts;
	List<Vector2> uvs;
	Vector2 raws;


	public override void _Ready()
	{
		meshInstance = new MeshInstance2D();
		AddChild(meshInstance);

		immediateMesh = new ImmediateMesh();
		meshInstance.Mesh = immediateMesh;

		// need to copy material for each ball or else they overwrite eachother's parameters
		material = (ShaderMaterial)GD.Load<ShaderMaterial>("res://shaders/line_shader.tres").Duplicate(true);

        Texture2D texture = GD.Load<Texture2D>("res://pet/data/textures/hair6.bmp");

        Texture2D palette = GD.Load<Texture2D>("res://pet/data/textures/petzpalette.png");

		//TODO setup line parameters

	

		Vector2 start = new Vector2(0,0);
		Vector2 end = new Vector2(20,30);

        calcRectangle(start, end, 10, 25);

        material.SetShaderParameter("tex", texture);
        material.SetShaderParameter("palette", palette);

        material.SetShaderParameter("r_outline_color", new Vector3(0,0,0));
        material.SetShaderParameter("l_outline_color", new Vector3(0, 0, 0));

		material.SetShaderParameter("color_index", (float)100);

        material.SetShaderParameter("transparent_color_index", (float)0);
		material.SetShaderParameter("max_uvs", raws);

	    material.SetShaderParameter("center", this.GlobalPosition);
		material.SetShaderParameter("vec_to_upright", Vector2.FromAngle(start.AngleToPoint(end)));

    }


	public override void _Process(double delta)
	{
		immediateMesh.ClearSurfaces();
		immediateMesh.SurfaceBegin(Mesh.PrimitiveType.Triangles);

		drawVetices();

		immediateMesh.SurfaceEnd();

		immediateMesh.SurfaceSetMaterial(0, material);
		meshInstance.Material = material;
	}

	private void drawVetices()
	{
		int size = 500;

		immediateMesh.SurfaceSetUV(uvs[2]);
		immediateMesh.SurfaceAddVertex(pnts[2]);

		immediateMesh.SurfaceSetUV(uvs[1]);
		immediateMesh.SurfaceAddVertex(pnts[1]);

		immediateMesh.SurfaceSetUV(uvs[0]);
		immediateMesh.SurfaceAddVertex(pnts[0]);


		immediateMesh.SurfaceSetUV(uvs[3]);
		immediateMesh.SurfaceAddVertex(pnts[3]);

		immediateMesh.SurfaceSetUV(uvs[2]);
		immediateMesh.SurfaceAddVertex(pnts[2]);

		immediateMesh.SurfaceSetUV(uvs[0]);
		immediateMesh.SurfaceAddVertex(pnts[0]);

	}

	private void calcRectangle(Vector2 start, Vector2 end, int startWidth, int endWidth)
	{
		pnts = new List<Vector3>();
		float length = (end - start).Length() / 2;

		pnts.Add(new Vector3(length,     2*startWidth, 0));
        pnts.Add(new Vector3(-1*length,  2*endWidth, 0));
        pnts.Add(new Vector3(-1*length, -2*endWidth, 0));
        pnts.Add(new Vector3(length,    -2*startWidth, 0));

		uvs = new List<Vector2>();
		uvs.Add(new Vector2(length, 0));
		uvs.Add(new Vector2(0, 0));
        uvs.Add(new Vector2(0, endWidth * 4));
        uvs.Add(new Vector2(length, startWidth * 4));

		raws = new Vector2(startWidth *4, endWidth * 4);

    }
}
