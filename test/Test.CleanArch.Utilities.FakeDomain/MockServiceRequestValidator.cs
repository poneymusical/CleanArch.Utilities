using FluentValidation;

namespace Test.CleanArch.Utilities.FakeDomain
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