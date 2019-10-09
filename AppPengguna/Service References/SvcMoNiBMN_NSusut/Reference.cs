﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppPengguna.SvcMoNiBMN_NSusut {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://oracle.com/sca/soapservice/siman_dev_maset/aset/satkerMonBmnSelectNSusut", ConfigurationName="SvcMoNiBMN_NSusut.satkerMonBmnSelectNSusut_ptt")]
    public interface satkerMonBmnSelectNSusut_ptt {
        
        // CODEGEN: Generating message contract since the operation execute is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="execute", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        AppPengguna.SvcMoNiBMN_NSusut.executeResponse execute(AppPengguna.SvcMoNiBMN_NSusut.executeRequest request);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="execute", ReplyAction="*")]
        System.IAsyncResult Beginexecute(AppPengguna.SvcMoNiBMN_NSusut.executeRequest request, System.AsyncCallback callback, object asyncState);
        
        AppPengguna.SvcMoNiBMN_NSusut.executeResponse Endexecute(System.IAsyncResult result);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_ROW_MON_BMN_SATKER_NSUSUT")]
    public partial class InputParameters : object, System.ComponentModel.INotifyPropertyChanged {
        
        private System.Nullable<decimal> p_YEARField;
        
        private bool p_YEARFieldSpecified;
        
        private System.Nullable<int> p_MONTH1Field;
        
        private bool p_MONTH1FieldSpecified;
        
        private System.Nullable<int> p_MONTH2Field;
        
        private bool p_MONTH2FieldSpecified;
        
        private System.Nullable<decimal> p_ID_KANWILField;
        
        private bool p_ID_KANWILFieldSpecified;
        
        private System.Nullable<decimal> p_ID_ESELON1Field;
        
        private bool p_ID_ESELON1FieldSpecified;
        
        private System.Nullable<decimal> p_ID_KLField;
        
        private bool p_ID_KLFieldSpecified;
        
        private System.Nullable<decimal> p_ID_KORWILField;
        
        private bool p_ID_KORWILFieldSpecified;
        
        private System.Nullable<decimal> p_ID_KPKNLField;
        
        private bool p_ID_KPKNLFieldSpecified;
        
        private System.Nullable<decimal> p_ID_KPPNField;
        
        private bool p_ID_KPPNFieldSpecified;
        
        private System.Nullable<decimal> p_ID_SATKERField;
        
        private bool p_ID_SATKERFieldSpecified;
        
        private string sTR_WHEREField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=0)]
        public System.Nullable<decimal> P_YEAR {
            get {
                return this.p_YEARField;
            }
            set {
                this.p_YEARField = value;
                this.RaisePropertyChanged("P_YEAR");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool P_YEARSpecified {
            get {
                return this.p_YEARFieldSpecified;
            }
            set {
                this.p_YEARFieldSpecified = value;
                this.RaisePropertyChanged("P_YEARSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=1)]
        public System.Nullable<int> P_MONTH1 {
            get {
                return this.p_MONTH1Field;
            }
            set {
                this.p_MONTH1Field = value;
                this.RaisePropertyChanged("P_MONTH1");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool P_MONTH1Specified {
            get {
                return this.p_MONTH1FieldSpecified;
            }
            set {
                this.p_MONTH1FieldSpecified = value;
                this.RaisePropertyChanged("P_MONTH1Specified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=2)]
        public System.Nullable<int> P_MONTH2 {
            get {
                return this.p_MONTH2Field;
            }
            set {
                this.p_MONTH2Field = value;
                this.RaisePropertyChanged("P_MONTH2");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool P_MONTH2Specified {
            get {
                return this.p_MONTH2FieldSpecified;
            }
            set {
                this.p_MONTH2FieldSpecified = value;
                this.RaisePropertyChanged("P_MONTH2Specified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=3)]
        public System.Nullable<decimal> P_ID_KANWIL {
            get {
                return this.p_ID_KANWILField;
            }
            set {
                this.p_ID_KANWILField = value;
                this.RaisePropertyChanged("P_ID_KANWIL");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool P_ID_KANWILSpecified {
            get {
                return this.p_ID_KANWILFieldSpecified;
            }
            set {
                this.p_ID_KANWILFieldSpecified = value;
                this.RaisePropertyChanged("P_ID_KANWILSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=4)]
        public System.Nullable<decimal> P_ID_ESELON1 {
            get {
                return this.p_ID_ESELON1Field;
            }
            set {
                this.p_ID_ESELON1Field = value;
                this.RaisePropertyChanged("P_ID_ESELON1");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool P_ID_ESELON1Specified {
            get {
                return this.p_ID_ESELON1FieldSpecified;
            }
            set {
                this.p_ID_ESELON1FieldSpecified = value;
                this.RaisePropertyChanged("P_ID_ESELON1Specified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=5)]
        public System.Nullable<decimal> P_ID_KL {
            get {
                return this.p_ID_KLField;
            }
            set {
                this.p_ID_KLField = value;
                this.RaisePropertyChanged("P_ID_KL");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool P_ID_KLSpecified {
            get {
                return this.p_ID_KLFieldSpecified;
            }
            set {
                this.p_ID_KLFieldSpecified = value;
                this.RaisePropertyChanged("P_ID_KLSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=6)]
        public System.Nullable<decimal> P_ID_KORWIL {
            get {
                return this.p_ID_KORWILField;
            }
            set {
                this.p_ID_KORWILField = value;
                this.RaisePropertyChanged("P_ID_KORWIL");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool P_ID_KORWILSpecified {
            get {
                return this.p_ID_KORWILFieldSpecified;
            }
            set {
                this.p_ID_KORWILFieldSpecified = value;
                this.RaisePropertyChanged("P_ID_KORWILSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=7)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=8)]
        public System.Nullable<decimal> P_ID_KPPN {
            get {
                return this.p_ID_KPPNField;
            }
            set {
                this.p_ID_KPPNField = value;
                this.RaisePropertyChanged("P_ID_KPPN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool P_ID_KPPNSpecified {
            get {
                return this.p_ID_KPPNFieldSpecified;
            }
            set {
                this.p_ID_KPPNFieldSpecified = value;
                this.RaisePropertyChanged("P_ID_KPPNSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=9)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=10)]
        public string STR_WHERE {
            get {
                return this.sTR_WHEREField;
            }
            set {
                this.sTR_WHEREField = value;
                this.RaisePropertyChanged("STR_WHERE");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="BPSIMAN.SROW_MON_BMN_SATKER_NSUSUT", Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_ROW_MON_BMN_SATKER_NSUSUT")]
    public partial class BPSIMANSROW_MON_BMN_SATKER_NSUSUT : object, System.ComponentModel.INotifyPropertyChanged {
        
        private System.Nullable<decimal> nUMField;
        
        private bool nUMFieldSpecified;
        
        private string kD_KLField;
        
        private string kD_SATKERField;
        
        private string uR_SATKERField;
        
        private System.Nullable<decimal> iNTRAKOMPATIBELField;
        
        private bool iNTRAKOMPATIBELFieldSpecified;
        
        private System.Nullable<decimal> eKSTRAKOMPATIBELField;
        
        private bool eKSTRAKOMPATIBELFieldSpecified;
        
        private System.Nullable<decimal> gABUNGANField;
        
        private bool gABUNGANFieldSpecified;
        
        private string kDKPKNLField;
        
        private string uRKPKNLField;
        
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
        public string KD_KL {
            get {
                return this.kD_KLField;
            }
            set {
                this.kD_KLField = value;
                this.RaisePropertyChanged("KD_KL");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=2)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=3)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=4)]
        public System.Nullable<decimal> INTRAKOMPATIBEL {
            get {
                return this.iNTRAKOMPATIBELField;
            }
            set {
                this.iNTRAKOMPATIBELField = value;
                this.RaisePropertyChanged("INTRAKOMPATIBEL");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool INTRAKOMPATIBELSpecified {
            get {
                return this.iNTRAKOMPATIBELFieldSpecified;
            }
            set {
                this.iNTRAKOMPATIBELFieldSpecified = value;
                this.RaisePropertyChanged("INTRAKOMPATIBELSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=5)]
        public System.Nullable<decimal> EKSTRAKOMPATIBEL {
            get {
                return this.eKSTRAKOMPATIBELField;
            }
            set {
                this.eKSTRAKOMPATIBELField = value;
                this.RaisePropertyChanged("EKSTRAKOMPATIBEL");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool EKSTRAKOMPATIBELSpecified {
            get {
                return this.eKSTRAKOMPATIBELFieldSpecified;
            }
            set {
                this.eKSTRAKOMPATIBELFieldSpecified = value;
                this.RaisePropertyChanged("EKSTRAKOMPATIBELSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=6)]
        public System.Nullable<decimal> GABUNGAN {
            get {
                return this.gABUNGANField;
            }
            set {
                this.gABUNGANField = value;
                this.RaisePropertyChanged("GABUNGAN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool GABUNGANSpecified {
            get {
                return this.gABUNGANFieldSpecified;
            }
            set {
                this.gABUNGANFieldSpecified = value;
                this.RaisePropertyChanged("GABUNGANSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=7)]
        public string KDKPKNL {
            get {
                return this.kDKPKNLField;
            }
            set {
                this.kDKPKNLField = value;
                this.RaisePropertyChanged("KDKPKNL");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=8)]
        public string URKPKNL {
            get {
                return this.uRKPKNLField;
            }
            set {
                this.uRKPKNLField = value;
                this.RaisePropertyChanged("URKPKNL");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_ROW_MON_BMN_SATKER_NSUSUT")]
    public partial class OutputParameters : object, System.ComponentModel.INotifyPropertyChanged {
        
        private BPSIMANSROW_MON_BMN_SATKER_NSUSUT[] sF_ROW_MON_BMN_SATKER_NSUSUTField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("SF_ROW_MON_BMN_SATKER_NSUSUT_ITEM")]
        public BPSIMANSROW_MON_BMN_SATKER_NSUSUT[] SF_ROW_MON_BMN_SATKER_NSUSUT {
            get {
                return this.sF_ROW_MON_BMN_SATKER_NSUSUTField;
            }
            set {
                this.sF_ROW_MON_BMN_SATKER_NSUSUTField = value;
                this.RaisePropertyChanged("SF_ROW_MON_BMN_SATKER_NSUSUT");
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
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_ROW_MON_BMN_SATKER_NSUSUT", Order=0)]
        public AppPengguna.SvcMoNiBMN_NSusut.InputParameters InputParameters;
        
        public executeRequest() {
        }
        
        public executeRequest(AppPengguna.SvcMoNiBMN_NSusut.InputParameters InputParameters) {
            this.InputParameters = InputParameters;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class executeResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_ROW_MON_BMN_SATKER_NSUSUT", Order=0)]
        public AppPengguna.SvcMoNiBMN_NSusut.OutputParameters OutputParameters;
        
        public executeResponse() {
        }
        
        public executeResponse(AppPengguna.SvcMoNiBMN_NSusut.OutputParameters OutputParameters) {
            this.OutputParameters = OutputParameters;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface satkerMonBmnSelectNSusut_pttChannel : AppPengguna.SvcMoNiBMN_NSusut.satkerMonBmnSelectNSusut_ptt, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class executeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public executeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public AppPengguna.SvcMoNiBMN_NSusut.OutputParameters Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((AppPengguna.SvcMoNiBMN_NSusut.OutputParameters)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class satkerMonBmnSelectNSusut_pttClient : System.ServiceModel.ClientBase<AppPengguna.SvcMoNiBMN_NSusut.satkerMonBmnSelectNSusut_ptt>, AppPengguna.SvcMoNiBMN_NSusut.satkerMonBmnSelectNSusut_ptt {
        
        private BeginOperationDelegate onBeginexecuteDelegate;
        
        private EndOperationDelegate onEndexecuteDelegate;
        
        private System.Threading.SendOrPostCallback onexecuteCompletedDelegate;
        
        public satkerMonBmnSelectNSusut_pttClient() {
        }
        
        public satkerMonBmnSelectNSusut_pttClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public satkerMonBmnSelectNSusut_pttClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public satkerMonBmnSelectNSusut_pttClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public satkerMonBmnSelectNSusut_pttClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public event System.EventHandler<executeCompletedEventArgs> executeCompleted;
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        AppPengguna.SvcMoNiBMN_NSusut.executeResponse AppPengguna.SvcMoNiBMN_NSusut.satkerMonBmnSelectNSusut_ptt.execute(AppPengguna.SvcMoNiBMN_NSusut.executeRequest request) {
            return base.Channel.execute(request);
        }
        
        public AppPengguna.SvcMoNiBMN_NSusut.OutputParameters execute(AppPengguna.SvcMoNiBMN_NSusut.InputParameters InputParameters) {
            AppPengguna.SvcMoNiBMN_NSusut.executeRequest inValue = new AppPengguna.SvcMoNiBMN_NSusut.executeRequest();
            inValue.InputParameters = InputParameters;
            AppPengguna.SvcMoNiBMN_NSusut.executeResponse retVal = ((AppPengguna.SvcMoNiBMN_NSusut.satkerMonBmnSelectNSusut_ptt)(this)).execute(inValue);
            return retVal.OutputParameters;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult AppPengguna.SvcMoNiBMN_NSusut.satkerMonBmnSelectNSusut_ptt.Beginexecute(AppPengguna.SvcMoNiBMN_NSusut.executeRequest request, System.AsyncCallback callback, object asyncState) {
            return base.Channel.Beginexecute(request, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult Beginexecute(AppPengguna.SvcMoNiBMN_NSusut.InputParameters InputParameters, System.AsyncCallback callback, object asyncState) {
            AppPengguna.SvcMoNiBMN_NSusut.executeRequest inValue = new AppPengguna.SvcMoNiBMN_NSusut.executeRequest();
            inValue.InputParameters = InputParameters;
            return ((AppPengguna.SvcMoNiBMN_NSusut.satkerMonBmnSelectNSusut_ptt)(this)).Beginexecute(inValue, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        AppPengguna.SvcMoNiBMN_NSusut.executeResponse AppPengguna.SvcMoNiBMN_NSusut.satkerMonBmnSelectNSusut_ptt.Endexecute(System.IAsyncResult result) {
            return base.Channel.Endexecute(result);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public AppPengguna.SvcMoNiBMN_NSusut.OutputParameters Endexecute(System.IAsyncResult result) {
            AppPengguna.SvcMoNiBMN_NSusut.executeResponse retVal = ((AppPengguna.SvcMoNiBMN_NSusut.satkerMonBmnSelectNSusut_ptt)(this)).Endexecute(result);
            return retVal.OutputParameters;
        }
        
        private System.IAsyncResult OnBeginexecute(object[] inValues, System.AsyncCallback callback, object asyncState) {
            AppPengguna.SvcMoNiBMN_NSusut.InputParameters InputParameters = ((AppPengguna.SvcMoNiBMN_NSusut.InputParameters)(inValues[0]));
            return this.Beginexecute(InputParameters, callback, asyncState);
        }
        
        private object[] OnEndexecute(System.IAsyncResult result) {
            AppPengguna.SvcMoNiBMN_NSusut.OutputParameters retVal = this.Endexecute(result);
            return new object[] {
                    retVal};
        }
        
        private void OnexecuteCompleted(object state) {
            if ((this.executeCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.executeCompleted(this, new executeCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void executeAsync(AppPengguna.SvcMoNiBMN_NSusut.InputParameters InputParameters) {
            this.executeAsync(InputParameters, null);
        }
        
        public void executeAsync(AppPengguna.SvcMoNiBMN_NSusut.InputParameters InputParameters, object userState) {
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
