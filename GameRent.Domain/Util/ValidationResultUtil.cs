using FluentValidation.Results;
using GameRent.Domain.Shared;
using System.Collections.Generic;
using System.Linq;

namespace GameRent.Domain.Util
{
    public static class ValidationResultUtil
    {
        public static IEnumerable<ValidationResultError> GetValidationResultErrors(ValidationResult validationResult)
        {
            return validationResult.Errors
                .Select(x => new ValidationResultError
                    { 
                        Property = x.PropertyName,
                        Error = x.ErrorMessage
                    }).ToList();
        }
    }
}