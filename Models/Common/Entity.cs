using System.ComponentModel.DataAnnotations;

namespace Models.Common;

public abstract class Entity : Entity<int> { }

public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
{
    [Key]
    public virtual TPrimaryKey Id { get; set; } = default!;
}
