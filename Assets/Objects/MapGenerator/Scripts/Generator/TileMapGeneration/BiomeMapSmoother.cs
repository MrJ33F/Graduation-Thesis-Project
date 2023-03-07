using System.Linq;
using System.Collections.Generic;

namespace MapGenerator.Generator{
    public class BiomeMapSmoother{
        private BaseModels.TilesMap map;
        private Stack<BaseModels.Vector2Int> positionToCheck = new Stack<BaseModels.Vector2Int>();

        public void SmoothBiomeMap(BaseModels.TilesMap map){
            this.map = map;

            for(int i = 0; i < map.Height; ++i){
                for(int j = 0; j < map.Width; ++j){
                    positionToCheck.Push(new BaseModels.Vector2Int(j, i));
                }
            }

            int loopCount = map.Width * map.Height * 2;
            while(positionToCheck.Count > 0){
                SmoothBiome(positionToCheck.Pop());

                if(--loopCount < 0) break;
            }
        }

        private void SmoothBiome(BaseModels.Vector2Int position){
            int identicBiomes = 0;
            List<BaseModels.Vector2Int> neighbours = new List<BaseModels.Vector2Int>();
            BaseModels.Vector2Int last;

            if (map.IsOnMap(last = new BaseModels.Vector2Int(position.X - 1, position.Y)))
            {
                if (map[position.Y, position.X].Biome == map[position.Y, position.X - 1].Biome)
                    identicBiomes++;

                neighbours.Add(last);
            }

            if (map.IsOnMap(last = new BaseModels.Vector2Int(position.X, position.Y - 1)))
            {
                if (map[position.Y, position.X].Biome == map[position.Y - 1, position.X].Biome)
                    identicBiomes++;

                neighbours.Add(last);
            }

            if (map.IsOnMap(last = new BaseModels.Vector2Int(position.X + 1, position.Y)))
            {
                if (map[position.Y, position.X].Biome == map[position.Y, position.X + 1].Biome)
                    identicBiomes++;

                neighbours.Add(last);
            }

            if (map.IsOnMap(last = new BaseModels.Vector2Int(position.X, position.Y + 1)))
            {
                if (map[position.Y, position.X].Biome == map[position.Y + 1, position.X].Biome)
                    identicBiomes++;

                neighbours.Add(last);
            }

            if(identicBiomes < 2){
                BaseModels.BiomeModel mostFrequentBiome = neighbours.GroupBy(x => map[x.Y, x.X].Biome)
                                                          .Select(x => new { biome = x, cnt = x.Count() })
                                                          .OrderByDescending(g => g.cnt)
                                                          .Select(g => g.biome).First().Key;
                map[position.Y, position.X].Biome = mostFrequentBiome;
                neighbours.ForEach(x => positionToCheck.Push(x));
            }
        }
    }
}