using System;

namespace ArchitectureShowcase.UserMgmt.Models;
public class B2CApiConnectorInput
{
    public string Email { get; set; }
    public string DisplayName { get; set; }
    public Guid ObjectId { get; set; }
}
