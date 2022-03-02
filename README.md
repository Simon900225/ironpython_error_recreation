# ironpython_error_recreation

I found an error in iron python where I get 
Microsoft.Scripting.ArgumentTypeException: 'Multiple targets could match'
when assemblies are loaded into the ScriptRuntime.

It seems like the code doesn't know which assembly to call.

This didn't cause an error in versions 2.7.9 or lower of IronPython.
