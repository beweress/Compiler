﻿using System;
using System.IO;

using CommandLineParser;
using CommandLineParser.Arguments;
using CommandLineParser.Exceptions;

using FreeImageAPI;

namespace Compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandLineParser.CommandLineParser parser = new CommandLineParser.CommandLineParser();

            SwitchArgument debugFlag = new SwitchArgument('l', "debug", "Enable debug mode", false);
            FileArgument definitionFile = new FileArgument('d', "definition", "Path to skin definition file");
            definitionFile.Optional = false;
            DirectoryArgument steamDirectory = new DirectoryArgument('s', "steam", "Path to Steam directory");
            steamDirectory.Optional = false;
            SwitchArgument nobackupFlag = new SwitchArgument('b', "nobackup", "Backup old skin folder before writing new one", false);
            SwitchArgument activateSkinFlag = new SwitchArgument('a', "activate", "Activate skin after compilation", false);

            //dumbResponse = Array.Exists(args, el => el == "--dumb") || Array.Exists(args, el => el == "-q");

            parser.Arguments.Add(debugFlag);
            parser.Arguments.Add(definitionFile);
            parser.Arguments.Add(steamDirectory);
            parser.Arguments.Add(nobackupFlag);
            parser.Arguments.Add(activateSkinFlag);

#if !DEBUG
            try
            {
#endif
                parser.ParseCommandLine(args);
#if !DEBUG
                try
                {
#endif
                    Core.backupEnabled = !nobackupFlag.Value;
                    Core.debugMode = debugFlag.Value;
                    Core.activateSkin = activateSkinFlag.Value;
                    Core.Compile(definitionFile.Value, steamDirectory.Value);
#if !DEBUG
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Environment.Exit(2);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(1);
            }
#endif
#if DEBUG
            Console.ReadKey();
#endif
        }
    }
}
