using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using mtg.Models;

namespace MTG_Helper.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            ProcessArguments(args);
        }

        private static void ProcessArguments(string[] args)
        {
            if (args.Length == 0)
            {
                System.Console.WriteLine("Please enter a controller.  Use '?' to see available controllers.");
                return;
            }

            var controller = GetController(args[0]);
            if (controller == null) return;

            if (args.Length == 1)
            {
                System.Console.WriteLine("Please enter a command.  Use '?' to see available commands.");
                return;
            }

            var command = GetCommand(controller, args[1]);
            if (command == null) return;

            var argsList = args.ToList();
            argsList.RemoveRange(0, 2);

            var commandArguments = SetArguments(argsList);
            var paramList = GetParams(command, commandArguments);
            
            command.Invoke(null, BindingFlags.Static, null, paramList.ToArray(), null);
        }

        private static List<CommandLineArguments> SetArguments(IEnumerable<string> args)
        {
            var arguments = new List<CommandLineArguments>();
            CommandLineArguments tempArg = null;

            foreach (var argument in args)
            {
                if (argument.StartsWith("-"))
                {
                    if (tempArg != null) arguments.Add(tempArg);
                    tempArg = new CommandLineArguments
                    {
                        Command = argument.Substring(1),
                        Order = arguments.Count + 1
                    };
                }
                else
                {
                    tempArg.Value = argument;
                }
            }
            arguments.Add(tempArg);

            return arguments;
        }

        private static Type GetController(string name)
        {
            var namespaceName = "mtg.Controllers";
            var classList = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsClass && t.Namespace == namespaceName && Attribute.GetCustomAttributes(t).Any(a => a is CliController));

            if (name == "?") System.Console.WriteLine("The possible controllers to use are:");

            foreach (var t in classList)
            {
                var attrs = Attribute.GetCustomAttributes(t);

                foreach (var attr in attrs)
                {
                    if (!(attr is CliController)) continue;
                    var a = (CliController)attr;

                    if (name == "?")
                    {
                        System.Console.WriteLine($"{a.Name} - {a.Description}");
                    }
                    else
                    {
                        if (a.Name == name) return t;
                    }
                }
            }

            return null;
        }

        private static MethodInfo GetCommand(Type controller, string commandName)
        {
            var commands = controller.GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(m => Attribute.GetCustomAttributes(m).Any(a => a is CliCommand));

            if (commandName == "?")
            {
                System.Console.WriteLine("The possible commands to use are:");
            }

            foreach (var command in commands)
            {
                var attrs = Attribute.GetCustomAttributes(command);

                foreach (var attr in attrs)
                {
                    if (!(attr is CliCommand)) continue;
                    var a = (CliCommand)attr;

                    if (commandName == "?")
                    {
                        System.Console.WriteLine();
                        System.Console.WriteLine($"{a.Name}");
                        System.Console.WriteLine($"Description: {a.Description}");
                        var commandParams = command.GetParameters();
                        if (commandParams.Length > 0)
                        {
                            System.Console.WriteLine($"Parameters:");
                            foreach (var cp in commandParams)
                            {
                                var priorityString = cp.HasDefaultValue ? "Optional" : "Required";
                                var temp = Nullable.GetUnderlyingType(cp.ParameterType);
                                var typeName = temp == null ? cp.ParameterType.Name : temp.Name;
                                System.Console.WriteLine($"-{cp.Name} ({typeName}): This parameter is {priorityString}.");
                            }
                        }
                    }
                    else
                    {
                        if (a.Name == commandName) return command;
                    }
                }
            }

            return null;
        }

        private static object[] GetParams(MethodInfo command, List<CommandLineArguments> args)
        {
            var paramList = new List<object>();

            foreach (var parameter in command.GetParameters())
            {
                var wasFound = false;
                foreach (var argument in args)
                {
                    if (argument.Command.ToLower() != parameter.Name.ToLower()) continue;
                    object paramValue;

                    if (Nullable.GetUnderlyingType(parameter.ParameterType) != null)
                    {
                        var underType = Nullable.GetUnderlyingType(parameter.ParameterType);
                        paramValue = Convert.ChangeType(argument.Value, underType);
                    }
                    else
                    {
                        paramValue = Convert.ChangeType(argument.Value, parameter.ParameterType);
                    }

                    paramList.Add(paramValue);
                    wasFound = true;
                }

                if (!wasFound) paramList.Add(null);
            }

            return paramList.ToArray();
        }
    }
}
