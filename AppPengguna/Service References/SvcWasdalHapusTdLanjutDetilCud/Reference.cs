﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppPengguna.SvcWasdalHapusTdLanjutDetilCud {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://oracle.com/sca/soapservice/siman_wasdal/TLWasdalHapus/detilCud", ConfigurationName="SvcWasdalHapusTdLanjutDetilCud.execute_ptt")]
    public interface execute_ptt {
        
        // CODEGEN: Generating message contract since the operation execute is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="execute", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        AppPengguna.SvcWasdalHapusTdLanjutDetilCud.executeResponse execute(AppPengguna.SvcWasdalHapusTdLanjutDetilCud.executeRequest request);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="execute", ReplyAction="*")]
        System.IAsyncResult Beginexecute(AppPengguna.SvcWasdalHapusTdLanjutDetilCud.executeRequest request, System.AsyncCallback callback, object asyncState);
        
        AppPengguna.SvcWasdalHapusTdLanjutDetilCud.executeResponse Endexecute(System.IAsyncResult result);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SP_SK_WASDAL_BMN_HAPUS_TL")]
    public partial class InputParameters : object, System.ComponentModel.INotifyPropertyChanged {
        
        private System.Nullable<decimal> p_ID_TL_WASDAL_PENGHAPUSANField;
        
        private bool p_ID_TL_WASDAL_PENGHAPUSANFieldSpecified;
        
        private System.Nullable<decimal> p_ID_SK_WASDAL_HAPUSField;
        
        private bool p_ID_SK_WASDAL_HAPUSFieldSpecified;
        
        private string p_SK_KEPUTUSANField;
        
        private string p_ID_ASETField;
        
        private string p_NO_BUKTI_LAKSANAField;
        
        private string p_KD_PELAYANANField;
        
        private string p_SELECTField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=0)]
        public System.Nullable<decimal> P_ID_TL_WASDAL_PENGHAPUSAN {
            get {
                return this.p_ID_TL_WASDAL_PENGHAPUSANField;
            }
            set {
                this.p_ID_TL_WASDAL_PENGHAPUSANField = value;
                this.RaisePropertyChanged("P_ID_TL_WASDAL_PENGHAPUSAN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool P_ID_TL_WASDAL_PENGHAPUSANSpecified {
            get {
                return this.p_ID_TL_WASDAL_PENGHAPUSANFieldSpecified;
            }
            set {
                this.p_ID_TL_WASDAL_PENGHAPUSANFieldSpecified = value;
                this.RaisePropertyChanged("P_ID_TL_WASDAL_PENGHAPUSANSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=1)]
        public System.Nullable<decimal> P_ID_SK_WASDAL_HAPUS {
            get {
                return this.p_ID_SK_WASDAL_HAPUSField;
            }
            set {
                this.p_ID_SK_WASDAL_HAPUSField = value;
                this.RaisePropertyChanged("P_ID_SK_WASDAL_HAPUS");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool P_ID_SK_WASDAL_HAPUSSpecified {
            get {
                return this.p_ID_SK_WASDAL_HAPUSFieldSpecified;
            }
            set {
                this.p_ID_SK_WASDAL_HAPUSFieldSpecified = value;
                this.RaisePropertyChanged("P_ID_SK_WASDAL_HAPUSSpecified");
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=3)]
        public string P_ID_ASET {
            get {
                return this.p_ID_ASETField;
            }
            set {
                this.p_ID_ASETField = value;
                this.RaisePropertyChanged("P_ID_ASET");
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=6)]
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SP_SK_WASDAL_BMN_HAPUS_TL")]
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
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SP_SK_WASDAL_BMN_HAPUS_TL", Order=0)]
        public AppPengguna.SvcWasdalHapusTdLanjutDetilCud.InputParameters InputParameters;
        
        public executeRequest() {
        }
        
        public executeRequest(AppPengguna.SvcWasdalHapusTdLanjutDetilCud.InputParameters InputParameters) {
            this.InputParameters = InputParameters;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class executeResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SP_SK_WASDAL_BMN_HAPUS_TL", Order=0)]
        public AppPengguna.SvcWasdalHapusTdLanjutDetilCud.OutputParameters OutputParameters;
        
        public executeResponse() {
        }
        
        public executeResponse(AppPengguna.SvcWasdalHapusTdLanjutDetilCud.OutputParameters OutputParameters) {
            this.OutputParameters = OutputParameters;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface execute_pttChannel : AppPengguna.SvcWasdalHapusTdLanjutDetilCud.execute_ptt, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class executeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public executeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public AppPengguna.SvcWasdalHapusTdLanjutDetilCud.OutputParameters Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((AppPengguna.SvcWasdalHapusTdLanjutDetilCud.OutputParameters)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class execute_pttClient : System.ServiceModel.ClientBase<AppPengguna.SvcWasdalHapusTdLanjutDetilCud.execute_ptt>, AppPengguna.SvcWasdalHapusTdLanjutDetilCud.execute_ptt {
        
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
        AppPengguna.SvcWasdalHapusTdLanjutDetilCud.executeResponse AppPengguna.SvcWasdalHapusTdLanjutDetilCud.execute_ptt.execute(AppPengguna.SvcWasdalHapusTdLanjutDetilCud.executeRequest request) {
            return base.Channel.execute(request);
        }
        
        public AppPengguna.SvcWasdalHapusTdLanjutDetilCud.OutputParameters execute(AppPengguna.SvcWasdalHapusTdLanjutDetilCud.InputParameters InputParameters) {
            AppPengguna.SvcWasdalHapusTdLanjutDetilCud.executeRequest inValue = new AppPengguna.SvcWasdalHapusTdLanjutDetilCud.executeRequest();
            inValue.InputParameters = InputParameters;
            AppPengguna.SvcWasdalHapusTdLanjutDetilCud.executeResponse retVal = ((AppPengguna.SvcWasdalHapusTdLanjutDetilCud.execute_ptt)(this)).execute(inValue);
            return retVal.OutputParameters;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult AppPengguna.SvcWasdalHapusTdLanjutDetilCud.execute_ptt.Beginexecute(AppPengguna.SvcWasdalHapusTdLanjutDetilCud.executeRequest request, System.AsyncCallback callback, object asyncState) {
            return base.Channel.Beginexecute(request, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult Beginexecute(AppPengguna.SvcWasdalHapusTdLanjutDetilCud.InputParameters InputParameters, System.AsyncCallback callback, object asyncState) {
            AppPengguna.SvcWasdalHapusTdLanjutDetilCud.executeRequest inValue = new AppPengguna.SvcWasdalHapusTdLanjutDetilCud.executeRequest();
            inValue.InputParameters = InputParameters;
            return ((AppPengguna.SvcWasdalHapusTdLanjutDetilCud.execute_ptt)(this)).Beginexecute(inValue, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        AppPengguna.SvcWasdalHapusTdLanjutDetilCud.executeResponse AppPengguna.SvcWasdalHapusTdLanjutDetilCud.execute_ptt.Endexecute(System.IAsyncResult result) {
            return base.Channel.Endexecute(result);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public AppPengguna.SvcWasdalHapusTdLanjutDetilCud.OutputParameters Endexecute(System.IAsyncResult result) {
            AppPengguna.SvcWasdalHapusTdLanjutDetilCud.executeResponse retVal = ((AppPengguna.SvcWasdalHapusTdLanjutDetilCud.execute_ptt)(this)).Endexecute(result);
            return retVal.OutputParameters;
        }
        
        private System.IAsyncResult OnBeginexecute(object[] inValues, System.AsyncCallback callback, object asyncState) {
            AppPengguna.SvcWasdalHapusTdLanjutDetilCud.InputParameters InputParameters = ((AppPengguna.SvcWasdalHapusTdLanjutDetilCud.InputParameters)(inValues[0]));
            return this.Beginexecute(InputParameters, callback, asyncState);
        }
        
        private object[] OnEndexecute(System.IAsyncResult result) {
            AppPengguna.SvcWasdalHapusTdLanjutDetilCud.OutputParameters retVal = this.Endexecute(result);
            return new object[] {
                    retVal};
        }
        
        private void OnexecuteCompleted(object state) {
            if ((this.executeCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.executeCompleted(this, new executeCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void executeAsync(AppPengguna.SvcWasdalHapusTdLanjutDetilCud.InputParameters InputParameters) {
            this.executeAsync(InputParameters, null);
        }
        
        public void executeAsync(AppPengguna.SvcWasdalHapusTdLanjutDetilCud.InputParameters InputParameters, object userState) {
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