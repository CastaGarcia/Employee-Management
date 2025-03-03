namespace Management.Inputs
{
    /// <summary>
    /// Create User
    /// </summary>
    /// <param name="UserName"></param>
    /// <param name="PassWord"></param>
    public record UserCreationData(string UserName, string PassWord);
}
