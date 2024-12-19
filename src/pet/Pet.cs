using Godot;
using System;
using System.Collections.Generic;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

public partial class Pet : Node2D
{
	private PetRenderer petRenderer;


	public override void _Ready()
	{
		World.pets.Add(this);

		PetRenderer petRenderer = new PetRenderer();
		
		AddChild(petRenderer);
	}

	public override void _ExitTree()
	{
		World.pets.Remove(this);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public override void _Draw()
	{

	}

}
