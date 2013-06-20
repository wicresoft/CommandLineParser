//**********************************************************************
//Create By：       Naren Chejara
//Create Date：     09 May, 2012
//Update History:
//**********************************************************************


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wicresoft.ASPNET.Perf
{
    public class CommandLineParser
    {
        private static char _separator = ':'; // by default separator is : Sign
        private static readonly char[] specialChar = "@#$%&*=".ToCharArray(); // Allowable separator
        public static char Separator
        {
            get { return _separator; }
            set
            {
                int count = 0;
                if (value == '/' || value == '-')
                    throw new Exception("\t'/' or '-' are reserved by current program.\n\tplease add any other symbol!");

                for (int i = 0; i < specialChar.Length; i++)
                    if (value == specialChar[i])
                        count++;

                if (count == 0)
                    throw new Exception("Only these [@ # $ % & * =] Separator are allowed, : is default separator!");

                _separator = value;
            }
        }
        private static Dictionary<string, ArgumentStruct> allROArgs = new Dictionary<string, ArgumentStruct>();
        private static string[] parseArg = null; // It contain current argument passed by user, initialized in Parse()
        #region "Old code, separate default value from argument"
        //public static void AddFields(ArgumentStruct[] args)
        //{
        //    try
        //    {
        //        if (args != null)
        //        {
        //            foreach (ArgumentStruct arg in args)
        //            {
        //                if (allROArgs.ContainsKey(arg.Name)) // Check name is contain in the addField
        //                    throw new Exception("An item is already in a list!");
        //                allROArgs.Add(arg.Name, arg); // add argument name as a key and ArgumentStruct class as the value.
        //            }
        //        }
        //        else
        //        {
        //            throw new Exception("Null argument cannot be cannot be accept by AddField!");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("[Error occur at AddOptionalFeild ]: " + ex.Message);
        //    }
        //}
        //public static void AddRequiredField(string name, int type)
        //{
        //    AddRequiredField(name, type, "No description was added!");
        //}
        //public static void AddRequiredField(string name, int type, string description)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(name)) // check name is  empty or null
        //            throw new Exception("Argument name cannot be null or empty!");
        //        if (type == -1)
        //            throw new Exception("Argument value should be a positive number!");
        //        if (allROArgs.ContainsKey(name)) // Check name is contain in the addField
        //            throw new Exception("Name is all ready aded into the list!");

        //        int idx = name.IndexOf(Separator);
        //        if (idx == -1)
        //            throw new Exception("Argument is not contain '" + Separator + "' saperetor");

        //        string value = string.Empty;
        //        value = name.Substring(idx + 1); // getting argument value
        //        name = name.Substring(0, idx); // getting argument name

        //        allROArgs.Add(name, new ArgumentStruct(name, value, type,description, true));


        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("[Error occur at AddRequiredFeild ]: " + ex.Message);
        //    }
        //}
        //public static void AddOptionalField(string name, int type)
        //{
        //    AddOptionalField(name, type, "No description was added");
        //}
        //public static void AddOptionalField(string name, int type, string description)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(name))
        //            throw new Exception("Argument name cannot be null or empty!");

        //        if (type == -1)
        //            throw new Exception("Argument value should be a positive number!");

        //        if (allROArgs.ContainsKey(name)) // Check name is contain in the addField
        //            throw new Exception("Name is all ready aded into the list!");

        //        int idx = name.IndexOf(Separator);
        //        if (idx == -1)
        //            throw new Exception("Argument is not contain '" + Separator + "' saperetor");

        //        string value = null;
        //        value = name.Substring(idx + 1); // getting argument value
        //        name = name.Substring(0, idx); // getting argument name

        //        allROArgs.Add(name, new ArgumentStruct(name, value, type,description));


        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("[Error occur at AddOptionalFeild ]: " + ex.Message);
        //    }
        //}
        #endregion
        /// <summary>
        /// Add Required and optional argument in bulk.
        /// </summary>
        /// <param name="args"></param>
        public static void AddFields(ArgumentStruct[] args)
        {
            try
            {
                if (args == null)
                    throw new Exception("Null argument cannot be accept by AddField!");

                foreach (ArgumentStruct arg in args)
                {
                    if (allROArgs.ContainsKey(arg.name.ToLower())) // Check name is contain in the addField
                        throw new Exception("An item is already in a list!");

                    allROArgs.Add(arg.name, arg); // add argument name as a key and ArgumentStruct class as the value.
                }

            }
            catch (Exception ex)
            {
                throw new Exception("[Error AddOptionalFeild ]: " + ex.Message);
            }
        }
        /// <summary>
        /// Add required argument.
        /// </summary>
        /// <param name="name">Argument name</param>
        /// <param name="type">Argument type [string, stringArray, int, bool ]</param>
        /// <param name="defaultValue">Argument value</param>
        public static void AddRequiredField(string name, int type, string defaultValue)
        {

            AddRequiredField(name, type, defaultValue, "No description was added");
        }
        public static void AddRequiredField(string name, int type, string defaultValue, string description)
        {
            //[To Do] : if type is bool and user not provide the default value then we will consider false as the default value;
            try
            {
                if (string.IsNullOrEmpty(name)) // check name is  empty or null
                    throw new Exception("Argument name cannot be null or empty!");
                if (type <= -1 || type > 3)
                    throw new Exception("Argument value should be a positive number!");
                if (allROArgs.ContainsKey(name)) // Check name is contain in the addField
                    throw new Exception("Name is all ready aded into the list!");
                if (string.IsNullOrEmpty(defaultValue))
                    throw new Exception("Default value cannot be null!");

                if (type == 3)
                    if (string.IsNullOrEmpty(defaultValue)) // adding default value false if args type is bool
                        defaultValue = "false";
                //if argument is not add the '/'
                name = ArgumentStruct.AddFirstChar(name);

                allROArgs.Add(name.ToLower(), new ArgumentStruct(name, defaultValue, type, description, true));

            }
            catch (Exception ex)
            {
                throw new Exception("\n\n[Error AddRequiredFeild ]: " + ex.Message);
            }

        }
        /// <summary>
        /// Add optional argument.
        /// </summary>
        /// <param name="name">Argument name</param>
        /// <param name="type">Argument type [string, stringArray, int, bool ]</param>
        /// <param name="defaultValue">Argument value</param>
        public static void AddOptionalField(string name, int type, string defaultValue)
        {
            AddOptionalField(name, type, defaultValue, "No description was added");
        }
        public static void AddOptionalField(string name, int type, string defaultValue, string description)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                    throw new Exception("Argument name cannot be null or empty!");

                if (type <= -1 || type > 3)
                    throw new Exception("Argument value should be [0-3] number!");

                if (allROArgs.ContainsKey(name)) // Check name is contain in the addField
                    throw new Exception("Name is all ready aded into the list!");
                if (string.IsNullOrEmpty(defaultValue))
                    throw new Exception("Default value cannot be null!");

                if (type == 3)
                    if (string.IsNullOrEmpty(defaultValue)) // adding default value false if args type is bool
                        defaultValue = "false";

                //Add '/' char if argument is not added one
                name = ArgumentStruct.AddFirstChar(name);
                allROArgs.Add(name.ToLower(), new ArgumentStruct(name, defaultValue, type, description));


            }
            catch (Exception ex)
            {
                throw new Exception("[Error AddOptionalFeild ]: " + ex.Message);
            }
        }
        /// <summary>
        /// Parse the all argument which provided by user
        /// Check argument is valid argument
        /// Get argument default value if value is not provided by the user
        /// Check isRequired argument is not missing. if it missing throw exception
        /// </summary>
        /// <param name="args">Parse argument</param>
        /// <returns>Parse argument</returns>
        public static string[] Parse(string[] args)
        {
            parseArg = args; // intialized parseArg for identify current value for CurrntValue() method.
            try
            {
                args = FilterArgument(args); // filter the argument and recreated it like /arg:val /arg2:val1 val2 val3
                //If special Argument is contain then execute special argument only
                if (!IsSpecialArgsContain(args))
                {
                    return args;
                }
                else
                {
                    if (!IsValidArgument(args)) // verify all the argument are valid argument
                        throw new Exception("Argument is not valid argument. Make sure argument start with '/' or '-'.");

                    if (allROArgs.Count == 0)
                        throw new Exception("List is empty, please add fields in the list first!");

                    parseArg = args; // intialized parseArg for identify current value for CurrntValue() method.
                    args = ParseDefaultValue(args); //getting argument default value if user is not enter value

                    if (!IsRequiredArgsMissing(args))
                        throw new Exception("Required argument is missing, Please add required argument!");

                    return args;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("\n\n[Error Parse ]: " + ex.Message);
            }
        }
        /// <summary>
        /// Handle special argument if special argument is  contain then method handle special argument and return false. 
        /// </summary>
        /// <param name="args">Parse Array</param>
        /// <returns>false : if special argument contain</returns>
        public static bool IsSpecialArgsContain(string[] args)
        {
            // this is only for ? and help
            string argsName = GetArgsName(ArgumentStruct.AddFirstChar(args[0]));
            switch (argsName)
            {
                case "/?":
                     Console.WriteLine(HelpInfo());
                     return false;
                case "/help" :
                     Console.WriteLine(HelpInfo());
                     return false;
                case "/checkout" :
                      if(args.Length == 1)
                          Console.WriteLine(HelpInfo());
                      if(GetArgsName(GetNextArgs(args)) != "/databasename" ) // just check if 2nd argument it /databasename
                          Console.WriteLine(HelpInfo());
                     return false;
                case "/databasename" :
                     if (args.Length == 1)
                         Console.WriteLine(HelpInfo());
                     if(GetArgsName(GetNextArgs(args)) != "/list")
                         Console.WriteLine(HelpInfo());  
                     return false;
                case "/comment" :
                     if (args.Length != 2)
                         Console.WriteLine(HelpInfo());
                     if (GetArgsName(GetNextArgs(args)) != "/checkin")
                         Console.WriteLine(HelpInfo());  
                     return false;
                case "/checkin" :
                     if (args.Length != 2)
                         Console.WriteLine(HelpInfo());
                     if (GetArgsName(GetNextArgs(args)) != "/comment")
                         Console.WriteLine(HelpInfo());  
                     return false;
                    
            }
            return true; // it mean argument is not contain special args ...
        }
        private static string GetNextArgs(String[] args)
        {
            string currentArgs = string.Empty;
            // start checking from 2nd argument
            for(int i = 1; i < args.Length; i++)
            {
                currentArgs = args[i];
                if(currentArgs[0] == '/' || currentArgs[0] == '-')
                    return currentArgs;
            }
            return ""; // no argument is contain
        }
        /// <summary>
        /// Get user argument and Filter it according to CommandLineParser.
        /// * QUALIFERS - Qualifiers are name-value pairs. The following syntax is supported.
        ///        * -QUALIFER
        ///        * -QUALIFER:VALUE 
        ///        * -QUALIFER=VALUE [User need to set the separator(by calling CommandLineParser.SetSeparator() if he want to use otherthan Colon sign(:)]
        ///        * -QUALIFER VALUE
        /// </summary>
        /// <param name="args">User array</param>
        /// <returns>Filter Array</returns>
        public static string[] FilterArgument(string[] args)
        {
            int count = 0;
            List<string> listArgs = new List<string>();
            string argsName = string.Empty;
            string currentArgs = string.Empty;
            for (int i = 0; i < args.Length; i++)
            {
                currentArgs = args[i];
                if (string.IsNullOrEmpty(currentArgs))
                    throw new Exception("Argument value cannot be empty ...");
                if (currentArgs[0] == '/' || currentArgs[0] == '-')
                {

                    if (currentArgs.IndexOf(Separator) != -1)// Argument itself contain separator
                    {
                        if (!string.IsNullOrEmpty(argsName))
                            listArgs.Add(argsName);
                        listArgs.Add(currentArgs);
                        argsName = "";
                    }
                    else
                    {
                        if (count == 0) // get first argument start with /
                        {
                            argsName = currentArgs + Separator;
                            count = 1;
                        }
                        else
                        {
                            listArgs.Add(argsName); // Continuse getting argument with '/'
                            argsName = currentArgs + Separator;
                            count = 1;

                        }
                    }
                }
                else
                {
                    if (i == 0) // First argument without '/'
                        throw new Exception(args[i] + " argument should start with '/' or '-'");
                    else
                        argsName = argsName + currentArgs + " ";

                }

                if (i == args.Length - 1) // Last argument
                    if (argsName != "") // if all argument contain the separator then no need to add last element it is not present
                        listArgs.Add(argsName);
            }

            return listArgs.ToArray();

        }
        /// <summary>
        /// Checking the required argugment is added by user if user miss to add argument then this method throw missing argument 
        /// </summary>
        /// <param name="args">Parse argument</param>
        /// <returns>Return true if all required argument are added by the user</returns>
        private static bool IsRequiredArgsMissing(string[] args)
        {
            int rCount = 0;
            int count = 0;
            foreach (string kName in allROArgs.Keys)
                if (allROArgs[kName].isRequired == true)
                {
                    rCount++;
                    for (int i = 0; i < args.Length; i++)
                        if (allROArgs[kName].name == GetArgsName(args[i]))
                        {
                            count++;
                            break;
                        }
                    if (rCount != count) // Display which argument is missing
                        throw new Exception("[ " + allROArgs[kName].name + " ] argument is missing, plese check in below table." + CurrentValue());
                }

            if (rCount != count)
                return false;

            return true;
        }
        /// <summary>
        /// Getting the argumnet name which pass by user
        /// </summary>
        /// <param name="argsName"></param>
        /// <returns></returns>
        private static string GetArgsName(string argsName)
        {
            if (string.IsNullOrEmpty(argsName)) // check name is  empty or null
                throw new Exception("Argument name cannot be null or empty!");

            argsName = ArgumentStruct.AddFirstChar(argsName);
            int idx = argsName.IndexOf(Separator);

            if (idx == -1)
                throw new Exception("'" + Separator + "' saperetor is missing.");

            argsName = argsName.Substring(0, idx).ToLower(); // getting argument name [removing ('/' or '-') or separator]
            return argsName;
        }
        /// <summary>
        /// Add default value if argument doesn't contain its value.
        /// Default value fatch from available field (which we already added by calling AddFeild method).
        /// </summary>
        /// <param name="arg">Parse argument</param>
        /// <returns>argument with default value</returns>
        private static string[] ParseDefaultValue(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                int idx = args[i].IndexOf(Separator);
                if (idx == -1)
                    throw new Exception(args[i] + " Separator ['" + Separator + "'] is not match! Make sure you set correct Separator.");
                string value = string.Empty;
                string name = args[i].Substring(0, idx).ToLower();
                value = args[i].Substring(idx + 1);// getting argument value

                if (string.IsNullOrEmpty(value))
                    //Calling GetValue methord to get argument default value. readded into argument
                    args[i] = args[i] + GetValue(args[i].Substring(0, idx)); // getting argument name
                else
                {
                    /*
                     * Checking argument type, Argument value type is not match throw exception
                     */
                    if (allROArgs[name].type == 2) // Argument type is string
                    {
                        try
                        {
                            Convert.ToInt32(value.Trim());
                        }
                        catch
                        {
                            PrintArgs(args, args[i],ConsoleColor.Red);
                            throw new Exception("'" + name + "' argument type is declare Integer and its '" + value + "' value is not integer, Verify the value is integer");
                        }
                    }
                    else if(allROArgs[name].type == 3)
                        
                    {
                        try
                        {
                            Convert.ToBoolean(value.Trim());
                        }
                        catch
                        {
                            PrintArgs(args, args[i], ConsoleColor.Red);
                            throw new Exception("'" + name + "' argument type is declare Integer and its '" + value + "' value is not integer, Verify the value is integer");
                        }

                    }
                }
            }
            return args;


        }
        /// <summary>
        /// Verify all argument should start with '/' or '-'
        /// </summary>
        /// <param name="args">Parse array</param>
        /// <returns>Return true if all are valid argument otherwise return false.</returns>
        private static bool IsValidArgument(string[] args)
        {

            for (int i = 0; i < args.Length; i++)
            {
                string currentArgs = args[i];
                if (currentArgs[0] != '/' && currentArgs[0] != '-')
                {
                    PrintArgs(args, args[i], ConsoleColor.Red);
                    throw new Exception("'" + currentArgs + "' is not a valid Argument.[Must start with '/' or '-']");
                }
            }
            return true;

        }
        /// <summary>
        /// Getting the argument value by its name
        /// </summary>
        /// <param name="name">Argument name</param>
        /// <returns>Argument vaule </returns>
        public static string GetValue(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                    throw new Exception("Value cannot be null or empty!");

                name = ArgumentStruct.AddFirstChar(name.ToLower());
                return allROArgs[name].value.ToString(); // if it is null or empty exception throw automatic 
            }
            catch
            {
                throw new Exception(name + " is not find in the list, Make sure your write correct name.");
            }
        }
        /// <summary>
        /// Getting all available argument details ...
        /// </summary>
        /// <returns>Return the argument list into array </returns>
        public static string HelpInfo()
        {
            //if (allROArgs.Count == 0)
            //    throw new Exception("No argument added into the list! List was empty!");
            string args = string.Empty;
            args = args + "\nAvailable argument List:\n <" + Separator + "> is the default argument separater.\n";
            args = args + "ArgumentName <ArgumentValue> <Required/Optional>\n\t Descreption\n";
            foreach (string kName in allROArgs.Keys)
            {
                // get all the details ...
                string RequriedStatus = string.Empty;
                if (allROArgs[kName].isRequired == true)
                    RequriedStatus = "Required";
                else
                    RequriedStatus = "Optional";

                args = args + allROArgs[kName].name + " <" + allROArgs[kName].value + "> < " + RequriedStatus + " > \n\t" + allROArgs[kName].description + "\n\n";
            }
            if (allROArgs.Count == 0)
                args = args + "No argument field added yet!";
            return args;
        }
        /// <summary>
        /// Display default value and the current value(pass by user)
        /// </summary>
        /// <returns></returns>
        public static string CurrentValue()
        {
            string fieldList = "\n List of all available argument\n";
            string argList = "\n List of all user argument\n";


            if (allROArgs.Count == 0)
                fieldList = fieldList + "\tNo argument in the list..";
            else
            {
                string RequriedStatus = string.Empty;
                foreach (string kName in allROArgs.Keys)
                {
                    if (allROArgs[kName].isRequired == true)
                        RequriedStatus = "Required";
                    else
                        RequriedStatus = "Optional";
                    fieldList = fieldList + "\t< " + RequriedStatus + " >\t" + allROArgs[kName].name + " <" + allROArgs[kName].value + ">\n";
                }
            }
            if (parseArg == null)
                argList = argList + "\tArgument not passed by user yet.";
            else
            {
                for (int i = 0; i < parseArg.Length; i++)
                {

                    int idx = parseArg[i].IndexOf(Separator);
                    if (idx == -1)
                        throw new Exception("'" + Separator + "' saperetor is missing.");
                    string value = parseArg[i].Substring(idx + 1);
                    if (string.IsNullOrEmpty(value))
                        value = "No value added by user.";


                    argList = argList + "\t" + parseArg[i].Substring(0, idx) + " <" + value + ">\n";

                }
            }
            return fieldList + argList;

        }
        private static void PrintArgs(string[] args, string argsName, ConsoleColor color)
        {
            Console.WriteLine("User argument ... ");
            for (int i = 0; i < args.Length; i++)
            {
                if (string.Equals(args[i], argsName))
                {
                    Console.ForegroundColor = (ConsoleColor)color;
                    Console.Write(args[i] + " ");
                }
                else
                {

                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(args[i] + " ");
                }
            }
        }

    }




}
