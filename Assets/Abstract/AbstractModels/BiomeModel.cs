using System.Collections.Generic;

namespace MapGenerator.BaseModels{
    public class BiomeModel{
        public AbstractObjectModel Ground {get; set;}
    
        public float TreeIntensity { get; set; }
        public List<PriorityModel> Priorities { get; set; }

        public float ObjectsIntesity { get; set; }
        public List<PriorityModel<ObjectModel>> Objects {get; set; }

        public float LocationsIntensity { get; set; }
        public List<PriorityModel<LocationModel>> Locations { get; set; }

    }
}