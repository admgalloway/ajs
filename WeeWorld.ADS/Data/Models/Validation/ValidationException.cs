using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeeWorld.ADS.Models.Validation
{
    
    /// <summary>Dedicated exception type used when validation errors are detected whilst
    /// saving / creating objects</summary>
    public class ValidationException : Exception
    {
        public ValidationException(ValidationErrorList errors) 
            : base("Validation Error(s). Inspect error list for details")
        {
            Errors = errors;
        }

        public ValidationException(string field, string details) : this(new ValidationErrorList(field, details))
        {
        }

        public ValidationErrorList Errors;
    }
}