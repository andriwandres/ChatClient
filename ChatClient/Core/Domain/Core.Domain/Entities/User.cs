using System;

namespace Core.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string DisplayId { get; set; }

        public int? CountryId { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime Created { get; set; }
        public bool IsEmailConfirmed { get; set; }

        public Country Country { get; set; }
    }
}
