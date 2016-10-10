using GroupOneToDo.Commons;

namespace GroupOneToDo.Model
{
    public interface IPrincipal : IIdentifiable<string>
    {

        string FullName { get; set; }

    }
}
