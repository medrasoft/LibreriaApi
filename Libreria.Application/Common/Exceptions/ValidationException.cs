using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Libreria.Application.Common.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public ValidationException(): base("Se presentaron unos o mas errores de validacion") 
        { 
            Errors=new Dictionary<string, string[ ]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            Errors = failures
                .GroupBy(m => m.PropertyName , e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key , failureGroup => failureGroup.ToArray());

        }

        public IDictionary<string , string[ ]> Errors { get; set; }
    }
}
