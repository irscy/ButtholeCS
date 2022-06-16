using Godot;
using Butthole;

namespace Butthole.Settings
{
	class MoveController : Node2D
	{

		//objects
		AnimatedSprite definedSprite;

		AnimationPlayer FlipAnim;

		Vector2 xSpeed;
		Vector2 ySpeed;

		//fields

		[Export] string horizSpr;
		[Export] string upSpr;
		[Export] string downSpr;

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
			definedSprite = GetChild<AnimatedSprite>(0);
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
			Position = GetViewportRect().Size * 0.5f;
			SetIndexed("position:y", GetViewportRect().Size.y * 0.5f + 75);
		}

		public override void _PhysicsProcess(float delta)
		{
			EnableMoveControls(delta);
		}

		void ResetAnims()
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
					Scale = new Vector2(1, 1);
					left = true;
					horiz = true;
					if (canPlayHorizAnim)
					{
						ResetAnims();
						FlipAnim.Play("FlipDirSquish");
					}
					canPlayHorizAnim = false;
					definedSprite.Animation = horizSpr;
					definedSprite.Playing = true;
					break;

				//on release
				case { } when Input.IsActionJustReleased("Move Left"):					
					canPlayHorizAnim = true;
					left = false;
					horiz = false;
					definedSprite.Playing = false;
					break;

				//RIGHT MOVEMENT
				//on press
				case { } when Input.IsActionPressed("Move Right") && !left && !vertic:
					GlobalPosition += xSpeed * delta;
					Scale = new Vector2(-1, 1);
					right = true;
					horiz = true;
					if (canPlayHorizAnim)
					{
						ResetAnims();
						FlipAnim.Play("FlipDirSquish");
					}
					canPlayHorizAnim = false;
					definedSprite.Animation = horizSpr;
					definedSprite.Playing = true;
					break;

				//on release
				case { } when Input.IsActionJustReleased("Move Right"):	
					canPlayHorizAnim = true;
					right = false;
					horiz = false;
					definedSprite.Playing = false;
					break;

				//UP MOVEMENT
				//on press
				case { } when Input.IsActionPressed("Move Up") && !down:
					GlobalPosition -= ySpeed * delta;
					up = true;
					vertic = true;
					if (canPlayUpAnim)
					{
						ResetAnims();
						FlipAnim.Play("FlipDirUp");
					}
					canPlayUpAnim = false;
					definedSprite.Animation = upSpr;
					definedSprite.Playing = true;
					break;

				//on release
				case { } when Input.IsActionJustReleased("Move Up"):	
					canPlayUpAnim = true;
					up = false;
					vertic = false;
					definedSprite.Playing = false;
					break;

				//DOWN MOVEMENT
				//on press
				case { } when Input.IsActionPressed("Move Down") && !up:
					GlobalPosition += ySpeed * delta;
					down = true;
					vertic = true;
					if (canPlayDownAnim)
					{
						ResetAnims();
						FlipAnim.Play("FlipDirDown");
					}
					canPlayDownAnim = false;
					definedSprite.Animation = downSpr;
					definedSprite.Playing = true;
					break;

				//on release
				case { } when Input.IsActionJustReleased("Move Down"):
					canPlayDownAnim = true;
					down = false;
					vertic = false;
					definedSprite.Playing = false;
					break;
			}
		}
	}
}
