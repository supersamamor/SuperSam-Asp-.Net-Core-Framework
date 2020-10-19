namespace ProjectNamePlaceHolder.Application.Exception
{
    public class UnAuthorizedException : System.Exception
    {
        public UnAuthorizedException()
        {
        }

        public UnAuthorizedException(string message)
            : base(message)
        {
        }

        public UnAuthorizedException(string message, System.Exception inner)
            : base(message, inner)
        {
        }
    }
}
