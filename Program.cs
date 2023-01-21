using ChallengeCode.Services;

var inputOutSupplierService = new InputOutSupplierService();
var dataTransformerService = new DataTransformerService();

var travelManagementService = new TravelManagementService(inputOutSupplierService, dataTransformerService);

string path = @"D:\.net\ChallengeCode\Inputs\Inputs.txt";
string pathOut = @"D:\.net\ChallengeCode\Inputs\Outputs.txt";

travelManagementService.LoadRawDataFromFile(path);

travelManagementService.GenerateTrips();

travelManagementService.SaveOutputToFile(pathOut);