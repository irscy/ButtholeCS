using Godot;
using Butthole;

namespace Butthole.Settings
{
	partial class CameraFollow : Camera2D
	{
		Node2D Target;
		[Export] string TargetPath;

		public override void _Ready()
		{
			Target = GetNode<Node2D>(TargetPath);
		}

		public override void _PhysicsProcess(float delta)
		{
			Position = Target.Position;
		}
	}
}
