using Closavy.Server.Data;
using Closavy.Server.Dtos.Account;
using Closavy.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Closavy.Server.Services.Account;

public class AccountService(
    GameDbContext dbContext,
    ICurrentAccountService currentAccountService
) : IAccountService
{
    public async Task<AccountResponseDto> CreateAccountAsync(AccountCreateDto request, CancellationToken ct)
    {
        if (await IsAccountDisplayNameAlreadyInUse(request.DisplayName))
            throw new Exception("Account display name is already in use!");

        AccountEntity account = new()
        {
            DisplayName = request.DisplayName,
        };

        await dbContext.AddAsync(account, ct);
        await dbContext.SaveChangesAsync(ct);

        return new AccountResponseDto
        {
            Id = account.Id,
            DisplayName = account.DisplayName,
        };
    }

    public async Task<AccountResponseDto> GetAccountAsync(int accountId, CancellationToken ct)
    {
        AccountEntity? account = await dbContext.Accounts.FindAsync([accountId], cancellationToken: ct) ?? throw new Exception("Account not found");
        
        return new AccountResponseDto
        {
            Id = account.Id,
            DisplayName = account.DisplayName,
        };
    }

    public async Task<AccountResponseDto> GetLoggedInUserAsync(CancellationToken ct)
    {
        int loggedInUserId = currentAccountService.GetLoggedInUser();

        AccountEntity? account = await dbContext.Accounts.FindAsync([loggedInUserId], cancellationToken: ct) ?? throw new Exception("Account not found");
        
        return new AccountResponseDto
        {
            Id = account.Id,
            DisplayName = account.DisplayName,
        };
    }


    private async Task<bool> IsAccountDisplayNameAlreadyInUse(string name)
    {
        AccountEntity? account = await dbContext.Accounts.SingleOrDefaultAsync(c => c.DisplayName == name);
        return account != null;
    }
}
