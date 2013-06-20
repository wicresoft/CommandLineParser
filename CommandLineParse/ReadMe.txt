Command Line Parse (CommandLineParse.cs)
	This API help to parsing command line argument and provide an interface to the user so he/she can dynamically add argument(required and optional).
	Using this API user can do following work.
	1. User can add argument info in bulk(Argument name, Argument value, Argument type and argument is required or not).
	2. User can add required argument if it is not exists.
	3. User can add optional argument if it is not exists.
	4. Parse the argument. 
		I. Checking all argument(which provide by user) are valid. If argument is not valid then it throw an Exception.
		II. Checking all argument have thier value. if argument value not provide by user then this API fatch default value(which is already added) of the argumet
		III.Checking all required argument is provoid by user. If some argument is missing then it throw the exception.

	

Property:
	Separator:
		Allow user to add separator dynamically, Default separator is seted Colon Sign(:). this property allow user to set limited separator.
		User can only set these [ @ # $ % & * = ] symbols as the separator. if user try to add otherthan these separator then methord shows exception. 

Method :

	AddFields:
		This method help to add dynamically argument(user can add argument in bulk by passing a array).
		
		Signature: [public static void AddFields(ArgumentStruct[] args){}]
			Here ArgumentStruct is a class that can contain Argument name, Argument value, Argument Type(string, stringArray,int and bool), argument description and Argument required or not. 
			[stringArray]: if any argument type is stringArray it means the value should be in [val,val,val] format.

			This method is using Deractory to store ArgumnetStrct classs information with argument name is the key name. 

	AddRequiredField:
		This method is very like to AddField method but this method add required argument info one by one. This method also have one override methord.
		
		Signature: [public static void AddRequiredField(string name, int type, string defaultValue) ]
			
	AddOptionalField:
		This method is very like to AddField method but this method add optional argument info one by one. This method also have one override methord.
		
		Signature: [public static void AddOptionalField(string name, int type, string defaultValue) ]

	Parse:
		Vallidate argument which pass by user. Check all argument start with '/' or '-'. Get argument default value if user not specified the value.
		Check all required argument. After done all validation it return a Parse array. 
		
		Signature: [public static string[] Parse(string[] args)]

	IsRequiredArgsMissing:
		Checking all required argument(argument which is provide by user) in the available list. if user forget to add required argument this method throw the exception.

		Signature: [private static bool IsRequiredArgsMissing(string[] args)]

	GetArgsName:
		Get the argument name from argument provide by user. This method separate the argument and its value. return argument name.
		Signature: [private static string GetArgsName(string argsName)]

	ParseDefaultValue:
		Get default value from the list if user not provide argument value. Passing user argument array and getting default value. 
		add default value into the array and return the array.

		Signature: [private static string[] ParseDefaultValue(string[] args)]

	IsValidArgument:
		Check argument start with '/' or '-' if it is not start with this throw the exception.
		Signature: [private static bool IsValidArgument(string[] args)]

	GetValue:
		Get argument value from the list(List already added by calling AddFields);

		Signature:[ public static string GetValue(string name)]

	HelpInfo:
		Return a string with all the with all the argument details which was added in the list.

		Signature:[public static string HelpInfo()]