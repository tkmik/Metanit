using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task28_1.Models;

namespace Task28_1
{
    public class EFStringLocalizerFactory : IStringLocalizerFactory
    {
        string _connectionString;
        public EFStringLocalizerFactory(string connetion)
        {
            _connectionString = connetion;
        }
        public IStringLocalizer Create(Type resourceSource)
        {
            return CreateStringLocalizer();
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return CreateStringLocalizer();
        }
        private IStringLocalizer CreateStringLocalizer()
        {
            LocalizationContext _context = new LocalizationContext(
                new DbContextOptionsBuilder<LocalizationContext>()
                    .UseSqlServer(_connectionString)
                    .Options);

            if (!_context.Cultures.Any())
            {
                _context.AddRange(
                    new Culture
                    {
                        Name = "en",
                        Resources = new List<Resource>()
                        {
                            new Resource { Key = "Header", Value = "Hello" },
                            new Resource { Key = "Message", Value = "Welcome" }
                        }
                    },
                    new Culture
                    {
                        Name = "ru",
                        Resources = new List<Resource>()
                        {
                            new Resource { Key = "Header", Value = "Привет" },
                            new Resource { Key = "Message", Value = "Добро пожаловать" }
                        }
                    },
                    new Culture
                    {
                        Name = "de",
                        Resources = new List<Resource>()
                        {
                            new Resource { Key = "Header", Value = "Hallo" },
                            new Resource { Key = "Message", Value = "Willkommen" }
                        }
                    }
                );
                _context.SaveChanges();
            }
            return new EFStringLocalizer(_context);
        }
    }
}
