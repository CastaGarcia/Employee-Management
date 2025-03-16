using FluentValidation;

namespace UsersManagementsEmployees.Web.Components.ViewModels
{
    public class RegistrationValidationVM: AbstractValidator<RegistrationVM>
    {
        public RegistrationValidationVM()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required");
            RuleFor(x => x.PassWord).NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters")
                .MaximumLength(20).WithMessage("Password must be at most 20 characters")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,20}$").WithMessage("Password must contain at least one uppercase letter, one lowercase letter, and one number")
                .NotEmpty().WithMessage("Password is required");
                RuleFor(x => x.ConfirmPassWord).Equal(x => x.PassWord).WithMessage("Passwords do not match");


        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<RegistrationVM>.CreateWithOptions((RegistrationVM)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
