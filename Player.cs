using Godot;
using Godot.Collections;
using System;

public partial class Player : CharacterBody2D, Saveable
{
	AnimatedSprite2D animated_sprite_2d;
	[Export] float speed = 400;
	bool idle = true;
	public override void _Ready()
	{
		animated_sprite_2d = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

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

	public Godot.Collections.Dictionary<string, Variant> Save()
	{
		return new Godot.Collections.Dictionary<string, Variant>()
		{
			{ "Filename", SceneFilePath },
			{ "Parent", GetParent().GetPath() },
			{ "PosX", Position.X },
			{ "PosY", Position.Y },
			{ "Animation", animated_sprite_2d.Animation },
		};
	}

	public void Load(Dictionary<string, Variant> nodeData)
	{
		Position = new Vector2((float)nodeData["PosX"], (float)nodeData["PosY"]);
		animated_sprite_2d.Play((string)nodeData["Animation"]);
	}
}
