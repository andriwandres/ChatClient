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
        private readonly int _length;

        public DisplayIdGenerator(int length)
        {
            _length = length;
        }

        public override string Next(EntityEntry entry)
        {
            const string alphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            byte[] data = new byte[4 * _length];
            
            using RNGCryptoServiceProvider cryptoProvider = new RNGCryptoServiceProvider();

            cryptoProvider.GetBytes(data);

            StringBuilder stringBuilder = new StringBuilder(_length);

            for (int index = 0; index < _length; index++)
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
