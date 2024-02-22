using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MassTransit;

namespace FSH.WebApi.Domain.Common.Contracts;

public abstract class BaseEntity : BaseEntity<int>
{
    protected BaseEntity() => Id = default!;
}

public abstract class BaseEntity<TId> : IEntity<int>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public int Id { get; protected set; } = default!;

}