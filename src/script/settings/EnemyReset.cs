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
		Enemy e;
        int randY;

		public override void _Ready()
		{
			floppa = GD.Load<PackedScene>("res://src/scene/FloppaNPC.tscn");
			e = GetNode<Enemy>("/root/Main/FloppaNPC");
			scene = GetParent<Node2D>();
		}

		public override void _PhysicsProcess(float delta)
		{
			if(Input.IsActionJustPressed("Reset Enemies"))
			{
				if(e.numberSpawned == 0)
				{
					randY = rand.Next(100, 525);
					var ins = floppa.Instance();
					scene.AddChild(ins);
					((Enemy)ins).FixTransform();
					((Node2D)ins).Position = new Vector2(500, 400);
					e.numberSpawned += 1;
				}
			}

			if(Input.IsActionJustPressed("DEBUG"))
			{
				GD.Print(e.numberSpawned);
			}
		}
	}
}