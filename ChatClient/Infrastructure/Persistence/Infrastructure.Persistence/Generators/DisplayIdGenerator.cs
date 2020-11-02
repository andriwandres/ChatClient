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
        private const int DisplayIdLength = 8;

        public override string Next(EntityEntry entry)
        {
            const string alphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            byte[] data = new byte[4 * DisplayIdLength];
            
            using RNGCryptoServiceProvider cryptoProvider = new RNGCryptoServiceProvider();

            cryptoProvider.GetBytes(data);

            StringBuilder stringBuilder = new StringBuilder(DisplayIdLength);

            for (int index = 0; index < DisplayIdLength; index++)
            {
                int random = BitConverter.ToInt32(data, index * 4);
                int characterIndex = random % alphabet.Length;

                stringBuilder.Append(alphabet.ElementAt(characterIndex));
            }

            return stringBuilder.ToString();
        }

        public override bool GeneratesTemporaryValues { get; }
    }
}
