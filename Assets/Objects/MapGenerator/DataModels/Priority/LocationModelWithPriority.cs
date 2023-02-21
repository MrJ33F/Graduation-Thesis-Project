using System;
using System.Collections.Generic;

namespace MapGenerator.Models
{
    [Serializable]
    public class LocationModelWithPriority : ModelWithPriority<LocationModel>, ModelValidator.IDataModelValidation
    {
        public BaseModels.PriorityModel<BaseModels.LocationModel> ToModel()
        {
            return new BaseModels.PriorityModel<BaseModels.LocationModel>()
            {
                Priority = priority,
                Model = model.ToModel()
            };
        }

        public IEnumerable<ModelValidator.ValidationError> Validate()
        {
            ModelValidator.DataModelValidator dataModelValidator = new ModelValidator.DataModelValidator();
            dataModelValidator.ValidateModel(model, "Model");

            return dataModelValidator.Errors;
        }
    }
}