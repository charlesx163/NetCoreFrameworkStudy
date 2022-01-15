using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace OptionEx
{
    class Program
    {
        static void Main(string[] args)
        {
            var profile = new ServiceCollection()
                .AddOptions()
                .Configure<Profile>(p =>
                {
                    p.Gender = Gender.Male;
                    p.Age = 18;
                    p.ContactInfo = new ContactInfo
                    {
                        PhoneNo = "123456789",
                        EmailAddress = "qq"
                    };

                }).BuildServiceProvider()
                .GetRequiredService<IOptions<Profile>>()
                .Value;
            Console.WriteLine(profile.Age);
            Console.WriteLine("Hello World!");
        }
    }
}
