using Godot;
using Butthole;

namespace Butthole.Settings
{
	class CameraFollow : Camera2D
	{
		Node2D target;
		[Export] string targetPath;

		public override void _Ready()
		{
			target = GetNode<Node2D>(targetPath);
		}

		public override void _PhysicsProcess(float delta)
		{
			Position = target.Position;
		}
	}
}
