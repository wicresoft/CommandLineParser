using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Wicresoft.ASPNET.Perf;

namespace ConsoleApplication1
{
    class Program
    {
        public static String cmd = null;
        
        static void Main(string[] args)
        {
            
            DisplayUsage(args);

        }
        public static void DisplayUsage(string[] argsn)
        {
            int ch = -1;

            do
            {
                Usage();
                Console.WriteLine("Enter your choice : ");
                try
                {
                    ch = Convert.ToInt32(Console.ReadLine());
                    switch (ch)
                    {
                        
                        case 1:
                            CommandLineParser.AddRequiredField("Import", ArgumentStruct._string, "ImportValue");
                            CommandLineParser.AddRequiredField("Recurse", ArgumentStruct._string, "Recurse");
                            CommandLineParser.AddRequiredField("Directory", ArgumentStruct._int32, "D:\adfasdf,C:asdfasdf");
                            break;
                        case 2:
                            CommandLineParser.AddOptionalField("Arch", ArgumentStruct._string, "amd64");
                            CommandLineParser.AddOptionalField("MainVersion", ArgumentStruct._int32, "4.0");
                            CommandLineParser.AddOptionalField("MajorVersion", ArgumentStruct._int32, "30319");
                            break;
                        ///action:KickOff /FXLab:Dev10 /Branch:Plan9_MVC4 /Arch:amd64 /MainVersion:4.0 /MajorVersion:30319 /MinorVersion:01 /Config:%ConfigFile% /PrivateBuild:%PrivateBuild%
                        ///
                        case 3: // Add all argument in the project
                            ArgumentStruct[] argStruct = {
                                new ArgumentStruct("-Run","dir1,dir2,dir3",2,"Derectory name", true),
                                new ArgumentStruct("LabConfig","LabConfigvalue1",1,"LabConfig description"),
                                new ArgumentStruct("/Analyze","Analyzevalue",2,"Analyser",true)
                                };

                            CommandLineParser.AddFields(argStruct);
                            break;
                        case 4:
                            //string[] argsParse = { "/run:", "/Analyze","Hello","Naren" , "-Arch", "/MainVersion","4.0", "/Import","ImportValue", "/Recurse","Recurse"};
                            //string[] argsParse = { "/run:", "run:", "-Arch:", "/MainVersion:tes", "/Import:ImportValue", "/Recurse:Recurse" };
                            //string[] argsParse = { "-checkout", "file1", "file2", "file3","/test:test1" };
                            //string[] argsParse = { "?"}; // help, /help, -?
                            //string[] argsParse = { "-comment", "hello", "-checkin" };  
                            //string[] argsParse = { "-checkin","/comment", "This is my comment"};
                            string[] argsParse = { "-Directory",  "/Import", "hello", "/Recurse", "areyouready"};
                            argsParse = CommandLineParser.Parse(argsParse);
                            
                            break;
                        case 5:

                            Console.WriteLine(CommandLineParser.HelpInfo());
                            break;
                        case 6:
                            CommandLineParser.Separator = '$';
                            break;
                        case 7:
                            Console.WriteLine(CommandLineParser.CurrentValue());
                            break;
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (ch != 0);

        }
        public static void Usage()
        {
            Console.WriteLine("*********************************************************");
            Console.WriteLine("Press < 1 > to add required argument.");
            Console.WriteLine("Press < 2 > to add optional argument.");
            Console.WriteLine("Press < 3 > to add all fields.");
            Console.WriteLine("Press < 4 > to pass array for parse");
            Console.WriteLine("Press < 5 > to Display all available argument list.");
            Console.WriteLine("Press < 6 > to add Separator. [Default separator is(:)]");
            Console.WriteLine("Press < 7 > to Display Current Value.");
            Console.WriteLine("Press < 0 > to Exit.");
            Console.WriteLine("=========================================================");
        }
    }
}
