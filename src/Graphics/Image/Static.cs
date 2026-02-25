using Godot;
using System;
using System.IO;
using System.Collections.Generic;

namespace OpenPetz.Graphics.Image
{
	public partial class Static : MeshInstance2D
	{
		public Vector2 Size = Vector2.Zero;
		
		private Texture2D Palette = null;
		private Texture2D Data = null;
		
		private ShaderMaterial ShaderMaterial;
		private Type Type;
		
		public Static(){}
		
		public void CreateFromStream(Stream _stream)
		{
			Bmp bmp = new Bmp();
			bmp.LoadFileStream(_stream, Bmp.LoadType.All);
			
			if (!bmp.Loaded)
				return;
			
			Size = new Vector2(bmp.Width, bmp.Height);
			
			Data = bmp.GetData();
			Palette = bmp.GetPalette();
	
			Mesh = MeshManager.FetchNormalMesh();
			
			if (bmp.BitCount == 8)
			{
				ShaderMaterial = ShaderManager.FetchShaderMaterial("bitmap/indexed");
				ShaderMaterial.SetShaderParameter(StringManager.S("palette"), Palette);
				
			} else {
				ShaderMaterial = ShaderManager.FetchShaderMaterial("bitmap/background");
			}
			
			ShaderMaterial.SetShaderParameter(StringManager.S("tex"), Data);
			ShaderMaterial.SetShaderParameter(StringManager.S("size"), Size);
			ShaderMaterial.SetShaderParameter(StringManager.S("pos"), GlobalPosition);
			
			Material = ShaderMaterial;
		}
		
		public void CreateFromTexture(Texture2D _data, Type _type, Vector2 _size, Texture2D _pal = null)
		{
			Size = _size;
			
			Data = _data;
			Palette = _pal;
	
			Mesh = MeshManager.FetchNormalMesh();
			
			if (_type == Type.Indexed)
			{
				ShaderMaterial = ShaderManager.FetchShaderMaterial("bitmap/indexed");
				ShaderMaterial.SetShaderParameter(StringManager.S("palette"), Palette);
				
			} else {
				ShaderMaterial = ShaderManager.FetchShaderMaterial("bitmap/background");
			}
			
			ShaderMaterial.SetShaderParameter(StringManager.S("tex"), Data);
			ShaderMaterial.SetShaderParameter(StringManager.S("size"), Size);
			ShaderMaterial.SetShaderParameter(StringManager.S("pos"), GlobalPosition);
			
			Material = ShaderMaterial;
		}
		
		public override void _Process(double delta)
		{
			ShaderMaterial.SetShaderParameter(StringManager.S("pos"), GlobalPosition);
		}
	}
}