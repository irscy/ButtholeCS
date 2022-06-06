using Godot;
using Butthole;
using System;

namespace Butthole.Settings
{
	class MoveController : Node2D
	{
		//objects
		Node2D scene = new Node2D();
		Node2D jinx = new Node2D();
		Sprite jinxSprite = new Sprite();
		AnimationPlayer anim = new AnimationPlayer();

		//variables
		bool left;
		bool right;
		bool canPlayAnim;
		
		public override void _Ready()
		{
			canPlayAnim = true;
			SetObjectValues();	
		}

		public void EnableMoveControls(float delta)
		{
			switch(this)
			{
				//LEFT MOVEMENT
				//on press
				case {} when Input.IsActionPressed("Move Left") && !right:
					jinx.Position -= new Vector2(200, 0) * delta;
					jinxSprite.FlipH = false;
					left = true;
					if(canPlayAnim)
					{
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
					jinx.Position += new Vector2(200, 0) * delta;
					jinxSprite.FlipH = true;

					right = true;
					if(canPlayAnim)
					{
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

		void SetObjectValues()
		{
			scene = GetNode<Node2D>("/root/Main");
			jinx = GetParent<Node2D>();
			jinxSprite = jinx.GetChild<Sprite>(0);
			anim = jinxSprite.GetChild<AnimationPlayer>(0);
		}
	}
}
