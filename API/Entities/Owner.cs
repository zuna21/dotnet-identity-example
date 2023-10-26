using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace API;

public class Owner
{
    public int Id { get; set; }
    [Required]
    public string IdentityUserId { get; set; }
    public IdentityUser IdentityUser { get; set; }
    public string RestaurantName { get; set; }
    public int EmployeesNumber { get; set; }
    public ICollection<Restaurant> Restaurants { get; set; }
}
