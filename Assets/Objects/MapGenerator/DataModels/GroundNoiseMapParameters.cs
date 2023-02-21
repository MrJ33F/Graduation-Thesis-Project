using System;
using UnityEngine;

namespace MapGenerator.Models
{
    [Serializable]
    public class GroundNoiseMapParameters : NoiseMapParameters
    {
        [Range(0, 1)]
        public float minValue = 0;

        [Range(0, 1)]
        public float maxValue = 1;

        public new BaseModels.GroundNoiseMapParametersModel ToModel()
        {
            return new BaseModels.GroundNoiseMapParametersModel
            {
                Octaves = octaves,
                Frequency = frequency,
                TargetValue = CalculateTargetValue(),
                MinValue = minValue,
                MaxValue = maxValue
            };
        }
    }
}
