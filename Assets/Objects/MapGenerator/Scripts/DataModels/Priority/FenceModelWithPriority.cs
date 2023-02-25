using System;
using System.Collections.Generic;

namespace MapGenerator.Models
{
    [Serializable]
    public class FenceModelWithPriority : ModelWithPriority<FenceModel>, ModelValidator.IDataModelValidation
    {
        public BaseModels.PriorityModel<BaseModels.FenceModel> ToModel()
        {
            return new BaseModels.PriorityModel<BaseModels.FenceModel>()
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