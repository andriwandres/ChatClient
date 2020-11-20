using System.Collections.Generic;

namespace Core.Domain.Entities
{
    public class Country
    {
        public int CountryId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }

        public Country()
        {
            Users = new HashSet<User>();
        }
    }
}
