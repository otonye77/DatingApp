﻿using DatingApp.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace DatingApp.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context)       
        {
            if (await context.Users.AnyAsync()) return;
           
            var userData = await File.ReadAllTextAsync("Data/UserSeedData.json"); 
            var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData, options);
            foreach (var user in users)
            {
                using var hmac = new HMACSHA512();
                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Passord"));
                user.PasswordSalt = hmac.Key;

                context.Users.Add(user);
            }
            await context.SaveChangesAsync();
        }
    }
}
