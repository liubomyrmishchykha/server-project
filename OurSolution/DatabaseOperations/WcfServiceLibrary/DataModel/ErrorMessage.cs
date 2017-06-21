using System.Runtime.Serialization;

namespace WcfServiceLibrary.DataModel
{
    /// <summary>
    /// Message that will be sent to user when unhandled exception raised
    /// </summary>
    [DataContract]
    public class ErrorMessage
    {
        [DataMember(Name = "errorMessage")]
        public string Message { get; set; }

        public ErrorMessage() { }

        public ErrorMessage(string message)
        {
            Message = message;
        }
    }
}