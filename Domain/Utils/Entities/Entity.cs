using System.ComponentModel.DataAnnotations;

namespace Domain.Utils.Entities
{
    public abstract class Entity<TKey> : IEntity<TKey>
        where TKey : notnull
    {
        [Key]
        public TKey Id { get; protected set; } = default!;
    }
}