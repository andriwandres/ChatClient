using System;

namespace ChatClient.Core.Exceptions.Auth
{
    public class InvalidCredentialsException : Exception
    {
        public readonly string ModelStateKey = "InvalidCredentials";
        private const string DefaultMessage = "The user has provided invalid credentials";

        public InvalidCredentialsException() : base(DefaultMessage) { }
        public InvalidCredentialsException(string message) : base(message) { }
        public InvalidCredentialsException(string message, Exception inner) : base(message, inner) { }
    }
}
