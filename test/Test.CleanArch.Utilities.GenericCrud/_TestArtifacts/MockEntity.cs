using System;
using CleanArch.Utilities.GenericCrud.Entities;

namespace Test.CleanArch.Utilities.GenericCrud._TestArtifacts
{
    public class MockEntity : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
    }
}
