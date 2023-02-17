using System.Collections.Generic;

public interface IDataModelValidation{
    IEnumerable<ValidationDataError> Validate();
}