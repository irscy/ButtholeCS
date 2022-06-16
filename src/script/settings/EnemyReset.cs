using Butthole.Settings;
using Godot;

namespace Butthole.Settings
{
	class EnemyReset : Node2D
	{
		PackedScene floppa;
		Node2D scene;
		Enemy e;

		public override void _Ready()
		{
			floppa = GD.Load<PackedScene>("res://src/scene/FloppaNPC.tscn");
			e = GetNode<Enemy>("/root/Main/FloppaNPC");
			scene = GetParent<Node2D>();
		}

		public override void _PhysicsProcess(float delta)
		{
			if(Input.IsActionJustPressed("Reset Enemies") && e.numberSpawned < 1)
			{
				var ins = floppa.Instance();
				scene.AddChild(ins);
				((Node2D)ins).Position = new Vector2(500, 400);
				e.numberSpawned += 1;
				GD.Print(e.numberSpawned);
			}
			if(Input.IsActionJustPressed("Reset Enemies") && e.numberSpawned < 0)
			{
				GD.Print("An enemy is currently alive, kill that first");
			}
		}
	}
}
