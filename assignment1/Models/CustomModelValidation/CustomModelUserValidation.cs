using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace assignment1.Models.CustomModelValidation
{
    public class CustomModelUserValidation: ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            //Regex dateValidation = new Regex( );
            
            return base.IsValid(value);
        }
    }
}
