using Godot;
using System;
using System.Collections.Generic;


public partial class Geometry : MeshInstance2D
{
	public Vector3 Rotation3D = new Vector3(0.0f, 0.0f, 0.0f);
	public TextureAtlas Atlas {get; protected set;} = null;
	
	protected ShaderMaterial ShaderMaterial;
	
	
	public Geometry()
	{
		
	}
	
	public virtual void SetTextureAtlas()
	{

	}
}