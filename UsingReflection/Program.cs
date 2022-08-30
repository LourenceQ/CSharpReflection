using System.Reflection;
using UsingReflection;
using static System.Console;


string _typeFromConfiguration = "UsingReflection.Person";

var pType = typeof(Person);
var pConstructors = pType.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

foreach (var pConstructor in pConstructors)
    WriteLine(pConstructor);

var privatePConstructor = pType.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic
    , null
    , new Type[] { typeof(string), typeof(int) }
    , null);

WriteLine(privatePConstructor);

var p1 = pConstructors[0].Invoke(null);
var p2 = pConstructors[1].Invoke(new object[] { "Lawrence the Developer" });
var p3 = pConstructors[2].Invoke(new object[] { "Lawrence the Developer", 33 });

var p4 = Activator.CreateInstance("UsingReflection", "UsingReflection.Person").Unwrap();
var p5 = Activator.CreateInstance("UsingReflection", "UsingReflection.Person"
    , true, BindingFlags.Instance | BindingFlags.Public
    , null, new object[] { "Lourence" }, null, null);

var pTypeFromString = Type.GetType("UsingReflection.Person");
var p6 = Activator.CreateInstance(pTypeFromString, new object[] { "Lourence from string" });

var p7 = Activator.CreateInstance("UsingReflection", "UsingReflection.Person"
    , true, BindingFlags.Instance | BindingFlags.NonPublic
    , null, new object[] { "Lou p7", 39 }, null, null);

var assembly = Assembly.GetExecutingAssembly();
var p8 = assembly.CreateInstance("UsingReflection.Person");

var actualTypeFromConfiguration = Type.GetType(_typeFromConfiguration);
var iTalkInstance = Activator.CreateInstance(actualTypeFromConfiguration) as ITalk;
iTalkInstance.Talk("Hy! I'm talking");

dynamic dynamicInstance = Activator.CreateInstance(actualTypeFromConfiguration);
dynamicInstance.Talk("Talking through dynamic");

var personForManipulation = Activator.CreateInstance("UsingReflection", "UsingReflection.Person"
    , true, BindingFlags.Instance | BindingFlags.NonPublic
    , null, new object[] { "Lourence", 33 }, null, null).Unwrap();

var nameProperty = personForManipulation.GetType().GetProperty("Name");
nameProperty.SetValue(personForManipulation, "Louuu");

WriteLine(personForManipulation);

var ageField = personForManipulation.GetType().GetField("age");
ageField.SetValue(personForManipulation, 66);

var privateField = personForManipulation.GetType().GetField("_aPrivateField"
    , BindingFlags.Instance | BindingFlags.NonPublic);
privateField.SetValue(personForManipulation, "updated private field value");

personForManipulation.GetType().InvokeMember("Name"
    , BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty
    , null, personForManipulation, new[] { "Emma" });

personForManipulation.GetType().InvokeMember("_aPrivateField"
    , BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.SetField
    , null, personForManipulation, new[] { "Second update for private field value" });

WriteLine(personForManipulation);

ReadLine();




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
