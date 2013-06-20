using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wicresoft.ASPNET.Perf
{
    public class ArgumentStruct
    {
        #region "Argument type"
            readonly public static int _string = 0; // if arg value is string then it treats as 0
            readonly public static int _stringArray = 1; // if arg value is strin array then it treats as 1
            readonly public static int _int32 = 2; // if arg value is integer then it treats as 2
            readonly public static int _bool = 3; // if arg value is bool then it treats as 1
        #endregion
        // declare all variable public because all are using outside of the class
        public string name = string.Empty;
        public string value = string.Empty;
        public int type = -1;
        // true it means argument is required, By default it will be false if user not provide it.
        public bool isRequired = false; // 
        // By default it will be add "No description was added" if user not provide the description
        public string description = string.Empty; 
        
       
        #region "Old code"
        //public ArgumentStruct(string name, string value, int type, bool isRequired = false)
        //{
        //    if (name[0] != '/') // Checking the argument [argument should start with '/' if not then throw exception
        //        throw new Exception("'" + name + "' is not a valid argument.Valid argument should start with '/'.");

        //    if (string.IsNullOrEmpty(name))
        //        throw new Exception("Argument cannot be empty or null!");

        //    if (type == -1)
        //        throw new Exception("Type Argument  should be a positive number!");

        //    this.Name = name.ToLower();
        //    this.value = value.ToLower();
        //    this.type = type;
        //    this.isRequired = isRequired;
            
        //}
        #endregion
        /// <summary>
        /// Initializing value of all argument ...
        /// </summary>
        /// <param name="name">Name of the argument eg. /Run:</param>
        /// <param name="value">Value of the argument</param>
        /// <param name="type">Type of argument such int,string , string Array or bool</param>
        /// <param name="isRequired">true: if arg is required; false: if args is not required.</param>
        public ArgumentStruct(string name, string value, int type, string description, bool isRequired = false)
        {
            name = AddFirstChar(name);

            if (string.IsNullOrEmpty(name))
                throw new Exception("Argument cannot be empty or null!");

            if (type <= -1 || type > 3) // only accept[0 1 2 3]
                throw new Exception("Type Argument  should be a positive number or [0-3]!");
            if (string.IsNullOrEmpty(description))
                description = "No description was added";
            if (type == 3)
                if(string.IsNullOrEmpty(value)) // adding default value false if args type is bool
                    value = "false";

            if (type == 2) // Argument type is string
            {
                try
                {
                    Convert.ToInt32(value.Trim());
                }
                catch
                {
                    throw new Exception("'" + name + "' argument type is declare Integer and its '"+ value +"' value is not integer, Verify the value is integer");
                }
            }
            else if (type == 3)
            {
                try
                {
                    Convert.ToBoolean(value.Trim());
                }
                catch
                {

                    throw new Exception("'" + name + "' argument type is declare Integer and its '" + value + "' value is not integer, Verify the value is integer");
                }

            }
            this.name = name.ToLower();
            this.value = value;
            this.type = type;
            this.description = description;
            this.isRequired = isRequired;
        }
        /// <summary>
        /// Just add '/' before the argument name, if user not provide 
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>name with / char</returns>
        public static string AddFirstChar(string name)
        {
            if (name[0] != '/' && name[0] != '-')
                name = "/" + name;
            if (name[0] == '-')
                name = "/" + name.Substring(1);

            return name;
        }
        
    }
}
