using Godot;
using System;
using System.Collections.Generic;

//simple eye ball, for now

public partial class EyeBall : Ball
{
	//public TextureAtlas atlas {get; private set;} = null;

	private SubTextureCoordinations atlasCoords = new SubTextureCoordinations(0.0f, 0.0f, 1.0f, 1.0f); 

	public EyeBall()
	{

	}

	//public Ball(TextureAtlas _atlas, Texture2D texture, Texture2D palette, int diameter, int color_index, int fuzz, int outline_width, int outline_color)
	public EyeBall(TextureAtlas _atlas, BallParams _params)
	{
		Info = _params;

		Atlas = _atlas;
		
		Mesh = MeshManager.FetchDefaultMesh();

		ShaderMaterial = ShaderManager.FetchShaderMaterial("eyeball");
		
		Material = this.ShaderMaterial;
	}

	public override void _Ready()
	{
		//Set Material uniform parameters
		ShaderMaterial.SetShaderParameter(StringManager.S("palette"), Atlas.Palette);
		
		ShaderMaterial.SetShaderParameter(StringManager.S("diameter"), Info.Diameter);
		
		ShaderMaterial.SetShaderParameter(StringManager.S("rotation"), rotation);

		ShaderMaterial.SetShaderParameter(StringManager.S("center"), this.GlobalPosition);
	}


	public override void _Process(double dt)
	{
		var point = GetViewport().GetMousePosition();
		
		rotation.Y = (float)Math.Atan2((double)(GlobalPosition.X - point.X), (double)Info.Diameter*8d);
		rotation.X = (float)Math.Atan2((double)(GlobalPosition.Y - point.Y), (double)Info.Diameter*8d);
		
		ShaderMaterial.SetShaderParameter(StringManager.S("rotation"), rotation);
		ShaderMaterial.SetShaderParameter(StringManager.S("center"), this.GlobalPosition);
	}
}
