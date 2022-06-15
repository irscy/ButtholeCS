using Godot;
using Butthole;

namespace Butthole.Settings
{
	class MoveController : Node2D
	{

		//objects
		Sprite definedSprite;

		[Export] Texture upSpr;
		[Export] Texture downSpr;
		[Export] Texture horizSpr;

		AnimationPlayer FlipAnim;

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
			definedSprite = GetChild<Sprite>(0);
			FlipAnim = definedSprite.GetChild<AnimationPlayer>(0);
			canPlayHorizAnim = true;
			canPlayUpAnim = true;
			canPlayDownAnim = true;
			xSpeed = new Vector2(200, 0);
			ySpeed = new Vector2(0, 200);
		}

		public override void _Ready()
		{
			SetObjectValues();
		}

		public override void _PhysicsProcess(float delta)
		{
			EnableMoveControls(delta);
		}

		void ResetAllAnims()
		{
			FlipAnim.Stop(true);
		}

		public void EnableMoveControls(float delta)
		{
			switch (this)
			{
				//LEFT MOVEMENT
				//on press
				case { } when Input.IsActionPressed("Move Left") && !right && !vertic:
					GlobalPosition -= xSpeed * delta;
					left = true;
					horiz = true;
					if (canPlayHorizAnim)
					{
						ResetAllAnims();
						FlipAnim.Play("FlipDirSquish");
					}
					canPlayHorizAnim = false;
					definedSprite.Texture = horizSpr;
					definedSprite.FlipH = false;
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
					GlobalPosition += xSpeed * delta;
					right = true;
					horiz = true;
					if (canPlayHorizAnim)
					{
						ResetAllAnims();
						FlipAnim.Play("FlipDirSquish");
					}
					canPlayHorizAnim = false;
					definedSprite.Texture = horizSpr;
					definedSprite.FlipH = true;
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
					GlobalPosition -= ySpeed * delta;
					up = true;
					vertic = true;
					if (canPlayUpAnim)
					{
						ResetAllAnims();
						FlipAnim.Play("FlipDirUp");
					}
					canPlayUpAnim = false;
					definedSprite.Texture = upSpr;
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
					GlobalPosition += ySpeed * delta;
					down = true;
					vertic = true;
					if (canPlayDownAnim)
					{
						ResetAllAnims();
						FlipAnim.Play("FlipDirDown");
					}
					canPlayDownAnim = false;
					definedSprite.Texture = downSpr;
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
