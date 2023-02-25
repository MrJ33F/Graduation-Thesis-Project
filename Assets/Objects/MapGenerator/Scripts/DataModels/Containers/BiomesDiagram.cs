using System;
using System.Collections.Generic;

namespace MapGenerator.Models
{
    [Serializable]
    public class BiomesDiagram : ModelValidator.IDataModelValidation
    {
        public const int MaxXLayerCount = 6;
        public const int MaxYLayerCount = 6;

        public int temperatureLayerCount = 2;
        public int heightLayerCount = 3;

        public Biome[] biomes = new Biome[MaxXLayerCount * MaxYLayerCount];

        public Biome this[int i, int j]
        {
            get { return biomes[i * MaxYLayerCount + j]; }
            set { biomes[i * MaxYLayerCount + j] = value; }
        }

        public BaseModels.BiomeModel[,] ToModel()
        {
            BaseModels.BiomeModel[,] biomeArray = new BaseModels.BiomeModel[heightLayerCount, temperatureLayerCount];

            for (int i = 0; i < heightLayerCount; i++)
            {
                for (int j = 0; j < temperatureLayerCount; j++)
                {
                    biomeArray[i, j] = this[i,j].ToModel();
                }
            }

            return biomeArray;
        }

        public IEnumerable<ModelValidator.ValidationError> Validate()
        {
            List<ModelValidator.ValidationError> errors = new List<ModelValidator.ValidationError>();

            for (int i = 0; i < heightLayerCount; ++i)
            {
                for (int j = 0; j < temperatureLayerCount; ++j)
                {
                    string elementName = $"Diagrama biome (inaltime: {heightLayerCount - i} temperatura: {j+1})";

                    if (this[i, j] == null)
                        errors.Add(new ModelValidator.ValidationError(ModelValidator.ValidationErrorType.EmptyCollection, elementName));
                    else
                        errors.AddRange(ValidateCollection(this[i, j], elementName));
                }
            }

            return errors;
        }

        private IEnumerable<ModelValidator.ValidationError> ValidateCollection(ModelValidator.IDataModelValidation dataModel, string parent)
        {
            var childErrors = dataModel.Validate();

            foreach (var childError in childErrors)
                childError.CallStack.Push(parent);

            return childErrors;
        }
    }
}
