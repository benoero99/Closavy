using Closavy.Server.Data;
using Closavy.Server.Dtos.Account;
using Closavy.Server.Models;
using Closavy.Server.Services.Account;
using Microsoft.EntityFrameworkCore;

namespace Closavy.Server.Services.Auth;

public class AuthService(
    GameDbContext dbContext,
    ICurrentAccountService currentAccountService
) : IAuthService
{
    public async Task<AccountResponseDto> LoginAsync(string displayName, CancellationToken ct)
    {
        AccountEntity accountEntity = await dbContext.Accounts.SingleOrDefaultAsync(a => a.DisplayName.ToLower() == displayName.ToLower(), ct) ?? throw new Exception("Account not found");
        currentAccountService.Login(accountEntity.Id);

        return new AccountResponseDto
        {
            Id = accountEntity.Id,
            DisplayName = accountEntity.DisplayName,
        };
    }

    public void Logout()
    {
        currentAccountService.Logout();
    }
}
