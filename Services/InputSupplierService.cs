using ChallengeCode.DTO;
using System.IO;

namespace ChallengeCode.Services;

public class InputSupplierService{
    public (List<Drone>, List<Location>) ProvideInputTest(){
        var drones = new List<Drone>();
        var locations = new List<Location>();

        var dron1 = new Drone{
            Name = "Drone1",
            MaximumWeight = 25.8
        };

        var dron2 = new Drone{
            Name = "Drone2",
            MaximumWeight = 12.5
        };

        var dron3 = new Drone{
            Name = "Drone3",
            MaximumWeight = 30.0
        };

        drones.Add(dron1);
        drones.Add(dron2);
        drones.Add(dron3);

        var location1 = new Location{
            Name = "Drone1",
            PackageWeight = 10.0
        };

        var location2 = new Location{
            Name = "Drone2",
            PackageWeight = 5.0
        };

        var location3 = new Location{
            Name = "Drone3",
            PackageWeight = 50.0
        };

        locations.Add(location1);
        locations.Add(location2);
        locations.Add(location3);

        var result = (drones,locations);
        return result;
    }

    public (List<Drone>, List<Location>) ProvideInput(string path){
        var drones = new List<Drone>();
        var locations = new List<Location>();
        var result = (drones, locations);

        FileInfo file = new FileInfo(path);
        FileStream fileStream = file.Open(FileMode.OpenOrCreate, FileAccess.Read , FileShare.Read);
        StreamReader streamReader = new StreamReader(fileStream);
        while(!streamReader.EndOfStream){
            string line = streamReader.ReadLine();
        }
        streamReader.Close();
        fileStream.Close();
        return result;
    }
}