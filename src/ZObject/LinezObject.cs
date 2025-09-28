using Godot;
using System;
using System.Collections.Generic;

using OpenPetz;

public partial class LinezObject : Node2D
{
	public OpenPetz.Linez.Database Linez = new OpenPetz.Linez.Database();
	
	public List<Fudger> Fudgers = new List<Fudger>();
	
	public Vector3 Rotation3D = new Vector3(-0.125f, 0.0f, 0.0f);
	
	public LinezObject()
	{
		
	}
}
