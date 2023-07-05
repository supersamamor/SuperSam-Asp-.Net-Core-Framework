using FluentValidation;
using FluentValidation.Results;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;

namespace CNPlaceHolder.Common.Utility.Validators;

/// <summary>
/// A class for collecting the errors from a collection
/// of <see cref="IValidator{T}"/>.
/// </summary>
/// <typeparam name="T"></typeparam>
public class CompositeValidator<T> : IValidator<T>
{
    readonly IEnumerable<IValidator<T>> _validators;

    /// <summary>
    /// Initializes an instance of <see cref="CompositeValidator{T}"/>
    /// from a collection of <see cref="IValidator{T}"/>.
    /// </summary>
    /// <param name="validators"></param>
    public CompositeValidator(IEnumerable<IValidator<T>> validators)
    {
        _validators = validators;
    }

    /// <summary>
    ///  Checks to see whether the validator can validate objects of the specified type.
    ///  NOTE: Method is not implemented.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public bool CanValidateInstancesOfType(Type type)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Creates a hook to access various meta data properties.
    /// NOTE: Method is not implemented.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public IValidatorDescriptor CreateDescriptor()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Given a collection of <see cref="IValidator{T}"/>, 
    /// use each validator to validate the specified instance
    /// and collect the errors.
    /// </summary>
    /// <param name="instance"></param>
    /// <returns></returns>
    public ValidationResult Validate(T instance)
    {
        var failures = _validators.Select(v => v.Validate(instance))
                                  .SelectMany(validationResult => validationResult.Errors)
                                  .Where(f => f != null)
                                  .ToList();

        return new ValidationResult(failures);
    }

    /// <summary>
    /// Given a collection of <see cref="IValidator{T}"/>, 
    /// use each validator to validate the specified instance
    /// and collect the errors.
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public ValidationResult Validate(IValidationContext context)
    {
        var failures = _validators.Select(v => v.Validate(context))
                                  .SelectMany(validationResult => validationResult.Errors)
                                  .Where(f => f != null)
                                  .ToList();

        return new ValidationResult(failures);
    }

    /// <summary>
    /// Given a collection of <see cref="IValidator{T}"/>, 
    /// use each validator to validate the specified instance
    /// and collect the errors.
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Given a collection of <see cref="IValidator{T}"/>, 
    /// use each validator to validate the specified instance
    /// and collect the errors.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Given a collection of <see cref="IValidator{T}"/>, 
    /// use each validator to validate the specified instance
    /// and collect the errors.
    /// </summary>
    /// <param name="instance"></param>
    /// <returns></returns>
    public Validation<Error, T> ValidateT(T instance)
    {
        var result = Validate(instance);
        return result.IsValid ? instance : Fail<Error, T>(result.Errors.Select(e => Error.New(e.ErrorMessage)).ToSeq());
    }

    /// <summary>
    /// Given a collection of <see cref="IValidator{T}"/>, 
    /// use each validator to validate the specified instance
    /// and collect the errors.
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    public async Task<Validation<Error, T>> ValidateTAsync(T instance, CancellationToken cancellation = default)
    {
        var result = await ValidateAsync(instance, cancellation);
        return result.IsValid ? instance : Fail<Error, T>(result.Errors.Select(e => Error.New(e.ErrorMessage)).ToSeq());
    }
}