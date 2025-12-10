using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dsr_web_api.Models;

public class AttandanceInfo : BaseModel
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public int UserId { get; set; }
    [Required]
    public DateTime AttandanceDate { get; set; }
    public bool IsPresent { get; set; }
    public bool IsDSRSent { get; set; }
}
