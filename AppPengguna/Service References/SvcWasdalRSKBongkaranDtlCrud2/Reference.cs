﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppPengguna.SvcWasdalRSKBongkaranDtlCrud2 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://oracle.com/sca/soapservice/siman_wasdal/wasRekamSkBnk2/dtlCud", ConfigurationName="SvcWasdalRSKBongkaranDtlCrud2.execute_ptt")]
    public interface execute_ptt {
        
        // CODEGEN: Generating message contract since the operation execute is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="execute", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        AppPengguna.SvcWasdalRSKBongkaranDtlCrud2.executeResponse execute(AppPengguna.SvcWasdalRSKBongkaranDtlCrud2.executeRequest request);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="execute", ReplyAction="*")]
        System.IAsyncResult Beginexecute(AppPengguna.SvcWasdalRSKBongkaranDtlCrud2.executeRequest request, System.AsyncCallback callback, object asyncState);
        
        AppPengguna.SvcWasdalRSKBongkaranDtlCrud2.executeResponse Endexecute(System.IAsyncResult result);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2102.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SP_SK_BONGKARAN_BMN")]
    public partial class InputParameters : object, System.ComponentModel.INotifyPropertyChanged {
        
        private System.Nullable<decimal> p_ID_SK_BNKField;
        
        private bool p_ID_SK_BNKFieldSpecified;
        
        private string p_SK_KEPUTUSAN_BNKField;
        
        private System.Nullable<System.DateTime> p_TGL_SK_BNKField;
        
        private bool p_TGL_SK_BNKFieldSpecified;
        
        private string p_ID_ASETField;
        
        private System.Nullable<decimal> p_KUANTITASField;
        
        private bool p_KUANTITASFieldSpecified;
        
        private System.Nullable<decimal> p_NILAI_PERSETUJUANField;
        
        private bool p_NILAI_PERSETUJUANFieldSpecified;
        
        private string p_KD_STATUSField;
        
        private string p_KETField;
        
        private System.Nullable<decimal> p_LUAS_LAYANANField;
        
        private bool p_LUAS_LAYANANFieldSpecified;
        
        private string p_NM_PHK_LAINField;
        
        private string p_NPWP_PHK_LAINField;
        
        private System.Nullable<decimal> p_JANGKA_WAKTUField;
        
        private bool p_JANGKA_WAKTUFieldSpecified;
        
        private string p_PERIODEField;
        
        private System.Nullable<System.DateTime> p_DARI_TGLField;
        
        private bool p_DARI_TGLFieldSpecified;
        
        private System.Nullable<System.DateTime> p_SD_TGLField;
        
        private bool p_SD_TGLFieldSpecified;
        
        private string p_NTPNField;
        
        private System.Nullable<System.DateTime> p_TGL_SETORField;
        
        private bool p_TGL_SETORFieldSpecified;
        
        private string p_NTBField;
        
        private System.Nullable<System.DateTime> p_TGL_TRANSAKSIField;
        
        private bool p_TGL_TRANSAKSIFieldSpecified;
        
        private string p_NM_PENYETORField;
        
        private string p_KD_AKUNField;
        
        private string p_KD_PELAYANANField;
        
        private System.Nullable<decimal> p_NILAI_PNBPField;
        
        private bool p_NILAI_PNBPFieldSpecified;
        
        private string p_IS_VALIDField;
        
        private string p_SELECTField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=0)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=1)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=2)]
        public System.Nullable<System.DateTime> P_TGL_SK_BNK {
            get {
                return this.p_TGL_SK_BNKField;
            }
            set {
                this.p_TGL_SK_BNKField = value;
                this.RaisePropertyChanged("P_TGL_SK_BNK");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool P_TGL_SK_BNKSpecified {
            get {
                return this.p_TGL_SK_BNKFieldSpecified;
            }
            set {
                this.p_TGL_SK_BNKFieldSpecified = value;
                this.RaisePropertyChanged("P_TGL_SK_BNKSpecified");
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
        public System.Nullable<decimal> P_LUAS_LAYANAN {
            get {
                return this.p_LUAS_LAYANANField;
            }
            set {
                this.p_LUAS_LAYANANField = value;
                this.RaisePropertyChanged("P_LUAS_LAYANAN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool P_LUAS_LAYANANSpecified {
            get {
                return this.p_LUAS_LAYANANFieldSpecified;
            }
            set {
                this.p_LUAS_LAYANANFieldSpecified = value;
                this.RaisePropertyChanged("P_LUAS_LAYANANSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=9)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=10)]
        public string P_NPWP_PHK_LAIN {
            get {
                return this.p_NPWP_PHK_LAINField;
            }
            set {
                this.p_NPWP_PHK_LAINField = value;
                this.RaisePropertyChanged("P_NPWP_PHK_LAIN");
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=13)]
        public System.Nullable<System.DateTime> P_DARI_TGL {
            get {
                return this.p_DARI_TGLField;
            }
            set {
                this.p_DARI_TGLField = value;
                this.RaisePropertyChanged("P_DARI_TGL");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool P_DARI_TGLSpecified {
            get {
                return this.p_DARI_TGLFieldSpecified;
            }
            set {
                this.p_DARI_TGLFieldSpecified = value;
                this.RaisePropertyChanged("P_DARI_TGLSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=14)]
        public System.Nullable<System.DateTime> P_SD_TGL {
            get {
                return this.p_SD_TGLField;
            }
            set {
                this.p_SD_TGLField = value;
                this.RaisePropertyChanged("P_SD_TGL");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool P_SD_TGLSpecified {
            get {
                return this.p_SD_TGLFieldSpecified;
            }
            set {
                this.p_SD_TGLFieldSpecified = value;
                this.RaisePropertyChanged("P_SD_TGLSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=15)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=16)]
        public System.Nullable<System.DateTime> P_TGL_SETOR {
            get {
                return this.p_TGL_SETORField;
            }
            set {
                this.p_TGL_SETORField = value;
                this.RaisePropertyChanged("P_TGL_SETOR");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool P_TGL_SETORSpecified {
            get {
                return this.p_TGL_SETORFieldSpecified;
            }
            set {
                this.p_TGL_SETORFieldSpecified = value;
                this.RaisePropertyChanged("P_TGL_SETORSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=17)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=18)]
        public System.Nullable<System.DateTime> P_TGL_TRANSAKSI {
            get {
                return this.p_TGL_TRANSAKSIField;
            }
            set {
                this.p_TGL_TRANSAKSIField = value;
                this.RaisePropertyChanged("P_TGL_TRANSAKSI");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool P_TGL_TRANSAKSISpecified {
            get {
                return this.p_TGL_TRANSAKSIFieldSpecified;
            }
            set {
                this.p_TGL_TRANSAKSIFieldSpecified = value;
                this.RaisePropertyChanged("P_TGL_TRANSAKSISpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=19)]
        public string P_NM_PENYETOR {
            get {
                return this.p_NM_PENYETORField;
            }
            set {
                this.p_NM_PENYETORField = value;
                this.RaisePropertyChanged("P_NM_PENYETOR");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=20)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=21)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=22)]
        public System.Nullable<decimal> P_NILAI_PNBP {
            get {
                return this.p_NILAI_PNBPField;
            }
            set {
                this.p_NILAI_PNBPField = value;
                this.RaisePropertyChanged("P_NILAI_PNBP");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool P_NILAI_PNBPSpecified {
            get {
                return this.p_NILAI_PNBPFieldSpecified;
            }
            set {
                this.p_NILAI_PNBPFieldSpecified = value;
                this.RaisePropertyChanged("P_NILAI_PNBPSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=23)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=24)]
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SP_SK_BONGKARAN_BMN")]
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
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SP_SK_BONGKARAN_BMN", Order=0)]
        public AppPengguna.SvcWasdalRSKBongkaranDtlCrud2.InputParameters InputParameters;
        
        public executeRequest() {
        }
        
        public executeRequest(AppPengguna.SvcWasdalRSKBongkaranDtlCrud2.InputParameters InputParameters) {
            this.InputParameters = InputParameters;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class executeResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SP_SK_BONGKARAN_BMN", Order=0)]
        public AppPengguna.SvcWasdalRSKBongkaranDtlCrud2.OutputParameters OutputParameters;
        
        public executeResponse() {
        }
        
        public executeResponse(AppPengguna.SvcWasdalRSKBongkaranDtlCrud2.OutputParameters OutputParameters) {
            this.OutputParameters = OutputParameters;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface execute_pttChannel : AppPengguna.SvcWasdalRSKBongkaranDtlCrud2.execute_ptt, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class executeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public executeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public AppPengguna.SvcWasdalRSKBongkaranDtlCrud2.OutputParameters Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((AppPengguna.SvcWasdalRSKBongkaranDtlCrud2.OutputParameters)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class execute_pttClient : System.ServiceModel.ClientBase<AppPengguna.SvcWasdalRSKBongkaranDtlCrud2.execute_ptt>, AppPengguna.SvcWasdalRSKBongkaranDtlCrud2.execute_ptt {
        
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
        AppPengguna.SvcWasdalRSKBongkaranDtlCrud2.executeResponse AppPengguna.SvcWasdalRSKBongkaranDtlCrud2.execute_ptt.execute(AppPengguna.SvcWasdalRSKBongkaranDtlCrud2.executeRequest request) {
            return base.Channel.execute(request);
        }
        
        public AppPengguna.SvcWasdalRSKBongkaranDtlCrud2.OutputParameters execute(AppPengguna.SvcWasdalRSKBongkaranDtlCrud2.InputParameters InputParameters) {
            AppPengguna.SvcWasdalRSKBongkaranDtlCrud2.executeRequest inValue = new AppPengguna.SvcWasdalRSKBongkaranDtlCrud2.executeRequest();
            inValue.InputParameters = InputParameters;
            AppPengguna.SvcWasdalRSKBongkaranDtlCrud2.executeResponse retVal = ((AppPengguna.SvcWasdalRSKBongkaranDtlCrud2.execute_ptt)(this)).execute(inValue);
            return retVal.OutputParameters;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult AppPengguna.SvcWasdalRSKBongkaranDtlCrud2.execute_ptt.Beginexecute(AppPengguna.SvcWasdalRSKBongkaranDtlCrud2.executeRequest request, System.AsyncCallback callback, object asyncState) {
            return base.Channel.Beginexecute(request, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult Beginexecute(AppPengguna.SvcWasdalRSKBongkaranDtlCrud2.InputParameters InputParameters, System.AsyncCallback callback, object asyncState) {
            AppPengguna.SvcWasdalRSKBongkaranDtlCrud2.executeRequest inValue = new AppPengguna.SvcWasdalRSKBongkaranDtlCrud2.executeRequest();
            inValue.InputParameters = InputParameters;
            return ((AppPengguna.SvcWasdalRSKBongkaranDtlCrud2.execute_ptt)(this)).Beginexecute(inValue, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        AppPengguna.SvcWasdalRSKBongkaranDtlCrud2.executeResponse AppPengguna.SvcWasdalRSKBongkaranDtlCrud2.execute_ptt.Endexecute(System.IAsyncResult result) {
            return base.Channel.Endexecute(result);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public AppPengguna.SvcWasdalRSKBongkaranDtlCrud2.OutputParameters Endexecute(System.IAsyncResult result) {
            AppPengguna.SvcWasdalRSKBongkaranDtlCrud2.executeResponse retVal = ((AppPengguna.SvcWasdalRSKBongkaranDtlCrud2.execute_ptt)(this)).Endexecute(result);
            return retVal.OutputParameters;
        }
        
        private System.IAsyncResult OnBeginexecute(object[] inValues, System.AsyncCallback callback, object asyncState) {
            AppPengguna.SvcWasdalRSKBongkaranDtlCrud2.InputParameters InputParameters = ((AppPengguna.SvcWasdalRSKBongkaranDtlCrud2.InputParameters)(inValues[0]));
            return this.Beginexecute(InputParameters, callback, asyncState);
        }
        
        private object[] OnEndexecute(System.IAsyncResult result) {
            AppPengguna.SvcWasdalRSKBongkaranDtlCrud2.OutputParameters retVal = this.Endexecute(result);
            return new object[] {
                    retVal};
        }
        
        private void OnexecuteCompleted(object state) {
            if ((this.executeCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.executeCompleted(this, new executeCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void executeAsync(AppPengguna.SvcWasdalRSKBongkaranDtlCrud2.InputParameters InputParameters) {
            this.executeAsync(InputParameters, null);
        }
        
        public void executeAsync(AppPengguna.SvcWasdalRSKBongkaranDtlCrud2.InputParameters InputParameters, object userState) {
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
