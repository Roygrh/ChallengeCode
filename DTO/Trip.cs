using ChallengeCode.DTO;

namespace ChallengeCode.DTO;

public class Trip{
    public Drone Drone { get; set;}
    public List<Location> Locations { get; set;}

    public Trip(){
        this.Drone = new Drone();
        this.Locations = new List<Location>();
    }

    public Trip(Drone drone, List<Location> locations){
        this.Drone = drone;
        this.Locations = locations;
    }
}