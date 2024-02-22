namespace FSH.WebApi.Domain.Common.Contracts;

public abstract class AuditableEntity : AuditableEntity<DefaultIdType>
{
}

public abstract class AuditableEntity<T> : BaseEntity<T>, IAuditableEntity, ISoftDelete
{
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; private set; }
    public string LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public bool IsActive { get; set; }

    protected AuditableEntity()
    {
        CreatedOn = DateTime.UtcNow;
        IsActive = true;
        LastModifiedOn = DateTime.UtcNow;
    }
}