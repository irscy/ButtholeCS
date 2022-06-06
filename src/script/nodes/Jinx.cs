using Godot;
using System;
using Butthole.Settings;

namespace Butthole.Nodes
{
	//jinx's epic gamer script
	public class Jinx : Node2D
	{
		MoveController Mover = new MoveController();

		public override void _Ready()
		{
			Mover = (MoveController)GetNode<Node2D>("/root/Main/JINX/MoveController");
			Scale.x.Equals(new Vector2(0.6f, 0.35f));
		}

		public override void _PhysicsProcess(float delta)
		{
			Mover.EnableMoveControls(delta);
		}	
	}
}
