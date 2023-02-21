using System;
using UnityEngine;

namespace MapGenerator.Models
{
    [Serializable]
    public abstract class ModelWithPriority<T> where T : ModelValidator.IDataModelValidation
    {
        [Range(1, 100)]
        public int priority = 20;

        public T model;
    }
}