using Godot;
using System;
using System.ComponentModel;

public partial class Entity : CharacterBody2D
{
    [Export]
    public int Health = 3;

    public bool OnInvincibilityTime = false;
    public float InvincibilityTime = 1;

    public bool TakeDamage(int Damage)
    {
        if (OnInvincibilityTime) return false;
        Health -= Damage;
        FloatingDamage DamageLabel = new FloatingDamage()
        {
            Damage = Damage,
            GlobalPosition = this.GlobalPosition,
        };
        AddChild(DamageLabel);

        GD.Print($"Damage is taken {Damage}");

        OnInvincibilityTime = true;
        return true;
    }

    public override void _Process(double delta)
    {
        if (OnInvincibilityTime)
        {
            InvincibilityTime -= (float)delta;
            if (InvincibilityTime <= 0)
            {
                OnInvincibilityTime = false;
                InvincibilityTime = 1;
            }
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        Gravity(delta);
    }

    public void Gravity(double delta)
    {
        if (!IsOnFloor())
        {
            Velocity += GetGravity() * (float)delta;
        }
    }

}
