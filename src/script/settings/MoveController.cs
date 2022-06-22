using Godot;
using Butthole;

namespace Butthole.Settings
{
	class MoveController : Node2D
	{

		//objects
		AnimatedSprite DefinedSprite;

		AnimationPlayer FlipAnim;

		Vector2 XSpeed;
		Vector2 YSpeed;

		//fields
		[Export] string HorizSpr;
		[Export] string UpSpr;
		[Export] string DownSpr;
		[Export] string IdleSpr;

		bool Left;
		bool Right;
		bool Up;
		bool Down;
		bool Horiz;
		bool Vertic;
		bool CanPlayHorizAnim;
		bool CanPlayUpAnim;
		bool CanPlayDownAnim;

		public void SetObjectValues()
		{
			DefinedSprite = GetChild<AnimatedSprite>(0);
			DefinedSprite.Animation = IdleSpr;
			FlipAnim = DefinedSprite.GetChild<AnimationPlayer>(0);
			CanPlayHorizAnim = true;
			CanPlayUpAnim = true;
			CanPlayDownAnim = true;
			XSpeed = new Vector2(200, 0);
			YSpeed = new Vector2(0, 200);
		}

		public override void _Ready()
		{
			SetObjectValues();
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
				case { } when Input.IsActionPressed("Move Left") && !Right && !Vertic:
					GlobalPosition -= XSpeed * delta;
					Scale = new Vector2(1, 1);
					Left = true;
					Horiz = true;
					if (CanPlayHorizAnim)
					{
						ResetAnims();
						FlipAnim.Play("FlipDirSquish");
					}
					CanPlayHorizAnim = false;
					DefinedSprite.Animation = HorizSpr;
					DefinedSprite.Playing = true;
					break;

				//on release
				case { } when Input.IsActionJustReleased("Move Left"):					
					CanPlayHorizAnim = true;
					Left = false;
					Horiz = false;
					DefinedSprite.Animation = IdleSpr;
					DefinedSprite.Playing = false;
					break;

				//RIGHT MOVEMENT
				//on press
				case { } when Input.IsActionPressed("Move Right") && !Left && !Vertic:
					GlobalPosition += XSpeed * delta;
					Scale = new Vector2(-1, 1);
					Right = true;
					Horiz = true;
					if (CanPlayHorizAnim)
					{
						ResetAnims();
						FlipAnim.Play("FlipDirSquish");
					}
					CanPlayHorizAnim = false;
					DefinedSprite.Animation = HorizSpr;
					DefinedSprite.Playing = true;
					break;

				//on release
				case { } when Input.IsActionJustReleased("Move Right"):	
					CanPlayHorizAnim = true;
					Right = false;
					Horiz = false;
					DefinedSprite.Animation = IdleSpr;
					DefinedSprite.Playing = false;
					break;

				//Up MOVEMENT
				//on press
				case { } when Input.IsActionPressed("Move Up") && !Down:
					GlobalPosition -= YSpeed * delta;
					Up = true;
					Vertic = true;
					if (CanPlayUpAnim)
					{
						ResetAnims();
						FlipAnim.Play("FlipDirUp");
					}
					CanPlayUpAnim = false;
					DefinedSprite.Animation = UpSpr;
					DefinedSprite.Playing = true;
					break;

				//on release
				case { } when Input.IsActionJustReleased("Move Up"):	
					CanPlayUpAnim = true;
					Up = false;
					Vertic = false;
					DefinedSprite.Animation = IdleSpr;
					DefinedSprite.Playing = false;
					break;

				//Down MOVEMENT
				//on press
				case { } when Input.IsActionPressed("Move Down") && !Up:
					GlobalPosition += YSpeed * delta;
					Down = true;
					Vertic = true;
					if (CanPlayDownAnim)
					{
						ResetAnims();
						FlipAnim.Play("FlipDirDown");
					}
					CanPlayDownAnim = false;
					DefinedSprite.Animation = DownSpr;
					DefinedSprite.Playing = true;
					break;

				//on release
				case { } when Input.IsActionJustReleased("Move Down"):
					CanPlayDownAnim = true;
					Down = false;
					Vertic = false;
					DefinedSprite.Animation = IdleSpr;
					DefinedSprite.Playing = false;
					break;
			}
		}
	}
}
