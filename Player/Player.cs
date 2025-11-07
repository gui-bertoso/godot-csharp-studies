using Godot;
using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

public partial class Player : Entity
{
	private Timer AttackTimer;
	private AnimationPlayer Animation;



	private bool IsAttacking = false;

	private string DirectionToAttack = "Right";

	[Export]
	private float Speed = 300.0f;

	[Export]
	private float JumpVelocity = -400.0f;

	public override void _Ready()
	{
		base._Ready();

		Health = Globals.Life;

		AttackTimer = GetNode<Timer>("AttackTimer");
		Animation = GetNode<AnimationPlayer>("AnimationPlayer");
		Animation.AnimationFinished += AnimationFinished;

	}

	private Vector2 GetKnockbackFromDirection(string direction)
	{
		switch (direction)
		{
			case "Up":
				return new Vector2(0, -1);
			case "Bottom":
				return new Vector2(0, 1);
			case "Left":
				return new Vector2(-1, 0);
			case "Right":
				return new Vector2(1, 0);
			default:
				return Vector2.Zero;
		}
	}

	private int GetKnockbackForceFromDirection(string direction)
	{
		switch (direction)
		{
			case "Up":
				return 0;
			case "Bottom":
				return -1000;
			case "Left":
				return -1000;
			case "Right":
				return -1000;
			default:
				return 0;
		}
	}

	private void AnimationFinished(StringName AnimationName)
	{
		IsAttacking = false;
	}

	public override void _Process(double delta)
	{
		base._Process(delta);

		ActionBehavior();
	}

	public void TakeKnockback(Node body)
	{
		Vector2 KnockbackVector = GetKnockbackFromDirection(DirectionToAttack) * GetKnockbackForceFromDirection(DirectionToAttack);
		Velocity += KnockbackVector;
	}


	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		ActionBehavior();

		MoveBehavior();

		UpdateAttackDirection();

		MoveAndSlide();
	}

	public void UpdateAttackDirection()
	{
		if (Input.IsActionPressed("attack_up"))
		{
			DirectionToAttack = "Up";
		}
		else if (Input.IsActionPressed("attack_down"))
		{
			DirectionToAttack = "Bottom";
		}
		else if (Input.IsActionPressed("attack_left"))
		{
			DirectionToAttack = "Left";
		}
		else if (Input.IsActionPressed("attack_right"))
		{
			DirectionToAttack = "Right";
		}
	}

	public void MoveBehavior()
	{
		Vector2 new_velocity = Velocity;
		float direction = Input.GetAxis("move_left", "move_right");
		if (direction != 0)
		{
			new_velocity.X = direction * Speed;
		}
		else
		{
			new_velocity.X = Mathf.MoveToward(new_velocity.X, 0, Speed);
		}
		Velocity = new_velocity;
	}

	public void ActionBehavior()
	{
		Vector2 new_velocity = Velocity;
		if (Input.IsActionJustPressed("action_jump") && IsOnFloor())
		{
			new_velocity.Y = JumpVelocity;
		}
		Velocity = new_velocity;

		if (Input.IsActionJustPressed("action_attack"))
		{
			if (!IsAttacking)
			{
				IsAttacking = true;
				Animation.Play($"Attack{DirectionToAttack}");
				GD.Print($"Attacking{DirectionToAttack}");
			}
		}
	}
}
