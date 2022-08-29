using System.Reflection;
using static System.Console;


var pType = typeof(UsingReflection.Person);
var pConstructors = pType.GetConstructors(BindingFlags.Instance | BindingFlags.Public| BindingFlags.NonPublic);

foreach (var pConstructor in pConstructors)
    WriteLine(pConstructor);

var privatePConstructor = pType.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic
    , null
    , new Type[] { typeof(string), typeof(int) }
    , null);

WriteLine(privatePConstructor);

var p1 = pConstructors[0].Invoke(null);
var p2 = pConstructors[1].Invoke(new object[] { "Lawrence the Developer" });
var p3 = pConstructors[2].Invoke(new object[] { "Lawrence the Developer", 33});


/*ReadLine();*/

void InspectingMetaData()
{
    string name = "Lourence";
    var stringType = name.GetType();

    Console.WriteLine(stringType);

    var currAssembly = Assembly.GetExecutingAssembly();
    var typesFromCurrAssembly = currAssembly.GetTypes();

    foreach (var type in typesFromCurrAssembly)
        WriteLine(type.Name);


    var oneTypeFromCurrAssembly = currAssembly
        .GetType("UsingReflection.Person");

    WriteLine($"Type from assembly: {oneTypeFromCurrAssembly.Name}");



    var externalAssembly = Assembly.Load("System.Text.Json");
    var typesFromExternalAssembly = externalAssembly.GetTypes();
    var oneTypeFromExternalAssembly = externalAssembly
        .GetType("System.Text.Json.JsonProperty");

    var modulesFromExternalAssembly = externalAssembly.GetModules();
    var oneModuleFromExternalAssembly = externalAssembly
        .GetModule("System.Text.Json.dll");

    var typesFromModuleFromExternalAssembly = oneModuleFromExternalAssembly
        .GetTypes();
    var oneTypeFromModuleFromExternalAssembly = oneModuleFromExternalAssembly
        .GetType("System.Text.Json.JsonProperty");
    /*
    foreach (var constructor in oneTypeFromCurrAssembly.GetMethods())
        WriteLine(constructor);

    foreach (var method in oneTypeFromExternalAssembly.GetMethods())
        WriteLine(method);*/

    foreach (var method in oneTypeFromCurrAssembly.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
        WriteLine($"{method}, public: {method.IsPublic}");


    foreach (var field in oneTypeFromCurrAssembly.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
        WriteLine($"{field}, public: {field.IsPublic}");
}
