using Godot;
using Butthole;
using System;

namespace Butthole.Settings
{
	class MoveController : Node2D
	{
		//objects
		Node2D scene;
		Node2D jinx;
		Sprite jinxSprite;
		AnimationPlayer anim;
		Vector2 xSpeed;

		//variables
		bool left;
		bool right;
		bool canPlayAnim;

		public void SetObjectValues()
		{
			scene = GetNode<Node2D>("/root/Main");
			jinx = GetParent<Node2D>();
			jinx.GlobalPosition = new Vector2(500, 360);
			jinxSprite = jinx.GetChild<Sprite>(0);
			anim = jinxSprite.GetChild<AnimationPlayer>(0);
			canPlayAnim = true;
			xSpeed = new Vector2(200, 0);
		}
		
		public override void _Ready()
		{

		}

		public void EnableMoveControls(float delta)
		{
			switch(this)
			{
				//LEFT MOVEMENT
				//on press
				case {} when Input.IsActionPressed("Move Left") && !right:
					jinx.GlobalPosition -= xSpeed * delta;
					jinxSprite.FlipH = false;
					left = true;
					if(canPlayAnim)
					{
						anim.Stop(true);
						anim.Play("FlipDirSquish");
					}
					canPlayAnim = false;
					break;

				//on release
				case {} when Input.IsActionJustReleased("Move Left"):
					left = false;
					canPlayAnim = true;
					break;
				
				//RIGHT MOVEMENT
				//on press
				case {} when Input.IsActionPressed("Move Right") && !left:
					jinx.GlobalPosition += xSpeed * delta;
					jinxSprite.FlipH = true;

					right = true;
					if(canPlayAnim)
					{
						anim.Stop(true);
						anim.Play("FlipDirSquish");
					}
					canPlayAnim = false;
					break;
				
				//on release
				case {} when Input.IsActionJustReleased("Move Right"):
					right = false;
					canPlayAnim = true;
					break;
				
			}
		} 
	}
}
