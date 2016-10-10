namespace GroupOneToDo.Commons
{
    public interface IAggregateRoot<out TId> : IIdentifiable<TId>
    {
        
    }
}
