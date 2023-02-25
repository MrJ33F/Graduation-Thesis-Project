using System.Collections.Generic;
using UnityEngine;

namespace MapGenerator.Models{
   [CreateAssetMenu(menuName = "MapGenerator/Fence")]
   public class FenceModel : ScriptableObject, ModelValidator.IDataModelValidation{
        public GameObject topLeft;
        public GameObject topLeftInside;
        public GameObject topMiddle;
        public GameObject topRight;
        public GameObject topRightInside;

        public GameObject middleLeft;
        public GameObject middleRight;

        public GameObject bottomLeft;
        public GameObject bottomLeftInside;
        public GameObject bottomMiddle;
        public GameObject bottomRight;
        public GameObject bottomRightInside;

        public BaseModels.FenceModel ToModel()
        {
            return new BaseModels.FenceModel()
            {
                TopLeft = new BaseModels.AbstractObjectModel() { AbstractObject = topLeft },
                TopLeftInside = new BaseModels.AbstractObjectModel { AbstractObject = topLeftInside },
                TopMiddle = new BaseModels.AbstractObjectModel { AbstractObject = topMiddle },
                TopRight = new BaseModels.AbstractObjectModel { AbstractObject = topRight },
                TopRightInside = new BaseModels.AbstractObjectModel { AbstractObject = topRightInside },
                MiddleLeft = new BaseModels.AbstractObjectModel { AbstractObject = middleLeft },
                MiddleRight = new BaseModels.AbstractObjectModel { AbstractObject = middleRight },
                BottomLeft = new BaseModels.AbstractObjectModel { AbstractObject = bottomLeft },
                BottomLeftInside = new BaseModels.AbstractObjectModel { AbstractObject = bottomLeftInside },
                BottomMiddle = new BaseModels.AbstractObjectModel { AbstractObject = bottomMiddle },
                BottomRight = new BaseModels.AbstractObjectModel { AbstractObject = bottomRight },
                BottomRightInside = new BaseModels.AbstractObjectModel { AbstractObject = bottomRightInside }
            };
        }

        public IEnumerable<ModelValidator.ValidationError> Validate()
        {
            ModelValidator.DataModelValidator dataModelValidator = new ModelValidator.DataModelValidator();
            dataModelValidator.ValidateProperty(topLeft, "TopLeft");
            dataModelValidator.ValidateProperty(topLeftInside, "TopLeftInside");
            dataModelValidator.ValidateProperty(topMiddle, "TopMiddle");
            dataModelValidator.ValidateProperty(topRight, "TopRight");
            dataModelValidator.ValidateProperty(topRightInside, "TopRightInside");
            dataModelValidator.ValidateProperty(middleLeft, "MiddleLeft");
            dataModelValidator.ValidateProperty(middleRight, "MiddleRight");
            dataModelValidator.ValidateProperty(bottomLeft, "BottomLeft");
            dataModelValidator.ValidateProperty(bottomLeftInside, "BottomLeftInside");
            dataModelValidator.ValidateProperty(bottomMiddle, "BottomMiddle");
            dataModelValidator.ValidateProperty(bottomRight, "BottomRight");
            dataModelValidator.ValidateProperty(bottomRightInside, "BottomRightInside");

            return dataModelValidator.Errors;
        }
    }
} 
