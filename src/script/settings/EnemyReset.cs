using Butthole.Settings;
using Godot;
using System;

namespace Butthole.Settings
{
	class EnemyReset : Node2D
	{
		Random Rand = new Random();
		PackedScene Floppa;
		Node2D Scene;
		Node2D FloppaCollection;
		Enemy e;
		int RandY;

		public override void _Ready()
		{
			//set object values
			Scene = GetParent<Node2D>();
			FloppaCollection = Scene.GetChild<Node2D>(0);
			Floppa = GD.Load<PackedScene>("res://src/Scene/floppaNPC.tscn");

			//instance the first spawn
			var firstSpawn = Floppa.Instance();
			FloppaCollection.AddChild(firstSpawn);

			e = GetNode<Enemy>("/root/Main/FloppaCollection/floppaNPC");
			e.FixTransform();
		}

		public override void _PhysicsProcess(float delta)
		{
			//read if the Q key is pressed and that only 1 enemy is alive. all Floppas are stored under FloppaCollection
			if(Input.IsActionJustPressed("Reset Enemies") && FloppaCollection.GetChildCount() < 1)
			{
				RandY = Rand.Next(100, 525);
				var Ins = Floppa.Instance();
				FloppaCollection.AddChild(Ins);
				((Enemy)Ins).FixTransform();
				((Node2D)Ins).Position = new Vector2(400, 400);
			}

			//print enemy count
			if(Input.IsActionJustPressed("DEBUG"))
			{
				GD.Print(FloppaCollection.GetChildCount());
			}
		}
	}
}
