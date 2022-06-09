using Godot;
using System;
using Butthole.Settings;

namespace Butthole.Nodes
{
	//jinx's epic gamer script
	public class Jinx : Node2D
	{
		MoveController Mover;
		[Export] Texture JinxUp;
		[Export] Texture JinxHoriz;

		public override void _Ready()
		{
			SetMoverValues();
			Mover.definedNode_Sprite.Scale.x.Equals(new Vector2(0.6f, 0.35f));
		}

		public override void _PhysicsProcess(float delta)
		{
			Mover.EnableMoveControls(delta);
		}	

		void SetMoverValues()
		{
			Mover = (MoveController)GetNode<Node2D>("/root/Main/JINX/MoveController");
			Mover.definedNode = GetNode<Node2D>(".");
			Mover.definedNode_Sprite = GetChild<Sprite>(0);
			Mover.FlipDirSquish = Mover.definedNode_Sprite.GetChild<AnimationPlayer>(0);
			Mover.FlipDirUp = Mover.definedNode_Sprite.GetChild<AnimationPlayer>(1);
			Mover.SetObjectValues();
		}
	}
}

