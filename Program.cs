
using ChallengeCode.Services;

Console.WriteLine("Hello, World!");

var input = new InputSupplierService();
var management = new TravelManagementService(input.ProvideInputTest());
