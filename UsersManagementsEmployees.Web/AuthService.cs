using Management;
using Management.Inputs;
using Refit;

public class AuthService
{
    private readonly IAccountSdk _accountSdk;
    public AuthService(IAccountSdk accountSdk)
    {
        _accountSdk = accountSdk;
    }
    
    public async Task<bool> ValidateUserAsync(string UserName, string PassWord)
    {
        var userLogin = new UserLogin(UserName, PassWord);


        try
        {
            var userOutput = await _accountSdk.GetToken(userLogin);

            if (userOutput != null && !string.IsNullOrEmpty(userOutput.Id))
            {
                Console.WriteLine($"Token recibido: {userOutput.Id}"); 
                return true;
            }
            return false;
        }
        catch (ApiException apiEx)
        {
            Console.WriteLine($"API Error: {apiEx.Message}");
            return false;
        }
    }
}
