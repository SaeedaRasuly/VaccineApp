﻿using Auth.Factory;
using Core.Models;
using Newtonsoft.Json;

namespace Auth.Services;

public class AccountService
{
    private readonly IHttpClientFactory _clientFactory;

    public AccountService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }
    public async Task<string> ChangeAccountPassword(string token, string password)
    {
        var client = _clientFactory.CreateClient();

        var requestUri = String.Concat(
            "https://identitytoolkit.googleapis.com/v1/accounts:update?key=",
            AuthConfigs.FirebaseApiKey);

        if (Connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            throw new Exception("No internet connection");
        }
        else
        {
            try
            {
                var identityModel = new ChangeAccountPasswordModel() { IdToken = token, Password = password, ReturnSecureToken = true };
                var content = JsonConvert.SerializeObject(identityModel);

                var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                var byteContent = new ByteArrayContent(buffer);

                var s = await client.PostAsync(requestUri, byteContent);

                if (s.IsSuccessStatusCode)
                {
                    return await s.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new Exception("Password not changed!");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}