using Godot;
using Butthole;

namespace Butthole.Settings
{
	class Weapon : Node2D
	{
		Node2D spriteChild;
		Node2D weaponHolder;
		Sprite weaponHolderSprite;
		int swingIndex;

		public override void _Ready()
		{
			swingIndex = 1;
			spriteChild = GetChild<Sprite>(0);
			weaponHolder = GetParent<Node2D>();
			weaponHolderSprite = weaponHolder.GetChild<Sprite>(0);
		}

		public override void _PhysicsProcess(float delta)
		{
			RunAnchor(delta);
			RunSwing(delta);
		}

		void RunAnchor(float delta)
		{
			//Check if the jinx sprite is flipped or not.
			//if not flipped
			if (!weaponHolderSprite.FlipH)
			{
				Position = new Vector2(-14.972f, -38);
				spriteChild.Scale = new Vector2(0.3f, 0.3f);
				RotationDegrees = 11;
			}
			//if flipped
			if (weaponHolderSprite.FlipH)
			{
				Position = new Vector2(37.676f, -38);
				spriteChild.Scale = new Vector2(-0.3f, 0.3f);
				RotationDegrees = -11;
			}
		}

		void RunSwing(float delta)
		{
			if(Input.IsActionJustPressed("Swing Weapon") && swingIndex % 2 == 0)
			{
				GD.Print("Swung Down");
				swingIndex += 1;
			}

			else if(Input.IsActionJustPressed("Swing Weapon") && swingIndex % 2 != 0)
			{
				GD.Print("Swung Up");
				swingIndex += 1;
			}
		}
	}
}
