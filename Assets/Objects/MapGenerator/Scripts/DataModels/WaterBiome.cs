using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapGenerator.Models
{
    [Serializable]
    public class WaterBiome : ModelValidator.IDataModelValidation
    {
        [Range(0, 1)]
        [Tooltip("Limita la care se va genera un Biome de tip acvatic.")]
        public float waterThresholding = 0.5f;

        [Tooltip("Biome-ul generat la Biomeul de apa dat.")]
        public Biome biome;

        public BaseModels.WaterBiomeModel ToModel()
        {
            return new BaseModels.WaterBiomeModel
            {
                Biome = biome.ToModel(),
                WaterThresholding = waterThresholding
            };
        }

        public IEnumerable<ModelValidator.ValidationError> Validate()
        {
            ModelValidator.DataModelValidator dataModelValidator = new ModelValidator.DataModelValidator();
            dataModelValidator.ValidateModel(biome, "Biome");

            return dataModelValidator.Errors;
        }
    }
}
