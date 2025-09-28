using Godot;
using System;
using System.Collections.Generic;

public struct BallParams {
	public int Fuzz {get; set;} = 0;
	public int Diameter {get; set;} = 1;
	public int ColorIndex {get; set;} = 0;
	public int OutlineType {get; set;} = 0;
	public int OutlineColor {get; set;} = 0;
	public int TextureIndex {get; set;} = -1;
	public BallParams(){}
}

public partial class Ball : Geometry
{

	private List<PaintBallGroup> paintBallGroups = null;

	//public TextureAtlas atlas {get; private set;} = null;
	
	public BallParams Info {get; protected set;} = new BallParams();
	
	public Vector3 rotation = new Vector3(0.0f, 0.0f, 0.0f);

	private SubTextureCoordinations atlasCoords = new SubTextureCoordinations(0.0f, 0.0f, 1.0f, 1.0f); 

	public Ball()
	{

	}

	//public Ball(TextureAtlas _atlas, Texture2D texture, Texture2D palette, int diameter, int color_index, int fuzz, int outline_width, int outline_color)
	public Ball(TextureAtlas _atlas, BallParams _params)
	{
		Info = _params;

		Atlas = _atlas;
		
		Mesh = MeshManager.FetchDefaultMesh();

		ShaderMaterial = ShaderManager.FetchShaderMaterial("ball");
		
		Material = this.ShaderMaterial;
	}

	public override void _Ready()
	{
		//Set Material uniform parameters

		ShaderMaterial.SetShaderParameter(StringManager.S("fuzz"), Info.Fuzz);
		ShaderMaterial.SetShaderParameter(StringManager.S("diameter"), Info.Diameter);
		ShaderMaterial.SetShaderParameter(StringManager.S("outline_width"), Info.OutlineType);

		ShaderMaterial.SetShaderParameter(StringManager.S("outline_color"), (float)Info.OutlineColor);
		
		ShaderMaterial.SetShaderParameter(StringManager.S("tex"), TextureManager.FetchEmptyTexture());
		ShaderMaterial.SetShaderParameter(StringManager.S("color_index"), Info.ColorIndex);
		ShaderMaterial.SetShaderParameter(StringManager.S("palette"), Atlas.Palette);

		ShaderMaterial.SetShaderParameter(StringManager.S("center"), this.GlobalPosition);

		
		SetTextureAtlas();
	}


	public override void _Process(double dt)
	{
		ShaderMaterial.SetShaderParameter(StringManager.S("center"), this.GlobalPosition);
	}
	
	// CUSTOM METHODS
	
	public void AddPaintBalls(List<PaintBall> _paintBalls)
	{
		if (paintBallGroups == null)
			paintBallGroups = new List<PaintBallGroup>();
		
		var pbg = new PaintBallGroup(Atlas, this, _paintBalls);
		paintBallGroups.Add(pbg);
		AddChild(pbg);
	}
	
	public override void SetTextureAtlas()
	{
		//atlas = _atlas;
		
		if (Atlas.TextureData != null)
		{
			atlasCoords = Atlas.GetSubTextureCoords(Info.TextureIndex, Info.ColorIndex);

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
