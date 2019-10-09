﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppPengguna.SvcPnbpValidasiSatker {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://oracle.com/sca/soapservice/siman_wasdal/wasSpan2/validasiSatker", ConfigurationName="SvcPnbpValidasiSatker.execute_ptt")]
    public interface execute_ptt {
        
        // CODEGEN: Generating message contract since the operation execute is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="execute", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        AppPengguna.SvcPnbpValidasiSatker.executeResponse execute(AppPengguna.SvcPnbpValidasiSatker.executeRequest request);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="execute", ReplyAction="*")]
        System.IAsyncResult Beginexecute(AppPengguna.SvcPnbpValidasiSatker.executeRequest request, System.AsyncCallback callback, object asyncState);
        
        AppPengguna.SvcPnbpValidasiSatker.executeResponse Endexecute(System.IAsyncResult result);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.79.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SP_CUD_PNBP_SIMAN")]
    public partial class InputParameters : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string p_PERIODEField;
        
        private string p_NTPNField;
        
        private string p_NTBField;
        
        private string p_KD_BILLINGField;
        
        private System.Nullable<System.DateTime> p_TANGGALField;
        
        private bool p_TANGGALFieldSpecified;
        
        private System.Nullable<decimal> p_NIL_PNBP_SPANField;
        
        private bool p_NIL_PNBP_SPANFieldSpecified;
        
        private System.Nullable<decimal> p_NIL_PNBP_SIMANField;
        
        private bool p_NIL_PNBP_SIMANFieldSpecified;
        
        private System.Nullable<decimal> p_SELISIHField;
        
        private bool p_SELISIHFieldSpecified;
        
        private string p_KD_AKUNField;
        
        private string p_UR_PENDAPATANField;
        
        private string p_KD_SATKER_SPANField;
        
        private System.Nullable<decimal> p_ID_SATKERField;
        
        private bool p_ID_SATKERFieldSpecified;
        
        private string p_KD_SATKERField;
        
        private string p_UR_SATKERField;
        
        private string p_SK_KEPUTUSANField;
        
        private string p_CATATANField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=0)]
        public string P_PERIODE {
            get {
                return this.p_PERIODEField;
            }
            set {
                this.p_PERIODEField = value;
                this.RaisePropertyChanged("P_PERIODE");
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
        public string P_NTB {
            get {
                return this.p_NTBField;
            }
            set {
                this.p_NTBField = value;
                this.RaisePropertyChanged("P_NTB");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=3)]
        public string P_KD_BILLING {
            get {
                return this.p_KD_BILLINGField;
            }
            set {
                this.p_KD_BILLINGField = value;
                this.RaisePropertyChanged("P_KD_BILLING");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=4)]
        public System.Nullable<System.DateTime> P_TANGGAL {
            get {
                return this.p_TANGGALField;
            }
            set {
                this.p_TANGGALField = value;
                this.RaisePropertyChanged("P_TANGGAL");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool P_TANGGALSpecified {
            get {
                return this.p_TANGGALFieldSpecified;
            }
            set {
                this.p_TANGGALFieldSpecified = value;
                this.RaisePropertyChanged("P_TANGGALSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=5)]
        public System.Nullable<decimal> P_NIL_PNBP_SPAN {
            get {
                return this.p_NIL_PNBP_SPANField;
            }
            set {
                this.p_NIL_PNBP_SPANField = value;
                this.RaisePropertyChanged("P_NIL_PNBP_SPAN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool P_NIL_PNBP_SPANSpecified {
            get {
                return this.p_NIL_PNBP_SPANFieldSpecified;
            }
            set {
                this.p_NIL_PNBP_SPANFieldSpecified = value;
                this.RaisePropertyChanged("P_NIL_PNBP_SPANSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=6)]
        public System.Nullable<decimal> P_NIL_PNBP_SIMAN {
            get {
                return this.p_NIL_PNBP_SIMANField;
            }
            set {
                this.p_NIL_PNBP_SIMANField = value;
                this.RaisePropertyChanged("P_NIL_PNBP_SIMAN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool P_NIL_PNBP_SIMANSpecified {
            get {
                return this.p_NIL_PNBP_SIMANFieldSpecified;
            }
            set {
                this.p_NIL_PNBP_SIMANFieldSpecified = value;
                this.RaisePropertyChanged("P_NIL_PNBP_SIMANSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=7)]
        public System.Nullable<decimal> P_SELISIH {
            get {
                return this.p_SELISIHField;
            }
            set {
                this.p_SELISIHField = value;
                this.RaisePropertyChanged("P_SELISIH");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool P_SELISIHSpecified {
            get {
                return this.p_SELISIHFieldSpecified;
            }
            set {
                this.p_SELISIHFieldSpecified = value;
                this.RaisePropertyChanged("P_SELISIHSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=8)]
        public string P_KD_AKUN {
            get {
                return this.p_KD_AKUNField;
            }
            set {
                this.p_KD_AKUNField = value;
                this.RaisePropertyChanged("P_KD_AKUN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=9)]
        public string P_UR_PENDAPATAN {
            get {
                return this.p_UR_PENDAPATANField;
            }
            set {
                this.p_UR_PENDAPATANField = value;
                this.RaisePropertyChanged("P_UR_PENDAPATAN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=10)]
        public string P_KD_SATKER_SPAN {
            get {
                return this.p_KD_SATKER_SPANField;
            }
            set {
                this.p_KD_SATKER_SPANField = value;
                this.RaisePropertyChanged("P_KD_SATKER_SPAN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=11)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=12)]
        public string P_KD_SATKER {
            get {
                return this.p_KD_SATKERField;
            }
            set {
                this.p_KD_SATKERField = value;
                this.RaisePropertyChanged("P_KD_SATKER");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=13)]
        public string P_UR_SATKER {
            get {
                return this.p_UR_SATKERField;
            }
            set {
                this.p_UR_SATKERField = value;
                this.RaisePropertyChanged("P_UR_SATKER");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=14)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=15)]
        public string P_CATATAN {
            get {
                return this.p_CATATANField;
            }
            set {
                this.p_CATATANField = value;
                this.RaisePropertyChanged("P_CATATAN");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.79.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SP_CUD_PNBP_SIMAN")]
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
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SP_CUD_PNBP_SIMAN", Order=0)]
        public AppPengguna.SvcPnbpValidasiSatker.InputParameters InputParameters;
        
        public executeRequest() {
        }
        
        public executeRequest(AppPengguna.SvcPnbpValidasiSatker.InputParameters InputParameters) {
            this.InputParameters = InputParameters;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class executeResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SP_CUD_PNBP_SIMAN", Order=0)]
        public AppPengguna.SvcPnbpValidasiSatker.OutputParameters OutputParameters;
        
        public executeResponse() {
        }
        
        public executeResponse(AppPengguna.SvcPnbpValidasiSatker.OutputParameters OutputParameters) {
            this.OutputParameters = OutputParameters;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface execute_pttChannel : AppPengguna.SvcPnbpValidasiSatker.execute_ptt, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class executeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public executeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public AppPengguna.SvcPnbpValidasiSatker.OutputParameters Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((AppPengguna.SvcPnbpValidasiSatker.OutputParameters)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class execute_pttClient : System.ServiceModel.ClientBase<AppPengguna.SvcPnbpValidasiSatker.execute_ptt>, AppPengguna.SvcPnbpValidasiSatker.execute_ptt {
        
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
        AppPengguna.SvcPnbpValidasiSatker.executeResponse AppPengguna.SvcPnbpValidasiSatker.execute_ptt.execute(AppPengguna.SvcPnbpValidasiSatker.executeRequest request) {
            return base.Channel.execute(request);
        }
        
        public AppPengguna.SvcPnbpValidasiSatker.OutputParameters execute(AppPengguna.SvcPnbpValidasiSatker.InputParameters InputParameters) {
            AppPengguna.SvcPnbpValidasiSatker.executeRequest inValue = new AppPengguna.SvcPnbpValidasiSatker.executeRequest();
            inValue.InputParameters = InputParameters;
            AppPengguna.SvcPnbpValidasiSatker.executeResponse retVal = ((AppPengguna.SvcPnbpValidasiSatker.execute_ptt)(this)).execute(inValue);
            return retVal.OutputParameters;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult AppPengguna.SvcPnbpValidasiSatker.execute_ptt.Beginexecute(AppPengguna.SvcPnbpValidasiSatker.executeRequest request, System.AsyncCallback callback, object asyncState) {
            return base.Channel.Beginexecute(request, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult Beginexecute(AppPengguna.SvcPnbpValidasiSatker.InputParameters InputParameters, System.AsyncCallback callback, object asyncState) {
            AppPengguna.SvcPnbpValidasiSatker.executeRequest inValue = new AppPengguna.SvcPnbpValidasiSatker.executeRequest();
            inValue.InputParameters = InputParameters;
            return ((AppPengguna.SvcPnbpValidasiSatker.execute_ptt)(this)).Beginexecute(inValue, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        AppPengguna.SvcPnbpValidasiSatker.executeResponse AppPengguna.SvcPnbpValidasiSatker.execute_ptt.Endexecute(System.IAsyncResult result) {
            return base.Channel.Endexecute(result);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public AppPengguna.SvcPnbpValidasiSatker.OutputParameters Endexecute(System.IAsyncResult result) {
            AppPengguna.SvcPnbpValidasiSatker.executeResponse retVal = ((AppPengguna.SvcPnbpValidasiSatker.execute_ptt)(this)).Endexecute(result);
            return retVal.OutputParameters;
        }
        
        private System.IAsyncResult OnBeginexecute(object[] inValues, System.AsyncCallback callback, object asyncState) {
            AppPengguna.SvcPnbpValidasiSatker.InputParameters InputParameters = ((AppPengguna.SvcPnbpValidasiSatker.InputParameters)(inValues[0]));
            return this.Beginexecute(InputParameters, callback, asyncState);
        }
        
        private object[] OnEndexecute(System.IAsyncResult result) {
            AppPengguna.SvcPnbpValidasiSatker.OutputParameters retVal = this.Endexecute(result);
            return new object[] {
                    retVal};
        }
        
        private void OnexecuteCompleted(object state) {
            if ((this.executeCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.executeCompleted(this, new executeCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void executeAsync(AppPengguna.SvcPnbpValidasiSatker.InputParameters InputParameters) {
            this.executeAsync(InputParameters, null);
        }
        
        public void executeAsync(AppPengguna.SvcPnbpValidasiSatker.InputParameters InputParameters, object userState) {
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
