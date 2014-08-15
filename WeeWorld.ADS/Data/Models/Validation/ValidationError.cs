using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeeWorld.ADS.Models.Validation
{
    /// <summary>Class used for tracking validation errors</summary>
    public class ValidationError
    {

        public ValidationError(string field, string details)
        {
            Field = field;
            Details = details;
        }

        public string Field { get; set; }
        public string Details { get; set; }
    }
    
    ///<summary>Derived list of ValidationErrors with helper methods to make adding errors simpler</summary>
    public class ValidationErrorList : List<ValidationError>
    {
        public ValidationErrorList()
        {
        }

        public ValidationErrorList(string field, string details)
        {
            Add(field, details);
        }

        public void Add(string field, string details)
        {
            var error = new ValidationError(field, details);
            this.Add(error);
        }
    }


}