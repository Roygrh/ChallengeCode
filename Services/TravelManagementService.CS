using ChallengeCode.DTO;
using System.Linq;

namespace ChallengeCode.Services{
    public class TravelManagementService{

        private (List<Drone>, List<Location>) _inputs { get; set;}
        private List<Trip> _trips { get; set;}
        private InputOutSupplierService _inputOutSupplierService { get; set;}
        private DataTransformerService _dataTransformerService { get; set;}

        public TravelManagementService(InputOutSupplierService inputOutSupplierService, DataTransformerService dataTransformerService){
            this._inputOutSupplierService = inputOutSupplierService;
            this._dataTransformerService = dataTransformerService;
        }

        public TravelManagementService LoadRawDataFromFile (string path){
            var rawData = this._inputOutSupplierService.ProvideInput(path);
            this._inputs = this._dataTransformerService.LoadStringList(rawData).GenerateInputTuple().GetInputTuple();
            return this;
        }

        public TravelManagementService SaveOutputToFile (string pathOut){
            var tripString = this._dataTransformerService.TransformTripsToStringList(this._trips);
            this._inputOutSupplierService.ProvideOutput(pathOut, tripString);
            return this;
        }

        public TravelManagementService LoadRawData (List<string> rawData)
        {
            this._inputs = this._dataTransformerService.LoadStringList(rawData).GenerateInputTuple().GetInputTuple();
            return this;
        }

        public List<Trip> GetTrips() {
            return this._trips;
        }

        public TravelManagementService GenerateTrips()
        {

            var trips = new List<Trip>();

            var drones = this._inputs.Item1.ToArray().ToList();
            var locations = this._inputs.Item2.ToArray().ToList();

            while (locations.Count > 0){
                drones = this._inputs.Item1.ToArray().ToList();
                bool flag = true;
            while (locations.Count > 0 && drones.Count() > 0 && flag)
            {
                var tmpLocations = locations.ToArray().ToList();
                var MaxLocation = tmpLocations.MaxBy(e => e.PackageWeight);
                var tmpDrones = drones.Where(e => e.MaximumWeight >= MaxLocation.PackageWeight).ToList();
                if (tmpDrones.Count() == 0) flag = false;
                Trip trip = null;
                if (tmpDrones.Count() == 1){
                    trip = new Trip();
                    trip.Drone = tmpDrones.First();
                    trip.Locations.Add(MaxLocation);
                    tmpLocations.Remove(MaxLocation);
                    
                    Location[] restOfLocations = MaximumNumberOfLocations(trip.Drone.MaximumWeight - MaxLocation.PackageWeight, tmpLocations.ToArray());
                    trip.Locations.AddRange(restOfLocations);
                    trips.Add(trip);

                    tmpDrones.Remove(trip.Drone);
                    tmpLocations.RemoveAll(l => restOfLocations.Contains(l));
                    drones.Remove(trip.Drone);
                    locations.Remove(MaxLocation);
                    locations.RemoveAll(l => restOfLocations.Contains(l));
                }
                else {
                    trip = GetDroneWithMoreLocations(tmpDrones, tmpLocations);
                    if (trip != null){
                        trips.Add(trip);
                        tmpDrones.Remove(trip.Drone);
                        tmpLocations.RemoveAll(l => trip.Locations.Contains(l));
                        drones.Remove(trip.Drone);
                        locations.RemoveAll(l => trip.Locations.Contains(l));
                    }
                }
            }
            }
            this._trips = trips.OrderBy(t => t.Drone.Name).ToList();

            return this;
        }

        public Trip GetDroneWithMoreLocations (List<Drone> drones, List<Location> locations)
        {
            Trip trip = null;
            Location[] selectedLocations = {};
            drones.ForEach(drone => {
                var tmpLocations = MaximumNumberOfLocations(drone.MaximumWeight, locations.ToArray());
                if (selectedLocations.Count() < tmpLocations.Count())
                {
                    trip = new Trip();
                    selectedLocations = tmpLocations;
                    trip.Locations = selectedLocations.ToList();
                    trip.Drone = drone;
                }
            });
            return trip;
        }

        public Location[] MaximumNumberOfLocations(int amount, Location[] locations)
        {
            locations = locations.OrderByDescending(l => l.PackageWeight).ToArray();
            Location[] maxSubsequence = {};
            for (int i = 0; i < locations.Length; i++)
            {
                int currentAmount = locations[i].PackageWeight;
                Location[] currentSubsequence = new Location[] { locations[i] };
                for (int j = i + 1; j < locations.Length; j++)
                {
                    if (currentAmount + locations[j].PackageWeight <= amount)
                    {
                        currentAmount += locations[j].PackageWeight;
                        currentSubsequence = currentSubsequence.Concat(new Location[] { locations[j] }).ToArray();
                    }
                }
                if (currentSubsequence.Length > maxSubsequence.Length)
                {
                    maxSubsequence = currentSubsequence;
                }
            }
            return maxSubsequence;
        }
    }
}