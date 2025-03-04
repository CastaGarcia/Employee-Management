namespace Management.Inputs
{
    /// <summary>
    /// Create User
    /// </summary>
    /// <param name="UserName"></param>
    /// <param name="PassWord"></param>
    ///  /// <param name="Id"></param>
    public record UserCreationData(string Id, string UserName, string PassWord);
}
