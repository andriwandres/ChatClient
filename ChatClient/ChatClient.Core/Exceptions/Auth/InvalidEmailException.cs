using System;

namespace ChatClient.Core.Exceptions.Auth
{
    public class InvalidEmailException : InvalidCredentialsException
    {
        public InvalidEmailException() : base() { }
        public InvalidEmailException(string message) : base(message) { }
        public InvalidEmailException(string message, Exception inner) : base(message, inner) { }
    }
}
