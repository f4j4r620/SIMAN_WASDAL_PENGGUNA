﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppPengguna.SvcMonSatkerGunaA23 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://oracle.com/sca/soapservice/siman_wasdal/wasdalMonSatker/selectGunaA23", ConfigurationName="SvcMonSatkerGunaA23.execute_ptt")]
    public interface execute_ptt {
        
        // CODEGEN: Generating message contract since the operation execute is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="execute", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        AppPengguna.SvcMonSatkerGunaA23.executeResponse execute(AppPengguna.SvcMonSatkerGunaA23.executeRequest request);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="execute", ReplyAction="*")]
        System.IAsyncResult Beginexecute(AppPengguna.SvcMonSatkerGunaA23.executeRequest request, System.AsyncCallback callback, object asyncState);
        
        AppPengguna.SvcMonSatkerGunaA23.executeResponse Endexecute(System.IAsyncResult result);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_MON_GUNA_SATKER_A23")]
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
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="WASDAL.SROW_MON_GUNA_SATKER_A23", Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_MON_GUNA_SATKER_A23")]
    public partial class WASDALSROW_MON_GUNA_SATKER_A23 : object, System.ComponentModel.INotifyPropertyChanged {
        
        private System.Nullable<decimal> nUMField;
        
        private bool nUMFieldSpecified;
        
        private System.Nullable<decimal> iD_ASETField;
        
        private bool iD_ASETFieldSpecified;
        
        private string kD_BRGField;
        
        private string uR_BRGField;
        
        private System.Nullable<decimal> nUPField;
        
        private bool nUPFieldSpecified;
        
        private string kD_JNS_DOKField;
        
        private string uR_JNS_DOKField;
        
        private string nO_DOKField;
        
        private System.Nullable<System.DateTime> tGL_DOKField;
        
        private bool tGL_DOKFieldSpecified;
        
        private System.Nullable<decimal> iD_SATKERField;
        
        private bool iD_SATKERFieldSpecified;
        
        private string kD_SATKERField;
        
        private string uR_SATKERField;
        
        private System.Nullable<decimal> iD_KPKNLField;
        
        private bool iD_KPKNLFieldSpecified;
        
        private System.Nullable<decimal> iD_KANWILField;
        
        private bool iD_KANWILFieldSpecified;
        
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
        public System.Nullable<decimal> ID_ASET {
            get {
                return this.iD_ASETField;
            }
            set {
                this.iD_ASETField = value;
                this.RaisePropertyChanged("ID_ASET");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ID_ASETSpecified {
            get {
                return this.iD_ASETFieldSpecified;
            }
            set {
                this.iD_ASETFieldSpecified = value;
                this.RaisePropertyChanged("ID_ASETSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=2)]
        public string KD_BRG {
            get {
                return this.kD_BRGField;
            }
            set {
                this.kD_BRGField = value;
                this.RaisePropertyChanged("KD_BRG");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=3)]
        public string UR_BRG {
            get {
                return this.uR_BRGField;
            }
            set {
                this.uR_BRGField = value;
                this.RaisePropertyChanged("UR_BRG");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=4)]
        public System.Nullable<decimal> NUP {
            get {
                return this.nUPField;
            }
            set {
                this.nUPField = value;
                this.RaisePropertyChanged("NUP");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NUPSpecified {
            get {
                return this.nUPFieldSpecified;
            }
            set {
                this.nUPFieldSpecified = value;
                this.RaisePropertyChanged("NUPSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=5)]
        public string KD_JNS_DOK {
            get {
                return this.kD_JNS_DOKField;
            }
            set {
                this.kD_JNS_DOKField = value;
                this.RaisePropertyChanged("KD_JNS_DOK");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=6)]
        public string UR_JNS_DOK {
            get {
                return this.uR_JNS_DOKField;
            }
            set {
                this.uR_JNS_DOKField = value;
                this.RaisePropertyChanged("UR_JNS_DOK");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=7)]
        public string NO_DOK {
            get {
                return this.nO_DOKField;
            }
            set {
                this.nO_DOKField = value;
                this.RaisePropertyChanged("NO_DOK");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=8)]
        public System.Nullable<System.DateTime> TGL_DOK {
            get {
                return this.tGL_DOKField;
            }
            set {
                this.tGL_DOKField = value;
                this.RaisePropertyChanged("TGL_DOK");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TGL_DOKSpecified {
            get {
                return this.tGL_DOKFieldSpecified;
            }
            set {
                this.tGL_DOKFieldSpecified = value;
                this.RaisePropertyChanged("TGL_DOKSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=9)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=10)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=11)]
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_MON_GUNA_SATKER_A23")]
    public partial class OutputParameters : object, System.ComponentModel.INotifyPropertyChanged {
        
        private WASDALSROW_MON_GUNA_SATKER_A23[] sF_MON_GUNA_SATKER_A23Field;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("SF_MON_GUNA_SATKER_A23_ITEM")]
        public WASDALSROW_MON_GUNA_SATKER_A23[] SF_MON_GUNA_SATKER_A23 {
            get {
                return this.sF_MON_GUNA_SATKER_A23Field;
            }
            set {
                this.sF_MON_GUNA_SATKER_A23Field = value;
                this.RaisePropertyChanged("SF_MON_GUNA_SATKER_A23");
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
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_MON_GUNA_SATKER_A23", Order=0)]
        public AppPengguna.SvcMonSatkerGunaA23.InputParameters InputParameters;
        
        public executeRequest() {
        }
        
        public executeRequest(AppPengguna.SvcMonSatkerGunaA23.InputParameters InputParameters) {
            this.InputParameters = InputParameters;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class executeResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_MON_GUNA_SATKER_A23", Order=0)]
        public AppPengguna.SvcMonSatkerGunaA23.OutputParameters OutputParameters;
        
        public executeResponse() {
        }
        
        public executeResponse(AppPengguna.SvcMonSatkerGunaA23.OutputParameters OutputParameters) {
            this.OutputParameters = OutputParameters;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface execute_pttChannel : AppPengguna.SvcMonSatkerGunaA23.execute_ptt, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class executeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public executeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public AppPengguna.SvcMonSatkerGunaA23.OutputParameters Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((AppPengguna.SvcMonSatkerGunaA23.OutputParameters)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class execute_pttClient : System.ServiceModel.ClientBase<AppPengguna.SvcMonSatkerGunaA23.execute_ptt>, AppPengguna.SvcMonSatkerGunaA23.execute_ptt {
        
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
        AppPengguna.SvcMonSatkerGunaA23.executeResponse AppPengguna.SvcMonSatkerGunaA23.execute_ptt.execute(AppPengguna.SvcMonSatkerGunaA23.executeRequest request) {
            return base.Channel.execute(request);
        }
        
        public AppPengguna.SvcMonSatkerGunaA23.OutputParameters execute(AppPengguna.SvcMonSatkerGunaA23.InputParameters InputParameters) {
            AppPengguna.SvcMonSatkerGunaA23.executeRequest inValue = new AppPengguna.SvcMonSatkerGunaA23.executeRequest();
            inValue.InputParameters = InputParameters;
            AppPengguna.SvcMonSatkerGunaA23.executeResponse retVal = ((AppPengguna.SvcMonSatkerGunaA23.execute_ptt)(this)).execute(inValue);
            return retVal.OutputParameters;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult AppPengguna.SvcMonSatkerGunaA23.execute_ptt.Beginexecute(AppPengguna.SvcMonSatkerGunaA23.executeRequest request, System.AsyncCallback callback, object asyncState) {
            return base.Channel.Beginexecute(request, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult Beginexecute(AppPengguna.SvcMonSatkerGunaA23.InputParameters InputParameters, System.AsyncCallback callback, object asyncState) {
            AppPengguna.SvcMonSatkerGunaA23.executeRequest inValue = new AppPengguna.SvcMonSatkerGunaA23.executeRequest();
            inValue.InputParameters = InputParameters;
            return ((AppPengguna.SvcMonSatkerGunaA23.execute_ptt)(this)).Beginexecute(inValue, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        AppPengguna.SvcMonSatkerGunaA23.executeResponse AppPengguna.SvcMonSatkerGunaA23.execute_ptt.Endexecute(System.IAsyncResult result) {
            return base.Channel.Endexecute(result);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public AppPengguna.SvcMonSatkerGunaA23.OutputParameters Endexecute(System.IAsyncResult result) {
            AppPengguna.SvcMonSatkerGunaA23.executeResponse retVal = ((AppPengguna.SvcMonSatkerGunaA23.execute_ptt)(this)).Endexecute(result);
            return retVal.OutputParameters;
        }
        
        private System.IAsyncResult OnBeginexecute(object[] inValues, System.AsyncCallback callback, object asyncState) {
            AppPengguna.SvcMonSatkerGunaA23.InputParameters InputParameters = ((AppPengguna.SvcMonSatkerGunaA23.InputParameters)(inValues[0]));
            return this.Beginexecute(InputParameters, callback, asyncState);
        }
        
        private object[] OnEndexecute(System.IAsyncResult result) {
            AppPengguna.SvcMonSatkerGunaA23.OutputParameters retVal = this.Endexecute(result);
            return new object[] {
                    retVal};
        }
        
        private void OnexecuteCompleted(object state) {
            if ((this.executeCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.executeCompleted(this, new executeCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void executeAsync(AppPengguna.SvcMonSatkerGunaA23.InputParameters InputParameters) {
            this.executeAsync(InputParameters, null);
        }
        
        public void executeAsync(AppPengguna.SvcMonSatkerGunaA23.InputParameters InputParameters, object userState) {
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
