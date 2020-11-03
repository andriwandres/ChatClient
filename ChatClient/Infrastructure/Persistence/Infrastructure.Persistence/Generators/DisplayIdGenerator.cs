using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Persistence.Generators
{
    public class DisplayIdGenerator : ValueGenerator<string>
    {
        private const int Length = 8;
        public override bool GeneratesTemporaryValues => false;

        public override string Next(EntityEntry entry)
        {
            char[] characters = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

            byte[] data = new byte[4 * Length];

            using RNGCryptoServiceProvider cryptoProvider = new RNGCryptoServiceProvider();

            cryptoProvider.GetBytes(data);

            StringBuilder stringBuilder = new StringBuilder(Length);

            for (int index = 0; index < Length; index++)
            {
                uint random = BitConverter.ToUInt32(data, index * 4);
                long characterIndex = random % characters.Length;

                stringBuilder.Append(characters[characterIndex]);
            }

            return stringBuilder.ToString();
        }
    }
}
