
using Closavy.Server.Dtos.Account;

namespace Closavy.Server.Services.Auth;

public interface IAuthService
{
    public Task<AccountResponseDto> LoginAsync(string displayName, CancellationToken ct);
    public void Logout();
}
