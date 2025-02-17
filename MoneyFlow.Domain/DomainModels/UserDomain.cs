﻿namespace MoneyFlow.Domain.DomainModels
{
    public class UserDomain
    {
        public int IdUser { get; set; }
        public string? UserName { get; set; }
        public byte[]? Avatar { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int? IdGender { get; set; }
    }
}
