using Butthole.Settings;
using Godot;
using System;

namespace Butthole.Settings
{
	partial class EnemyReset : Node2D
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
			FloppaCollection = Scene.GetChild<Node2D>(1);
			Floppa = ResourceLoader.Load<PackedScene>("res://src/scene/FloppaNPC.tscn");

			//instance the first spawn
			var FirstSpawn = Floppa.Instantiate();
			FloppaCollection.AddChild(FirstSpawn);

			e = GetNode<Enemy>("/root/Main/FloppaCollection/FloppaNPC");
			e.FixTransform();
		}

		public override void _PhysicsProcess(float delta)
		{
			//read if the Q key is pressed and that only 1 enemy is alive. all Floppas are stored under FloppaCollection
			if(Input.IsActionJustPressed("Reset Enemies") && FloppaCollection.GetChildCount() < 1)
			{
				RandY = Rand.Next(100, 525);
				var Ins = Floppa.Instantiate();
				FloppaCollection.AddChild(Ins);
				((Enemy)Ins).FixTransform();
				((Node2D)Ins).Position = new Vector2(512, 400);
			}
		}
	}
}
