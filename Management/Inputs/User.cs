namespace Management.Inputs
{
    /// <summary>
    /// Create User
    /// </summary>
    /// <param name="UserName"></param>
    /// <param name="PassWord"></param>
    ///  /// <param name="Id"></param>
    public record UserCreationData(string Id, string UserName, string PassWord);

    /// <summary>
    /// Update User
    /// </summary>
    /// <param name="UserName"></param>
    /// <param name="PassWord"></param>
    ///  /// <param name="Id"></param>
    public record UserUpdateData(string Id, string UserName, string PassWord);

    public record UserLogin(string UserName, string PassWord);

}
