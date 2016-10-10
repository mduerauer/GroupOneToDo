namespace GroupOneToDo.Commons
{
    public interface IIdentifiable<out TId>
    {
        TId Id { get; }

    }
}
