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
		Sprite jinx_DefinedSprite;
		Texture jinxSprite_horizontal;
		Texture jinxSprite_up;
		AnimationPlayer anim;
		Vector2 xSpeed;
		Vector2 ySpeed;

		//variables
		bool left;
		bool right;
		bool canPlayAnim;

		public void SetObjectValues()
		{
			scene = GetNode<Node2D>("/root/Main");
			jinx = GetParent<Node2D>();
			jinx.GlobalPosition = new Vector2(500, 360);
			jinx_DefinedSprite = jinx.GetChild<Sprite>(0);
			jinxSprite_up = ResourceLoader.Load<Texture>("res://src/sprite/jinxSPRITE_UP.png");
			jinxSprite_horizontal = ResourceLoader.Load<Texture>("res://src/sprite/jinxSPRITE.png");
			jinx_DefinedSprite.Texture = jinxSprite_horizontal;
			jinx_DefinedSprite.Scale = new Vector2(0.6f, 0.35f);
			anim = jinx_DefinedSprite.GetChild<AnimationPlayer>(0);			
			canPlayAnim = true;
			xSpeed = new Vector2(200, 0);
			ySpeed = new Vector2(0, 200);
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
					jinx_DefinedSprite.Texture = jinxSprite_horizontal;
					jinx_DefinedSprite.FlipH = false;
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
					jinx_DefinedSprite.Texture = jinxSprite_horizontal;
					jinx_DefinedSprite.FlipH = true;

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

				case {} when Input.IsActionPressed("Move Up"):
					jinx.GlobalPosition -= ySpeed * delta;		
					jinx_DefinedSprite.Texture = jinxSprite_up;
					break;		
			}
		} 
	}
}
