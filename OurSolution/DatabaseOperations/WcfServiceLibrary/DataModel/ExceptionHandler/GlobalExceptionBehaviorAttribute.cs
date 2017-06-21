using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace WcfServiceLibrary.ExceptionHandler
{
    public class GlobalExceptionBehaviorAttribute : Attribute, IServiceBehavior
    {
        private readonly Type _exceptionHandlerType;

        public GlobalExceptionBehaviorAttribute(Type exceptionHandlerType)
        {
            this._exceptionHandlerType = exceptionHandlerType;
        }
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints,
            BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            IErrorHandler exceptionHandler;
            try
            {
                exceptionHandler = (IErrorHandler)Activator.CreateInstance(_exceptionHandlerType);
            }
            catch (MissingMethodException ex)
            {
                throw new ArgumentException("The errorHandlerType specified in the ErrorBehaviorAttribute constructor must have a public empty constructor.", ex);
            }
            catch (InvalidCastException ex)
            {
                throw new ArgumentException("The errorHandlerType specified in the ErrorBehaviorAttribute constructor must implement System.ServiceModel.Dispatcher.IErrorHandler.", ex);
            }
            foreach (ChannelDispatcherBase channelDispatcherBase in
           serviceHostBase.ChannelDispatchers)
            {
                ChannelDispatcher channelDispatcher = channelDispatcherBase as ChannelDispatcher;
                channelDispatcher.ErrorHandlers.Add(exceptionHandler);
            }
        }
    }
}
