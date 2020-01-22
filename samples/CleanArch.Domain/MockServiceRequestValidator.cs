using FluentValidation;

namespace CleanArch.Domain
{
    public class MockServiceRequestValidator : AbstractValidator<MockServiceRequest>
    {
        public MockServiceRequestValidator()
        {
            RuleFor(x => x.Value)
                .GreaterThan(0);
        }
    }
}