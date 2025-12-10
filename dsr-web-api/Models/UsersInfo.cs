using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dsr_web_api.Models;

public class UsersInfo : BaseModel
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public required string FirtName { get; set; }
    [Required]
    public required string LastName { get; set; }
    [Required]
    public required string Email { get; set; }
    [Required]
    public required string Mobile { get; set; }
    [Required]
    public required int UserRoleId { get; set; }
    [Required]
    public required string UserName { get; set; }
    [Required]
    public required string PasswordHash { get; set; }
    public bool IsActive { get; set; }
    public DateTime LoginDateTime { get; set; }
    public DateTime LastLoginDateTime { get; set; }
}
