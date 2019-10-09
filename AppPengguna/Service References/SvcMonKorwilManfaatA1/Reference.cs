﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppPengguna.SvcMonKorwilManfaatA1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://oracle.com/sca/soapservice/siman_wasdal/wasdalMonKorwil/selectManfaatA1", ConfigurationName="SvcMonKorwilManfaatA1.execute_ptt")]
    public interface execute_ptt {
        
        // CODEGEN: Generating message contract since the operation execute is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="execute", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        AppPengguna.SvcMonKorwilManfaatA1.executeResponse execute(AppPengguna.SvcMonKorwilManfaatA1.executeRequest request);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="execute", ReplyAction="*")]
        System.IAsyncResult Beginexecute(AppPengguna.SvcMonKorwilManfaatA1.executeRequest request, System.AsyncCallback callback, object asyncState);
        
        AppPengguna.SvcMonKorwilManfaatA1.executeResponse Endexecute(System.IAsyncResult result);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_MON_MANFAAT_KORWIL_A1")]
    public partial class InputParameters : object, System.ComponentModel.INotifyPropertyChanged {
        
        private System.Nullable<decimal> p_MINField;
        
        private bool p_MINFieldSpecified;
        
        private System.Nullable<decimal> p_MAXField;
        
        private bool p_MAXFieldSpecified;
        
        private string sTR_WHEREField;
        
        private string p_COLField;
        
        private string p_SORTField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=0)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=1)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=2)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=3)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=4)]
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="WASDAL.SROW_MON_MANFAAT_KORWIL_A1", Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_MON_MANFAAT_KORWIL_A1")]
    public partial class WASDALSROW_MON_MANFAAT_KORWIL_A1 : object, System.ComponentModel.INotifyPropertyChanged {
        
        private System.Nullable<decimal> nUMField;
        
        private bool nUMFieldSpecified;
        
        private System.Nullable<decimal> iD_KORWILField;
        
        private bool iD_KORWILFieldSpecified;
        
        private System.Nullable<decimal> iD_SATKERField;
        
        private bool iD_SATKERFieldSpecified;
        
        private string kD_SATKERField;
        
        private string uR_SATKERField;
        
        private System.Nullable<decimal> nIL_PERSETUJUAN_SEWAField;
        
        private bool nIL_PERSETUJUAN_SEWAFieldSpecified;
        
        private System.Nullable<decimal> nIL_PELAKSANAAN_SEWAField;
        
        private bool nIL_PELAKSANAAN_SEWAFieldSpecified;
        
        private System.Nullable<decimal> jML_SRT_PERSETUJUAN_PINJAMField;
        
        private bool jML_SRT_PERSETUJUAN_PINJAMFieldSpecified;
        
        private System.Nullable<decimal> jML_SRT_PELAKSANAAN_PINJAMField;
        
        private bool jML_SRT_PELAKSANAAN_PINJAMFieldSpecified;
        
        private System.Nullable<decimal> nIL_PERSETUJUAN_KERJASAMAField;
        
        private bool nIL_PERSETUJUAN_KERJASAMAFieldSpecified;
        
        private System.Nullable<decimal> nIL_PELAKSANAAN_KERJASAMAField;
        
        private bool nIL_PELAKSANAAN_KERJASAMAFieldSpecified;
        
        private System.Nullable<decimal> nIL_PERSETUJUAN_BGSBSGField;
        
        private bool nIL_PERSETUJUAN_BGSBSGFieldSpecified;
        
        private System.Nullable<decimal> nIL_PELAKSANAAN_BGSBSGField;
        
        private bool nIL_PELAKSANAAN_BGSBSGFieldSpecified;
        
        private System.Nullable<decimal> nIL_PERSETUJUAN_KSPIField;
        
        private bool nIL_PERSETUJUAN_KSPIFieldSpecified;
        
        private System.Nullable<decimal> nIL_PELAKSANAAN_KSPIField;
        
        private bool nIL_PELAKSANAAN_KSPIFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=0)]
        public System.Nullable<decimal> NUM {
            get {
                return this.nUMField;
            }
            set {
                this.nUMField = value;
                this.RaisePropertyChanged("NUM");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NUMSpecified {
            get {
                return this.nUMFieldSpecified;
            }
            set {
                this.nUMFieldSpecified = value;
                this.RaisePropertyChanged("NUMSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=1)]
        public System.Nullable<decimal> ID_KORWIL {
            get {
                return this.iD_KORWILField;
            }
            set {
                this.iD_KORWILField = value;
                this.RaisePropertyChanged("ID_KORWIL");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ID_KORWILSpecified {
            get {
                return this.iD_KORWILFieldSpecified;
            }
            set {
                this.iD_KORWILFieldSpecified = value;
                this.RaisePropertyChanged("ID_KORWILSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=2)]
        public System.Nullable<decimal> ID_SATKER {
            get {
                return this.iD_SATKERField;
            }
            set {
                this.iD_SATKERField = value;
                this.RaisePropertyChanged("ID_SATKER");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ID_SATKERSpecified {
            get {
                return this.iD_SATKERFieldSpecified;
            }
            set {
                this.iD_SATKERFieldSpecified = value;
                this.RaisePropertyChanged("ID_SATKERSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=3)]
        public string KD_SATKER {
            get {
                return this.kD_SATKERField;
            }
            set {
                this.kD_SATKERField = value;
                this.RaisePropertyChanged("KD_SATKER");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=4)]
        public string UR_SATKER {
            get {
                return this.uR_SATKERField;
            }
            set {
                this.uR_SATKERField = value;
                this.RaisePropertyChanged("UR_SATKER");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=5)]
        public System.Nullable<decimal> NIL_PERSETUJUAN_SEWA {
            get {
                return this.nIL_PERSETUJUAN_SEWAField;
            }
            set {
                this.nIL_PERSETUJUAN_SEWAField = value;
                this.RaisePropertyChanged("NIL_PERSETUJUAN_SEWA");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NIL_PERSETUJUAN_SEWASpecified {
            get {
                return this.nIL_PERSETUJUAN_SEWAFieldSpecified;
            }
            set {
                this.nIL_PERSETUJUAN_SEWAFieldSpecified = value;
                this.RaisePropertyChanged("NIL_PERSETUJUAN_SEWASpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=6)]
        public System.Nullable<decimal> NIL_PELAKSANAAN_SEWA {
            get {
                return this.nIL_PELAKSANAAN_SEWAField;
            }
            set {
                this.nIL_PELAKSANAAN_SEWAField = value;
                this.RaisePropertyChanged("NIL_PELAKSANAAN_SEWA");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NIL_PELAKSANAAN_SEWASpecified {
            get {
                return this.nIL_PELAKSANAAN_SEWAFieldSpecified;
            }
            set {
                this.nIL_PELAKSANAAN_SEWAFieldSpecified = value;
                this.RaisePropertyChanged("NIL_PELAKSANAAN_SEWASpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=7)]
        public System.Nullable<decimal> JML_SRT_PERSETUJUAN_PINJAM {
            get {
                return this.jML_SRT_PERSETUJUAN_PINJAMField;
            }
            set {
                this.jML_SRT_PERSETUJUAN_PINJAMField = value;
                this.RaisePropertyChanged("JML_SRT_PERSETUJUAN_PINJAM");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool JML_SRT_PERSETUJUAN_PINJAMSpecified {
            get {
                return this.jML_SRT_PERSETUJUAN_PINJAMFieldSpecified;
            }
            set {
                this.jML_SRT_PERSETUJUAN_PINJAMFieldSpecified = value;
                this.RaisePropertyChanged("JML_SRT_PERSETUJUAN_PINJAMSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=8)]
        public System.Nullable<decimal> JML_SRT_PELAKSANAAN_PINJAM {
            get {
                return this.jML_SRT_PELAKSANAAN_PINJAMField;
            }
            set {
                this.jML_SRT_PELAKSANAAN_PINJAMField = value;
                this.RaisePropertyChanged("JML_SRT_PELAKSANAAN_PINJAM");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool JML_SRT_PELAKSANAAN_PINJAMSpecified {
            get {
                return this.jML_SRT_PELAKSANAAN_PINJAMFieldSpecified;
            }
            set {
                this.jML_SRT_PELAKSANAAN_PINJAMFieldSpecified = value;
                this.RaisePropertyChanged("JML_SRT_PELAKSANAAN_PINJAMSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=9)]
        public System.Nullable<decimal> NIL_PERSETUJUAN_KERJASAMA {
            get {
                return this.nIL_PERSETUJUAN_KERJASAMAField;
            }
            set {
                this.nIL_PERSETUJUAN_KERJASAMAField = value;
                this.RaisePropertyChanged("NIL_PERSETUJUAN_KERJASAMA");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NIL_PERSETUJUAN_KERJASAMASpecified {
            get {
                return this.nIL_PERSETUJUAN_KERJASAMAFieldSpecified;
            }
            set {
                this.nIL_PERSETUJUAN_KERJASAMAFieldSpecified = value;
                this.RaisePropertyChanged("NIL_PERSETUJUAN_KERJASAMASpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=10)]
        public System.Nullable<decimal> NIL_PELAKSANAAN_KERJASAMA {
            get {
                return this.nIL_PELAKSANAAN_KERJASAMAField;
            }
            set {
                this.nIL_PELAKSANAAN_KERJASAMAField = value;
                this.RaisePropertyChanged("NIL_PELAKSANAAN_KERJASAMA");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NIL_PELAKSANAAN_KERJASAMASpecified {
            get {
                return this.nIL_PELAKSANAAN_KERJASAMAFieldSpecified;
            }
            set {
                this.nIL_PELAKSANAAN_KERJASAMAFieldSpecified = value;
                this.RaisePropertyChanged("NIL_PELAKSANAAN_KERJASAMASpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=11)]
        public System.Nullable<decimal> NIL_PERSETUJUAN_BGSBSG {
            get {
                return this.nIL_PERSETUJUAN_BGSBSGField;
            }
            set {
                this.nIL_PERSETUJUAN_BGSBSGField = value;
                this.RaisePropertyChanged("NIL_PERSETUJUAN_BGSBSG");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NIL_PERSETUJUAN_BGSBSGSpecified {
            get {
                return this.nIL_PERSETUJUAN_BGSBSGFieldSpecified;
            }
            set {
                this.nIL_PERSETUJUAN_BGSBSGFieldSpecified = value;
                this.RaisePropertyChanged("NIL_PERSETUJUAN_BGSBSGSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=12)]
        public System.Nullable<decimal> NIL_PELAKSANAAN_BGSBSG {
            get {
                return this.nIL_PELAKSANAAN_BGSBSGField;
            }
            set {
                this.nIL_PELAKSANAAN_BGSBSGField = value;
                this.RaisePropertyChanged("NIL_PELAKSANAAN_BGSBSG");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NIL_PELAKSANAAN_BGSBSGSpecified {
            get {
                return this.nIL_PELAKSANAAN_BGSBSGFieldSpecified;
            }
            set {
                this.nIL_PELAKSANAAN_BGSBSGFieldSpecified = value;
                this.RaisePropertyChanged("NIL_PELAKSANAAN_BGSBSGSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=13)]
        public System.Nullable<decimal> NIL_PERSETUJUAN_KSPI {
            get {
                return this.nIL_PERSETUJUAN_KSPIField;
            }
            set {
                this.nIL_PERSETUJUAN_KSPIField = value;
                this.RaisePropertyChanged("NIL_PERSETUJUAN_KSPI");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NIL_PERSETUJUAN_KSPISpecified {
            get {
                return this.nIL_PERSETUJUAN_KSPIFieldSpecified;
            }
            set {
                this.nIL_PERSETUJUAN_KSPIFieldSpecified = value;
                this.RaisePropertyChanged("NIL_PERSETUJUAN_KSPISpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=14)]
        public System.Nullable<decimal> NIL_PELAKSANAAN_KSPI {
            get {
                return this.nIL_PELAKSANAAN_KSPIField;
            }
            set {
                this.nIL_PELAKSANAAN_KSPIField = value;
                this.RaisePropertyChanged("NIL_PELAKSANAAN_KSPI");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NIL_PELAKSANAAN_KSPISpecified {
            get {
                return this.nIL_PELAKSANAAN_KSPIFieldSpecified;
            }
            set {
                this.nIL_PELAKSANAAN_KSPIFieldSpecified = value;
                this.RaisePropertyChanged("NIL_PELAKSANAAN_KSPISpecified");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_MON_MANFAAT_KORWIL_A1")]
    public partial class OutputParameters : object, System.ComponentModel.INotifyPropertyChanged {
        
        private WASDALSROW_MON_MANFAAT_KORWIL_A1[] sF_MON_MANFAAT_KORWIL_A1Field;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("SF_MON_MANFAAT_KORWIL_A1_ITEM")]
        public WASDALSROW_MON_MANFAAT_KORWIL_A1[] SF_MON_MANFAAT_KORWIL_A1 {
            get {
                return this.sF_MON_MANFAAT_KORWIL_A1Field;
            }
            set {
                this.sF_MON_MANFAAT_KORWIL_A1Field = value;
                this.RaisePropertyChanged("SF_MON_MANFAAT_KORWIL_A1");
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
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_MON_MANFAAT_KORWIL_A1", Order=0)]
        public AppPengguna.SvcMonKorwilManfaatA1.InputParameters InputParameters;
        
        public executeRequest() {
        }
        
        public executeRequest(AppPengguna.SvcMonKorwilManfaatA1.InputParameters InputParameters) {
            this.InputParameters = InputParameters;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class executeResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_MON_MANFAAT_KORWIL_A1", Order=0)]
        public AppPengguna.SvcMonKorwilManfaatA1.OutputParameters OutputParameters;
        
        public executeResponse() {
        }
        
        public executeResponse(AppPengguna.SvcMonKorwilManfaatA1.OutputParameters OutputParameters) {
            this.OutputParameters = OutputParameters;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface execute_pttChannel : AppPengguna.SvcMonKorwilManfaatA1.execute_ptt, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class executeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public executeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public AppPengguna.SvcMonKorwilManfaatA1.OutputParameters Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((AppPengguna.SvcMonKorwilManfaatA1.OutputParameters)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class execute_pttClient : System.ServiceModel.ClientBase<AppPengguna.SvcMonKorwilManfaatA1.execute_ptt>, AppPengguna.SvcMonKorwilManfaatA1.execute_ptt {
        
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
        AppPengguna.SvcMonKorwilManfaatA1.executeResponse AppPengguna.SvcMonKorwilManfaatA1.execute_ptt.execute(AppPengguna.SvcMonKorwilManfaatA1.executeRequest request) {
            return base.Channel.execute(request);
        }
        
        public AppPengguna.SvcMonKorwilManfaatA1.OutputParameters execute(AppPengguna.SvcMonKorwilManfaatA1.InputParameters InputParameters) {
            AppPengguna.SvcMonKorwilManfaatA1.executeRequest inValue = new AppPengguna.SvcMonKorwilManfaatA1.executeRequest();
            inValue.InputParameters = InputParameters;
            AppPengguna.SvcMonKorwilManfaatA1.executeResponse retVal = ((AppPengguna.SvcMonKorwilManfaatA1.execute_ptt)(this)).execute(inValue);
            return retVal.OutputParameters;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult AppPengguna.SvcMonKorwilManfaatA1.execute_ptt.Beginexecute(AppPengguna.SvcMonKorwilManfaatA1.executeRequest request, System.AsyncCallback callback, object asyncState) {
            return base.Channel.Beginexecute(request, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult Beginexecute(AppPengguna.SvcMonKorwilManfaatA1.InputParameters InputParameters, System.AsyncCallback callback, object asyncState) {
            AppPengguna.SvcMonKorwilManfaatA1.executeRequest inValue = new AppPengguna.SvcMonKorwilManfaatA1.executeRequest();
            inValue.InputParameters = InputParameters;
            return ((AppPengguna.SvcMonKorwilManfaatA1.execute_ptt)(this)).Beginexecute(inValue, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        AppPengguna.SvcMonKorwilManfaatA1.executeResponse AppPengguna.SvcMonKorwilManfaatA1.execute_ptt.Endexecute(System.IAsyncResult result) {
            return base.Channel.Endexecute(result);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public AppPengguna.SvcMonKorwilManfaatA1.OutputParameters Endexecute(System.IAsyncResult result) {
            AppPengguna.SvcMonKorwilManfaatA1.executeResponse retVal = ((AppPengguna.SvcMonKorwilManfaatA1.execute_ptt)(this)).Endexecute(result);
            return retVal.OutputParameters;
        }
        
        private System.IAsyncResult OnBeginexecute(object[] inValues, System.AsyncCallback callback, object asyncState) {
            AppPengguna.SvcMonKorwilManfaatA1.InputParameters InputParameters = ((AppPengguna.SvcMonKorwilManfaatA1.InputParameters)(inValues[0]));
            return this.Beginexecute(InputParameters, callback, asyncState);
        }
        
        private object[] OnEndexecute(System.IAsyncResult result) {
            AppPengguna.SvcMonKorwilManfaatA1.OutputParameters retVal = this.Endexecute(result);
            return new object[] {
                    retVal};
        }
        
        private void OnexecuteCompleted(object state) {
            if ((this.executeCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.executeCompleted(this, new executeCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void executeAsync(AppPengguna.SvcMonKorwilManfaatA1.InputParameters InputParameters) {
            this.executeAsync(InputParameters, null);
        }
        
        public void executeAsync(AppPengguna.SvcMonKorwilManfaatA1.InputParameters InputParameters, object userState) {
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
