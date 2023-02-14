using Microsoft.AspNetCore.Identity;

namespace Acorn.Domain.Entities.User;

public class User : IdentityUser
{
    #region Aggregate Root
    // we need this here because we can only have one base class
    // IdentityUser is required for the auth stuff to work
    public Guid CreatedBy { get; protected set; }
    public DateTime CreatedOn { get; protected set; } = DateTime.UtcNow;

    public void SetCreatedBy(Guid createdBy)
    {
        CreatedBy = createdBy;
    }
    #endregion
}