using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class PaintBallGroup: Geometry
{
	private Ball baseBall = null;
	private List <PaintBall> paintBallz = null;
	//Since Godot doesn't have 3D rotation for 2D objects, own 3D rotation "field" needs to be declared.
	public Vector3 rotation = new Vector3(0, 0, 0);
	
	private Mesh meshBuffer = null;
	private ShaderMaterial material = null;
	//private SubTextureCoordinations atlasCoords = new SubTextureCoordinations(0.0f, 0.0f, 1.0f, 1.0f); 

	public PaintBallGroup(TextureAtlas _atlas, Ball _base, List < PaintBall > _paintBallz) 
	{
		Atlas = _atlas;
		baseBall = _base;
		paintBallz = _paintBallz;

		Position = new Vector2(0.0f, 0.0f);
	}

	public override void _Ready() 
	{
		ShaderMaterial = ShaderManager.FetchShaderMaterial("paintball");
		
		Material = ShaderMaterial;
		
		var st = new SurfaceTool();
		
		var cols = new Godot.Collections.Array<float>();
		var sizes = new Godot.Collections.Array<float>();
		var fuzzs = new Godot.Collections.Array<float>();
		var coords = new Godot.Collections.Array<Vector3>();

		st.Begin(Mesh.PrimitiveType.Triangles);

		st.SetCustomFormat(0, SurfaceTool.CustomFormat.RFloat);
		
		//For the sake of keeping paintballz of one ball to one drawcall (for performance reasons), we need to generate a single surface from the list of paintballz and pass indice as vertex attribute.

		for (int i = 0; i < Math.Min(paintBallz.Count, 16); i++)
		{
			var paintBall = paintBallz[i];
			// "color" (it is infact, not color)
			//CUSTOM0.r channel: The index
			var info = new Color((float)i, 0.0f, 0.0f, 0.0f);
			
			st.SetCustom(0, info);
			//And finally, add the vertex position (relative)
			st.AddVertex(new Vector3(-1, -1, 0));
			
			//Repeat this 5 more times (a quad requires two triangles, requiring 6 vertices per quad)
			
			st.SetCustom(0, info);
			st.AddVertex(new Vector3(-1, 1, 0));
			
			st.SetCustom(0, info);
			st.AddVertex(new Vector3(1, 1, 0));
			
			st.SetCustom(0, info);
			st.AddVertex(new Vector3(-1, -1, 0));
			
			st.SetCustom(0, info);
			st.AddVertex(new Vector3(1, -1, 0));
			
			st.SetCustom(0, info);
			st.AddVertex(new Vector3(1, 1, 0));
			
			cols.Add(paintBall.Info.ColorIndex);
			sizes.Add((float)paintBall.Info.Diameter / 100f);
			fuzzs.Add(paintBall.Info.Fuzz);
			coords.Add(paintBall.Info.Direction);

		}
		
		ShaderMaterial.SetShaderParameter("p_size", sizes);
		ShaderMaterial.SetShaderParameter("p_fuzz", fuzzs);
		ShaderMaterial.SetShaderParameter("p_coordination", coords);
		ShaderMaterial.SetShaderParameter("p_color", cols);

		// And finally, generate the mesh.		
		Mesh = st.Commit();
		
		//Uniform variables are universal for all of the paintballz of a ball.
		//the base ball's fuzz, diameter and position (center) are rrquired for creating the clipping mask in the shader
		ShaderMaterial.SetShaderParameter(StringManager.S("fuzz"), baseBall.Info.Fuzz);
		ShaderMaterial.SetShaderParameter(StringManager.S("diameter"), baseBall.Info.Diameter);

		ShaderMaterial.SetShaderParameter(StringManager.S("tex"), TextureManager.FetchEmptyTexture());
		ShaderMaterial.SetShaderParameter(StringManager.S("palette"), Atlas.Palette);

		ShaderMaterial.SetShaderParameter(StringManager.S("center"), this.GlobalPosition);
		SetTextureAtlas();
	}

	public override void _Process( double delta )
	{
		ShaderMaterial.SetShaderParameter(StringManager.S("center"), this.GlobalPosition);
		ShaderMaterial.SetShaderParameter(StringManager.S("rotation"), baseBall.Rotation3D);
	}
	
	// CUSTOM METHODS
	
	public override void SetTextureAtlas()
	{	
		if (Atlas.TextureData != null)
		{
			//atlasCoords = atlas.GetSubTextureCoords(0, color_index);
			
			ShaderMaterial.SetShaderParameter(StringManager.S("tex"), Atlas.TextureData);
			
			var positions = new Godot.Collections.Array<Vector2>();
			var sizes = new Godot.Collections.Array<Vector2>();

			foreach (var paintBall in paintBallz)
			{
				var atlasCoords = Atlas.GetSubTextureCoords(paintBall.Info.TextureIndex, (int)paintBall.Info.ColorIndex);
				var position = atlasCoords.Position;
				var size = atlasCoords.Size;
				positions.Add(position);
				sizes.Add(size);
			}
			
   			ShaderMaterial.SetShaderParameter(StringManager.S("p_atlas_position"), positions);
      		ShaderMaterial.SetShaderParameter(StringManager.S("p_atlas_size"), sizes);
			
		} else {
			ShaderMaterial.SetShaderParameter(StringManager.S("tex"), TextureManager.FetchEmptyTexture());
		}
	}
}