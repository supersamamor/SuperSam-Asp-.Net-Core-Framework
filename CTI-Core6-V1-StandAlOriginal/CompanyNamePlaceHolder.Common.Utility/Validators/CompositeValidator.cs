using FluentValidation;
using FluentValidation.Results;
using LanguageExt;
using LanguageExt.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.Common.Utility.Validators
{
    public class CompositeValidator<T> : IValidator<T>
    {
        readonly IEnumerable<IValidator<T>> _validators;

        public CompositeValidator(IEnumerable<IValidator<T>> validators)
        {
            _validators = validators;
        }

        public bool CanValidateInstancesOfType(Type type)
        {
            throw new NotImplementedException();
        }

        public IValidatorDescriptor CreateDescriptor()
        {
            throw new NotImplementedException();
        }

        public ValidationResult Validate(T instance)
        {
            var failures = _validators.Select(v => v.Validate(instance))
                                      .SelectMany(validationResult => validationResult.Errors)
                                      .Where(f => f != null)
                                      .ToList();

            return new ValidationResult(failures);
        }

        public ValidationResult Validate(IValidationContext context)
        {
            var failures = _validators.Select(v => v.Validate(context))
                                      .SelectMany(validationResult => validationResult.Errors)
                                      .Where(f => f != null)
                                      .ToList();

            return new ValidationResult(failures);
        }

        public async Task<ValidationResult> ValidateAsync(T instance, CancellationToken cancellation = default)
        {
            var validationResults = new List<ValidationResult>();
            foreach (var validator in _validators)
            {
                var res = await validator.ValidateAsync(instance, cancellation);
                validationResults.Add(res);
            }
            return new ValidationResult(validationResults.SelectMany(vr => vr.Errors));
        }

        public async Task<ValidationResult> ValidateAsync(IValidationContext context, CancellationToken cancellation = default)
        {
            var validationResults = new List<ValidationResult>();
            foreach (var validator in _validators)
            {
                var res = await validator.ValidateAsync(context, cancellation);
                validationResults.Add(res);
            }
            return new ValidationResult(validationResults.SelectMany(vr => vr.Errors));
        }

        public Validation<Error, T> ValidateT(T instance)
        {
            var result = Validate(instance);
            return result.IsValid ? instance : Fail<Error, T>(result.Errors.Select(e => Error.New(e.ErrorMessage)).ToSeq());
        }

        public async Task<Validation<Error, T>> ValidateTAsync(T instance, CancellationToken cancellation = default)
        {
            var result = await ValidateAsync(instance, cancellation);
            return result.IsValid ? instance : Fail<Error, T>(result.Errors.Select(e => Error.New(e.ErrorMessage)).ToSeq());
        }
    }
}