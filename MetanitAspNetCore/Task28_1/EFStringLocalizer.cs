using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Task28_1.Models;

namespace Task28_1
{
    public class EFStringLocalizer : IStringLocalizer
    {
        private readonly LocalizationContext _context;
        public EFStringLocalizer(LocalizationContext context)
        {
            _context = context;
        }
        public LocalizedString this[string name] 
        {
            get 
            {
                var value = GetString(name);
                return new LocalizedString(name, value ?? name, resourceNotFound: value == null);
            }
        }

        public LocalizedString this[string name, params object[] arguments] 
        {
            get
            {
                var format = GetString(name);
                var value = string.Format(format ?? name, arguments);
                return new LocalizedString(name, value, resourceNotFound: format == null);
            }
        }
        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            CultureInfo.DefaultThreadCurrentCulture = culture;
            return new EFStringLocalizer(_context);
        }
        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            return _context.Resources
                .Include(r => r.Culture)
                .Where(r => r.Culture.Name == CultureInfo.CurrentCulture.Name)
                .Select(r => new LocalizedString(r.Key, r.Value));
        }

        private string GetString(string name)
        {
            return _context.Resources
                .Include(r => r.Culture)
                .Where(r => r.Culture.Name == CultureInfo.CurrentCulture.Name)
                .FirstOrDefault(r => r.Key == name)?.Value;
        }
    }
}
