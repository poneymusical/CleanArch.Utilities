using CleanArch.Utilities.GenericCrud.Entities;

namespace CleanArch.Domain
{
    public class MyEntity : IIdentifiable<int>
    {
        public int Id { get; set; }

        public string Value { get; set; }
    }
}