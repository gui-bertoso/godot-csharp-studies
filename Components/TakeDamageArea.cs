using Godot;
using System;

public partial class TakeDamageComponent : Area2D
{
    [Signal]
    public delegate void DamageDealtEventHandler();

    [Export]
    public int Damage = 0;

    public override void _Process(double delta)
    {
        if (GetOverlappingBodies().Count > 0)
        {
            foreach (var body in GetOverlappingBodies())
            {
                if (body is Entity entity)
                {
                    if (entity.TakeDamage(Damage))
                    {
                        EmitSignal("DamageDealt");
                    }
                }
            }
        }
    }
}
