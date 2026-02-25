using Godot;
using System;
using System.Collections.Generic;

//For the lack of better words... layer
namespace OpenPetz.Graphics {
	//in order to render the layers.
	public partial class LayersNode : Node2D 
	{
		
	}
	
	public partial class Layer : MultiMeshInstance2D
	{
		private ShaderMaterial ShaderMaterial;
		private TextureAtlas textureAtlas;
		
		private int visibleAmount = 1;
		
		public int VisibleAmount {
			get { return visibleAmount; } 
			set 
			{ 
				int val = value > Renderer.Amount ? Renderer.Amount : value;
				val = val < 0 ? 0 : val;
				visibleAmount = val; 
				Multimesh.VisibleInstanceCount = val;
			} 
		}
		
		public Layer(int _index, SoA.Buffer _soa, TextureAtlas _atlas)
		{
			textureAtlas = _atlas;
			
			var colors = new ArraySegment<int>(_soa.ColorAndOutlineColor, Renderer.Amount * _index, Renderer.Amount);
			var coords = new ArraySegment<Vector2>(_soa.Position, Renderer.Amount * _index, Renderer.Amount);
			var diameters = new ArraySegment<float>(_soa.Diameter, Renderer.Amount * _index, Renderer.Amount);
			
			var atlasPosition = new ArraySegment<Vector2>(_soa.AtlasPosition, Renderer.Amount * _index, Renderer.Amount);
			var atlasSizes = new ArraySegment<Vector2>(_soa.AtlasSize, Renderer.Amount * _index, Renderer.Amount);
			
			ShaderMaterial = ShaderManager.FetchShaderMaterial("instance3d");
			
			Material = this.ShaderMaterial;
			
			ShaderMaterial.SetShaderParameter(StringManager.S("global_center"), GlobalPosition);
			ShaderMaterial.SetShaderParameter(StringManager.S("palette"), textureAtlas.Palette);
			
			if (textureAtlas.TextureData != null)
			{
				ShaderMaterial.SetShaderParameter(StringManager.S("tex"), textureAtlas.TextureData);
				
				ShaderMaterial.SetShaderParameter(StringManager.S("atlas_position"), atlasPosition.ToArray());
				ShaderMaterial.SetShaderParameter(StringManager.S("atlas_size"), atlasSizes.ToArray());
			}
			else
			{
				ShaderMaterial.SetShaderParameter(StringManager.S("tex"), TextureManager.FetchEmptyTexture());
			}

			ShaderMaterial.SetShaderParameter(StringManager.S("center"), coords.ToArray());
			ShaderMaterial.SetShaderParameter(StringManager.S("color"), colors.ToArray());
			ShaderMaterial.SetShaderParameter(StringManager.S("diameter"), diameters.ToArray());
			
			// Create the multimesh.
			Multimesh = new MultiMesh();
			
			Multimesh.Mesh = MeshManager.FetchDefaultMesh();
			// Set the format first.
			//Multimesh.TransformFormat = MultiMesh.TransformFormatEnum.Transform2D;
			// Then resize (otherwise, changing the format is not allowed)
			Multimesh.InstanceCount = Renderer.Amount;
			// Maybe not all of them should be visible at first.
			Multimesh.VisibleInstanceCount = VisibleAmount;

			for (int i = 0; i < Renderer.Amount; i++)
			{
				Multimesh.SetInstanceTransform2D(i, new Transform2D(0f, new Vector2(0f, 0f)));
			}
		}
		
		public override void _Ready()
		{
			
		}
		
		/*public override void _Ready()
		{
			
			//ShaderMaterial = ShaderManager.FetchShaderMaterial("silly");
			
			//Material = this.ShaderMaterial;
			
			//ShaderMaterial.SetShaderParameter(StringManager.S("diameter"), 32);
			//ShaderMaterial.SetShaderParameter(StringManager.S("center"), cols);
			// Create the multimesh.
			Mesh = MeshManager.FetchNormalMesh();
		}*/
		
		public void Update(int _index, SoA.Buffer _soa)
		{
			var colors = new ArraySegment<int>(_soa.ColorAndOutlineColor, Renderer.Amount * _index, Renderer.Amount);
			var diameters = new ArraySegment<float>(_soa.Diameter, Renderer.Amount * _index, Renderer.Amount);
			var coords = new ArraySegment<Vector2>(_soa.Position, Renderer.Amount * _index, Renderer.Amount);
			
			var atlasPosition = new ArraySegment<Vector2>(_soa.AtlasPosition, Renderer.Amount * _index, Renderer.Amount);
			var atlasSizes = new ArraySegment<Vector2>(_soa.AtlasSize, Renderer.Amount * _index, Renderer.Amount);
			
			ShaderMaterial.SetShaderParameter(StringManager.S("global_center"), GlobalPosition);
			
			ShaderMaterial.SetShaderParameter(StringManager.S("center"), coords.ToArray());
			ShaderMaterial.SetShaderParameter(StringManager.S("diameter"), diameters.ToArray());
			ShaderMaterial.SetShaderParameter(StringManager.S("color"), colors.ToArray());
			
			ShaderMaterial.SetShaderParameter(StringManager.S("atlas_position"), atlasPosition.ToArray());
			ShaderMaterial.SetShaderParameter(StringManager.S("atlas_size"), atlasSizes.ToArray());
		}
		
		/*public override void _Process(double delta)
		{
			
			//GD.Print(Position);
		}*/
	}
}