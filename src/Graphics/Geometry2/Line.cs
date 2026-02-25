using Godot;
using System;
using System.Collections.Generic;

public struct LineParams {
	public Ball Start {get; set;} = null;
	public Ball End {get; set;} = null;
	public int StartThickness {get; set;} = 100;
	public int EndThickness {get; set;} = 100;
	public int LeftColor {get; set;} = -1;
	public int RightColor {get; set;} = 128;
	public LineParams(){}
}
public partial class Line : Geometry
{	
	public LineParams Info {get; private set;} = new LineParams();
	
	public bool Renderable {get; private set;} = false;
	
	//Do I have to...? GODOT?
	private Godot.Collections.Array<Vector2> coords = new Godot.Collections.Array<Vector2>();
	private Godot.Collections.Array<float> diameters = new Godot.Collections.Array<float>();
	
	private SubTextureCoordinations atlasCoords = new SubTextureCoordinations(0.0f, 0.0f, 1.0f, 1.0f); 
	
	public Line(Sprite3D _sprite, TextureAtlas _atlas, LineParams _params)
	{
		Visible = false;
		if (_params.Start == null || _params.End == null)
			return;
		
		Info = _params;

		Atlas = _atlas;
		
		Mesh = MeshManager.FetchDefaultLineMesh();

		ShaderMaterial = ShaderManager.FetchShaderMaterial("line");
		
		Material = this.ShaderMaterial;
		
		_sprite.LinezList.Add(this);
		_sprite.AddChild(this);
		
	}	
	
	public override void _Ready()
	{
		Renderable = true;
		
		coords.Add(Info.Start.Position);
		coords.Add(Info.End.Position);
		
		diameters.Add(Info.Start.Info.Diameter);
		diameters.Add(Info.End.Info.Diameter);
		
		ShaderMaterial.SetShaderParameter("ball_coords", coords);
		ShaderMaterial.SetShaderParameter("ball_diameters", diameters);
		ShaderMaterial.SetShaderParameter("angle_to", Info.Start.Position.AngleToPoint(Info.End.Position));
		
		ShaderMaterial.SetShaderParameter("color_index", Info.Start.Info.ColorIndex);
		
		ShaderMaterial.SetShaderParameter("left_color", (float)Info.LeftColor);
		ShaderMaterial.SetShaderParameter("right_color", (float)Info.RightColor);
		
		ShaderMaterial.SetShaderParameter(StringManager.S("palette"), Atlas.Palette);
		
		SetTextureAtlas();
		
		Visible = true;
	}
	
	public override void _Process(double dt)
	{
		if (!Renderable)
			return;
		
		coords[0] = Info.Start.Position;
		coords[1] = Info.End.Position;
		
		ShaderMaterial.SetShaderParameter("ball_coords", coords);
		ShaderMaterial.SetShaderParameter("angle_to", Info.Start.Position.AngleToPoint(Info.End.Position));
	}
	
	public override void SetTextureAtlas()
	{
		//atlas = _atlas;
		if (!Renderable)
			return;
		
		if (Atlas.TextureData != null)
		{
			atlasCoords = Atlas.GetSubTextureCoords(Info.Start.Info.TextureIndex, Info.Start.Info.ColorIndex);

   			ShaderMaterial.SetShaderParameter(StringManager.S("atlas_position"), atlasCoords.Position);
      		ShaderMaterial.SetShaderParameter(StringManager.S("atlas_size"), atlasCoords.Size);
			ShaderMaterial.SetShaderParameter(StringManager.S("tex"), Atlas.TextureData);
			
			ShaderMaterial.SetShaderParameter(StringManager.S("transparency"), atlasCoords.Transparency);
		} else {
     		ShaderMaterial.SetShaderParameter(StringManager.S("atlas_position"), new Vector2(0.0f, 0.0f));
      		ShaderMaterial.SetShaderParameter(StringManager.S("atlas_size"), new Vector2(1.0f, 1.0f));
			ShaderMaterial.SetShaderParameter(StringManager.S("tex"), TextureManager.FetchEmptyTexture());
			
			ShaderMaterial.SetShaderParameter(StringManager.S("transparency"), 0);
		}
	}
}