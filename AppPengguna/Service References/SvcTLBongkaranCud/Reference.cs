﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppPengguna.SvcTLBongkaranCud {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://oracle.com/sca/soapservice/siman_wasdal/wasRekamTLBnk/cud", ConfigurationName="SvcTLBongkaranCud.execute_ptt")]
    public interface execute_ptt {
        
        // CODEGEN: Generating message contract since the operation execute is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="execute", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        AppPengguna.SvcTLBongkaranCud.executeResponse execute(AppPengguna.SvcTLBongkaranCud.executeRequest request);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="execute", ReplyAction="*")]
        System.IAsyncResult Beginexecute(AppPengguna.SvcTLBongkaranCud.executeRequest request, System.AsyncCallback callback, object asyncState);
        
        AppPengguna.SvcTLBongkaranCud.executeResponse Endexecute(System.IAsyncResult result);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2102.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SP_TL_CUD_BONGKARAN")]
    public partial class InputParameters : object, System.ComponentModel.INotifyPropertyChanged {
        
        private System.Nullable<decimal> p_ID_TL_WASDAL_BNKField;
        
        private bool p_ID_TL_WASDAL_BNKFieldSpecified;
        
        private System.Nullable<decimal> p_ID_SK_BNKField;
        
        private bool p_ID_SK_BNKFieldSpecified;
        
        private string p_SK_KEPUTUSAN_BNKField;
        
        private string p_JNS_BUKTI_LAKSANAANField;
        
        private string p_NO_BUKTI_LAKSANAField;
        
        private System.Nullable<System.DateTime> p_TGL_BUKTI_LAKSANAField;
        
        private bool p_TGL_BUKTI_LAKSANAFieldSpecified;
        
        private string p_KD_PELAYANANField;
        
        private string p_KETField;
        
        private string p_SELECTField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=0)]
        public System.Nullable<decimal> P_ID_TL_WASDAL_BNK {
            get {
                return this.p_ID_TL_WASDAL_BNKField;
            }
            set {
                this.p_ID_TL_WASDAL_BNKField = value;
                this.RaisePropertyChanged("P_ID_TL_WASDAL_BNK");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool P_ID_TL_WASDAL_BNKSpecified {
            get {
                return this.p_ID_TL_WASDAL_BNKFieldSpecified;
            }
            set {
                this.p_ID_TL_WASDAL_BNKFieldSpecified = value;
                this.RaisePropertyChanged("P_ID_TL_WASDAL_BNKSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=1)]
        public System.Nullable<decimal> P_ID_SK_BNK {
            get {
                return this.p_ID_SK_BNKField;
            }
            set {
                this.p_ID_SK_BNKField = value;
                this.RaisePropertyChanged("P_ID_SK_BNK");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool P_ID_SK_BNKSpecified {
            get {
                return this.p_ID_SK_BNKFieldSpecified;
            }
            set {
                this.p_ID_SK_BNKFieldSpecified = value;
                this.RaisePropertyChanged("P_ID_SK_BNKSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=2)]
        public string P_SK_KEPUTUSAN_BNK {
            get {
                return this.p_SK_KEPUTUSAN_BNKField;
            }
            set {
                this.p_SK_KEPUTUSAN_BNKField = value;
                this.RaisePropertyChanged("P_SK_KEPUTUSAN_BNK");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=3)]
        public string P_JNS_BUKTI_LAKSANAAN {
            get {
                return this.p_JNS_BUKTI_LAKSANAANField;
            }
            set {
                this.p_JNS_BUKTI_LAKSANAANField = value;
                this.RaisePropertyChanged("P_JNS_BUKTI_LAKSANAAN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=4)]
        public string P_NO_BUKTI_LAKSANA {
            get {
                return this.p_NO_BUKTI_LAKSANAField;
            }
            set {
                this.p_NO_BUKTI_LAKSANAField = value;
                this.RaisePropertyChanged("P_NO_BUKTI_LAKSANA");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=5)]
        public System.Nullable<System.DateTime> P_TGL_BUKTI_LAKSANA {
            get {
                return this.p_TGL_BUKTI_LAKSANAField;
            }
            set {
                this.p_TGL_BUKTI_LAKSANAField = value;
                this.RaisePropertyChanged("P_TGL_BUKTI_LAKSANA");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool P_TGL_BUKTI_LAKSANASpecified {
            get {
                return this.p_TGL_BUKTI_LAKSANAFieldSpecified;
            }
            set {
                this.p_TGL_BUKTI_LAKSANAFieldSpecified = value;
                this.RaisePropertyChanged("P_TGL_BUKTI_LAKSANASpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=6)]
        public string P_KD_PELAYANAN {
            get {
                return this.p_KD_PELAYANANField;
            }
            set {
                this.p_KD_PELAYANANField = value;
                this.RaisePropertyChanged("P_KD_PELAYANAN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=7)]
        public string P_KET {
            get {
                return this.p_KETField;
            }
            set {
                this.p_KETField = value;
                this.RaisePropertyChanged("P_KET");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=8)]
        public string P_SELECT {
            get {
                return this.p_SELECTField;
            }
            set {
                this.p_SELECTField = value;
                this.RaisePropertyChanged("P_SELECT");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2102.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SP_TL_CUD_BONGKARAN")]
    public partial class OutputParameters : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string pO_RESULTField;
        
        private string pO_RESULT_MESSAGEField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=0)]
        public string PO_RESULT {
            get {
                return this.pO_RESULTField;
            }
            set {
                this.pO_RESULTField = value;
                this.RaisePropertyChanged("PO_RESULT");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=1)]
        public string PO_RESULT_MESSAGE {
            get {
                return this.pO_RESULT_MESSAGEField;
            }
            set {
                this.pO_RESULT_MESSAGEField = value;
                this.RaisePropertyChanged("PO_RESULT_MESSAGE");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class executeRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SP_TL_CUD_BONGKARAN", Order=0)]
        public AppPengguna.SvcTLBongkaranCud.InputParameters InputParameters;
        
        public executeRequest() {
        }
        
        public executeRequest(AppPengguna.SvcTLBongkaranCud.InputParameters InputParameters) {
            this.InputParameters = InputParameters;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class executeResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SP_TL_CUD_BONGKARAN", Order=0)]
        public AppPengguna.SvcTLBongkaranCud.OutputParameters OutputParameters;
        
        public executeResponse() {
        }
        
        public executeResponse(AppPengguna.SvcTLBongkaranCud.OutputParameters OutputParameters) {
            this.OutputParameters = OutputParameters;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface execute_pttChannel : AppPengguna.SvcTLBongkaranCud.execute_ptt, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class executeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public executeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public AppPengguna.SvcTLBongkaranCud.OutputParameters Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((AppPengguna.SvcTLBongkaranCud.OutputParameters)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class execute_pttClient : System.ServiceModel.ClientBase<AppPengguna.SvcTLBongkaranCud.execute_ptt>, AppPengguna.SvcTLBongkaranCud.execute_ptt {
        
        private BeginOperationDelegate onBeginexecuteDelegate;
        
        private EndOperationDelegate onEndexecuteDelegate;
        
        private System.Threading.SendOrPostCallback onexecuteCompletedDelegate;
        
        public execute_pttClient() {
        }
        
        public execute_pttClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public execute_pttClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public execute_pttClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public execute_pttClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public event System.EventHandler<executeCompletedEventArgs> executeCompleted;
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        AppPengguna.SvcTLBongkaranCud.executeResponse AppPengguna.SvcTLBongkaranCud.execute_ptt.execute(AppPengguna.SvcTLBongkaranCud.executeRequest request) {
            return base.Channel.execute(request);
        }
        
        public AppPengguna.SvcTLBongkaranCud.OutputParameters execute(AppPengguna.SvcTLBongkaranCud.InputParameters InputParameters) {
            AppPengguna.SvcTLBongkaranCud.executeRequest inValue = new AppPengguna.SvcTLBongkaranCud.executeRequest();
            inValue.InputParameters = InputParameters;
            AppPengguna.SvcTLBongkaranCud.executeResponse retVal = ((AppPengguna.SvcTLBongkaranCud.execute_ptt)(this)).execute(inValue);
            return retVal.OutputParameters;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult AppPengguna.SvcTLBongkaranCud.execute_ptt.Beginexecute(AppPengguna.SvcTLBongkaranCud.executeRequest request, System.AsyncCallback callback, object asyncState) {
            return base.Channel.Beginexecute(request, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult Beginexecute(AppPengguna.SvcTLBongkaranCud.InputParameters InputParameters, System.AsyncCallback callback, object asyncState) {
            AppPengguna.SvcTLBongkaranCud.executeRequest inValue = new AppPengguna.SvcTLBongkaranCud.executeRequest();
            inValue.InputParameters = InputParameters;
            return ((AppPengguna.SvcTLBongkaranCud.execute_ptt)(this)).Beginexecute(inValue, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        AppPengguna.SvcTLBongkaranCud.executeResponse AppPengguna.SvcTLBongkaranCud.execute_ptt.Endexecute(System.IAsyncResult result) {
            return base.Channel.Endexecute(result);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public AppPengguna.SvcTLBongkaranCud.OutputParameters Endexecute(System.IAsyncResult result) {
            AppPengguna.SvcTLBongkaranCud.executeResponse retVal = ((AppPengguna.SvcTLBongkaranCud.execute_ptt)(this)).Endexecute(result);
            return retVal.OutputParameters;
        }
        
        private System.IAsyncResult OnBeginexecute(object[] inValues, System.AsyncCallback callback, object asyncState) {
            AppPengguna.SvcTLBongkaranCud.InputParameters InputParameters = ((AppPengguna.SvcTLBongkaranCud.InputParameters)(inValues[0]));
            return this.Beginexecute(InputParameters, callback, asyncState);
        }
        
        private object[] OnEndexecute(System.IAsyncResult result) {
            AppPengguna.SvcTLBongkaranCud.OutputParameters retVal = this.Endexecute(result);
            return new object[] {
                    retVal};
        }
        
        private void OnexecuteCompleted(object state) {
            if ((this.executeCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.executeCompleted(this, new executeCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void executeAsync(AppPengguna.SvcTLBongkaranCud.InputParameters InputParameters) {
            this.executeAsync(InputParameters, null);
        }
        
        public void executeAsync(AppPengguna.SvcTLBongkaranCud.InputParameters InputParameters, object userState) {
            if ((this.onBeginexecuteDelegate == null)) {
                this.onBeginexecuteDelegate = new BeginOperationDelegate(this.OnBeginexecute);
            }
            if ((this.onEndexecuteDelegate == null)) {
                this.onEndexecuteDelegate = new EndOperationDelegate(this.OnEndexecute);
            }
            if ((this.onexecuteCompletedDelegate == null)) {
                this.onexecuteCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnexecuteCompleted);
            }
            base.InvokeAsync(this.onBeginexecuteDelegate, new object[] {
                        InputParameters}, this.onEndexecuteDelegate, this.onexecuteCompletedDelegate, userState);
        }
    }
}
