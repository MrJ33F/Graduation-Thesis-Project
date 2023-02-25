using System;
using System.Collections.Generic;

namespace MapGenerator.Models
{
    [Serializable]
    public class ObjectModelWithPriority : ModelWithPriority<ObjectModel>, ModelValidator.IDataModelValidation
    {
        public BaseModels.PriorityModel<BaseModels.ObjectModel> ToModel()
        {
            return new BaseModels.PriorityModel<BaseModels.ObjectModel>()
            {
                Model = model.ToModel(),
                Priority = priority
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
