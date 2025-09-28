using Godot;
using System;
using System.Collections.Generic;
using OpenPetz;

//To Do: re-think if this class should inherit from Node2D
public partial class Sprite3D : Node2D //3d inheriting 2d? oh well 
{
	public Vector3 Rotation3D = new Vector3(0.0f, 0.0f, 0.0f);
	public Vector3 GlobalRotation3D = new Vector3(0.0f, 0.0f, 0.0f);
	
	protected BallzModel.Frame currentFrame = null;
	
	public int KeyBallIndex {get; protected set;} = 6; // catz default
	
	//Geometry containers
	public List<Ball> BallzList {get; protected set;} = new List<Ball> (); //store ballz
	public List<Line> LinezList {get; protected set;} = new List<Line> ();
	
	protected TextureAtlas textureAtlas = null;
	protected Vector3 AbsScale;

	public Sprite3D(){}
}