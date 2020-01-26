using System;
using CleanArch.Utilities.GenericCrud.Services.Create;

namespace Test.CleanArch.Utilities.GenericCrud._TestArtifacts
{
    public class MockCreateRequest : ICreateRequest<MockEntity, Guid>
    {
        public string Value { get; set; }
    }
}
