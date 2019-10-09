﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppPengguna.SvcWasdalLainTindakLanjutCud {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://oracle.com/sca/soapservice/siman_wasdal/TLWasdalLain/cud", ConfigurationName="SvcWasdalLainTindakLanjutCud.execute_ptt")]
    public interface execute_ptt {
        
        // CODEGEN: Generating message contract since the operation execute is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="execute", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        AppPengguna.SvcWasdalLainTindakLanjutCud.executeResponse execute(AppPengguna.SvcWasdalLainTindakLanjutCud.executeRequest request);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="execute", ReplyAction="*")]
        System.IAsyncResult Beginexecute(AppPengguna.SvcWasdalLainTindakLanjutCud.executeRequest request, System.AsyncCallback callback, object asyncState);
        
        AppPengguna.SvcWasdalLainTindakLanjutCud.executeResponse Endexecute(System.IAsyncResult result);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SP_WASDAL_PENGGUNAAN_TL2")]
    public partial class InputParameters : object, System.ComponentModel.INotifyPropertyChanged {
        
        private System.Nullable<decimal> p_ID_TL_WASDAL_GUNAField;
        
        private bool p_ID_TL_WASDAL_GUNAFieldSpecified;
        
        private System.Nullable<decimal> p_ID_SK_WASDALField;
        
        private bool p_ID_SK_WASDALFieldSpecified;
        
        private string p_SK_KEPUTUSANField;
        
        private string p_JNS_BUKTI_LAKSANAANField;
        
        private string p_NO_BUKTI_LAKSANAField;
        
        private string p_TGL_BUKTI_LAKSANAField;
        
        private string p_NO_REKAM_PERJANJIANField;
        
        private string p_TGL_REKAM_PERJANJIANField;
        
        private string p_UR_PHK_LAINField;
        
        private System.Nullable<decimal> p_JANGKA_WAKTUField;
        
        private bool p_JANGKA_WAKTUFieldSpecified;
        
        private string p_PERIODEField;
        
        private string p_DARI_TGLField;
        
        private string p_SD_TGLField;
        
        private string p_KD_PELAYANANField;
        
        private string p_KETField;
        
        private string p_STATUS_PERJANJIANField;
        
        private string p_NO_BASTField;
        
        private string p_TGL_BASTField;
        
        private byte[] p_FILE_BASTField;
        
        private string p_NM_FILE_BASTField;
        
        private string p_SELECTField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=0)]
        public System.Nullable<decimal> P_ID_TL_WASDAL_GUNA {
            get {
                return this.p_ID_TL_WASDAL_GUNAField;
            }
            set {
                this.p_ID_TL_WASDAL_GUNAField = value;
                this.RaisePropertyChanged("P_ID_TL_WASDAL_GUNA");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool P_ID_TL_WASDAL_GUNASpecified {
            get {
                return this.p_ID_TL_WASDAL_GUNAFieldSpecified;
            }
            set {
                this.p_ID_TL_WASDAL_GUNAFieldSpecified = value;
                this.RaisePropertyChanged("P_ID_TL_WASDAL_GUNASpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=1)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=6)]
        public string P_NO_REKAM_PERJANJIAN {
            get {
                return this.p_NO_REKAM_PERJANJIANField;
            }
            set {
                this.p_NO_REKAM_PERJANJIANField = value;
                this.RaisePropertyChanged("P_NO_REKAM_PERJANJIAN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=7)]
        public string P_TGL_REKAM_PERJANJIAN {
            get {
                return this.p_TGL_REKAM_PERJANJIANField;
            }
            set {
                this.p_TGL_REKAM_PERJANJIANField = value;
                this.RaisePropertyChanged("P_TGL_REKAM_PERJANJIAN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=8)]
        public string P_UR_PHK_LAIN {
            get {
                return this.p_UR_PHK_LAINField;
            }
            set {
                this.p_UR_PHK_LAINField = value;
                this.RaisePropertyChanged("P_UR_PHK_LAIN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=9)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=10)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=11)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=12)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=13)]
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
        public string P_STATUS_PERJANJIAN {
            get {
                return this.p_STATUS_PERJANJIANField;
            }
            set {
                this.p_STATUS_PERJANJIANField = value;
                this.RaisePropertyChanged("P_STATUS_PERJANJIAN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=16)]
        public string P_NO_BAST {
            get {
                return this.p_NO_BASTField;
            }
            set {
                this.p_NO_BASTField = value;
                this.RaisePropertyChanged("P_NO_BAST");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=17)]
        public string P_TGL_BAST {
            get {
                return this.p_TGL_BASTField;
            }
            set {
                this.p_TGL_BASTField = value;
                this.RaisePropertyChanged("P_TGL_BAST");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary", IsNullable=true, Order=18)]
        public byte[] P_FILE_BAST {
            get {
                return this.p_FILE_BASTField;
            }
            set {
                this.p_FILE_BASTField = value;
                this.RaisePropertyChanged("P_FILE_BAST");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=19)]
        public string P_NM_FILE_BAST {
            get {
                return this.p_NM_FILE_BASTField;
            }
            set {
                this.p_NM_FILE_BASTField = value;
                this.RaisePropertyChanged("P_NM_FILE_BAST");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=20)]
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SP_WASDAL_PENGGUNAAN_TL2")]
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
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SP_WASDAL_PENGGUNAAN_TL2", Order=0)]
        public AppPengguna.SvcWasdalLainTindakLanjutCud.InputParameters InputParameters;
        
        public executeRequest() {
        }
        
        public executeRequest(AppPengguna.SvcWasdalLainTindakLanjutCud.InputParameters InputParameters) {
            this.InputParameters = InputParameters;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class executeResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SP_WASDAL_PENGGUNAAN_TL2", Order=0)]
        public AppPengguna.SvcWasdalLainTindakLanjutCud.OutputParameters OutputParameters;
        
        public executeResponse() {
        }
        
        public executeResponse(AppPengguna.SvcWasdalLainTindakLanjutCud.OutputParameters OutputParameters) {
            this.OutputParameters = OutputParameters;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface execute_pttChannel : AppPengguna.SvcWasdalLainTindakLanjutCud.execute_ptt, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class executeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public executeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public AppPengguna.SvcWasdalLainTindakLanjutCud.OutputParameters Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((AppPengguna.SvcWasdalLainTindakLanjutCud.OutputParameters)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class execute_pttClient : System.ServiceModel.ClientBase<AppPengguna.SvcWasdalLainTindakLanjutCud.execute_ptt>, AppPengguna.SvcWasdalLainTindakLanjutCud.execute_ptt {
        
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
        AppPengguna.SvcWasdalLainTindakLanjutCud.executeResponse AppPengguna.SvcWasdalLainTindakLanjutCud.execute_ptt.execute(AppPengguna.SvcWasdalLainTindakLanjutCud.executeRequest request) {
            return base.Channel.execute(request);
        }
        
        public AppPengguna.SvcWasdalLainTindakLanjutCud.OutputParameters execute(AppPengguna.SvcWasdalLainTindakLanjutCud.InputParameters InputParameters) {
            AppPengguna.SvcWasdalLainTindakLanjutCud.executeRequest inValue = new AppPengguna.SvcWasdalLainTindakLanjutCud.executeRequest();
            inValue.InputParameters = InputParameters;
            AppPengguna.SvcWasdalLainTindakLanjutCud.executeResponse retVal = ((AppPengguna.SvcWasdalLainTindakLanjutCud.execute_ptt)(this)).execute(inValue);
            return retVal.OutputParameters;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult AppPengguna.SvcWasdalLainTindakLanjutCud.execute_ptt.Beginexecute(AppPengguna.SvcWasdalLainTindakLanjutCud.executeRequest request, System.AsyncCallback callback, object asyncState) {
            return base.Channel.Beginexecute(request, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult Beginexecute(AppPengguna.SvcWasdalLainTindakLanjutCud.InputParameters InputParameters, System.AsyncCallback callback, object asyncState) {
            AppPengguna.SvcWasdalLainTindakLanjutCud.executeRequest inValue = new AppPengguna.SvcWasdalLainTindakLanjutCud.executeRequest();
            inValue.InputParameters = InputParameters;
            return ((AppPengguna.SvcWasdalLainTindakLanjutCud.execute_ptt)(this)).Beginexecute(inValue, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        AppPengguna.SvcWasdalLainTindakLanjutCud.executeResponse AppPengguna.SvcWasdalLainTindakLanjutCud.execute_ptt.Endexecute(System.IAsyncResult result) {
            return base.Channel.Endexecute(result);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public AppPengguna.SvcWasdalLainTindakLanjutCud.OutputParameters Endexecute(System.IAsyncResult result) {
            AppPengguna.SvcWasdalLainTindakLanjutCud.executeResponse retVal = ((AppPengguna.SvcWasdalLainTindakLanjutCud.execute_ptt)(this)).Endexecute(result);
            return retVal.OutputParameters;
        }
        
        private System.IAsyncResult OnBeginexecute(object[] inValues, System.AsyncCallback callback, object asyncState) {
            AppPengguna.SvcWasdalLainTindakLanjutCud.InputParameters InputParameters = ((AppPengguna.SvcWasdalLainTindakLanjutCud.InputParameters)(inValues[0]));
            return this.Beginexecute(InputParameters, callback, asyncState);
        }
        
        private object[] OnEndexecute(System.IAsyncResult result) {
            AppPengguna.SvcWasdalLainTindakLanjutCud.OutputParameters retVal = this.Endexecute(result);
            return new object[] {
                    retVal};
        }
        
        private void OnexecuteCompleted(object state) {
            if ((this.executeCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.executeCompleted(this, new executeCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void executeAsync(AppPengguna.SvcWasdalLainTindakLanjutCud.InputParameters InputParameters) {
            this.executeAsync(InputParameters, null);
        }
        
        public void executeAsync(AppPengguna.SvcWasdalLainTindakLanjutCud.InputParameters InputParameters, object userState) {
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