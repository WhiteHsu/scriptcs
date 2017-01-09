﻿using System;
using System.Collections.Generic;
using ScriptCs.Contracts;

namespace ScriptCs
{
    public class ScriptEnvironment : IScriptEnvironment
    {
        private readonly IConsole _console;
        private readonly Printers _printers;
        private readonly string _scriptPath;
        private readonly string[] _loadedScripts;

        public ScriptEnvironment(string[] scriptArgs, IConsole console, Printers printers, string scriptPath = null, string[] loadedScripts = null )
        {
            _console = console;
            _printers = printers;
            _scriptPath = scriptPath;

            if (loadedScripts == null)
            {
                _loadedScripts = new string[] {};
            } 
            else
            {
                _loadedScripts = loadedScripts;
            }
            
            ScriptArgs = scriptArgs;
        }

        public IReadOnlyList<string> ScriptArgs { get; private set; }

        public void AddCustomPrinter<T>(Func<T, string> printer)
        {
            _console.WriteLine("Adding custom printer for " + typeof(T).Name);
            _printers.AddCustomPrinter<T>(printer);
        }

        public void Print(object o)
        {
            _console.WriteLine(_printers.GetStringFor(o));
        }

        public void Print<T>(T o)
        {
            _console.WriteLine(_printers.GetStringFor<T>(o));
        }

        public string ScriptPath
        {
            get { return _scriptPath; }
        }

        public string[] LoadedScripts
        {
            get { return _loadedScripts; }
        }
    }
}
