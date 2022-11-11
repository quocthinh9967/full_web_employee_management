using BE_TEST.Infrastructure;
using BE_TEST.Web.Models;
using FluentValidation;

namespace BE_TEST.Web.Validators
{
    public class EmployeeViewModelValidator : AbstractValidator<EmployeeViewModel>
    {
        public EmployeeViewModelValidator(TESTContext context)
        {
            RuleFor(x => x.Name).NotEmpty()
                .MaximumLength(30)
                .Must((model, value, x) =>
                {
                    return !(context.Employees!.Any(c => c.Name == value &&
                                                            c.Id != model.Id));
                })
                .WithMessage("Names be unique");

            RuleFor(x => x.Address).NotEmpty()
                                .MaximumLength(255);

            RuleFor(x => x.Email).NotEmpty()
                                .EmailAddress()
                                .MaximumLength(50)
                                .Must((model, value, x) =>
                                {
                                    return !(context.Employees!.Any(c => c.Email == value &&
                                                                            c.Id != model.Id));
                                })
                                .WithMessage("Email be unique");

            RuleFor(x => x.Phone).NotEmpty()
                                .MaximumLength(20)
                                .Must((model, value, x) =>
                                {
                                    return !(context.Employees!.Any(c => c.Phone == value &&
                                                                            c.Id != model.Id));
                                })
                                .WithMessage("Phone be unique");
        }
    }
}
