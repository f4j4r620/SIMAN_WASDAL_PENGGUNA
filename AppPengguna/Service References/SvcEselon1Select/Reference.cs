﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppPengguna.SvcEselon1Select {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://oracle.com/sca/soapservice/siman_referensi/dsREselon1/dsREselon1Select", ConfigurationName="SvcEselon1Select.dsREselon1Select_ptt")]
    public interface dsREselon1Select_ptt {
        
        // CODEGEN: Generating message contract since the operation execute is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="execute", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        AppPengguna.SvcEselon1Select.executeResponse execute(AppPengguna.SvcEselon1Select.executeRequest request);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="execute", ReplyAction="*")]
        System.IAsyncResult Beginexecute(AppPengguna.SvcEselon1Select.executeRequest request, System.AsyncCallback callback, object asyncState);
        
        AppPengguna.SvcEselon1Select.executeResponse Endexecute(System.IAsyncResult result);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_ROW_R_ESELON1")]
    public partial class InputParameters : object, System.ComponentModel.INotifyPropertyChanged {
        
        private System.Nullable<decimal> p_MINField;
        
        private bool p_MINFieldSpecified;
        
        private System.Nullable<decimal> p_MAXField;
        
        private bool p_MAXFieldSpecified;
        
        private string p_KD_KLField;
        
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=3)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=4)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=5)]
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="BPSIMAN.SROW_R_ESELON1", Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_ROW_R_ESELON1")]
    public partial class BPSIMANSROW_R_ESELON1 : object, System.ComponentModel.INotifyPropertyChanged {
        
        private System.Nullable<decimal> nUMField;
        
        private bool nUMFieldSpecified;
        
        private System.Nullable<decimal> iD_ESELON1Field;
        
        private bool iD_ESELON1FieldSpecified;
        
        private string kD_ESELONKLField;
        
        private string kD_ESELON1Field;
        
        private string uR_ESELON1Field;
        
        private System.Nullable<decimal> iD_KLField;
        
        private bool iD_KLFieldSpecified;
        
        private string aLAMAT_E1Field;
        
        private string kODE_KAB_KOTAField;
        
        private string nM_KAB_KOTAField;
        
        private string nO_TELP_KANTORField;
        
        private string nO_FAX_KANTORField;
        
        private string eMAIL_KANTORField;
        
        private System.Nullable<decimal> iD_KPKNLField;
        
        private bool iD_KPKNLFieldSpecified;
        
        private System.Nullable<decimal> iD_KANWILField;
        
        private bool iD_KANWILFieldSpecified;
        
        private string nIPField;
        
        private string nAMAField;
        
        private string jABATANField;
        
        private string kD_KLField;
        
        private string uR_KLField;
        
        private System.Nullable<decimal> tOTAL_DATAField;
        
        private bool tOTAL_DATAFieldSpecified;
        
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
        public System.Nullable<decimal> ID_ESELON1 {
            get {
                return this.iD_ESELON1Field;
            }
            set {
                this.iD_ESELON1Field = value;
                this.RaisePropertyChanged("ID_ESELON1");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ID_ESELON1Specified {
            get {
                return this.iD_ESELON1FieldSpecified;
            }
            set {
                this.iD_ESELON1FieldSpecified = value;
                this.RaisePropertyChanged("ID_ESELON1Specified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=2)]
        public string KD_ESELONKL {
            get {
                return this.kD_ESELONKLField;
            }
            set {
                this.kD_ESELONKLField = value;
                this.RaisePropertyChanged("KD_ESELONKL");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=3)]
        public string KD_ESELON1 {
            get {
                return this.kD_ESELON1Field;
            }
            set {
                this.kD_ESELON1Field = value;
                this.RaisePropertyChanged("KD_ESELON1");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=4)]
        public string UR_ESELON1 {
            get {
                return this.uR_ESELON1Field;
            }
            set {
                this.uR_ESELON1Field = value;
                this.RaisePropertyChanged("UR_ESELON1");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=5)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=6)]
        public string ALAMAT_E1 {
            get {
                return this.aLAMAT_E1Field;
            }
            set {
                this.aLAMAT_E1Field = value;
                this.RaisePropertyChanged("ALAMAT_E1");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=7)]
        public string KODE_KAB_KOTA {
            get {
                return this.kODE_KAB_KOTAField;
            }
            set {
                this.kODE_KAB_KOTAField = value;
                this.RaisePropertyChanged("KODE_KAB_KOTA");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=8)]
        public string NM_KAB_KOTA {
            get {
                return this.nM_KAB_KOTAField;
            }
            set {
                this.nM_KAB_KOTAField = value;
                this.RaisePropertyChanged("NM_KAB_KOTA");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=9)]
        public string NO_TELP_KANTOR {
            get {
                return this.nO_TELP_KANTORField;
            }
            set {
                this.nO_TELP_KANTORField = value;
                this.RaisePropertyChanged("NO_TELP_KANTOR");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=10)]
        public string NO_FAX_KANTOR {
            get {
                return this.nO_FAX_KANTORField;
            }
            set {
                this.nO_FAX_KANTORField = value;
                this.RaisePropertyChanged("NO_FAX_KANTOR");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=11)]
        public string EMAIL_KANTOR {
            get {
                return this.eMAIL_KANTORField;
            }
            set {
                this.eMAIL_KANTORField = value;
                this.RaisePropertyChanged("EMAIL_KANTOR");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=12)]
        public System.Nullable<decimal> ID_KPKNL {
            get {
                return this.iD_KPKNLField;
            }
            set {
                this.iD_KPKNLField = value;
                this.RaisePropertyChanged("ID_KPKNL");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ID_KPKNLSpecified {
            get {
                return this.iD_KPKNLFieldSpecified;
            }
            set {
                this.iD_KPKNLFieldSpecified = value;
                this.RaisePropertyChanged("ID_KPKNLSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=13)]
        public System.Nullable<decimal> ID_KANWIL {
            get {
                return this.iD_KANWILField;
            }
            set {
                this.iD_KANWILField = value;
                this.RaisePropertyChanged("ID_KANWIL");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ID_KANWILSpecified {
            get {
                return this.iD_KANWILFieldSpecified;
            }
            set {
                this.iD_KANWILFieldSpecified = value;
                this.RaisePropertyChanged("ID_KANWILSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=14)]
        public string NIP {
            get {
                return this.nIPField;
            }
            set {
                this.nIPField = value;
                this.RaisePropertyChanged("NIP");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=15)]
        public string NAMA {
            get {
                return this.nAMAField;
            }
            set {
                this.nAMAField = value;
                this.RaisePropertyChanged("NAMA");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=16)]
        public string JABATAN {
            get {
                return this.jABATANField;
            }
            set {
                this.jABATANField = value;
                this.RaisePropertyChanged("JABATAN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=17)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=18)]
        public string UR_KL {
            get {
                return this.uR_KLField;
            }
            set {
                this.uR_KLField = value;
                this.RaisePropertyChanged("UR_KL");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=19)]
        public System.Nullable<decimal> TOTAL_DATA {
            get {
                return this.tOTAL_DATAField;
            }
            set {
                this.tOTAL_DATAField = value;
                this.RaisePropertyChanged("TOTAL_DATA");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TOTAL_DATASpecified {
            get {
                return this.tOTAL_DATAFieldSpecified;
            }
            set {
                this.tOTAL_DATAFieldSpecified = value;
                this.RaisePropertyChanged("TOTAL_DATASpecified");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_ROW_R_ESELON1")]
    public partial class OutputParameters : object, System.ComponentModel.INotifyPropertyChanged {
        
        private BPSIMANSROW_R_ESELON1[] sF_ROW_R_ESELON1Field;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("SF_ROW_R_ESELON1_ITEM")]
        public BPSIMANSROW_R_ESELON1[] SF_ROW_R_ESELON1 {
            get {
                return this.sF_ROW_R_ESELON1Field;
            }
            set {
                this.sF_ROW_R_ESELON1Field = value;
                this.RaisePropertyChanged("SF_ROW_R_ESELON1");
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
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_ROW_R_ESELON1", Order=0)]
        public AppPengguna.SvcEselon1Select.InputParameters InputParameters;
        
        public executeRequest() {
        }
        
        public executeRequest(AppPengguna.SvcEselon1Select.InputParameters InputParameters) {
            this.InputParameters = InputParameters;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class executeResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_ROW_R_ESELON1", Order=0)]
        public AppPengguna.SvcEselon1Select.OutputParameters OutputParameters;
        
        public executeResponse() {
        }
        
        public executeResponse(AppPengguna.SvcEselon1Select.OutputParameters OutputParameters) {
            this.OutputParameters = OutputParameters;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface dsREselon1Select_pttChannel : AppPengguna.SvcEselon1Select.dsREselon1Select_ptt, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class executeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public executeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public AppPengguna.SvcEselon1Select.OutputParameters Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((AppPengguna.SvcEselon1Select.OutputParameters)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class dsREselon1Select_pttClient : System.ServiceModel.ClientBase<AppPengguna.SvcEselon1Select.dsREselon1Select_ptt>, AppPengguna.SvcEselon1Select.dsREselon1Select_ptt {
        
        private BeginOperationDelegate onBeginexecuteDelegate;
        
        private EndOperationDelegate onEndexecuteDelegate;
        
        private System.Threading.SendOrPostCallback onexecuteCompletedDelegate;
        
        public dsREselon1Select_pttClient() {
        }
        
        public dsREselon1Select_pttClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public dsREselon1Select_pttClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public dsREselon1Select_pttClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public dsREselon1Select_pttClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public event System.EventHandler<executeCompletedEventArgs> executeCompleted;
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        AppPengguna.SvcEselon1Select.executeResponse AppPengguna.SvcEselon1Select.dsREselon1Select_ptt.execute(AppPengguna.SvcEselon1Select.executeRequest request) {
            return base.Channel.execute(request);
        }
        
        public AppPengguna.SvcEselon1Select.OutputParameters execute(AppPengguna.SvcEselon1Select.InputParameters InputParameters) {
            AppPengguna.SvcEselon1Select.executeRequest inValue = new AppPengguna.SvcEselon1Select.executeRequest();
            inValue.InputParameters = InputParameters;
            AppPengguna.SvcEselon1Select.executeResponse retVal = ((AppPengguna.SvcEselon1Select.dsREselon1Select_ptt)(this)).execute(inValue);
            return retVal.OutputParameters;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult AppPengguna.SvcEselon1Select.dsREselon1Select_ptt.Beginexecute(AppPengguna.SvcEselon1Select.executeRequest request, System.AsyncCallback callback, object asyncState) {
            return base.Channel.Beginexecute(request, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult Beginexecute(AppPengguna.SvcEselon1Select.InputParameters InputParameters, System.AsyncCallback callback, object asyncState) {
            AppPengguna.SvcEselon1Select.executeRequest inValue = new AppPengguna.SvcEselon1Select.executeRequest();
            inValue.InputParameters = InputParameters;
            return ((AppPengguna.SvcEselon1Select.dsREselon1Select_ptt)(this)).Beginexecute(inValue, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        AppPengguna.SvcEselon1Select.executeResponse AppPengguna.SvcEselon1Select.dsREselon1Select_ptt.Endexecute(System.IAsyncResult result) {
            return base.Channel.Endexecute(result);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public AppPengguna.SvcEselon1Select.OutputParameters Endexecute(System.IAsyncResult result) {
            AppPengguna.SvcEselon1Select.executeResponse retVal = ((AppPengguna.SvcEselon1Select.dsREselon1Select_ptt)(this)).Endexecute(result);
            return retVal.OutputParameters;
        }
        
        private System.IAsyncResult OnBeginexecute(object[] inValues, System.AsyncCallback callback, object asyncState) {
            AppPengguna.SvcEselon1Select.InputParameters InputParameters = ((AppPengguna.SvcEselon1Select.InputParameters)(inValues[0]));
            return this.Beginexecute(InputParameters, callback, asyncState);
        }
        
        private object[] OnEndexecute(System.IAsyncResult result) {
            AppPengguna.SvcEselon1Select.OutputParameters retVal = this.Endexecute(result);
            return new object[] {
                    retVal};
        }
        
        private void OnexecuteCompleted(object state) {
            if ((this.executeCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.executeCompleted(this, new executeCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void executeAsync(AppPengguna.SvcEselon1Select.InputParameters InputParameters) {
            this.executeAsync(InputParameters, null);
        }
        
        public void executeAsync(AppPengguna.SvcEselon1Select.InputParameters InputParameters, object userState) {
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