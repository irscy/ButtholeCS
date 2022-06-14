using Godot;
using Butthole;

namespace Butthole.Settings
{
	class MoveController : Node2D
	{
		/* 
		NOTES (IN ORDER):
		--------godot--------
		- add MoveController script under a node and set MoveController object's value to that node
		- add sprite under definedNode as child index 0
		- set texture values under MoveController node
		- instance FlipDirSquish scene under sprite as child index 0
		- instance FlipDirUp scene under sprite as child index 1
		- instance FlipDirDown scene under sprite as child index 2
		------with object-----
		- set defined node (definedNode)
		- set defined sprite (definedNode_sprite)
		- set animation values (FlipDirSquish, FLipDirUp)
		- call SetObjectValues under _Ready()
		*/

		//objects
		public Node2D definedNode { get; set; }
		public Sprite definedNode_Sprite { get; set; }

		[Export] public Texture upSpr { get; set; }
		[Export] public Texture downSpr { get; set; }
		[Export] public Texture horizSpr { get; set; }

		public AnimationPlayer FlipDirSquish { get; set; }
		public AnimationPlayer FlipDirUp { get; set; }
		public AnimationPlayer FlipDirDown { get; set; }

		Vector2 xSpeed;
		Vector2 ySpeed;

		//variables
		bool left;
		bool right;
		bool up;
		bool down;
		bool horiz;
		bool vertic;
		bool canPlayHorizAnim;
		bool canPlayUpAnim;
		bool canPlayDownAnim;

		public void SetObjectValues()
		{
			canPlayHorizAnim = true;
			canPlayUpAnim = true;
			xSpeed = new Vector2(200, 0);
			ySpeed = new Vector2(0, 200);
		}

		public override void _Ready()
		{

		}

		void ResetAllAnims()
		{
			FlipDirDown.Stop(true);
			FlipDirUp.Stop(true);
			FlipDirSquish.Stop(true);
		}

		public void EnableMoveControls(float delta)
		{
			switch (this)
			{
				//LEFT MOVEMENT
				//on press
				case { } when Input.IsActionPressed("Move Left") && !right && !vertic:
					definedNode.GlobalPosition -= xSpeed * delta;
					left = true;
					horiz = true;
					if (canPlayHorizAnim)
					{
						ResetAllAnims();
						FlipDirSquish.Play("FlipDirSquish");
					}
					canPlayHorizAnim = false;
					definedNode_Sprite.Texture = horizSpr;
					definedNode_Sprite.FlipH = false;
					break;

				//on release
				case { } when Input.IsActionJustReleased("Move Left"):					
					canPlayHorizAnim = true;
					left = false;
					horiz = false;
					break;

				//RIGHT MOVEMENT
				//on press
				case { } when Input.IsActionPressed("Move Right") && !left && !vertic:
					definedNode.GlobalPosition += xSpeed * delta;
					right = true;
					horiz = true;
					if (canPlayHorizAnim)
					{
						ResetAllAnims();
						FlipDirSquish.Play("FlipDirSquish");
					}
					canPlayHorizAnim = false;
					definedNode_Sprite.Texture = horizSpr;
					definedNode_Sprite.FlipH = true;
					break;

				//on release
				case { } when Input.IsActionJustReleased("Move Right"):	
					canPlayHorizAnim = true;
					right = false;
					horiz = false;
					break;

				//UP MOVEMENT
				//on press
				case { } when Input.IsActionPressed("Move Up") && !down:
					definedNode.GlobalPosition -= ySpeed * delta;
					up = true;
					vertic = true;
					if (canPlayUpAnim)
					{
						ResetAllAnims();
						FlipDirUp.Play("FlipDirUp");
					}
					canPlayUpAnim = false;
					definedNode_Sprite.Texture = upSpr;
					break;

				//on release
				case { } when Input.IsActionJustReleased("Move Up"):	
					canPlayUpAnim = true;
					up = false;
					vertic = false;
					break;

				//DOWN MOVEMENT
				//on press
				case { } when Input.IsActionPressed("Move Down") && !up:
					definedNode.GlobalPosition += ySpeed * delta;
					down = true;
					vertic = true;
					if (canPlayDownAnim)
					{
						ResetAllAnims();
						FlipDirDown.Play("FlipDirDown");
					}
					canPlayDownAnim = false;
					definedNode_Sprite.Texture = downSpr;
					break;

				//on release
				case { } when Input.IsActionJustReleased("Move Down"):
					canPlayDownAnim = true;
					down = false;
					vertic = false;
					break;
			}
		}
	}
}
