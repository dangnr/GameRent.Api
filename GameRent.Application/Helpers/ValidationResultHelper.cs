using FluentValidation.Results;
using GameRent.Application.Shared;
using System.Collections.Generic;
using System.Linq;

namespace GameRent.Application.Helpers
{
    public static class ValidationResultHelper
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