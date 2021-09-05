using System;
using System.ComponentModel.DataAnnotations;

namespace MessageLogger.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class NotDefaultAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage = "The {0} field must be set. Default value is not allowed.";
        public NotDefaultAttribute() : base(DefaultErrorMessage) { }

        public override bool IsValid(object value)
        {
            return value switch
            {
                Guid guid => guid != Guid.Empty,
                DateTime date => date != DateTime.MinValue,
                object @object => @object is not null,
                _ => true // Create rule for all value types
            };
        }
    }
}
