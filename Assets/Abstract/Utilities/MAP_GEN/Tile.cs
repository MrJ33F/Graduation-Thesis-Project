public class Tile{
    public BiomeModel Biome { get; set; }
    public BiomeModel WaterBiome { get; set; }

    public bool HasWaterBiome {get {return WaterBiome!= null;}}
    public float WaterDepness { get; set; }
    public float Temperature { get; set; }
    public float Height { get; set; }

    public BiomeModel GetBiomeModel(){
        if(HasWaterBiome) return WaterBiome;
        else return Biome;
    }
}