using System;
using System.ComponentModel.DataAnnotations;

namespace dsr_web_api.Models;

public class BaseModel
{
    [Required]
    public bool IsDeleted { get; set; }

    [Required]
    public int CreatedBy { get; set; }

    [Required]
    public DateTime CreatedOn { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
}
