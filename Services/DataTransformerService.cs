using System.Text;
using ChallengeCode.DTO;

namespace ChallengeCode.Services {
    public class DataTransformerService {
        private List<string> _rawData { get; set;}
        private (List<Drone>, List<Location>) _inputs { get; set;}

        public DataTransformerService LoadStringList(List<string> rawData) {
            this._rawData = rawData;
            return this;
        }

        public DataTransformerService GenerateInputTuple() {
            var rawDrones = this._rawData.FirstOrDefault();
            var drones = TransformStringToDroneList(rawDrones);
            var rawLocations = this._rawData.Skip(1).ToList();
            var locations = TransformStringToLocationList(rawLocations);
            this._inputs = (drones, locations);
            return this;
        }

        public (List<Drone>, List<Location>) GetInputTuple() {
            return this._inputs;
        }

        public List<Drone> TransformStringToDroneList(string locationStrings) {
            var drones = new List<Drone>();
            if (!string.IsNullOrEmpty(locationStrings))
            {
                List<string> rawDrones = locationStrings.Split(",").ToList();
                for (int i = 0; i < rawDrones.Count(); i+=2){
                    var drone = new Drone();
                    drone.Name = rawDrones[i].Trim();
                    var tmpString = rawDrones[i+1].Trim().Replace("[","").Replace("]","");
                    drone.MaximumWeight = Int32.Parse(tmpString);
                    drones.Add(drone);
                }
            }
            return drones;
        }

        public List<Location> TransformStringToLocationList(List<string> locationStrings) {
            var locations = new List<Location>();

            locationStrings.ForEach(l => {
                List<string> rawLocation = l.Split(",").ToList();
                var location = new Location();
                location.Name = rawLocation.First().Trim();
                location.PackageWeight = Int32.Parse(rawLocation.Last().Trim().Replace("[","").Replace("]",""));
                locations.Add(location);
            }); 

            return locations;
        }

        public List<string> TransformTripsToStringList(List<Trip> trips){
            var stringTrips = new List<string>();
            Trip previousTrip = null;
            int tripCounter = 0;
            trips.ForEach(t => {
                if (previousTrip == null || !t.Drone.Name.Equals(previousTrip.Drone.Name)){
                    tripCounter = 1;
                    stringTrips.Add(t.Drone.Name);
                }
                else{
                    tripCounter++;
                }
                stringTrips.Add("Trip #" + tripCounter);
                StringBuilder line =  new StringBuilder();
                t.Locations.ForEach(l => {
                    line.Append(l.Name);
                    if (t.Locations.Last() != l)
                        line.Append(", ");
                });
                stringTrips.Add(line.ToString());
                previousTrip = t;
            });
            return stringTrips;
        }
    }
}