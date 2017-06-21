namespace WcfServiceLibrary.Configuration
{
    public class CorsDomain
    {
        public  const string Name = @"http://localhost:49679";
        public const string AllowMethods = "POST, PUT, DELETE, GET";
        public const string AllowHeaders = "X-Requested-With,Content-Type";
        public const bool AllowCredentials = true;
    }
}
