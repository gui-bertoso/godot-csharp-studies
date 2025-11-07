using Godot;
using System;

public partial class TakeDamageComponent : Area2D
{
    [Export]
    public int Damage = 1;

    public override void _Process(double delta)
    {
        if (GetOverlappingBodies().Count > 0)
        {
            foreach (var body in GetOverlappingBodies())
            {
                if (body is Entity entity)
                {
                    entity.TakeDamage(Damage);
                }
            }
        }
    }
}
