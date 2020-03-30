using System;

namespace ChatClient.Core.Exceptions.Auth
{
    public class InvalidPasswordException : InvalidCredentialsException
    {
        public InvalidPasswordException() : base() { }
        public InvalidPasswordException(string message) : base(message) { }
        public InvalidPasswordException(string message, Exception inner) : base(message, inner) { }
    }
}
