using System.Reflection;
using static System.Console;

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

ReadLine();
