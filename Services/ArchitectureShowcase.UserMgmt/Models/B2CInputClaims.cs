namespace ArchitectureShowcase.UserMgmt.Models;

public class B2CInputClaims
{
    // Demo: User's object id in Azure AD B2C
    public B2CInputClaims(string objectId, string email)
    {
        ObjectId = objectId;
        Email = email;
    }

    public string ObjectId { get; set; }

    public string Email { get; set; }

}
