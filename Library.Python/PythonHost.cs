using IronPython.Hosting;
using IronPython.Runtime;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Loader;

namespace GSAB.RSProduction.Framework.Scripting
{
    public class PythonHost
    {
        private ScriptRuntime _runtime;
        private ScriptEngine _python;

        public PythonHost()
        {
            InitializePythonEngine();
        }

        private static readonly object _lock = new object();

        public void InitializePythonEngine()
        {
            var setup = Python.CreateRuntimeSetup(null);

            var newRuntime = new ScriptRuntime(setup);

            //These assemblies are loaded in my application at startup
            AssemblyLoadContext.Default.LoadFromAssemblyName(new System.Reflection.AssemblyName("System.Linq, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"));
            AssemblyLoadContext.Default.LoadFromAssemblyName(new System.Reflection.AssemblyName("System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"));
            AssemblyLoadContext.Default.LoadFromAssemblyName(new System.Reflection.AssemblyName("netstandard, Version=2.1.0.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51"));

            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Distinct();

            ImportAssemblies(newRuntime, assemblies);

            var newPython = newRuntime.GetEngineByTypeName(typeof(PythonContext).AssemblyQualifiedName);

            this._python = newPython;
            this._runtime = newRuntime;
        }

        private void ImportAssemblies(ScriptRuntime runtime, IEnumerable<System.Reflection.Assembly> assemblies)
        {
            foreach (var assembly in assemblies.Where(x => !x.IsDynamic).ToList())
            {
                runtime.LoadAssembly(assembly);
            }
        }

        public dynamic Execute(string script)
        {
            return _python.Execute(script);
        }
    }
}
