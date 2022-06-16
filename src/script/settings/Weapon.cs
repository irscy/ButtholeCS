using Godot;
using Butthole;

namespace Butthole.Settings
{
	class Weapon : Node2D
	{
		//fields
		Node2D spriteChild;
		Node2D weaponHolder;
		Sprite weaponHolderSprite;
		AnimationPlayer swingAnim;
		int swingIndex;

		public override void _Ready()
		{
			//setting object values
			swingAnim = GetChild<AnimationPlayer>(1);
			swingIndex = 1;
			spriteChild = GetChild<Sprite>(0);
			weaponHolder = GetParent<Node2D>();
			weaponHolderSprite = weaponHolder.GetChild<Sprite>(0);

			//default position
			Position = new Vector2(-24, -40);
			RotationDegrees = 11;
		}

		public override void _PhysicsProcess(float delta)
		{
			//calling methods with float delta
			RunSwing(delta);
		}

		//used to see if swing index is even or odd, then swing in the appropriate direction
		void RunSwing(float delta)
		{
			if(Input.IsActionJustPressed("Swing Weapon") && swingIndex % 2 == 0)
			{
				swingAnim.Stop(true);
				swingAnim.Play("SwingDown");
				swingIndex += 1;
				GD.Print("Swung Down");		
				GD.Print(swingIndex);	
			}
			else if(Input.IsActionJustPressed("Swing Weapon") && swingIndex % 2 != 0)
			{
				swingAnim.Stop(true);
				swingAnim.Play("SwingUp");
				swingIndex += 1;
				GD.Print("Swung Up");		
				GD.Print(swingIndex);	
			}
		}
	}
}
