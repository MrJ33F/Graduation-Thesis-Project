namespace MapGenerator.BaseModels
{
    /// <summary>
    /// Date complete despre un tile al hartii.
    /// </summary>
    public class Tile
    {
        public BiomeModel Biome { get; set; }
        public BiomeModel WaterBiome { get; set; }

        public bool HasWaterBiome
        {
            get { return WaterBiome != null; }
        }
        public float WaterDeepness { get; set; }
        public float Temperature { get; set; }
        public float Height { get; set; }

        public BiomeModel GetBiomeModel()
        {
            if (HasWaterBiome)
                return WaterBiome;
            else
                return Biome;
        }
    }
}