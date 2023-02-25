using System;
using System.Collections.Generic;
using System.Linq;

namespace MapGenerator.Models
{
    [Serializable]
    public class WaterLayers : ModelValidator.IDataModelValidation
    {
        public List<WaterBiome> waterBiomes= new List<WaterBiome>();

        public void AddWaterLevel()
        {
            float newLevelThreshold = 0.5f;
            if (waterBiomes.Any())
                newLevelThreshold = waterBiomes.Last().waterThresholding + 0.01f;

            waterBiomes.Add(new WaterBiome { waterThresholding = newLevelThreshold });
        }

        public BaseModels.WaterBiomeModel[] ToModel()
        {
            return waterBiomes.Select(w => w.ToModel()).ToArray();
        }

        public IEnumerable<ModelValidator.ValidationError> Validate()
        {
            ModelValidator.DataModelValidator dataModelValidator = new ModelValidator.DataModelValidator();
            dataModelValidator.ValidateList(waterBiomes, "Water Level");
            dataModelValidator.Errors.AddRange(ValidateLevelsThresholding());

            return dataModelValidator.Errors;
        }

        private IEnumerable<ModelValidator.ValidationError> ValidateLevelsThresholding()
        {
            List<ModelValidator.ValidationError> errors = new List<ModelValidator.ValidationError>();
            for(int i=1; i<waterBiomes.Count; ++i)
            {
                if (waterBiomes[i - 1].waterThresholding >= waterBiomes[i].waterThresholding)
                    errors.Add(new ModelValidator.ValidationError(ModelValidator.ValidationErrorType.IncorrectData, $"Nivelul apei {i}", $"Limita a nivelului {i} trebuie sa fie mai mare decat cel precedent"));
            }

            return errors;
        }
    }
}
