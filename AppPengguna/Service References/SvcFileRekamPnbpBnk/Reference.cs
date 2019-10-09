﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppPengguna.SvcFileRekamPnbpBnk {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://oracle.com/sca/soapservice/siman_wasdal/pnbpBongkaran/fileRekamBnk", ConfigurationName="SvcFileRekamPnbpBnk.execute_ptt")]
    public interface execute_ptt {
        
        // CODEGEN: Generating message contract since the operation execute is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="execute", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        AppPengguna.SvcFileRekamPnbpBnk.executeResponse execute(AppPengguna.SvcFileRekamPnbpBnk.executeRequest request);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="execute", ReplyAction="*")]
        System.IAsyncResult Beginexecute(AppPengguna.SvcFileRekamPnbpBnk.executeRequest request, System.AsyncCallback callback, object asyncState);
        
        AppPengguna.SvcFileRekamPnbpBnk.executeResponse Endexecute(System.IAsyncResult result);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2102.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SP_REKAM_PNBP_BNK_FILE")]
    public partial class InputParameters : object, System.ComponentModel.INotifyPropertyChanged {
        
        private System.Nullable<decimal> p_ID_PNBP_BNKField;
        
        private bool p_ID_PNBP_BNKFieldSpecified;
        
        private string p_NTPNField;
        
        private string p_SK_KEPUTUSANField;
        
        private byte[] p_FILE_DOKField;
        
        private string p_NM_FILEField;
        
        private string p_SELECTField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=0)]
        public System.Nullable<decimal> P_ID_PNBP_BNK {
            get {
                return this.p_ID_PNBP_BNKField;
            }
            set {
                this.p_ID_PNBP_BNKField = value;
                this.RaisePropertyChanged("P_ID_PNBP_BNK");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool P_ID_PNBP_BNKSpecified {
            get {
                return this.p_ID_PNBP_BNKFieldSpecified;
            }
            set {
                this.p_ID_PNBP_BNKFieldSpecified = value;
                this.RaisePropertyChanged("P_ID_PNBP_BNKSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=1)]
        public string P_NTPN {
            get {
                return this.p_NTPNField;
            }
            set {
                this.p_NTPNField = value;
                this.RaisePropertyChanged("P_NTPN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=2)]
        public string P_SK_KEPUTUSAN {
            get {
                return this.p_SK_KEPUTUSANField;
            }
            set {
                this.p_SK_KEPUTUSANField = value;
                this.RaisePropertyChanged("P_SK_KEPUTUSAN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary", IsNullable=true, Order=3)]
        public byte[] P_FILE_DOK {
            get {
                return this.p_FILE_DOKField;
            }
            set {
                this.p_FILE_DOKField = value;
                this.RaisePropertyChanged("P_FILE_DOK");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=4)]
        public string P_NM_FILE {
            get {
                return this.p_NM_FILEField;
            }
            set {
                this.p_NM_FILEField = value;
                this.RaisePropertyChanged("P_NM_FILE");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=5)]
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SP_REKAM_PNBP_BNK_FILE")]
    public partial class OutputParameters : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string pO_RESULTField;
        
        private string pO_RESULT_MESSAGEField;
        
        private string pO_NM_FILEField;
        
        private byte[] pO_FILE_PNBPField;
        
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
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=2)]
        public string PO_NM_FILE {
            get {
                return this.pO_NM_FILEField;
            }
            set {
                this.pO_NM_FILEField = value;
                this.RaisePropertyChanged("PO_NM_FILE");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary", IsNullable=true, Order=3)]
        public byte[] PO_FILE_PNBP {
            get {
                return this.pO_FILE_PNBPField;
            }
            set {
                this.pO_FILE_PNBPField = value;
                this.RaisePropertyChanged("PO_FILE_PNBP");
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
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SP_REKAM_PNBP_BNK_FILE", Order=0)]
        public AppPengguna.SvcFileRekamPnbpBnk.InputParameters InputParameters;
        
        public executeRequest() {
        }
        
        public executeRequest(AppPengguna.SvcFileRekamPnbpBnk.InputParameters InputParameters) {
            this.InputParameters = InputParameters;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class executeResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SP_REKAM_PNBP_BNK_FILE", Order=0)]
        public AppPengguna.SvcFileRekamPnbpBnk.OutputParameters OutputParameters;
        
        public executeResponse() {
        }
        
        public executeResponse(AppPengguna.SvcFileRekamPnbpBnk.OutputParameters OutputParameters) {
            this.OutputParameters = OutputParameters;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface execute_pttChannel : AppPengguna.SvcFileRekamPnbpBnk.execute_ptt, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class executeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public executeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public AppPengguna.SvcFileRekamPnbpBnk.OutputParameters Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((AppPengguna.SvcFileRekamPnbpBnk.OutputParameters)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class execute_pttClient : System.ServiceModel.ClientBase<AppPengguna.SvcFileRekamPnbpBnk.execute_ptt>, AppPengguna.SvcFileRekamPnbpBnk.execute_ptt {
        
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
        AppPengguna.SvcFileRekamPnbpBnk.executeResponse AppPengguna.SvcFileRekamPnbpBnk.execute_ptt.execute(AppPengguna.SvcFileRekamPnbpBnk.executeRequest request) {
            return base.Channel.execute(request);
        }
        
        public AppPengguna.SvcFileRekamPnbpBnk.OutputParameters execute(AppPengguna.SvcFileRekamPnbpBnk.InputParameters InputParameters) {
            AppPengguna.SvcFileRekamPnbpBnk.executeRequest inValue = new AppPengguna.SvcFileRekamPnbpBnk.executeRequest();
            inValue.InputParameters = InputParameters;
            AppPengguna.SvcFileRekamPnbpBnk.executeResponse retVal = ((AppPengguna.SvcFileRekamPnbpBnk.execute_ptt)(this)).execute(inValue);
            return retVal.OutputParameters;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult AppPengguna.SvcFileRekamPnbpBnk.execute_ptt.Beginexecute(AppPengguna.SvcFileRekamPnbpBnk.executeRequest request, System.AsyncCallback callback, object asyncState) {
            return base.Channel.Beginexecute(request, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult Beginexecute(AppPengguna.SvcFileRekamPnbpBnk.InputParameters InputParameters, System.AsyncCallback callback, object asyncState) {
            AppPengguna.SvcFileRekamPnbpBnk.executeRequest inValue = new AppPengguna.SvcFileRekamPnbpBnk.executeRequest();
            inValue.InputParameters = InputParameters;
            return ((AppPengguna.SvcFileRekamPnbpBnk.execute_ptt)(this)).Beginexecute(inValue, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        AppPengguna.SvcFileRekamPnbpBnk.executeResponse AppPengguna.SvcFileRekamPnbpBnk.execute_ptt.Endexecute(System.IAsyncResult result) {
            return base.Channel.Endexecute(result);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public AppPengguna.SvcFileRekamPnbpBnk.OutputParameters Endexecute(System.IAsyncResult result) {
            AppPengguna.SvcFileRekamPnbpBnk.executeResponse retVal = ((AppPengguna.SvcFileRekamPnbpBnk.execute_ptt)(this)).Endexecute(result);
            return retVal.OutputParameters;
        }
        
        private System.IAsyncResult OnBeginexecute(object[] inValues, System.AsyncCallback callback, object asyncState) {
            AppPengguna.SvcFileRekamPnbpBnk.InputParameters InputParameters = ((AppPengguna.SvcFileRekamPnbpBnk.InputParameters)(inValues[0]));
            return this.Beginexecute(InputParameters, callback, asyncState);
        }
        
        private object[] OnEndexecute(System.IAsyncResult result) {
            AppPengguna.SvcFileRekamPnbpBnk.OutputParameters retVal = this.Endexecute(result);
            return new object[] {
                    retVal};
        }
        
        private void OnexecuteCompleted(object state) {
            if ((this.executeCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.executeCompleted(this, new executeCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void executeAsync(AppPengguna.SvcFileRekamPnbpBnk.InputParameters InputParameters) {
            this.executeAsync(InputParameters, null);
        }
        
        public void executeAsync(AppPengguna.SvcFileRekamPnbpBnk.InputParameters InputParameters, object userState) {
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
