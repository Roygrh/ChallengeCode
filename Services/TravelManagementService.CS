using ChallengeCode.DTO;
using System.Linq;

namespace ChallengeCode.Services;

public class TravelManagementService{

    private (List<Drone>, List<Location>) _inputs { get; set;}

    public TravelManagementService((List<Drone>, List<Location>) inputs){
        this._inputs = inputs;
    }

    public TravelManagementService GetTrips(){

        var trips = new List<Trip>();

        var drones = new List<Drone>();
        drones.AddRange(this._inputs.Item1);
        var locations = new List<Location>();
        locations.AddRange(this._inputs.Item2);

        while(drones.Count() > 0 && locations.Count() > 0){

            Trip trip = new Trip();

            var MaximumWeight = drones.Max(e => e.MaximumWeight);
            var drone = drones.Find(e => e.MaximumWeight == MaximumWeight);

            trip.Drone = drone;

            double currentWeight = 0.0;

            while(locations.Count() > 0 && currentWeight < MaximumWeight) {

                var minWeight = locations.Min(e => e.PackageWeight);
                var location = locations.Find(e => e.PackageWeight == minWeight);

                if (currentWeight + minWeight <= MaximumWeight)
                {
                    trip.Locations.Add(location);
                    currentWeight += minWeight;
                    locations.Remove(location);
                }

            }

            if (trip.Locations.Count() > 0)
                trips.Add(trip);

            drones.Remove(drone);
        }

        return this;
    }
}