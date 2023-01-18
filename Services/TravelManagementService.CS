using ChallengeCode.DTO;

namespace ChallengeCode.Services;

public class TravelManagementService{

    private List<Drone> _drones { get; set; }
    private List<Location> _locations { get; set;}

    public TravelManagementService((List<Drone>, List<Location>) input){
        this._drones = input.Item1;
        this._locations = input.Item2;
    }

    public TravelManagementService SetLocations(){
        return this;
    }

    public TravelManagementService SetDrones(){
        return this;
    }

    public TravelManagementService GetTrips(){
        var maximumWeight = this._drones.Max(e => e.MaximumWeight);
        var droneMax = this._drones.Find(e => e.MaximumWeight == maximumWeight);
        var minimumWeight = this._locations.Min(e => e.PackageWeight);
        var location = this._locations.Find(e => e.PackageWeight == minimumWeight);
        return this;
    }
}