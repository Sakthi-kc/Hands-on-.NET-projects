using System.ComponentModel.DataAnnotations;

namespace Product_Management.Validator
{
    public class DTOValidatorAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            //keep this if it can be nullabale, check for non-null fields with [Required] in DTo
            if (value == null) return true;

            if (value is int i) return i >= 0;

            if (value is decimal d) return d >= 0;

            return false;
        }
    }
}
