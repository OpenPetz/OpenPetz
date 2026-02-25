using Godot;
using System;
using System.Collections.Generic;

namespace OpenPetz {
	public static class Renderer
	{
		public static List<Graphics.Layer> Layers { get; private set; } = new List<Graphics.Layer>();
		public static Graphics.LayersNode LayersNode { get; private set; } = new Graphics.LayersNode();
		
		public static readonly int Amount = 128;
		
		public static void Init()
		{
			/*var layer = new Graphics.Layer();
			Layers.Add(layer);
			LayersNode.AddChild(layer);*/
		}
	}
}