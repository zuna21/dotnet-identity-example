namespace API;

public interface IAccountService
{
    Task<ServiceResponse<string>> RegisterOwner(OwnerRegisterDto ownerRegisterDto);
    Task<ServiceResponse<string>> LoginOwner(OwnerLoginDto ownerLoginDto);
}
