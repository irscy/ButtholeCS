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
			scene = GetParent<Node2D>();
			floppaCollection = scene.GetChild<Node2D>(0);
			floppa = GD.Load<PackedScene>("res://src/scene/FloppaNPC.tscn");

			var firstSpawn = floppa.Instance();
			floppaCollection.AddChild(firstSpawn);

			e = GetNode<Enemy>("/root/Main/FloppaCollection/FloppaNPC");
			e.FixTransform();
		}

		public override void _PhysicsProcess(float delta)
		{
			if(Input.IsActionJustPressed("Reset Enemies") && floppaCollection.GetChildCount() < 1)
			{
				randY = rand.Next(100, 525);
				var ins = floppa.Instance();
				floppaCollection.AddChild(ins);
				((Enemy)ins).FixTransform();
				((Node2D)ins).Position = new Vector2(500, 400);
			}

			if(Input.IsActionJustPressed("DEBUG"))
			{
				GD.Print(floppaCollection.GetChildCount());
			}
		}
	}
}
