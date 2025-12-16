using System;

namespace dsr_admin.Models;

public class RoleResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsDefault { get; set; }

}
