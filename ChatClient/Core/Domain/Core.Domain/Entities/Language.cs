using System.Collections.Generic;

namespace Core.Domain.Entities
{
    public class Language
    {
        public int LanguageId { get; set; }
        public int CountryId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public Country Country { get; set; }
        public ICollection<Translation> Translations { get; set; }

        public Language()
        {
            Translations = new HashSet<Translation>();
        }
    }
}
