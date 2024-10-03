using Godot;
using System;

public partial class Player : CharacterBody2D
{
	AnimatedSprite2D animated_sprite_2d;
	[Export] float speed = 10000;
	bool idle = true;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animated_sprite_2d = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{

		Velocity = Input.GetVector("Left", "Right", "Up", "Down") * speed;

		if (Velocity != Vector2.Zero)
		{
			idle = false;
		}

		if (Input.IsActionPressed("Up")) animated_sprite_2d.Play("Up");
		else if (Input.IsActionPressed("Down")) animated_sprite_2d.Play("Down");
		else if (Input.IsActionPressed("Left")) animated_sprite_2d.Play("Left");
		else if (Input.IsActionPressed("Right")) animated_sprite_2d.Play("Right");


		else if (idle == false)
		{
			idle = true;
			animated_sprite_2d.Play("Idle_" + animated_sprite_2d.Animation);
		}

		MoveAndSlide();
	}
}
