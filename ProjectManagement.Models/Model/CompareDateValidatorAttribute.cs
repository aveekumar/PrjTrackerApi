using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace ProjectManagement.Models
{
    /// <summary>
    /// Date validation. Project End Date cannot be greater than Project Start Date.
    /// </summary>
    public sealed class CompareDateValidatorAttribute : ValidationAttribute
    {
        private string _dateToCompare;
        private const string _errorMessage = "{0} should be greater than {1}";

        public CompareDateValidatorAttribute(string dateToCompare) : base(_errorMessage)
        {
            _dateToCompare = dateToCompare;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(_errorMessage, name, _dateToCompare);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dateToCompare = validationContext.ObjectType.GetProperty(_dateToCompare);
            var dateToCompareValue = dateToCompare.GetValue(validationContext.ObjectInstance, null);
            if (dateToCompareValue != null && value != null && (DateTime)value <= (DateTime)dateToCompareValue)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            return null;
        }
    }

    /// <summary>
    /// Validate Allocation Percentage Value
    /// </summary>
    public sealed class ValidateAllocationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var regExPercentage = @"^(?:100|[1-9]?[0-9])%";
            if (!(Regex.IsMatch(Convert.ToString(value), regExPercentage)))
            {
                return new ValidationResult("Invalid Allocation Percentage.Valid allocation percentage value is: 30% or 70%.");
            }
            return null;
        }
    }

    /// <summary>
    /// Validate Skill Set - Member should posses at least 3 skillsets.
    /// </summary>
    public sealed class ValidateSkillSetAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var skillsets = value as List<Skillset>;
            if (!(skillsets.Count >= 3))
            {
                return new ValidationResult("Member should possess at least 3 Skillsets.");
            }
            return null;
        }
    }

    public sealed class CompareTaskDateValidatorAttribute : ValidationAttribute
    {
        private string _dateToCompare;
        private const string _errorMessage = "{0} should be greater than {1}";

        public CompareTaskDateValidatorAttribute(string dateToCompare) : base(_errorMessage)
        {
            _dateToCompare = dateToCompare;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(_errorMessage, name, _dateToCompare);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dateToCompare = validationContext.ObjectType.GetProperty(_dateToCompare);
            var dateToCompareValue = dateToCompare.GetValue(validationContext.ObjectInstance, null);
            if (dateToCompareValue != null && value != null && (DateTime)value <= (DateTime)dateToCompareValue)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            return null;
        }
    }
}
