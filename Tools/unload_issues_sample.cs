using Godot;
using System;

[Tool]
public partial class UnloadingIssuesSample : Node
{
    public override void _Ready()
    {
        // block unloading with a strong handle
        var handle = System.Runtime.InteropServices.GCHandle.Alloc(this);

        // register cleanup code to prevent unloading issues
        System.Runtime.Loader.AssemblyLoadContext.GetLoadContext(System.Reflection.Assembly.GetExecutingAssembly()).Unloading += alc =>
        {
            // handle.Free();
        };
    }
}