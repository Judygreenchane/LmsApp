﻿namespace Domain.Models.Configuration;
#nullable disable
public class JwtConfiguration
{
    public string SecretKey { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int Expires { get; set; }
}
