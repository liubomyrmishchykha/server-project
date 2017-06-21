using System;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using LoggerService;
using WcfServiceLibrary.DataModel;

namespace WcfServiceLibrary.ExceptionHandler
{
    [DataContract]
    class GlobalExceptionHandler : IErrorHandler
    {
        /// <summary>
        /// Providing error message to client side
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="version"></param>
        /// <param name="fault"></param>
        public void ProvideFault(Exception ex, MessageVersion version, ref Message fault)
        {
            var errorMessage = new ErrorMessage()
            {
                Message = "Server error encountered. For more info contact to administrator",
            };

            fault = Message.CreateMessage(version, "", errorMessage, new DataContractJsonSerializer(typeof(ErrorMessage)));
            var wbf = new WebBodyFormatMessageProperty(WebContentFormat.Json);
            fault.Properties.Add(WebBodyFormatMessageProperty.Name, wbf);
            var response = WebOperationContext.Current.OutgoingResponse;
            response.ContentType = "application/json";
            response.StatusCode = HttpStatusCode.InternalServerError;
        }

        /// <summary>
        /// Logging exception details to log file
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public bool HandleError(Exception ex)
        {
            Logger.WriteLog(LogLevel.Error, "Unhandled exception encountered on server side", ex);
            return true;
        }

    }
}
