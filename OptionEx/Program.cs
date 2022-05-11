using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace OptionEx
{
    class Program
    {
        static void Main(string[] args)
        {
            #region
            //var profile = new ServiceCollection()
            //    .AddOptions()
            //    .Configure<Profile>(p =>
            //    {
            //        p.Gender = Gender.Male;
            //        p.Age = 18;
            //        p.ContactInfo = new ContactInfo
            //        {
            //            PhoneNo = "123456789",
            //            EmailAddress = "qq"
            //        };

            //    }).BuildServiceProvider()
            //    .GetRequiredService<IOptions<Profile>>()
            //    .Value;
            //Console.WriteLine(profile.Age);
            #endregion
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("profile.json")
                .Build();
            var profile = new ServiceCollection().AddOptions()
                .Configure<Profile>(configuration)
                .BuildServiceProvider()
                .GetRequiredService<IOptions<Profile>>()
                .Value;
            Console.WriteLine($"Gender: {profile.Gender}");
            Console.WriteLine($"Age: {profile.Age}");
            Console.WriteLine($"Email Address: {profile.ContactInfo.EmailAddress}");
            Console.WriteLine($"Phone No: {profile.ContactInfo.PhoneNo}");
        }
    }
}
