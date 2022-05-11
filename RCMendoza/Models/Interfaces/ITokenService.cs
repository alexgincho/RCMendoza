﻿using RCMendoza.Models.ModelDB;

namespace RCMendoza.Models.Interfaces
{
    public interface ITokenService
    {
        string BuildToken(string key, string issuer, Usuario user);
        bool IsTokenValid(string key, string issuer, string token);
    }
}
