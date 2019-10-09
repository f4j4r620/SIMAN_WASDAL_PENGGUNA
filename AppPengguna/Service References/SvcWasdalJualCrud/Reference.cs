﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppPengguna.SvcWasdalJualCrud {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://oracle.com/sca/soapservice/siman_wasdal/wasdalPTJual/cud", ConfigurationName="SvcWasdalJualCrud.execute_ptt")]
    public interface execute_ptt {
        
        // CODEGEN: Generating message contract since the operation execute is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="execute", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        AppPengguna.SvcWasdalJualCrud.executeResponse execute(AppPengguna.SvcWasdalJualCrud.executeRequest request);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="execute", ReplyAction="*")]
        System.IAsyncResult Beginexecute(AppPengguna.SvcWasdalJualCrud.executeRequest request, System.AsyncCallback callback, object asyncState);
        
        AppPengguna.SvcWasdalJualCrud.executeResponse Endexecute(System.IAsyncResult result);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SP_SK_WASDAL_PT_JUAL")]
    public partial class InputParameters : object, System.ComponentModel.INotifyPropertyChanged {
        
        private System.Nullable<decimal> p_ID_SK_WASDAL_PINDAHTANGANField;
        
        private bool p_ID_SK_WASDAL_PINDAHTANGANFieldSpecified;
        
        private string p_SK_KEPUTUSANField;
        
        private string p_TGL_SKField;
        
        private string p_URAIAN_KEPUTUSANField;
        
        private string p_IS_TBField;
        
        private string p_KD_PELAYANANField;
        
        private System.Nullable<decimal> p_ID_KPKNLField;
        
        private bool p_ID_KPKNLFieldSpecified;
        
        private System.Nullable<decimal> p_ID_USERField;
        
        private bool p_ID_USERFieldSpecified;
        
        private System.Nullable<decimal> p_ID_SATKERField;
        
        private bool p_ID_SATKERFieldSpecified;
        
        private string p_KD_SATKERField;
        
        private string p_UR_SATKERField;
        
        private string p_KD_PENERBIT_SKField;
        
        private string p_NM_PENERBIT_SKField;
        
        private string p_TGL_CREATEDField;
        
        private string p_NIP_PENANDATANGANField;
        
        private string p_NM_PENANDATANGANField;
        
        private string p_JABATAN_TTDField;
        
        private string p_KD_PENERBIT_SK_DTLField;
        
        private string p_NM_PENERBIT_SK_DTLField;
        
        private System.Nullable<decimal> p_ID_PEMOHONField;
        
        private bool p_ID_PEMOHONFieldSpecified;
        
        private string p_TIPE_PEMOHONField;
        
        private string p_TIPE_PENGELOLAField;
        
        private string p_THN_ANGField;
        
        private string p_KD_KLField;
        
        private System.Nullable<decimal> p_NILAI_PEROLEHANField;
        
        private bool p_NILAI_PEROLEHANFieldSpecified;
        
        private System.Nullable<decimal> p_NILAI_LIMITField;
        
        private bool p_NILAI_LIMITFieldSpecified;
        
        private string p_SELECTField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=0)]
        public System.Nullable<decimal> P_ID_SK_WASDAL_PINDAHTANGAN {
            get {
                return this.p_ID_SK_WASDAL_PINDAHTANGANField;
            }
            set {
                this.p_ID_SK_WASDAL_PINDAHTANGANField = value;
                this.RaisePropertyChanged("P_ID_SK_WASDAL_PINDAHTANGAN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool P_ID_SK_WASDAL_PINDAHTANGANSpecified {
            get {
                return this.p_ID_SK_WASDAL_PINDAHTANGANFieldSpecified;
            }
            set {
                this.p_ID_SK_WASDAL_PINDAHTANGANFieldSpecified = value;
                this.RaisePropertyChanged("P_ID_SK_WASDAL_PINDAHTANGANSpecified");
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
        public string P_URAIAN_KEPUTUSAN {
            get {
                return this.p_URAIAN_KEPUTUSANField;
            }
            set {
                this.p_URAIAN_KEPUTUSANField = value;
                this.RaisePropertyChanged("P_URAIAN_KEPUTUSAN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=4)]
        public string P_IS_TB {
            get {
                return this.p_IS_TBField;
            }
            set {
                this.p_IS_TBField = value;
                this.RaisePropertyChanged("P_IS_TB");
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
        public System.Nullable<decimal> P_ID_KPKNL {
            get {
                return this.p_ID_KPKNLField;
            }
            set {
                this.p_ID_KPKNLField = value;
                this.RaisePropertyChanged("P_ID_KPKNL");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool P_ID_KPKNLSpecified {
            get {
                return this.p_ID_KPKNLFieldSpecified;
            }
            set {
                this.p_ID_KPKNLFieldSpecified = value;
                this.RaisePropertyChanged("P_ID_KPKNLSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=7)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=8)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=9)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=10)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=11)]
        public string P_KD_PENERBIT_SK {
            get {
                return this.p_KD_PENERBIT_SKField;
            }
            set {
                this.p_KD_PENERBIT_SKField = value;
                this.RaisePropertyChanged("P_KD_PENERBIT_SK");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=12)]
        public string P_NM_PENERBIT_SK {
            get {
                return this.p_NM_PENERBIT_SKField;
            }
            set {
                this.p_NM_PENERBIT_SKField = value;
                this.RaisePropertyChanged("P_NM_PENERBIT_SK");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=13)]
        public string P_TGL_CREATED {
            get {
                return this.p_TGL_CREATEDField;
            }
            set {
                this.p_TGL_CREATEDField = value;
                this.RaisePropertyChanged("P_TGL_CREATED");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=14)]
        public string P_NIP_PENANDATANGAN {
            get {
                return this.p_NIP_PENANDATANGANField;
            }
            set {
                this.p_NIP_PENANDATANGANField = value;
                this.RaisePropertyChanged("P_NIP_PENANDATANGAN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=15)]
        public string P_NM_PENANDATANGAN {
            get {
                return this.p_NM_PENANDATANGANField;
            }
            set {
                this.p_NM_PENANDATANGANField = value;
                this.RaisePropertyChanged("P_NM_PENANDATANGAN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=16)]
        public string P_JABATAN_TTD {
            get {
                return this.p_JABATAN_TTDField;
            }
            set {
                this.p_JABATAN_TTDField = value;
                this.RaisePropertyChanged("P_JABATAN_TTD");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=17)]
        public string P_KD_PENERBIT_SK_DTL {
            get {
                return this.p_KD_PENERBIT_SK_DTLField;
            }
            set {
                this.p_KD_PENERBIT_SK_DTLField = value;
                this.RaisePropertyChanged("P_KD_PENERBIT_SK_DTL");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=18)]
        public string P_NM_PENERBIT_SK_DTL {
            get {
                return this.p_NM_PENERBIT_SK_DTLField;
            }
            set {
                this.p_NM_PENERBIT_SK_DTLField = value;
                this.RaisePropertyChanged("P_NM_PENERBIT_SK_DTL");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=19)]
        public System.Nullable<decimal> P_ID_PEMOHON {
            get {
                return this.p_ID_PEMOHONField;
            }
            set {
                this.p_ID_PEMOHONField = value;
                this.RaisePropertyChanged("P_ID_PEMOHON");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool P_ID_PEMOHONSpecified {
            get {
                return this.p_ID_PEMOHONFieldSpecified;
            }
            set {
                this.p_ID_PEMOHONFieldSpecified = value;
                this.RaisePropertyChanged("P_ID_PEMOHONSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=20)]
        public string P_TIPE_PEMOHON {
            get {
                return this.p_TIPE_PEMOHONField;
            }
            set {
                this.p_TIPE_PEMOHONField = value;
                this.RaisePropertyChanged("P_TIPE_PEMOHON");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=21)]
        public string P_TIPE_PENGELOLA {
            get {
                return this.p_TIPE_PENGELOLAField;
            }
            set {
                this.p_TIPE_PENGELOLAField = value;
                this.RaisePropertyChanged("P_TIPE_PENGELOLA");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=22)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=23)]
        public string P_KD_KL {
            get {
                return this.p_KD_KLField;
            }
            set {
                this.p_KD_KLField = value;
                this.RaisePropertyChanged("P_KD_KL");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=24)]
        public System.Nullable<decimal> P_NILAI_PEROLEHAN {
            get {
                return this.p_NILAI_PEROLEHANField;
            }
            set {
                this.p_NILAI_PEROLEHANField = value;
                this.RaisePropertyChanged("P_NILAI_PEROLEHAN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool P_NILAI_PEROLEHANSpecified {
            get {
                return this.p_NILAI_PEROLEHANFieldSpecified;
            }
            set {
                this.p_NILAI_PEROLEHANFieldSpecified = value;
                this.RaisePropertyChanged("P_NILAI_PEROLEHANSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=25)]
        public System.Nullable<decimal> P_NILAI_LIMIT {
            get {
                return this.p_NILAI_LIMITField;
            }
            set {
                this.p_NILAI_LIMITField = value;
                this.RaisePropertyChanged("P_NILAI_LIMIT");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool P_NILAI_LIMITSpecified {
            get {
                return this.p_NILAI_LIMITFieldSpecified;
            }
            set {
                this.p_NILAI_LIMITFieldSpecified = value;
                this.RaisePropertyChanged("P_NILAI_LIMITSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=26)]
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SP_SK_WASDAL_PT_JUAL")]
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
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SP_SK_WASDAL_PT_JUAL", Order=0)]
        public AppPengguna.SvcWasdalJualCrud.InputParameters InputParameters;
        
        public executeRequest() {
        }
        
        public executeRequest(AppPengguna.SvcWasdalJualCrud.InputParameters InputParameters) {
            this.InputParameters = InputParameters;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class executeResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SP_SK_WASDAL_PT_JUAL", Order=0)]
        public AppPengguna.SvcWasdalJualCrud.OutputParameters OutputParameters;
        
        public executeResponse() {
        }
        
        public executeResponse(AppPengguna.SvcWasdalJualCrud.OutputParameters OutputParameters) {
            this.OutputParameters = OutputParameters;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface execute_pttChannel : AppPengguna.SvcWasdalJualCrud.execute_ptt, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class executeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public executeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public AppPengguna.SvcWasdalJualCrud.OutputParameters Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((AppPengguna.SvcWasdalJualCrud.OutputParameters)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class execute_pttClient : System.ServiceModel.ClientBase<AppPengguna.SvcWasdalJualCrud.execute_ptt>, AppPengguna.SvcWasdalJualCrud.execute_ptt {
        
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
        AppPengguna.SvcWasdalJualCrud.executeResponse AppPengguna.SvcWasdalJualCrud.execute_ptt.execute(AppPengguna.SvcWasdalJualCrud.executeRequest request) {
            return base.Channel.execute(request);
        }
        
        public AppPengguna.SvcWasdalJualCrud.OutputParameters execute(AppPengguna.SvcWasdalJualCrud.InputParameters InputParameters) {
            AppPengguna.SvcWasdalJualCrud.executeRequest inValue = new AppPengguna.SvcWasdalJualCrud.executeRequest();
            inValue.InputParameters = InputParameters;
            AppPengguna.SvcWasdalJualCrud.executeResponse retVal = ((AppPengguna.SvcWasdalJualCrud.execute_ptt)(this)).execute(inValue);
            return retVal.OutputParameters;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult AppPengguna.SvcWasdalJualCrud.execute_ptt.Beginexecute(AppPengguna.SvcWasdalJualCrud.executeRequest request, System.AsyncCallback callback, object asyncState) {
            return base.Channel.Beginexecute(request, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult Beginexecute(AppPengguna.SvcWasdalJualCrud.InputParameters InputParameters, System.AsyncCallback callback, object asyncState) {
            AppPengguna.SvcWasdalJualCrud.executeRequest inValue = new AppPengguna.SvcWasdalJualCrud.executeRequest();
            inValue.InputParameters = InputParameters;
            return ((AppPengguna.SvcWasdalJualCrud.execute_ptt)(this)).Beginexecute(inValue, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        AppPengguna.SvcWasdalJualCrud.executeResponse AppPengguna.SvcWasdalJualCrud.execute_ptt.Endexecute(System.IAsyncResult result) {
            return base.Channel.Endexecute(result);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public AppPengguna.SvcWasdalJualCrud.OutputParameters Endexecute(System.IAsyncResult result) {
            AppPengguna.SvcWasdalJualCrud.executeResponse retVal = ((AppPengguna.SvcWasdalJualCrud.execute_ptt)(this)).Endexecute(result);
            return retVal.OutputParameters;
        }
        
        private System.IAsyncResult OnBeginexecute(object[] inValues, System.AsyncCallback callback, object asyncState) {
            AppPengguna.SvcWasdalJualCrud.InputParameters InputParameters = ((AppPengguna.SvcWasdalJualCrud.InputParameters)(inValues[0]));
            return this.Beginexecute(InputParameters, callback, asyncState);
        }
        
        private object[] OnEndexecute(System.IAsyncResult result) {
            AppPengguna.SvcWasdalJualCrud.OutputParameters retVal = this.Endexecute(result);
            return new object[] {
                    retVal};
        }
        
        private void OnexecuteCompleted(object state) {
            if ((this.executeCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.executeCompleted(this, new executeCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void executeAsync(AppPengguna.SvcWasdalJualCrud.InputParameters InputParameters) {
            this.executeAsync(InputParameters, null);
        }
        
        public void executeAsync(AppPengguna.SvcWasdalJualCrud.InputParameters InputParameters, object userState) {
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
