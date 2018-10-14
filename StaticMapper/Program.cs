using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using StaticMapper.Timing;

namespace StaticMapper
{
  class App
  {
    public void Run()
    {
      var timing = new FlatMapTiming();
      timing.Run();
    }
  }

  class Program
  {
    public static void JitMethods(Assembly assembly)
    {
      var types = assembly.GetTypes();
      foreach (Type type in types)
      {
        var methods = type.GetMethods(
          BindingFlags.DeclaredOnly
          | BindingFlags.NonPublic
          | BindingFlags.Public
          | BindingFlags.Instance
          | BindingFlags.Static
        );

        foreach (MethodInfo method in methods)
        {
          if (method.IsAbstract || method.ContainsGenericParameters)
            continue;

          RuntimeHelpers.PrepareMethod(method.MethodHandle);
        }
      }
    }

    static void JitAssemblies()
    {
      var sw = Stopwatch.StartNew();
      var basePath = AppDomain.CurrentDomain.BaseDirectory;
      //var assemblyNames = new[] { "StaticMapper.Models.dll" };

      //foreach (var assemblyName in assemblyNames)
      //{
      //  var assemblyPath = Path.Combine(basePath, assemblyName);
      //  var assembly = Assembly.LoadFile(assemblyPath);
      //  JitMethods(assembly);
      //}

      JitMethods(Assembly.GetExecutingAssembly());
      Console.WriteLine("Jitting: {0}ms", sw.ElapsedMilliseconds);
    }

    static void Main(string[] args)
    {
      try
      {
        JitAssemblies();
        var app = new App();
        app.Run();
      }
      catch(Exception e)
      {
        Console.WriteLine(e);
      }
    }
  }
}
