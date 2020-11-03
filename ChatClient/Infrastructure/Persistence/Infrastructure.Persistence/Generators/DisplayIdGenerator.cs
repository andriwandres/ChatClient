using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Persistence.Generators
{
    public class DisplayIdGenerator : ValueGenerator<string>
    {
        private const int Length = 8;
        private const string Characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public override string Next(EntityEntry entry)
        {
            byte[] data = new byte[4 * Length];
            
            using RNGCryptoServiceProvider cryptoProvider = new RNGCryptoServiceProvider();

            cryptoProvider.GetBytes(data);

            StringBuilder stringBuilder = new StringBuilder(Length);

            for (int index = 0; index < Length; index++)
            {
                int random = BitConverter.ToInt32(data, index * 4);
                int characterIndex = random % Characters.Length;

                stringBuilder.Append(Characters.ElementAt(characterIndex));
            }

            return stringBuilder.ToString();
        }

        public override bool GeneratesTemporaryValues { get; }
    }
}
