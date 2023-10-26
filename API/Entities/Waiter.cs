using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace API;

public class Waiter
{
    public int Id { get; set; }
    [Required]
    public string IdentityUserId { get; set; }
    public IdentityUser IdentityUser { get; set; }
    public string RestaurantName { get; set; }
    public int YearsOfExpirience { get; set; }
    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; }
}
