﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppPengguna.SvcWasdalPSPBMNLAINDetailCrud {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://oracle.com/sca/soapservice/siman_wasdal/wasdalLain/detilCud", ConfigurationName="SvcWasdalPSPBMNLAINDetailCrud.execute_ptt")]
    public interface execute_ptt {
        
        // CODEGEN: Generating message contract since the operation execute is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="execute", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        AppPengguna.SvcWasdalPSPBMNLAINDetailCrud.executeResponse execute(AppPengguna.SvcWasdalPSPBMNLAINDetailCrud.executeRequest request);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="execute", ReplyAction="*")]
        System.IAsyncResult Beginexecute(AppPengguna.SvcWasdalPSPBMNLAINDetailCrud.executeRequest request, System.AsyncCallback callback, object asyncState);
        
        AppPengguna.SvcWasdalPSPBMNLAINDetailCrud.executeResponse Endexecute(System.IAsyncResult result);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SP_SK_WASDAL_BMN_PSP_LAIN")]
    public partial class InputParameters : object, System.ComponentModel.INotifyPropertyChanged {
        
        private System.Nullable<decimal> p_ID_SK_WASDALField;
        
        private bool p_ID_SK_WASDALFieldSpecified;
        
        private string p_SK_KEPUTUSANField;
        
        private string p_TGL_SKField;
        
        private string p_ID_ASETField;
        
        private System.Nullable<decimal> p_KUANTITASField;
        
        private bool p_KUANTITASFieldSpecified;
        
        private System.Nullable<decimal> p_NILAI_PERSETUJUANField;
        
        private bool p_NILAI_PERSETUJUANFieldSpecified;
        
        private string p_KD_STATUSField;
        
        private string p_JNS_BUKTI_LAKSANAField;
        
        private string p_NO_BUKTI_LAKSANAField;
        
        private string p_TGL_BUKTI_LAKSANAField;
        
        private string p_NM_PHK_LAINField;
        
        private System.Nullable<decimal> p_JANGKA_WAKTUField;
        
        private bool p_JANGKA_WAKTUFieldSpecified;
        
        private string p_DARI_TGLField;
        
        private string p_SD_TGLField;
        
        private string p_KETField;
        
        private string p_PERIODEField;
        
        private string p_IS_VALIDField;
        
        private string p_SELECTField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=0)]
        public System.Nullable<decimal> P_ID_SK_WASDAL {
            get {
                return this.p_ID_SK_WASDALField;
            }
            set {
                this.p_ID_SK_WASDALField = value;
                this.RaisePropertyChanged("P_ID_SK_WASDAL");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool P_ID_SK_WASDALSpecified {
            get {
                return this.p_ID_SK_WASDALFieldSpecified;
            }
            set {
                this.p_ID_SK_WASDALFieldSpecified = value;
                this.RaisePropertyChanged("P_ID_SK_WASDALSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=1)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=2)]
        public string P_TGL_SK {
            get {
                return this.p_TGL_SKField;
            }
            set {
                this.p_TGL_SKField = value;
                this.RaisePropertyChanged("P_TGL_SK");
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
        public System.Nullable<decimal> P_KUANTITAS {
            get {
                return this.p_KUANTITASField;
            }
            set {
                this.p_KUANTITASField = value;
                this.RaisePropertyChanged("P_KUANTITAS");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool P_KUANTITASSpecified {
            get {
                return this.p_KUANTITASFieldSpecified;
            }
            set {
                this.p_KUANTITASFieldSpecified = value;
                this.RaisePropertyChanged("P_KUANTITASSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=5)]
        public System.Nullable<decimal> P_NILAI_PERSETUJUAN {
            get {
                return this.p_NILAI_PERSETUJUANField;
            }
            set {
                this.p_NILAI_PERSETUJUANField = value;
                this.RaisePropertyChanged("P_NILAI_PERSETUJUAN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool P_NILAI_PERSETUJUANSpecified {
            get {
                return this.p_NILAI_PERSETUJUANFieldSpecified;
            }
            set {
                this.p_NILAI_PERSETUJUANFieldSpecified = value;
                this.RaisePropertyChanged("P_NILAI_PERSETUJUANSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=6)]
        public string P_KD_STATUS {
            get {
                return this.p_KD_STATUSField;
            }
            set {
                this.p_KD_STATUSField = value;
                this.RaisePropertyChanged("P_KD_STATUS");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=7)]
        public string P_JNS_BUKTI_LAKSANA {
            get {
                return this.p_JNS_BUKTI_LAKSANAField;
            }
            set {
                this.p_JNS_BUKTI_LAKSANAField = value;
                this.RaisePropertyChanged("P_JNS_BUKTI_LAKSANA");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=8)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=9)]
        public string P_TGL_BUKTI_LAKSANA {
            get {
                return this.p_TGL_BUKTI_LAKSANAField;
            }
            set {
                this.p_TGL_BUKTI_LAKSANAField = value;
                this.RaisePropertyChanged("P_TGL_BUKTI_LAKSANA");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=10)]
        public string P_NM_PHK_LAIN {
            get {
                return this.p_NM_PHK_LAINField;
            }
            set {
                this.p_NM_PHK_LAINField = value;
                this.RaisePropertyChanged("P_NM_PHK_LAIN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=11)]
        public System.Nullable<decimal> P_JANGKA_WAKTU {
            get {
                return this.p_JANGKA_WAKTUField;
            }
            set {
                this.p_JANGKA_WAKTUField = value;
                this.RaisePropertyChanged("P_JANGKA_WAKTU");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool P_JANGKA_WAKTUSpecified {
            get {
                return this.p_JANGKA_WAKTUFieldSpecified;
            }
            set {
                this.p_JANGKA_WAKTUFieldSpecified = value;
                this.RaisePropertyChanged("P_JANGKA_WAKTUSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=12)]
        public string P_DARI_TGL {
            get {
                return this.p_DARI_TGLField;
            }
            set {
                this.p_DARI_TGLField = value;
                this.RaisePropertyChanged("P_DARI_TGL");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=13)]
        public string P_SD_TGL {
            get {
                return this.p_SD_TGLField;
            }
            set {
                this.p_SD_TGLField = value;
                this.RaisePropertyChanged("P_SD_TGL");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=14)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=15)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=16)]
        public string P_IS_VALID {
            get {
                return this.p_IS_VALIDField;
            }
            set {
                this.p_IS_VALIDField = value;
                this.RaisePropertyChanged("P_IS_VALID");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=17)]
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SP_SK_WASDAL_BMN_PSP_LAIN")]
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
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SP_SK_WASDAL_BMN_PSP_LAIN", Order=0)]
        public AppPengguna.SvcWasdalPSPBMNLAINDetailCrud.InputParameters InputParameters;
        
        public executeRequest() {
        }
        
        public executeRequest(AppPengguna.SvcWasdalPSPBMNLAINDetailCrud.InputParameters InputParameters) {
            this.InputParameters = InputParameters;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class executeResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SP_SK_WASDAL_BMN_PSP_LAIN", Order=0)]
        public AppPengguna.SvcWasdalPSPBMNLAINDetailCrud.OutputParameters OutputParameters;
        
        public executeResponse() {
        }
        
        public executeResponse(AppPengguna.SvcWasdalPSPBMNLAINDetailCrud.OutputParameters OutputParameters) {
            this.OutputParameters = OutputParameters;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface execute_pttChannel : AppPengguna.SvcWasdalPSPBMNLAINDetailCrud.execute_ptt, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class executeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public executeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public AppPengguna.SvcWasdalPSPBMNLAINDetailCrud.OutputParameters Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((AppPengguna.SvcWasdalPSPBMNLAINDetailCrud.OutputParameters)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class execute_pttClient : System.ServiceModel.ClientBase<AppPengguna.SvcWasdalPSPBMNLAINDetailCrud.execute_ptt>, AppPengguna.SvcWasdalPSPBMNLAINDetailCrud.execute_ptt {
        
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
        AppPengguna.SvcWasdalPSPBMNLAINDetailCrud.executeResponse AppPengguna.SvcWasdalPSPBMNLAINDetailCrud.execute_ptt.execute(AppPengguna.SvcWasdalPSPBMNLAINDetailCrud.executeRequest request) {
            return base.Channel.execute(request);
        }
        
        public AppPengguna.SvcWasdalPSPBMNLAINDetailCrud.OutputParameters execute(AppPengguna.SvcWasdalPSPBMNLAINDetailCrud.InputParameters InputParameters) {
            AppPengguna.SvcWasdalPSPBMNLAINDetailCrud.executeRequest inValue = new AppPengguna.SvcWasdalPSPBMNLAINDetailCrud.executeRequest();
            inValue.InputParameters = InputParameters;
            AppPengguna.SvcWasdalPSPBMNLAINDetailCrud.executeResponse retVal = ((AppPengguna.SvcWasdalPSPBMNLAINDetailCrud.execute_ptt)(this)).execute(inValue);
            return retVal.OutputParameters;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult AppPengguna.SvcWasdalPSPBMNLAINDetailCrud.execute_ptt.Beginexecute(AppPengguna.SvcWasdalPSPBMNLAINDetailCrud.executeRequest request, System.AsyncCallback callback, object asyncState) {
            return base.Channel.Beginexecute(request, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult Beginexecute(AppPengguna.SvcWasdalPSPBMNLAINDetailCrud.InputParameters InputParameters, System.AsyncCallback callback, object asyncState) {
            AppPengguna.SvcWasdalPSPBMNLAINDetailCrud.executeRequest inValue = new AppPengguna.SvcWasdalPSPBMNLAINDetailCrud.executeRequest();
            inValue.InputParameters = InputParameters;
            return ((AppPengguna.SvcWasdalPSPBMNLAINDetailCrud.execute_ptt)(this)).Beginexecute(inValue, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        AppPengguna.SvcWasdalPSPBMNLAINDetailCrud.executeResponse AppPengguna.SvcWasdalPSPBMNLAINDetailCrud.execute_ptt.Endexecute(System.IAsyncResult result) {
            return base.Channel.Endexecute(result);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public AppPengguna.SvcWasdalPSPBMNLAINDetailCrud.OutputParameters Endexecute(System.IAsyncResult result) {
            AppPengguna.SvcWasdalPSPBMNLAINDetailCrud.executeResponse retVal = ((AppPengguna.SvcWasdalPSPBMNLAINDetailCrud.execute_ptt)(this)).Endexecute(result);
            return retVal.OutputParameters;
        }
        
        private System.IAsyncResult OnBeginexecute(object[] inValues, System.AsyncCallback callback, object asyncState) {
            AppPengguna.SvcWasdalPSPBMNLAINDetailCrud.InputParameters InputParameters = ((AppPengguna.SvcWasdalPSPBMNLAINDetailCrud.InputParameters)(inValues[0]));
            return this.Beginexecute(InputParameters, callback, asyncState);
        }
        
        private object[] OnEndexecute(System.IAsyncResult result) {
            AppPengguna.SvcWasdalPSPBMNLAINDetailCrud.OutputParameters retVal = this.Endexecute(result);
            return new object[] {
                    retVal};
        }
        
        private void OnexecuteCompleted(object state) {
            if ((this.executeCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.executeCompleted(this, new executeCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void executeAsync(AppPengguna.SvcWasdalPSPBMNLAINDetailCrud.InputParameters InputParameters) {
            this.executeAsync(InputParameters, null);
        }
        
        public void executeAsync(AppPengguna.SvcWasdalPSPBMNLAINDetailCrud.InputParameters InputParameters, object userState) {
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
