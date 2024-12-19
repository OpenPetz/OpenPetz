using Godot;
using Godot.NativeInterop;
using System;
using System.Collections.Generic;
using System.Threading;

public partial class Line : MeshInstance2D
{
	//private MeshInstance2D meshInstance;
	public ImmediateMesh immediateMesh;
	public ShaderMaterial material;

	List<Vector3> pnts;
	List<Vector2> uvs;
	Vector2 raws;

	public Texture2D texture;
	public Texture2D palette;

	public Ball start;
	public Ball end;

	public int color;
	public int transparent_color_index;

	public int r_outline_color;
	public int l_outline_color;



	public Line()
	{


	}

	public Line(Texture2D texture, Texture2D palette, Ball start, Ball end, int color, int transparent_color_index, int r_outline_color, int l_outline_color)
	{
		this.texture = texture != null ? texture : start.texture;
		this.palette = palette != null ? palette : start.palette;
		this.start = start;
		this.end = end;
		this.color = color != -1 ? color : start.color_index;
		this.transparent_color_index = transparent_color_index;
		this.r_outline_color = r_outline_color;
		this.l_outline_color = l_outline_color;
	}

	public override void _Ready()
	{
		//meshInstance = new MeshInstance2D();
		//AddChild(meshInstance);

		immediateMesh = new ImmediateMesh();
		this.Mesh = immediateMesh;

		// need to copy material for each line or else they overwrite eachother's parameters
		// same as ball, this is horribly ineffecient but we're kicking that can down the road for now
		material = (ShaderMaterial)GD.Load<ShaderMaterial>("res://shaders/line_shader.tres").Duplicate(true);

		//Texture2D texture = GD.Load<Texture2D>("res://pet/data/textures/hair6.bmp");
		//Texture2D palette = GD.Load<Texture2D>("res://pet/data/textures/petzpalette.png");

		//Vector2 start = new Vector2(0,0);
		//Vector2 end = new Vector2(20,30);

		//calcRectangle(start.GlobalPosition, end.GlobalPosition, 10, 25);

		Vector2 Position = start.Position + (end.Position - start.Position) / 2; 

		this.Position = Position;

		this.Rotation = end.Position.AngleToPoint(start.Position);

		



		material.SetShaderParameter("tex", texture);
		material.SetShaderParameter("palette", palette);

		material.SetShaderParameter("fuzz", 0);

		material.SetShaderParameter("r_outline_color", new Vector3(0,0,0));
		material.SetShaderParameter("l_outline_color", new Vector3(0,0,0));

		material.SetShaderParameter("color_index", (float)this.color);

		material.SetShaderParameter("transparent_color_index", (float)0);
		material.SetShaderParameter("max_uvs", new Vector2(start.radius*4, end.radius*4));

		material.SetShaderParameter("center", Position);
		material.SetShaderParameter("vec_to_upright", Vector2.FromAngle(this.Rotation));
		
		
		//immediateMesh.SurfaceSetMaterial(0, material);
		this.Material = material;
	}


	public override void _Process(double delta)
	{
		Vector2 Position = start.Position + (end.Position - start.Position) / 2; 

		this.Position = Position;

		this.Rotation = end.Position.AngleToPoint(start.Position);
		
		
		immediateMesh.ClearSurfaces();
		immediateMesh.SurfaceBegin(Mesh.PrimitiveType.Triangles);

		drawVertices();

		immediateMesh.SurfaceEnd();
	}

	private void drawVertices()
	{
		int startWidth = start.radius * 2;
		int endWidth = end.radius * 2;
		float length = (end.Position - start.Position).Length() / 2;

		Vector3 vtBottomLeft =	new Vector3(-1 * length, -1 * endWidth, 0);
		Vector3 vtTopLeft =		new Vector3(-1 * length, endWidth, 0);
		Vector3 vtTopRight =	new Vector3(length, startWidth, 0);
		Vector3 vtBottomRight = new Vector3(length, -1 * startWidth, 0);

		Vector2 uvBottomLeft = new Vector2(0, endWidth * 2);
		Vector2 uvTopLeft = new Vector2(0, 0);
		Vector2 uvTopRight = new Vector2(length, 0);
		Vector2 uvBottomRight = new Vector2(length, startWidth * 2);	

		immediateMesh.SurfaceSetUV(uvBottomLeft);
		immediateMesh.SurfaceAddVertex(vtBottomLeft);

		immediateMesh.SurfaceSetUV(uvTopLeft);
		immediateMesh.SurfaceAddVertex(vtTopLeft);

		immediateMesh.SurfaceSetUV(uvTopRight);
		immediateMesh.SurfaceAddVertex(vtTopRight);


		immediateMesh.SurfaceSetUV(uvBottomRight);
		immediateMesh.SurfaceAddVertex(vtBottomRight);

		immediateMesh.SurfaceSetUV(uvBottomLeft);
		immediateMesh.SurfaceAddVertex(vtBottomLeft);

		immediateMesh.SurfaceSetUV(uvTopRight);
		immediateMesh.SurfaceAddVertex(vtTopRight);

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
