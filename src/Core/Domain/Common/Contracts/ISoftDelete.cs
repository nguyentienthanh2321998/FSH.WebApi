namespace FSH.WebApi.Domain.Common.Contracts;

public interface ISoftDelete
{
   bool IsActive { get; set; }
}