﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppPengguna.SvcGnrtLapPindahtgnPmkSkNew {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://oracle.com/sca/soapservice/siman_wasdal/wasdalLaporan/gnrtPindTgnPmkSkNewF" +
        "ile", ConfigurationName="SvcGnrtLapPindahtgnPmkSkNew.execute_ptt")]
    public interface execute_ptt {
        
        // CODEGEN: Generating message contract since the operation execute is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="execute", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        AppPengguna.SvcGnrtLapPindahtgnPmkSkNew.executeResponse execute(AppPengguna.SvcGnrtLapPindahtgnPmkSkNew.executeRequest request);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="execute", ReplyAction="*")]
        System.IAsyncResult Beginexecute(AppPengguna.SvcGnrtLapPindahtgnPmkSkNew.executeRequest request, System.AsyncCallback callback, object asyncState);
        
        AppPengguna.SvcGnrtLapPindahtgnPmkSkNew.executeResponse Endexecute(System.IAsyncResult result);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2046.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SP_GNRT_WASDAL_PDHTGN_PMKNEW")]
    public partial class InputParameters : object, System.ComponentModel.INotifyPropertyChanged {
        
        private System.Nullable<decimal> p_ID_USERField;
        
        private bool p_ID_USERFieldSpecified;
        
        private System.Nullable<decimal> p_MINField;
        
        private bool p_MINFieldSpecified;
        
        private System.Nullable<decimal> p_MAXField;
        
        private bool p_MAXFieldSpecified;
        
        private string p_COUNTField;
        
        private System.Nullable<decimal> p_THN_ANGField;
        
        private bool p_THN_ANGFieldSpecified;
        
        private System.Nullable<decimal> p_ID_SATKERField;
        
        private bool p_ID_SATKERFieldSpecified;
        
        private string sTR_WHEREField;
        
        private string p_JNSField;
        
        private string p_COLField;
        
        private string p_SORTField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=0)]
        public System.Nullable<decimal> P_ID_USER {
            get {
                return this.p_ID_USERField;
            }
            set {
                this.p_ID_USERField = value;
                this.RaisePropertyChanged("P_ID_USER");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool P_ID_USERSpecified {
            get {
                return this.p_ID_USERFieldSpecified;
            }
            set {
                this.p_ID_USERFieldSpecified = value;
                this.RaisePropertyChanged("P_ID_USERSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=1)]
        public System.Nullable<decimal> P_MIN {
            get {
                return this.p_MINField;
            }
            set {
                this.p_MINField = value;
                this.RaisePropertyChanged("P_MIN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool P_MINSpecified {
            get {
                return this.p_MINFieldSpecified;
            }
            set {
                this.p_MINFieldSpecified = value;
                this.RaisePropertyChanged("P_MINSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=2)]
        public System.Nullable<decimal> P_MAX {
            get {
                return this.p_MAXField;
            }
            set {
                this.p_MAXField = value;
                this.RaisePropertyChanged("P_MAX");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool P_MAXSpecified {
            get {
                return this.p_MAXFieldSpecified;
            }
            set {
                this.p_MAXFieldSpecified = value;
                this.RaisePropertyChanged("P_MAXSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=3)]
        public string P_COUNT {
            get {
                return this.p_COUNTField;
            }
            set {
                this.p_COUNTField = value;
                this.RaisePropertyChanged("P_COUNT");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=4)]
        public System.Nullable<decimal> P_THN_ANG {
            get {
                return this.p_THN_ANGField;
            }
            set {
                this.p_THN_ANGField = value;
                this.RaisePropertyChanged("P_THN_ANG");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool P_THN_ANGSpecified {
            get {
                return this.p_THN_ANGFieldSpecified;
            }
            set {
                this.p_THN_ANGFieldSpecified = value;
                this.RaisePropertyChanged("P_THN_ANGSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=5)]
        public System.Nullable<decimal> P_ID_SATKER {
            get {
                return this.p_ID_SATKERField;
            }
            set {
                this.p_ID_SATKERField = value;
                this.RaisePropertyChanged("P_ID_SATKER");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool P_ID_SATKERSpecified {
            get {
                return this.p_ID_SATKERFieldSpecified;
            }
            set {
                this.p_ID_SATKERFieldSpecified = value;
                this.RaisePropertyChanged("P_ID_SATKERSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=6)]
        public string STR_WHERE {
            get {
                return this.sTR_WHEREField;
            }
            set {
                this.sTR_WHEREField = value;
                this.RaisePropertyChanged("STR_WHERE");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=7)]
        public string P_JNS {
            get {
                return this.p_JNSField;
            }
            set {
                this.p_JNSField = value;
                this.RaisePropertyChanged("P_JNS");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=8)]
        public string P_COL {
            get {
                return this.p_COLField;
            }
            set {
                this.p_COLField = value;
                this.RaisePropertyChanged("P_COL");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=9)]
        public string P_SORT {
            get {
                return this.p_SORTField;
            }
            set {
                this.p_SORTField = value;
                this.RaisePropertyChanged("P_SORT");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2046.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SP_GNRT_WASDAL_PDHTGN_PMKNEW")]
    public partial class OutputParameters : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string pO_RESULTField;
        
        private string pO_RESULT_MESSAGEField;
        
        private byte[] pO_FILE_BLOBField;
        
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
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary", IsNullable=true, Order=2)]
        public byte[] PO_FILE_BLOB {
            get {
                return this.pO_FILE_BLOBField;
            }
            set {
                this.pO_FILE_BLOBField = value;
                this.RaisePropertyChanged("PO_FILE_BLOB");
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
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SP_GNRT_WASDAL_PDHTGN_PMKNEW", Order=0)]
        public AppPengguna.SvcGnrtLapPindahtgnPmkSkNew.InputParameters InputParameters;
        
        public executeRequest() {
        }
        
        public executeRequest(AppPengguna.SvcGnrtLapPindahtgnPmkSkNew.InputParameters InputParameters) {
            this.InputParameters = InputParameters;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class executeResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SP_GNRT_WASDAL_PDHTGN_PMKNEW", Order=0)]
        public AppPengguna.SvcGnrtLapPindahtgnPmkSkNew.OutputParameters OutputParameters;
        
        public executeResponse() {
        }
        
        public executeResponse(AppPengguna.SvcGnrtLapPindahtgnPmkSkNew.OutputParameters OutputParameters) {
            this.OutputParameters = OutputParameters;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface execute_pttChannel : AppPengguna.SvcGnrtLapPindahtgnPmkSkNew.execute_ptt, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class executeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public executeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public AppPengguna.SvcGnrtLapPindahtgnPmkSkNew.OutputParameters Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((AppPengguna.SvcGnrtLapPindahtgnPmkSkNew.OutputParameters)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class execute_pttClient : System.ServiceModel.ClientBase<AppPengguna.SvcGnrtLapPindahtgnPmkSkNew.execute_ptt>, AppPengguna.SvcGnrtLapPindahtgnPmkSkNew.execute_ptt {
        
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
        AppPengguna.SvcGnrtLapPindahtgnPmkSkNew.executeResponse AppPengguna.SvcGnrtLapPindahtgnPmkSkNew.execute_ptt.execute(AppPengguna.SvcGnrtLapPindahtgnPmkSkNew.executeRequest request) {
            return base.Channel.execute(request);
        }
        
        public AppPengguna.SvcGnrtLapPindahtgnPmkSkNew.OutputParameters execute(AppPengguna.SvcGnrtLapPindahtgnPmkSkNew.InputParameters InputParameters) {
            AppPengguna.SvcGnrtLapPindahtgnPmkSkNew.executeRequest inValue = new AppPengguna.SvcGnrtLapPindahtgnPmkSkNew.executeRequest();
            inValue.InputParameters = InputParameters;
            AppPengguna.SvcGnrtLapPindahtgnPmkSkNew.executeResponse retVal = ((AppPengguna.SvcGnrtLapPindahtgnPmkSkNew.execute_ptt)(this)).execute(inValue);
            return retVal.OutputParameters;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult AppPengguna.SvcGnrtLapPindahtgnPmkSkNew.execute_ptt.Beginexecute(AppPengguna.SvcGnrtLapPindahtgnPmkSkNew.executeRequest request, System.AsyncCallback callback, object asyncState) {
            return base.Channel.Beginexecute(request, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult Beginexecute(AppPengguna.SvcGnrtLapPindahtgnPmkSkNew.InputParameters InputParameters, System.AsyncCallback callback, object asyncState) {
            AppPengguna.SvcGnrtLapPindahtgnPmkSkNew.executeRequest inValue = new AppPengguna.SvcGnrtLapPindahtgnPmkSkNew.executeRequest();
            inValue.InputParameters = InputParameters;
            return ((AppPengguna.SvcGnrtLapPindahtgnPmkSkNew.execute_ptt)(this)).Beginexecute(inValue, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        AppPengguna.SvcGnrtLapPindahtgnPmkSkNew.executeResponse AppPengguna.SvcGnrtLapPindahtgnPmkSkNew.execute_ptt.Endexecute(System.IAsyncResult result) {
            return base.Channel.Endexecute(result);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public AppPengguna.SvcGnrtLapPindahtgnPmkSkNew.OutputParameters Endexecute(System.IAsyncResult result) {
            AppPengguna.SvcGnrtLapPindahtgnPmkSkNew.executeResponse retVal = ((AppPengguna.SvcGnrtLapPindahtgnPmkSkNew.execute_ptt)(this)).Endexecute(result);
            return retVal.OutputParameters;
        }
        
        private System.IAsyncResult OnBeginexecute(object[] inValues, System.AsyncCallback callback, object asyncState) {
            AppPengguna.SvcGnrtLapPindahtgnPmkSkNew.InputParameters InputParameters = ((AppPengguna.SvcGnrtLapPindahtgnPmkSkNew.InputParameters)(inValues[0]));
            return this.Beginexecute(InputParameters, callback, asyncState);
        }
        
        private object[] OnEndexecute(System.IAsyncResult result) {
            AppPengguna.SvcGnrtLapPindahtgnPmkSkNew.OutputParameters retVal = this.Endexecute(result);
            return new object[] {
                    retVal};
        }
        
        private void OnexecuteCompleted(object state) {
            if ((this.executeCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.executeCompleted(this, new executeCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void executeAsync(AppPengguna.SvcGnrtLapPindahtgnPmkSkNew.InputParameters InputParameters) {
            this.executeAsync(InputParameters, null);
        }
        
        public void executeAsync(AppPengguna.SvcGnrtLapPindahtgnPmkSkNew.InputParameters InputParameters, object userState) {
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
