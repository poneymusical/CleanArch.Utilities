using System;
using CleanArch.Utilities.GenericCrud.Services.Update;

namespace Test.CleanArch.Utilities.GenericCrud._TestArtifacts
{
    public class MockUpdateRequest : IUpdateRequest<MockEntity, Guid>
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
    }
}