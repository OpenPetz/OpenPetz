using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace OpenPetz 
{
	public partial class InstancedSprite3D : Node2D
	{
		public LinezObject Parent {get; protected set;}
		
		//
		
		public List<Graphics.Layer> Layers { get; protected set; } = new List<Graphics.Layer>();
		public Graphics.LayersNode LayersNode { get; protected set; } = new Graphics.LayersNode();
		
		protected Graphics.SoA.Buffer buffer = new Graphics.SoA.Buffer(Renderer.Amount);
		
		//
		protected List<Graphics.Geometry.Ball> Ballz = new List<Graphics.Geometry.Ball>();
		//
		protected List<Graphics.Geometry.Ball> RenderingQuene = new List<Graphics.Geometry.Ball>();
		//
		
		protected TextureAtlas textureAtlas;
		
		//
		
		public Vector3 Rotation3D = new Vector3(0.0f, 0.0f, 0.0f);
		public Vector3 GlobalRotation3D = new Vector3(0.0f, 0.0f, 0.0f);
		
		protected BallzModel.Frame currentFrame = null;
		
		public int KeyBallIndex {get; protected set;} = 6; // catz default
		
		protected Vector3 AbsScale;
		
		public InstancedSprite3D(LinezObject _parent)
		{
			Parent = _parent;
			
			AddChild(LayersNode);
			
			Texture2D palette = PaletteManager.FetchPalette("petz");
			textureAtlas = new TextureAtlas(palette, Guid.Empty, Parent.LinezDatabase.TextureList);

			AddChild(textureAtlas);
			
			//Visible = false;
		}
		
		public void SetFrame(BallzModel.Frame frame){
			currentFrame = frame;
		}
		
		public override void _Ready()
		{
			RenderingServer.FramePostDraw += SetupSprite;
		}
		
		public override void _Process(double delta)
		{
			//GD.Print($"am i processed? {RenderingQuene.Count}");
			Rotation3D = Parent.Rotation3D;
			if (currentFrame == null)
				return;
			
			var frame = currentFrame;
			
			for (int index = 0; index < Ballz.Count; index++)
			{
				var orien = frame.BallOrientation(index);
				
				var rotMat = Rotator.Rotate3D(orien.Position * AbsScale, Rotation3D);

				//Vector2 v = new Vector2(rotMat.X, rotMat.Y)/* */;

				Ballz[index].Position = rotMat;
				//Since Godot renders Nodes with highest Z on top of others unlike original petz l, we set negative of it
				//ballSoA.ZIndex[index] = (int)-rotMat.Z;
			}
			
			RenderingQuene.Sort(delegate(Graphics.Geometry.Ball a, Graphics.Geometry.Ball b) 
			{
				if (a.Position.Z == b.Position.Z)
					return 0;
				return (a.Position.Z > b.Position.Z) ? 1 : -1;
			});
			
			for (int j = 0; j < RenderingQuene.Count; j++)
			{
				RenderingQuene[j].PassData(buffer, j);
			}
			
			for (int i = 0; i < Layers.Count; i++)
			{
				Layers[i].Update(i, buffer);
			}
		}
		
		private void SetupSprite()
		{	
			//Visible = true;
			var scales = Parent.LinezDatabase.DefaultScales;
	
			float scalesUnit = (float)(150 + scales[0]) / 512f;
			AbsScale = new Vector3(scalesUnit,scalesUnit,scalesUnit);
			
			for (int i = 0; i < 67; i++)
			{
				var ballInfo = Parent.LinezDatabase.BallzInfo[i];
				//int diameter = Parent.catBhd.GetDefaultBallSize(i);
				var diameter = Parent.catBhd.GetDefaultBallSize(i) + ballInfo.SizeDifference;
			
				diameter = (int)((float)diameter * (float)(150 + scales[1]) / 512f);
				
				var ball = new Graphics.Geometry.Ball(textureAtlas, new Graphics.Geometry.Ball.Params {
					Diameter = diameter,// / 2,
					ColorIndex = ballInfo.Color,
					Fuzz = ballInfo.Fuzz,
					OutlineType = ballInfo.OutlineType,
					OutlineColor = ballInfo.OutlineColor,
					TextureIndex = ballInfo.Texture
				});
				
				Ballz.Add(ball);
				RenderingQuene.Add(ball);
				
				ball.PassDataOnce(buffer, i);
				
				ball.Position = Vector3.Zero;
			}
			
			for (int j = 0; j < buffer.Size / Renderer.Amount; j++)
			{		
				var layer = new Graphics.Layer(j, buffer, textureAtlas);
				layer.VisibleAmount = Ballz.Count;
				//layer.Position = new Vector2(0, 16 * j + 8);
			
				Layers.Add(layer);
				LayersNode.AddChild(layer);
			}
		
			RenderingServer.FramePostDraw -= SetupSprite;
		}
	}
}