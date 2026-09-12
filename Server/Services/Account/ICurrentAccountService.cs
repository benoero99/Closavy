namespace Closavy.Server.Services.Account;

public interface ICurrentAccountService
{
    public void Login(int accountId);
    public void Logout();
    public int GetLoggedInUser();
}
