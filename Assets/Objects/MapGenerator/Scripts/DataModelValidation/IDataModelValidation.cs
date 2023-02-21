using System.Collections.Generic;

namespace MapGenerator.ModelValidator{
    public interface IDataModelValidation{
        IEnumerable<ValidationError> Validate();
    }
}