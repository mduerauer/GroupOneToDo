namespace GroupOneToDo.Model
{
    public class User : IPrincipal
    {
        public string Id { get; }
        public string FullName { get; set; }

        public User(string id)
        {
            Id = id;
        }

    }
}
