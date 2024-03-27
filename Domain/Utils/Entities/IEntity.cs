namespace Domain.Utils.Entities
{
    public interface IEntity<out TKey>
    {
        TKey Id { get; }
    }
}