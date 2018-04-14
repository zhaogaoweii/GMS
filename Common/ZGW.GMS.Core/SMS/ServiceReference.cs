namespace ZGW.GMS.Core.SMS
{
    using System.Runtime.Serialization;
    using System;


    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "MessageE", Namespace = "http://www.ciicbj.com/")]
    [System.SerializableAttribute()]
    public partial class MessageE : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged
    {

        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string mobilePhoneField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string messageContentField;

        private int pubIdField;

        private int sendSysField;

        private int sendModelField;

        private int sendModeField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string sendUserField;

        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false)]
        public string mobilePhone
        {
            get
            {
                return this.mobilePhoneField;
            }
            set
            {
                if ((object.ReferenceEquals(this.mobilePhoneField, value) != true))
                {
                    this.mobilePhoneField = value;
                    this.RaisePropertyChanged("mobilePhone");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false, Order = 1)]
        public string messageContent
        {
            get
            {
                return this.messageContentField;
            }
            set
            {
                if ((object.ReferenceEquals(this.messageContentField, value) != true))
                {
                    this.messageContentField = value;
                    this.RaisePropertyChanged("messageContent");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(IsRequired = true, Order = 2)]
        public int pubId
        {
            get
            {
                return this.pubIdField;
            }
            set
            {
                if ((this.pubIdField.Equals(value) != true))
                {
                    this.pubIdField = value;
                    this.RaisePropertyChanged("pubId");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(IsRequired = true, Order = 3)]
        public int sendSys
        {
            get
            {
                return this.sendSysField;
            }
            set
            {
                if ((this.sendSysField.Equals(value) != true))
                {
                    this.sendSysField = value;
                    this.RaisePropertyChanged("sendSys");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(IsRequired = true, Order = 4)]
        public int sendModel
        {
            get
            {
                return this.sendModelField;
            }
            set
            {
                if ((this.sendModelField.Equals(value) != true))
                {
                    this.sendModelField = value;
                    this.RaisePropertyChanged("sendModel");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(IsRequired = true, Order = 5)]
        public int sendMode
        {
            get
            {
                return this.sendModeField;
            }
            set
            {
                if ((this.sendModeField.Equals(value) != true))
                {
                    this.sendModeField = value;
                    this.RaisePropertyChanged("sendMode");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false, Order = 6)]
        public string sendUser
        {
            get
            {
                return this.sendUserField;
            }
            set
            {
                if ((object.ReferenceEquals(this.sendUserField, value) != true))
                {
                    this.sendUserField = value;
                    this.RaisePropertyChanged("sendUser");
                }
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace = "http://www.ciicbj.com/", ConfigurationName = "ServiceReference1.WSSendMessageSoap")]
    public interface WSSendMessageSoap
    {

        // CODEGEN: 命名空间 http://www.ciicbj.com/ 的元素名称 MsgArrE 以后生成的消息协定未标记为 nillable
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.ciicbj.com/sendMessage", ReplyAction = "*")]
        ZGW.GMS.Core.SMS.sendMessageResponse sendMessage(ZGW.GMS.Core.SMS.sendMessageRequest request);
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class sendMessageRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Name = "sendMessage", Namespace = "http://www.ciicbj.com/", Order = 0)]
        public ZGW.GMS.Core.SMS.sendMessageRequestBody Body;

        public sendMessageRequest()
        {
        }

        public sendMessageRequest(ZGW.GMS.Core.SMS.sendMessageRequestBody Body)
        {
            this.Body = Body;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace = "http://www.ciicbj.com/")]
    public partial class sendMessageRequestBody
    {

        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false, Order = 0)]
        public ZGW.GMS.Core.SMS.MessageE[] MsgArrE;

        public sendMessageRequestBody()
        {
        }

        public sendMessageRequestBody(ZGW.GMS.Core.SMS.MessageE[] MsgArrE)
        {
            this.MsgArrE = MsgArrE;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class sendMessageResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Name = "sendMessageResponse", Namespace = "http://www.ciicbj.com/", Order = 0)]
        public ZGW.GMS.Core.SMS.sendMessageResponseBody Body;

        public sendMessageResponse()
        {
        }

        public sendMessageResponse(ZGW.GMS.Core.SMS.sendMessageResponseBody Body)
        {
            this.Body = Body;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace = "http://www.ciicbj.com/")]
    public partial class sendMessageResponseBody
    {

        [System.Runtime.Serialization.DataMemberAttribute(Order = 0)]
        public bool sendMessageResult;

        public sendMessageResponseBody()
        {
        }

        public sendMessageResponseBody(bool sendMessageResult)
        {
            this.sendMessageResult = sendMessageResult;
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface WSSendMessageSoapChannel : ZGW.GMS.Core.SMS.WSSendMessageSoap, System.ServiceModel.IClientChannel
    {
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WSSendMessageSoapClient : System.ServiceModel.ClientBase<ZGW.GMS.Core.SMS.WSSendMessageSoap>, ZGW.GMS.Core.SMS.WSSendMessageSoap
    {

        public WSSendMessageSoapClient()
        {
        }

        public WSSendMessageSoapClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public WSSendMessageSoapClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public WSSendMessageSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public WSSendMessageSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ZGW.GMS.Core.SMS.sendMessageResponse ZGW.GMS.Core.SMS.WSSendMessageSoap.sendMessage(ZGW.GMS.Core.SMS.sendMessageRequest request)
        {
            return base.Channel.sendMessage(request);
        }

        public bool sendMessage(ZGW.GMS.Core.SMS.MessageE[] MsgArrE)
        {
            ZGW.GMS.Core.SMS.sendMessageRequest inValue = new ZGW.GMS.Core.SMS.sendMessageRequest();
            inValue.Body = new ZGW.GMS.Core.SMS.sendMessageRequestBody();
            inValue.Body.MsgArrE = MsgArrE;
            ZGW.GMS.Core.SMS.sendMessageResponse retVal = ((ZGW.GMS.Core.SMS.WSSendMessageSoap)(this)).sendMessage(inValue);
            return retVal.Body.sendMessageResult;
        }
    }
}
