using Butthole.Settings;
using Godot;
using System;

namespace Butthole.Settings
{
	class EnemyReset : Node2D
	{
		Random rand = new Random();
		PackedScene floppa;
		Node2D scene;
		Node2D floppaCollection;
		Enemy e;
		int randY;

		public override void _Ready()
		{
			//set object values
			scene = GetParent<Node2D>();
			floppaCollection = scene.GetChild<Node2D>(0);
			floppa = GD.Load<PackedScene>("res://src/scene/FloppaNPC.tscn");

			//instance the first spawn
			var firstSpawn = floppa.Instance();
			floppaCollection.AddChild(firstSpawn);

			e = GetNode<Enemy>("/root/Main/FloppaCollection/FloppaNPC");
			e.FixTransform();
		}

		public override void _PhysicsProcess(float delta)
		{
			//read if the Q key is pressed and that only 1 enemy is alive. all floppas are stored under FloppaCollection
			if(Input.IsActionJustPressed("Reset Enemies") && floppaCollection.GetChildCount() < 1)
			{
				randY = rand.Next(100, 525);
				var ins = floppa.Instance();
				floppaCollection.AddChild(ins);
				((Enemy)ins).FixTransform();
				((Node2D)ins).Position = new Vector2(400, 400);
			}

			//print enemy count
			if(Input.IsActionJustPressed("DEBUG"))
			{
				GD.Print(floppaCollection.GetChildCount());
			}
		}
	}
}
