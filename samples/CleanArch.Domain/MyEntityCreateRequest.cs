using CleanArch.Utilities.GenericCrud.Services.Create;
using FluentValidation;

namespace CleanArch.Domain
{
    public class MyEntityCreateRequest : ICreateRequest<MyEntity, int>
    {
        public string Value { get; set; }
    }

    public class MyEntityCreateRequestValidator : AbstractValidator<MyEntityCreateRequest>
    {
        public MyEntityCreateRequestValidator()
        {
            RuleFor(x => x.Value)
                .NotEmpty();
        }
    }
}