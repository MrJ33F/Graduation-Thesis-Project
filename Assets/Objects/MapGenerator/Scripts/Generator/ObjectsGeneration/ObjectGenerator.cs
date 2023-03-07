using System;
using System.Linq;
using System.Collections.Generic;

using MapGenerator.BaseModels;

namespace MapGenerator.Generator{
    public class ObjectGenerator{
        private readonly Random random;
        private readonly TilesMap map;
        private readonly ObjectsMap objectsMap;
        private readonly LocationsMap locationsMap;

        private List<AwaitingObject> awaitingObjects = new List<AwaitingObject>();
        private RouletteWheelSelector routerWheelSelector;

        public ObjectGenerator(TilesMap map, Random random){
            this.random = random;
            this.map = map;

            routerWheelSelector = new RouletteWheelSelector(random);
            objectsMap = new ObjectsMap(map.Width, map.Height);
            locationsMap = new LocationsMap(map.Width, map.Height);
            locationsMap.MarkBlockedTiles(map);
        }

        public List<AwaitingObject> Generate(){
            GenerateLocations();
            GenerateTrees();
            GenerateObjects();

            return awaitingObjects;
        }

        private void GenerateLocations()
        {
            LocationGenerator locationGenerator = new LocationGenerator(map, locationsMap, objectsMap, random);

            int minX = (int)(objectsMap.Width * 0.05), maxX = (int)(objectsMap.Width * 0.95);
            int minY = (int)(objectsMap.Height * 0.05), maxY = (int)(objectsMap.Height * 0.95);

            for (int i = minY; i < maxY; ++i)
            {
                for (int j = minX; j < maxX; ++j)
                {
                    BiomeModel biom = map[i, j].GetBiomeModel();

                    if (biom.Locations.Any() && 
                        locationsMap.CanGenerateIn(new Vector2Int(j,i)) &&
                        random.Next(10000) / 100f < biom.LocationsIntensity)
                    {
                        LocationModel locationDataModel = routerWheelSelector.RouletteWheelSelection(biom.Locations);
                        
                        LocationInstance location = locationGenerator.GenerateLocation(locationDataModel, new Vector2Int(j, i));
                        if(location != null)
                        {
                            awaitingObjects.AddRange(location.BigObjects);
                            awaitingObjects.AddRange(location.Objects);
                            awaitingObjects.AddRange(location.Fence);

                            locationsMap.MarkLocation(location);
                        }  
                    }
                }
            }
        }

        private void GenerateTrees()
        {
            for (int i = 0; i < objectsMap.Height; ++i)
            {
                for (int j = 0; j < objectsMap.Width; j++)
                {
                    if (objectsMap.AreNeighboringTilesEmpty(new Vector2Int(j, i)) &&
                        map[i, j].GetBiomeModel().Trees.Any() && 
                        random.Next(10000) / 100f < map[i, j].GetBiomeModel().TreesIntensity)
                    {           
                        TreeModel model = routerWheelSelector.RouletteWheelSelection(map[i, j].GetBiomeModel().Trees);

                        AwaitingObject awaitingObject = new AwaitingObject()
                        {
                            AbstractObject = model.AbstractObject,
                            Position = new Vector2Float(j, i),
                            Scale = random.Next((int)(model.MinScale * 100), (int)(model.MaxScale * 100)) / 100f
                        };

                        awaitingObjects.Add(awaitingObject);
                        objectsMap.MarkTiles(new Vector2Int(j,i), ObjectsMap.ObjectType.Tree);
                    }
                }
            }
        }

        private void GenerateObjects()
        {
            for (int i = 0; i < objectsMap.Height; ++i)
            {
                for (int j = 0; j < objectsMap.Width; j++)
                {
                    BiomeModel biom = map[i, j].GetBiomeModel();

                    if (objectsMap.IsTileEmpty(new Vector2Int(j, i)) &&
                        biom.Objects.Any() &&
                        random.Next(10000) / 100f < biom.ObjectsIntensity)
                    {                    
                        ObjectModel objectModel = routerWheelSelector.RouletteWheelSelection(biom.Objects);

                        AwaitingObject awaitingObject = new AwaitingObject()
                        {
                            AbstractObject = objectModel.AbstractObject,
                            Position = new Vector2Float(j, i)
                        };

                        awaitingObjects.Add(awaitingObject);
                        objectsMap.MarkTiles(new Vector2Int(j,i), ObjectsMap.ObjectType.Object);
                    }
                }
            }
        }
    }
}