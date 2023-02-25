using System;
using UnityEngine;

namespace MapGenerator.Models
{
    [Serializable]
    public class WaterNoiseMapParameters : NoiseMapParameters
    {
        [Range(0, 1)]
        public float minWaterPercent = 0;

        [Range(0, 1)]
        public float maxWaterPercent = 1;

        public new BaseModels.WaterNoiseMapParametersModel ToModel()
        {
            return new BaseModels.WaterNoiseMapParametersModel
            {
                Octaves = octaves,
                Frequency = frequency,
                TargetValue = CalculateTargetValue(),
                MinWaterPercent = minWaterPercent,
                MaxWaterPercent = maxWaterPercent
            };
        }
    }
}
