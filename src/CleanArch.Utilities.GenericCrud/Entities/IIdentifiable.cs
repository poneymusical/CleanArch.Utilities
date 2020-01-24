using System.Linq;

namespace CleanArch.Utilities.GenericCrud.Entities
{
    public interface IIdentifiable<T>
    {
        T Id { get; set; }
    }
}