using Microsoft.AspNetCore.Mvc;

namespace API;


[ApiController]
[Route("api/owner/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    public AccountController(
        IAccountService accountService
    )
    {
        _accountService = accountService;
    }

    [HttpPost]
    [Route("register")]
    public async Task<ActionResult> OwnerRegister(OwnerRegisterDto ownerRegisterDto)
    {
        ServiceResponse<string> response = await _accountService.RegisterOwner(ownerRegisterDto);
        if (response.Status == ServiceResponseStatus.Success) return Ok(response.Message);
        return BadRequest(response.Message);
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult> OwnerLogin(OwnerLoginDto ownerLoginDto)
    {
        ServiceResponse<string> response = await _accountService.LoginOwner(ownerLoginDto);
        if (response.Status == ServiceResponseStatus.Unauthorized) return Unauthorized();
        return Ok(response.Message);
    }
}
