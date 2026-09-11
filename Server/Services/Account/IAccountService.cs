using Closavy.Server.Dtos.Account;

namespace Closavy.Server.Services.Account;

public interface IAccountService
{
    public Task<AccountResponseDto> CreateAccountAsync(AccountCreateDto request, CancellationToken ct);
    public Task<AccountResponseDto> GetAccountAsync(int accountId, CancellationToken ct);
}
