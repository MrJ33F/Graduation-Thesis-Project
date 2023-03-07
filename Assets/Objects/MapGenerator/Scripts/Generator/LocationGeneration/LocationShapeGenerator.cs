using System;
using System.Collections.Generic;

using MapGenerator.BaseModels;

namespace MapGenerator.Generator{
    public class LocationShapeGenerator{
        private readonly RouletteWheelSelector rouletteWheelSelector;
        private readonly LocationsMap locationsMap;
        private readonly TilesMap tilesMap;

        private Vector2Int startingPosition;

        private List<PriorityModel<Vector2Int>> possibleBlockPositions;

        public LocationShapeGenerator(TilesMap tileMap, LocationsMap locationsMap, Random random){
            this.tilesMap = tileMap;
            this.locationsMap = locationsMap;
            this.rouletteWheelSelector = new RouletteWheelSelector(random);
        }

        public bool[,] RandomLocationShape(Vector2Int startingPos, int locationSize)
        {
            this.startingPosition = startingPos;
            bool[,] locationShapeMap = new bool[locationsMap.Height, locationsMap.Width];

            possibleBlockPositions = new List<PriorityModel<Vector2Int>>()
            {
                new PriorityModel<Vector2Int> { Priority = 1, Model = startingPos }
            };

            while (locationSize > 0 && possibleBlockPositions.Count > 0)
            {
                Vector2Int selectedPosition = rouletteWheelSelector.RouletteWheelSelection(possibleBlockPositions);

                locationShapeMap[selectedPosition.Y, selectedPosition.X] = true;
                AddNeighbors(locationShapeMap, selectedPosition);
                possibleBlockPositions.Remove(possibleBlockPositions.Find(b => b.Model.Equals(selectedPosition)));

                locationSize--;
            }

            return locationShapeMap;
        }

        private void AddNeighbors(bool[,] placeShape, Vector2Int pos)
        {
            if (IsOnMap(new Vector2Int(-1, 0) + pos) && locationsMap.CanGenerateIn(new Vector2Int(pos.X - 1, pos.Y)) && !placeShape[pos.Y, pos.X - 1])
                AddOrUpdateNeighbor(new Vector2Int(-1, 0) + pos);

            if (IsOnMap(new Vector2Int(1, 0) + pos) && locationsMap.CanGenerateIn(new Vector2Int(pos.X + 1, pos.Y)) && !placeShape[pos.Y, pos.X + 1])
                AddOrUpdateNeighbor(new Vector2Int(1, 0) + pos);

            if (IsOnMap(new Vector2Int(0, -1) + pos) && locationsMap.CanGenerateIn(new Vector2Int(pos.X, pos.Y - 1)) && !placeShape[pos.Y - 1, pos.X])
                AddOrUpdateNeighbor(new Vector2Int(0, -1) + pos);

            if (IsOnMap(new Vector2Int(0, 1) + pos) && locationsMap.CanGenerateIn(new Vector2Int(pos.X, pos.Y + 1)) && !placeShape[pos.Y + 1, pos.X])
                AddOrUpdateNeighbor(new Vector2Int(0, 1) + pos);
        }

        private void AddOrUpdateNeighbor(Vector2Int neighborPos)
        {
            PriorityModel<Vector2Int> shapePoint = possibleBlockPositions.Find(point => point.Model.Equals(neighborPos));

            if (shapePoint == null)
            {
                PriorityModel<Vector2Int> possibleBlock = new PriorityModel<Vector2Int>
                {
                    Model = neighborPos,
                    Priority = (tilesMap[neighborPos.Y, neighborPos.X].Biome == tilesMap[startingPosition.Y, startingPosition.X].Biome) ? 4 : 1
                };

                possibleBlockPositions.Add(possibleBlock);
            }
            else
            {
                shapePoint.Priority++;
            }     
        }

        private bool IsOnMap(Vector2Int pos)
        {
            return pos.X >= 0 && pos.Y >= 0 && pos.X < locationsMap.Width && pos.Y < locationsMap.Height;
        }
    }
}