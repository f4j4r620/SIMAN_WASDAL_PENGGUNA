using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Configuration;
using System.Configuration;

namespace AppPengguna
{
    public class HttpUserAgentMessageInspector : IClientMessageInspector
    {
        private const string USER_AGENT_HTTP_HEADER = "user-agent";
        private string m_userAgent;

        public HttpUserAgentMessageInspector(string userAgent)
        {
            this.m_userAgent = userAgent;
        }

        #region IClientMessageInspector Members

        public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {

        }
        public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel)
        {

            HttpRequestMessageProperty httpRequestMessage;
            object httpRequestMessageObject;
            if (request.Properties.TryGetValue(HttpRequestMessageProperty.Name, out httpRequestMessageObject))
            {
                httpRequestMessage = httpRequestMessageObject as HttpRequestMessageProperty;
                if (string.IsNullOrEmpty(httpRequestMessage.Headers[USER_AGENT_HTTP_HEADER]))
                {
                    httpRequestMessage.Headers[USER_AGENT_HTTP_HEADER] = this.m_userAgent;
                }
            }
            else
            {
                httpRequestMessage = new HttpRequestMessageProperty();
                httpRequestMessage.Headers.Add(USER_AGENT_HTTP_HEADER, this.m_userAgent);
                request.Properties.Add(HttpRequestMessageProperty.Name, httpRequestMessage);
            }

            return null;

        }
        #endregion

     
    }

    public class HttpUserAgentEndpointBehavior : IEndpointBehavior
    {
        private string m_userAgent;
        public HttpUserAgentEndpointBehavior(string userAgent)
        {
            this.m_userAgent = userAgent;
        }

        #region IEndpointBehavior Members

        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {

        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
            HttpUserAgentMessageInspector inspector = new HttpUserAgentMessageInspector(this.m_userAgent);
            clientRuntime.MessageInspectors.Add(inspector);
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        {

        }
        public void Validate(ServiceEndpoint endpoint)
        {

        }
        #endregion

    }

    public class HttpUserAgentBehaviorExtensionElement : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get
            {
                return typeof(HttpUserAgentEndpointBehavior);
            }
        }
        protected override object CreateBehavior()
        {

            return new HttpUserAgentEndpointBehavior(UserAgent);

        }
        [ConfigurationProperty("userAgent", IsRequired = true)]
        public string UserAgent
        {
            get { return (string)base["userAgent"]; }
            set { base["userAgent"] = value; }
        }
    }

}
