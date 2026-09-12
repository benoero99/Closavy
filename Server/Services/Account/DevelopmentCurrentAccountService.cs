namespace Closavy.Server.Services.Account;

public class DevelopmentCurrentAccountService : ICurrentAccountService
{
    private static int? accountId;

    public int GetLoggedInUser()
    {
        if (accountId == null)
            return 0;

        return (int)accountId;
    }

    public void Login(int accountId)
    {
        DevelopmentCurrentAccountService.accountId = accountId;
    }

    public void Logout()
    {
        accountId = null;
    }
}