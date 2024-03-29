﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppPengguna.SvcMonKlPindahA1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://oracle.com/sca/soapservice/siman_wasdal/wasdalMonKl/selectPindahA1", ConfigurationName="SvcMonKlPindahA1.execute_ptt")]
    public interface execute_ptt {
        
        // CODEGEN: Generating message contract since the operation execute is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="execute", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        AppPengguna.SvcMonKlPindahA1.executeResponse execute(AppPengguna.SvcMonKlPindahA1.executeRequest request);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="execute", ReplyAction="*")]
        System.IAsyncResult Beginexecute(AppPengguna.SvcMonKlPindahA1.executeRequest request, System.AsyncCallback callback, object asyncState);
        
        AppPengguna.SvcMonKlPindahA1.executeResponse Endexecute(System.IAsyncResult result);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_MON_PINDAH_KL_A1")]
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
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="WASDAL.SROW_MON_PINDAH_KL_A1", Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_MON_PINDAH_KL_A1")]
    public partial class WASDALSROW_MON_PINDAH_KL_A1 : object, System.ComponentModel.INotifyPropertyChanged {
        
        private System.Nullable<decimal> nUMField;
        
        private bool nUMFieldSpecified;
        
        private System.Nullable<decimal> iD_KLField;
        
        private bool iD_KLFieldSpecified;
        
        private System.Nullable<decimal> iD_SATKERField;
        
        private bool iD_SATKERFieldSpecified;
        
        private string kD_SATKERField;
        
        private string uR_SATKERField;
        
        private System.Nullable<decimal> nIL_SETUJU_JUALField;
        
        private bool nIL_SETUJU_JUALFieldSpecified;
        
        private System.Nullable<decimal> nIL_PNBP_JUALField;
        
        private bool nIL_PNBP_JUALFieldSpecified;
        
        private System.Nullable<decimal> kUANTITAS_SETUJU_TUKARField;
        
        private bool kUANTITAS_SETUJU_TUKARFieldSpecified;
        
        private System.Nullable<decimal> kUANTITAS_PNBP_TUKARField;
        
        private bool kUANTITAS_PNBP_TUKARFieldSpecified;
        
        private System.Nullable<decimal> nIL_SETUJU_HIBAHField;
        
        private bool nIL_SETUJU_HIBAHFieldSpecified;
        
        private System.Nullable<decimal> nIL_PNBP_HIBAHField;
        
        private bool nIL_PNBP_HIBAHFieldSpecified;
        
        private System.Nullable<decimal> nIL_SETUJU_PMPField;
        
        private bool nIL_SETUJU_PMPFieldSpecified;
        
        private System.Nullable<decimal> nIL_PNBP_PMPField;
        
        private bool nIL_PNBP_PMPFieldSpecified;
        
        private System.Nullable<System.DateTime> tGL_CETAKField;
        
        private bool tGL_CETAKFieldSpecified;
        
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
        public System.Nullable<decimal> ID_KL {
            get {
                return this.iD_KLField;
            }
            set {
                this.iD_KLField = value;
                this.RaisePropertyChanged("ID_KL");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ID_KLSpecified {
            get {
                return this.iD_KLFieldSpecified;
            }
            set {
                this.iD_KLFieldSpecified = value;
                this.RaisePropertyChanged("ID_KLSpecified");
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
        public System.Nullable<decimal> NIL_SETUJU_JUAL {
            get {
                return this.nIL_SETUJU_JUALField;
            }
            set {
                this.nIL_SETUJU_JUALField = value;
                this.RaisePropertyChanged("NIL_SETUJU_JUAL");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NIL_SETUJU_JUALSpecified {
            get {
                return this.nIL_SETUJU_JUALFieldSpecified;
            }
            set {
                this.nIL_SETUJU_JUALFieldSpecified = value;
                this.RaisePropertyChanged("NIL_SETUJU_JUALSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=6)]
        public System.Nullable<decimal> NIL_PNBP_JUAL {
            get {
                return this.nIL_PNBP_JUALField;
            }
            set {
                this.nIL_PNBP_JUALField = value;
                this.RaisePropertyChanged("NIL_PNBP_JUAL");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NIL_PNBP_JUALSpecified {
            get {
                return this.nIL_PNBP_JUALFieldSpecified;
            }
            set {
                this.nIL_PNBP_JUALFieldSpecified = value;
                this.RaisePropertyChanged("NIL_PNBP_JUALSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=7)]
        public System.Nullable<decimal> KUANTITAS_SETUJU_TUKAR {
            get {
                return this.kUANTITAS_SETUJU_TUKARField;
            }
            set {
                this.kUANTITAS_SETUJU_TUKARField = value;
                this.RaisePropertyChanged("KUANTITAS_SETUJU_TUKAR");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool KUANTITAS_SETUJU_TUKARSpecified {
            get {
                return this.kUANTITAS_SETUJU_TUKARFieldSpecified;
            }
            set {
                this.kUANTITAS_SETUJU_TUKARFieldSpecified = value;
                this.RaisePropertyChanged("KUANTITAS_SETUJU_TUKARSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=8)]
        public System.Nullable<decimal> KUANTITAS_PNBP_TUKAR {
            get {
                return this.kUANTITAS_PNBP_TUKARField;
            }
            set {
                this.kUANTITAS_PNBP_TUKARField = value;
                this.RaisePropertyChanged("KUANTITAS_PNBP_TUKAR");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool KUANTITAS_PNBP_TUKARSpecified {
            get {
                return this.kUANTITAS_PNBP_TUKARFieldSpecified;
            }
            set {
                this.kUANTITAS_PNBP_TUKARFieldSpecified = value;
                this.RaisePropertyChanged("KUANTITAS_PNBP_TUKARSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=9)]
        public System.Nullable<decimal> NIL_SETUJU_HIBAH {
            get {
                return this.nIL_SETUJU_HIBAHField;
            }
            set {
                this.nIL_SETUJU_HIBAHField = value;
                this.RaisePropertyChanged("NIL_SETUJU_HIBAH");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NIL_SETUJU_HIBAHSpecified {
            get {
                return this.nIL_SETUJU_HIBAHFieldSpecified;
            }
            set {
                this.nIL_SETUJU_HIBAHFieldSpecified = value;
                this.RaisePropertyChanged("NIL_SETUJU_HIBAHSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=10)]
        public System.Nullable<decimal> NIL_PNBP_HIBAH {
            get {
                return this.nIL_PNBP_HIBAHField;
            }
            set {
                this.nIL_PNBP_HIBAHField = value;
                this.RaisePropertyChanged("NIL_PNBP_HIBAH");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NIL_PNBP_HIBAHSpecified {
            get {
                return this.nIL_PNBP_HIBAHFieldSpecified;
            }
            set {
                this.nIL_PNBP_HIBAHFieldSpecified = value;
                this.RaisePropertyChanged("NIL_PNBP_HIBAHSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=11)]
        public System.Nullable<decimal> NIL_SETUJU_PMP {
            get {
                return this.nIL_SETUJU_PMPField;
            }
            set {
                this.nIL_SETUJU_PMPField = value;
                this.RaisePropertyChanged("NIL_SETUJU_PMP");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NIL_SETUJU_PMPSpecified {
            get {
                return this.nIL_SETUJU_PMPFieldSpecified;
            }
            set {
                this.nIL_SETUJU_PMPFieldSpecified = value;
                this.RaisePropertyChanged("NIL_SETUJU_PMPSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=12)]
        public System.Nullable<decimal> NIL_PNBP_PMP {
            get {
                return this.nIL_PNBP_PMPField;
            }
            set {
                this.nIL_PNBP_PMPField = value;
                this.RaisePropertyChanged("NIL_PNBP_PMP");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NIL_PNBP_PMPSpecified {
            get {
                return this.nIL_PNBP_PMPFieldSpecified;
            }
            set {
                this.nIL_PNBP_PMPFieldSpecified = value;
                this.RaisePropertyChanged("NIL_PNBP_PMPSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=13)]
        public System.Nullable<System.DateTime> TGL_CETAK {
            get {
                return this.tGL_CETAKField;
            }
            set {
                this.tGL_CETAKField = value;
                this.RaisePropertyChanged("TGL_CETAK");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TGL_CETAKSpecified {
            get {
                return this.tGL_CETAKFieldSpecified;
            }
            set {
                this.tGL_CETAKFieldSpecified = value;
                this.RaisePropertyChanged("TGL_CETAKSpecified");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_MON_PINDAH_KL_A1")]
    public partial class OutputParameters : object, System.ComponentModel.INotifyPropertyChanged {
        
        private WASDALSROW_MON_PINDAH_KL_A1[] sF_MON_PINDAH_KL_A1Field;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("SF_MON_PINDAH_KL_A1_ITEM")]
        public WASDALSROW_MON_PINDAH_KL_A1[] SF_MON_PINDAH_KL_A1 {
            get {
                return this.sF_MON_PINDAH_KL_A1Field;
            }
            set {
                this.sF_MON_PINDAH_KL_A1Field = value;
                this.RaisePropertyChanged("SF_MON_PINDAH_KL_A1");
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
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_MON_PINDAH_KL_A1", Order=0)]
        public AppPengguna.SvcMonKlPindahA1.InputParameters InputParameters;
        
        public executeRequest() {
        }
        
        public executeRequest(AppPengguna.SvcMonKlPindahA1.InputParameters InputParameters) {
            this.InputParameters = InputParameters;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class executeResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_MON_PINDAH_KL_A1", Order=0)]
        public AppPengguna.SvcMonKlPindahA1.OutputParameters OutputParameters;
        
        public executeResponse() {
        }
        
        public executeResponse(AppPengguna.SvcMonKlPindahA1.OutputParameters OutputParameters) {
            this.OutputParameters = OutputParameters;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface execute_pttChannel : AppPengguna.SvcMonKlPindahA1.execute_ptt, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class executeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public executeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public AppPengguna.SvcMonKlPindahA1.OutputParameters Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((AppPengguna.SvcMonKlPindahA1.OutputParameters)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class execute_pttClient : System.ServiceModel.ClientBase<AppPengguna.SvcMonKlPindahA1.execute_ptt>, AppPengguna.SvcMonKlPindahA1.execute_ptt {
        
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
        AppPengguna.SvcMonKlPindahA1.executeResponse AppPengguna.SvcMonKlPindahA1.execute_ptt.execute(AppPengguna.SvcMonKlPindahA1.executeRequest request) {
            return base.Channel.execute(request);
        }
        
        public AppPengguna.SvcMonKlPindahA1.OutputParameters execute(AppPengguna.SvcMonKlPindahA1.InputParameters InputParameters) {
            AppPengguna.SvcMonKlPindahA1.executeRequest inValue = new AppPengguna.SvcMonKlPindahA1.executeRequest();
            inValue.InputParameters = InputParameters;
            AppPengguna.SvcMonKlPindahA1.executeResponse retVal = ((AppPengguna.SvcMonKlPindahA1.execute_ptt)(this)).execute(inValue);
            return retVal.OutputParameters;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult AppPengguna.SvcMonKlPindahA1.execute_ptt.Beginexecute(AppPengguna.SvcMonKlPindahA1.executeRequest request, System.AsyncCallback callback, object asyncState) {
            return base.Channel.Beginexecute(request, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult Beginexecute(AppPengguna.SvcMonKlPindahA1.InputParameters InputParameters, System.AsyncCallback callback, object asyncState) {
            AppPengguna.SvcMonKlPindahA1.executeRequest inValue = new AppPengguna.SvcMonKlPindahA1.executeRequest();
            inValue.InputParameters = InputParameters;
            return ((AppPengguna.SvcMonKlPindahA1.execute_ptt)(this)).Beginexecute(inValue, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        AppPengguna.SvcMonKlPindahA1.executeResponse AppPengguna.SvcMonKlPindahA1.execute_ptt.Endexecute(System.IAsyncResult result) {
            return base.Channel.Endexecute(result);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public AppPengguna.SvcMonKlPindahA1.OutputParameters Endexecute(System.IAsyncResult result) {
            AppPengguna.SvcMonKlPindahA1.executeResponse retVal = ((AppPengguna.SvcMonKlPindahA1.execute_ptt)(this)).Endexecute(result);
            return retVal.OutputParameters;
        }
        
        private System.IAsyncResult OnBeginexecute(object[] inValues, System.AsyncCallback callback, object asyncState) {
            AppPengguna.SvcMonKlPindahA1.InputParameters InputParameters = ((AppPengguna.SvcMonKlPindahA1.InputParameters)(inValues[0]));
            return this.Beginexecute(InputParameters, callback, asyncState);
        }
        
        private object[] OnEndexecute(System.IAsyncResult result) {
            AppPengguna.SvcMonKlPindahA1.OutputParameters retVal = this.Endexecute(result);
            return new object[] {
                    retVal};
        }
        
        private void OnexecuteCompleted(object state) {
            if ((this.executeCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.executeCompleted(this, new executeCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void executeAsync(AppPengguna.SvcMonKlPindahA1.InputParameters InputParameters) {
            this.executeAsync(InputParameters, null);
        }
        
        public void executeAsync(AppPengguna.SvcMonKlPindahA1.InputParameters InputParameters, object userState) {
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
