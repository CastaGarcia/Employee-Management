﻿namespace Management.Outputs
{
    public record UserOutput(string Id, string UserName, string Password);

    //ToDo: Add Expiration Time to Token
    public record UserTokenOutPut
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
