using UnityEngine;
using System.Linq;

using System.Collections.Generic;

namespace MapGenerator.Generator
{
    public class MapGeneratorTool : MonoBehaviour, ModelValidator.IDataModelValidation
    {
        public int width = 100;
        public int height = 100;

        public Models.WaterNoiseMapParameters waterNoiseMapParameters;
        public Models.GroundNoiseMapParameters heightNoiseMapParameters;
        public Models.GroundNoiseMapParameters temperatureNoiseMapParameters;

        public Models.BiomesDiagram biomesDiagram;
        public Models.WaterLayers waterLayers;

        public Graphical.GraphicalGenerationType generationType = Graphical.GraphicalGenerationType.TileMap;
        public Graphical.SpaceOrientationType orientationType = Graphical.SpaceOrientationType.Orientation3D;
        public bool generateOnStart = true;
        public bool generateRandomSeed = true;
        public int seed;

        public BaseModels.TilesMap Map { get; set; }

        private void Start()
        {
            if (generateOnStart)
            {
                if(generateRandomSeed)
                    RandomSeed();
                
                TryGenerate();
            }
        }

        public void RandomSeed()
        {
            seed = Random.Range(int.MinValue, int.MaxValue);
        }

        public void Clear()
        {
            for (int i = transform.childCount - 1; i >= 0; --i)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }

            Map = null;
        }

        public void TryGenerate()
        {
            IEnumerable<ModelValidator.ValidationError> errors = Validate();
            if(errors.Any())
            {
                foreach(var error in errors)
                    Debug.LogWarning(error);
            }
            else
            {
                Generate();
            }
        }

        private void Generate()
        {
            Clear();

            Generator generator = new Generator(width, height, seed, waterLayers.ToModel(), biomesDiagram.ToModel(),
                waterNoiseMapParameters.ToModel(), heightNoiseMapParameters.ToModel(), temperatureNoiseMapParameters.ToModel());

            generator.Generate();
            Map = generator.Map;

            Graphical.ISpaceOrientation spaceOrientation = new Graphical.SpaceOrientationFactory().GetSpaceOrientation(orientationType);

            Graphical.IGraphicalMapGenerator graphicalMapGenerator = new Graphical.GraphicalMapGeneratorFactory().GetGraphicalMapGenerator(generationType, spaceOrientation);
            graphicalMapGenerator.Render(transform, generator.Map);

            Graphical.ObjectsGenerator objectsGenerator = new Graphical.ObjectsGenerator(spaceOrientation);
            objectsGenerator.Render(transform, generator.AwaitingObjects);
        }  

        public IEnumerable<ModelValidator.ValidationError> Validate()
        {
            ModelValidator.DataModelValidator dataModelValidator = new ModelValidator.DataModelValidator();
            dataModelValidator.ValidateModel(biomesDiagram, "Biomes Diagram");
            dataModelValidator.ValidateModel(waterLayers, "Water Layers");

            return dataModelValidator.Errors;
        }
    }
}