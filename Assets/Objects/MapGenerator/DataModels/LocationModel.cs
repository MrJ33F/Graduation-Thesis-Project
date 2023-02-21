using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MapGenerator.Models
{
    [CreateAssetMenu(menuName = "Map Generator/Location")]
    public class LocationModel : ScriptableObject, ModelValidator.IDataModelValidation
    {
        public int minSize = 20;
        public int maxSize = 80;

        [Space]
        [Range(0, 100)]
        public float chanceForFence;
        public List<FenceModelWithPriority> fences;

        [Space]
        [Range(0, 100)]
        public float bigObjectsIntensity;
        public List<BigObjectModelWithPriority> bigObjects;

        [Space]
        [Range(0, 100)]
        public float objectsIntensity;
        public List<ObjectModelWithPriority> objects;

        public BaseModels.LocationModel ToModel()
        {
            return new BaseModels.LocationModel()
            {
                MinSize = minSize,
                MaxSize = maxSize,
                ChanceForFence = chanceForFence,
                Fences = fences.Select(f => f.ToModel()).ToList(),
                BigObjectsIntensity = bigObjectsIntensity,
                BigObjects = bigObjects.Select(b => b.ToModel()).ToList(),
                ObjectsIntensity = objectsIntensity,
                Objects = objects.Select(o => o.ToModel()).ToList()
            };
        }

        public IEnumerable<ModelValidator.ValidationError> Validate()
        {
            ModelValidator.DataModelValidator dataModelValidator = new ModelValidator.DataModelValidator();
            dataModelValidator.ValidateList(fences, "Fences");
            dataModelValidator.ValidateList(bigObjects, "BigObjects");
            dataModelValidator.ValidateList(objects, "Objects");

            return dataModelValidator.Errors;
        }
    }
}