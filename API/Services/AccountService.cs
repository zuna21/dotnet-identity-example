using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API;

public class AccountService : IAccountService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly DataContext _context;
    public AccountService(
        UserManager<IdentityUser> userManager,
        DataContext context
    )
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<ServiceResponse<string>> LoginOwner(OwnerLoginDto ownerLoginDto)
    {
        var user = await _userManager.FindByNameAsync(ownerLoginDto.Username);
        if (user == null) return new ServiceResponse<string>
        {
            Status = ServiceResponseStatus.Unauthorized,
            Data = null,
            Message = "Unauthorized"
        };
        var result = await _userManager.CheckPasswordAsync(user, ownerLoginDto.Password);
        if(!result) return new ServiceResponse<string>
        {
            Status = ServiceResponseStatus.Unauthorized,
            Data = null,
            Message = "Unauthorized"
        };
        var owner = await _context.Owners.FirstOrDefaultAsync(x => x.IdentityUserId == user.Id);
        if (owner == null) return new ServiceResponse<string>
        {
            Status = ServiceResponseStatus.Unauthorized,
            Data = null,
            Message = "Unauthorized"
        };
        return new ServiceResponse<string>
        {
            Status = ServiceResponseStatus.Success,
            Data = null,
            Message = "Successfully"
        };
    }

    public async Task<ServiceResponse<string>> RegisterOwner(OwnerRegisterDto ownerRegisterDto)
    {
        var userExists = await _userManager.FindByNameAsync(ownerRegisterDto.Username);
        if (userExists != null) return new ServiceResponse<string>
        {
            Status = ServiceResponseStatus.BadRequest,
            Data = null,
            Message = "Unauthorized"
        };
        IdentityUser user = new IdentityUser
        {
            UserName = ownerRegisterDto.Username,
            Email = ownerRegisterDto.Email
        };
        var result = await _userManager.CreateAsync(user, ownerRegisterDto.Password);
        if (!result.Succeeded) return new ServiceResponse<string>
        {
            Status = ServiceResponseStatus.Unauthorized,
            Data = null,
            Message = "Unauthorized"
        };
        Owner owner = new Owner
        {
            IdentityUserId = user.Id,
            IdentityUser = user,
            RestaurantName = ownerRegisterDto.RestaurantName,
            EmployeesNumber = ownerRegisterDto.EmployeesNumber
        };

        _context.Owners.Add(owner);
        if (await _context.SaveChangesAsync() > 0) return new ServiceResponse<string>
        {
            Status = ServiceResponseStatus.Success,
            Data = null,
            Message = "Success"
        };
        return new ServiceResponse<string>
        {
            Status = ServiceResponseStatus.Unauthorized,
            Data = null,
            Message = "Something went wrong"
        };
    }
}
