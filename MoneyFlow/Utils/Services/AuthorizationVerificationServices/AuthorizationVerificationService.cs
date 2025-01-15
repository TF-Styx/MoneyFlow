﻿using Microsoft.Extensions.DependencyInjection;
using MoneyFlow.MVVM.Models.MSSQL_DB;
using MoneyFlow.Utils.Services.DataBaseServices;
using System.IO;
using System.Text.Json;

namespace MoneyFlow.Utils.Services.AuthorizationVerificationServices
{
    public class AuthorizationVerificationService : IAuthorizationVerificationService
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly static string RoamingPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private readonly static string AppDirectory = Path.Combine(RoamingPath, "MoneyFlow");
        private readonly static string JsonFilePath = AppDirectory + @"\User.json";

        public User CurrentUser { get; private set; }

        public AuthorizationVerificationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            if (!Directory.Exists(AppDirectory))
            {
                Directory.CreateDirectory(AppDirectory);
            }
        }

        public bool CheckAuthorization()
        {
            if (File.Exists(JsonFilePath))
            {
                string json = File.ReadAllText(JsonFilePath);
                var authUser = JsonSerializer.Deserialize<User>(json);
                CurrentUser = authUser;

                return true;
            }
            else { return false; }
        }

        public void CreateJsonUser(User user)
        {
            string json = JsonSerializer.Serialize(user);
            File.WriteAllText(JsonFilePath, json);
            CurrentUser = user;
        }
    }
}