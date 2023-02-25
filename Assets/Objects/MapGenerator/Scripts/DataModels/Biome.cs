using System.Linq;
using System.Collections.Generic;

using UnityEngine;


namespace MapGenerator.Models{
    public class Biome : ScriptableObject, ModelValidator.IDataModelValidation{
        public Sprite ground;

        [Range(0, 100)]
        public float treesIntensity;
        public List<TreeModelWithPriority> trees;

        [Range(0, 100)]
        public float objectsIntensity;
        public List<ObjectModelWithPriority> objects;

        [Range(0, 100)]
        public float locationsIntensity;
        public List<LocationModelWithPriority> locations;

        public BaseModels.BiomeModel ToModel()
        {
            return new BaseModels.BiomeModel
            {
                Ground = new BaseModels.AbstractObjectModel() { AbstractObject = ground },
                TreesIntensity = treesIntensity,
                Trees = trees.Select(t => t.ToModel()).ToList(),
                ObjectsIntensity = objectsIntensity,
                Objects = objects.Select(o => o.ToModel()).ToList(),
                LocationsIntensity = locationsIntensity,
                Locations = locations.Select(l => l.ToModel()).ToList()
            };
        }

        public IEnumerable<ModelValidator.ValidationError> Validate()
        {
            ModelValidator.DataModelValidator dataModelValidator = new ModelValidator.DataModelValidator();
            dataModelValidator.ValidateProperty(ground, "Ground");
            dataModelValidator.ValidateList(trees, "Trees");
            dataModelValidator.ValidateList(locations, "Locations");
            dataModelValidator.ValidateList(objects, "Objects");

            return dataModelValidator.Errors;
        }
    }
}