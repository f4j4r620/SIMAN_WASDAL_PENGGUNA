﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppPengguna.SvcWasdalPenertibanCud {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://oracle.com/sca/soapservice/siman_wasdal/wasdalPenertiban/cud", ConfigurationName="SvcWasdalPenertibanCud.execute_ptt")]
    public interface execute_ptt {
        
        // CODEGEN: Generating message contract since the operation execute is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="execute", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        AppPengguna.SvcWasdalPenertibanCud.executeResponse execute(AppPengguna.SvcWasdalPenertibanCud.executeRequest request);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="execute", ReplyAction="*")]
        System.IAsyncResult Beginexecute(AppPengguna.SvcWasdalPenertibanCud.executeRequest request, System.AsyncCallback callback, object asyncState);
        
        AppPengguna.SvcWasdalPenertibanCud.executeResponse Endexecute(System.IAsyncResult result);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2046.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SP_WASDAL_PENERTIBAN")]
    public partial class InputParameters : object, System.ComponentModel.INotifyPropertyChanged {
        
        private System.Nullable<decimal> p_ID_PENERTIBANField;
        
        private bool p_ID_PENERTIBANFieldSpecified;
        
        private System.Nullable<decimal> p_ID_USERField;
        
        private bool p_ID_USERFieldSpecified;
        
        private string p_NM_PENGGUNAField;
        
        private System.Nullable<decimal> p_ID_SATKERField;
        
        private bool p_ID_SATKERFieldSpecified;
        
        private string p_KD_SATKERField;
        
        private string p_UR_SATKERField;
        
        private System.Nullable<decimal> p_ID_ASETField;
        
        private bool p_ID_ASETFieldSpecified;
        
        private string p_KD_BRGField;
        
        private string p_NUPField;
        
        private string p_NOREGField;
        
        private string p_UR_SSKELField;
        
        private string p_KUASA_PENGGUNA_BRGField;
        
        private string p_KETField;
        
        private string p_THN_ANGField;
        
        private string p_DASAR_PENERTIBANField;
        
        private string p_NO_LAPORANField;
        
        private string p_TGL_LAPORANField;
        
        private string p_BENTUK_PENERTIBANField;
        
        private string p_UR_BENTUK_PENERTIBANField;
        
        private string p_NO_SURAT_PENERTIBANField;
        
        private string p_TGL_SURAT_PENERTIBANField;
        
        private string p_URAIAN_PENERTIBAN_LAINField;
        
        private string p_TINDAK_LANJUT_HSLField;
        
        private string p_SELECTField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=0)]
        public System.Nullable<decimal> P_ID_PENERTIBAN {
            get {
                return this.p_ID_PENERTIBANField;
            }
            set {
                this.p_ID_PENERTIBANField = value;
                this.RaisePropertyChanged("P_ID_PENERTIBAN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool P_ID_PENERTIBANSpecified {
            get {
                return this.p_ID_PENERTIBANFieldSpecified;
            }
            set {
                this.p_ID_PENERTIBANFieldSpecified = value;
                this.RaisePropertyChanged("P_ID_PENERTIBANSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=1)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=2)]
        public string P_NM_PENGGUNA {
            get {
                return this.p_NM_PENGGUNAField;
            }
            set {
                this.p_NM_PENGGUNAField = value;
                this.RaisePropertyChanged("P_NM_PENGGUNA");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=3)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=4)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=5)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=6)]
        public System.Nullable<decimal> P_ID_ASET {
            get {
                return this.p_ID_ASETField;
            }
            set {
                this.p_ID_ASETField = value;
                this.RaisePropertyChanged("P_ID_ASET");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool P_ID_ASETSpecified {
            get {
                return this.p_ID_ASETFieldSpecified;
            }
            set {
                this.p_ID_ASETFieldSpecified = value;
                this.RaisePropertyChanged("P_ID_ASETSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=7)]
        public string P_KD_BRG {
            get {
                return this.p_KD_BRGField;
            }
            set {
                this.p_KD_BRGField = value;
                this.RaisePropertyChanged("P_KD_BRG");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=8)]
        public string P_NUP {
            get {
                return this.p_NUPField;
            }
            set {
                this.p_NUPField = value;
                this.RaisePropertyChanged("P_NUP");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=9)]
        public string P_NOREG {
            get {
                return this.p_NOREGField;
            }
            set {
                this.p_NOREGField = value;
                this.RaisePropertyChanged("P_NOREG");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=10)]
        public string P_UR_SSKEL {
            get {
                return this.p_UR_SSKELField;
            }
            set {
                this.p_UR_SSKELField = value;
                this.RaisePropertyChanged("P_UR_SSKEL");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=11)]
        public string P_KUASA_PENGGUNA_BRG {
            get {
                return this.p_KUASA_PENGGUNA_BRGField;
            }
            set {
                this.p_KUASA_PENGGUNA_BRGField = value;
                this.RaisePropertyChanged("P_KUASA_PENGGUNA_BRG");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=12)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=13)]
        public string P_THN_ANG {
            get {
                return this.p_THN_ANGField;
            }
            set {
                this.p_THN_ANGField = value;
                this.RaisePropertyChanged("P_THN_ANG");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=14)]
        public string P_DASAR_PENERTIBAN {
            get {
                return this.p_DASAR_PENERTIBANField;
            }
            set {
                this.p_DASAR_PENERTIBANField = value;
                this.RaisePropertyChanged("P_DASAR_PENERTIBAN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=15)]
        public string P_NO_LAPORAN {
            get {
                return this.p_NO_LAPORANField;
            }
            set {
                this.p_NO_LAPORANField = value;
                this.RaisePropertyChanged("P_NO_LAPORAN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=16)]
        public string P_TGL_LAPORAN {
            get {
                return this.p_TGL_LAPORANField;
            }
            set {
                this.p_TGL_LAPORANField = value;
                this.RaisePropertyChanged("P_TGL_LAPORAN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=17)]
        public string P_BENTUK_PENERTIBAN {
            get {
                return this.p_BENTUK_PENERTIBANField;
            }
            set {
                this.p_BENTUK_PENERTIBANField = value;
                this.RaisePropertyChanged("P_BENTUK_PENERTIBAN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=18)]
        public string P_UR_BENTUK_PENERTIBAN {
            get {
                return this.p_UR_BENTUK_PENERTIBANField;
            }
            set {
                this.p_UR_BENTUK_PENERTIBANField = value;
                this.RaisePropertyChanged("P_UR_BENTUK_PENERTIBAN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=19)]
        public string P_NO_SURAT_PENERTIBAN {
            get {
                return this.p_NO_SURAT_PENERTIBANField;
            }
            set {
                this.p_NO_SURAT_PENERTIBANField = value;
                this.RaisePropertyChanged("P_NO_SURAT_PENERTIBAN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=20)]
        public string P_TGL_SURAT_PENERTIBAN {
            get {
                return this.p_TGL_SURAT_PENERTIBANField;
            }
            set {
                this.p_TGL_SURAT_PENERTIBANField = value;
                this.RaisePropertyChanged("P_TGL_SURAT_PENERTIBAN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=21)]
        public string P_URAIAN_PENERTIBAN_LAIN {
            get {
                return this.p_URAIAN_PENERTIBAN_LAINField;
            }
            set {
                this.p_URAIAN_PENERTIBAN_LAINField = value;
                this.RaisePropertyChanged("P_URAIAN_PENERTIBAN_LAIN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=22)]
        public string P_TINDAK_LANJUT_HSL {
            get {
                return this.p_TINDAK_LANJUT_HSLField;
            }
            set {
                this.p_TINDAK_LANJUT_HSLField = value;
                this.RaisePropertyChanged("P_TINDAK_LANJUT_HSL");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=23)]
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2046.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SP_WASDAL_PENERTIBAN")]
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
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SP_WASDAL_PENERTIBAN", Order=0)]
        public AppPengguna.SvcWasdalPenertibanCud.InputParameters InputParameters;
        
        public executeRequest() {
        }
        
        public executeRequest(AppPengguna.SvcWasdalPenertibanCud.InputParameters InputParameters) {
            this.InputParameters = InputParameters;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class executeResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SP_WASDAL_PENERTIBAN", Order=0)]
        public AppPengguna.SvcWasdalPenertibanCud.OutputParameters OutputParameters;
        
        public executeResponse() {
        }
        
        public executeResponse(AppPengguna.SvcWasdalPenertibanCud.OutputParameters OutputParameters) {
            this.OutputParameters = OutputParameters;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface execute_pttChannel : AppPengguna.SvcWasdalPenertibanCud.execute_ptt, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class executeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public executeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public AppPengguna.SvcWasdalPenertibanCud.OutputParameters Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((AppPengguna.SvcWasdalPenertibanCud.OutputParameters)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class execute_pttClient : System.ServiceModel.ClientBase<AppPengguna.SvcWasdalPenertibanCud.execute_ptt>, AppPengguna.SvcWasdalPenertibanCud.execute_ptt {
        
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
        AppPengguna.SvcWasdalPenertibanCud.executeResponse AppPengguna.SvcWasdalPenertibanCud.execute_ptt.execute(AppPengguna.SvcWasdalPenertibanCud.executeRequest request) {
            return base.Channel.execute(request);
        }
        
        public AppPengguna.SvcWasdalPenertibanCud.OutputParameters execute(AppPengguna.SvcWasdalPenertibanCud.InputParameters InputParameters) {
            AppPengguna.SvcWasdalPenertibanCud.executeRequest inValue = new AppPengguna.SvcWasdalPenertibanCud.executeRequest();
            inValue.InputParameters = InputParameters;
            AppPengguna.SvcWasdalPenertibanCud.executeResponse retVal = ((AppPengguna.SvcWasdalPenertibanCud.execute_ptt)(this)).execute(inValue);
            return retVal.OutputParameters;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult AppPengguna.SvcWasdalPenertibanCud.execute_ptt.Beginexecute(AppPengguna.SvcWasdalPenertibanCud.executeRequest request, System.AsyncCallback callback, object asyncState) {
            return base.Channel.Beginexecute(request, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult Beginexecute(AppPengguna.SvcWasdalPenertibanCud.InputParameters InputParameters, System.AsyncCallback callback, object asyncState) {
            AppPengguna.SvcWasdalPenertibanCud.executeRequest inValue = new AppPengguna.SvcWasdalPenertibanCud.executeRequest();
            inValue.InputParameters = InputParameters;
            return ((AppPengguna.SvcWasdalPenertibanCud.execute_ptt)(this)).Beginexecute(inValue, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        AppPengguna.SvcWasdalPenertibanCud.executeResponse AppPengguna.SvcWasdalPenertibanCud.execute_ptt.Endexecute(System.IAsyncResult result) {
            return base.Channel.Endexecute(result);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public AppPengguna.SvcWasdalPenertibanCud.OutputParameters Endexecute(System.IAsyncResult result) {
            AppPengguna.SvcWasdalPenertibanCud.executeResponse retVal = ((AppPengguna.SvcWasdalPenertibanCud.execute_ptt)(this)).Endexecute(result);
            return retVal.OutputParameters;
        }
        
        private System.IAsyncResult OnBeginexecute(object[] inValues, System.AsyncCallback callback, object asyncState) {
            AppPengguna.SvcWasdalPenertibanCud.InputParameters InputParameters = ((AppPengguna.SvcWasdalPenertibanCud.InputParameters)(inValues[0]));
            return this.Beginexecute(InputParameters, callback, asyncState);
        }
        
        private object[] OnEndexecute(System.IAsyncResult result) {
            AppPengguna.SvcWasdalPenertibanCud.OutputParameters retVal = this.Endexecute(result);
            return new object[] {
                    retVal};
        }
        
        private void OnexecuteCompleted(object state) {
            if ((this.executeCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.executeCompleted(this, new executeCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void executeAsync(AppPengguna.SvcWasdalPenertibanCud.InputParameters InputParameters) {
            this.executeAsync(InputParameters, null);
        }
        
        public void executeAsync(AppPengguna.SvcWasdalPenertibanCud.InputParameters InputParameters, object userState) {
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