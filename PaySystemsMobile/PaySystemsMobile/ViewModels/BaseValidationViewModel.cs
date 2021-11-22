using FluentValidation;
using PaySystemsMobile.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace PaySystemsMobile.ViewModels
{
    public class BaseValidationViewModel:BaseViewModel, INotifyDataErrorInfo
    {
        IDictionary<string, List<string>> _errors =
             new Dictionary<string, List<string>>();

        private readonly IValidator _validator;

        public BaseValidationViewModel(IValidator validator)
        {
            _validator = validator;
        }

        public bool HasErrors =>
           _errors?.Any(x => x.Value?.Any() == true) == true;

        public IEnumerable GetErrors(string propertyName)
        {
            if (_errors.ContainsKey(propertyName)
                && _errors[propertyName].Any())
            {
                return _errors[propertyName];
            }

            return new List<string>();
        }

        public void Validate(object validationContext, [CallerMemberName] string propertyName = "")
        {
            if (_validator == null)
            {
                throw new NullReferenceException("An instance of IValidator is required");
            }
            var context = new ValidationContext<object>(validationContext);
            var result = _validator.Validate(context);

            var errors =
                result.Errors.Where(e => e.PropertyName == propertyName)
                    .Select(em => em.ErrorMessage).ToList();

            if (_errors.ContainsKey(propertyName))
            {
                _errors.Remove(propertyName);
            }

            if (errors.Count > 0)
            {
                foreach (var message in errors)
                {
                    if (!_errors.ContainsKey(propertyName))
                    {
                        _errors.Add(propertyName,
                            new List<string>() { message });
                    }
                    else
                    {
                        _errors[propertyName].Add(message);
                    }
                }
            }
            ErrorsChanged?.Invoke(this,
                new DataErrorsChangedEventArgs(propertyName));
        }
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
    }
}
