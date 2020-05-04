using Microsoft.AspNetCore.Identity;
using Store.BusinessLogicLayer.Helpers.Interfaces;
using static Store.BusinessLogicLayer.Common.Constants.PasswordGenerator.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Store.BusinessLogicLayer.Helpers
{
    public class PasswordHelper : IPasswordHelper
    {
        private readonly IConfigurationSection _configurationSection;
        public PasswordHelper(IConfiguration configuration)
        {
            _configurationSection = configuration.GetSection(PasswordGeneration.PasswordConfig);
        }

        public string GenerateRandomPassword()
        {           
            var opts = new PasswordOptions()
            {
                RequiredLength = _configurationSection.GetValue<int>(PasswordGeneration.RequiredLength),
                RequiredUniqueChars = _configurationSection.GetValue<int>(PasswordGeneration.RequiredUniqueChars),
                RequireDigit = _configurationSection.GetValue<bool>(PasswordGeneration.RequireDigit),
                RequireLowercase = _configurationSection.GetValue<bool>(PasswordGeneration.RequireLowercase),
                RequireNonAlphanumeric = _configurationSection.GetValue<bool>(PasswordGeneration.RequireNonAlphanumeric),
                RequireUppercase = _configurationSection.GetValue<bool>(PasswordGeneration.RequireUppercase)
            };

            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
            {
                chars.Insert(rand.Next(0, chars.Count),
                PasswordGeneration.AllChars[0][rand.Next(0, PasswordGeneration.AllChars[0].Length)]);
            }
            if (opts.RequireLowercase)
            {
                chars.Insert(rand.Next(1, chars.Count),
                PasswordGeneration.AllChars[1][rand.Next(0, PasswordGeneration.AllChars[1].Length)]);
            }
            if (opts.RequireDigit)
            {
                chars.Insert(rand.Next(1, chars.Count),
                PasswordGeneration.AllChars[2][rand.Next(0, PasswordGeneration.AllChars[2].Length)]);
            }
            if (opts.RequireNonAlphanumeric)
            {
                chars.Insert(rand.Next(1, chars.Count),
                PasswordGeneration.AllChars[3][rand.Next(0, PasswordGeneration.AllChars[3].Length)]);
            }
            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string randomChars = PasswordGeneration.AllChars[rand.Next(0, PasswordGeneration.AllChars.Length)];
                chars.Insert(rand.Next(1, chars.Count), randomChars[rand.Next(0, randomChars.Length)]);
            }
            return new string(chars.ToArray());
        }
    }
}
