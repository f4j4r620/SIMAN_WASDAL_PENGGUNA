﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppPengguna.SvcGridPnbpRekap {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://oracle.com/sca/soapservice/siman_wasdal/wasSpan2/gridPnbpRekap", ConfigurationName="SvcGridPnbpRekap.execute_ptt")]
    public interface execute_ptt {
        
        // CODEGEN: Generating message contract since the operation execute is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="execute", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        AppPengguna.SvcGridPnbpRekap.executeResponse execute(AppPengguna.SvcGridPnbpRekap.executeRequest request);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="execute", ReplyAction="*")]
        System.IAsyncResult Beginexecute(AppPengguna.SvcGridPnbpRekap.executeRequest request, System.AsyncCallback callback, object asyncState);
        
        AppPengguna.SvcGridPnbpRekap.executeResponse Endexecute(System.IAsyncResult result);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2102.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_GRID_REKAP_PNBP_SPAN")]
    public partial class InputParameters : object, System.ComponentModel.INotifyPropertyChanged {
        
        private System.Nullable<decimal> p_MINField;
        
        private bool p_MINFieldSpecified;
        
        private System.Nullable<decimal> p_MAXField;
        
        private bool p_MAXFieldSpecified;
        
        private string p_COUNTField;
        
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
        public string P_COUNT {
            get {
                return this.p_COUNTField;
            }
            set {
                this.p_COUNTField = value;
                this.RaisePropertyChanged("P_COUNT");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2102.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="WASDAL.SROW_GRID_REKAP_PNBP_SPAN", Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_GRID_REKAP_PNBP_SPAN")]
    public partial class WASDALSROW_GRID_REKAP_PNBP_SPAN : object, System.ComponentModel.INotifyPropertyChanged {
        
        private System.Nullable<decimal> nUMField;
        
        private bool nUMFieldSpecified;
        
        private string pERIODEField;
        
        private System.Nullable<decimal> iD_SATKERField;
        
        private bool iD_SATKERFieldSpecified;
        
        private string kD_SATKERField;
        
        private string uR_SATKERField;
        
        private System.Nullable<decimal> nIL_PNBP_SIMANField;
        
        private bool nIL_PNBP_SIMANFieldSpecified;
        
        private System.Nullable<decimal> nIL_PNBP_SPANField;
        
        private bool nIL_PNBP_SPANFieldSpecified;
        
        private System.Nullable<decimal> sELISIHField;
        
        private bool sELISIHFieldSpecified;
        
        private string kETField;
        
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
        public string PERIODE {
            get {
                return this.pERIODEField;
            }
            set {
                this.pERIODEField = value;
                this.RaisePropertyChanged("PERIODE");
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
        public System.Nullable<decimal> NIL_PNBP_SIMAN {
            get {
                return this.nIL_PNBP_SIMANField;
            }
            set {
                this.nIL_PNBP_SIMANField = value;
                this.RaisePropertyChanged("NIL_PNBP_SIMAN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NIL_PNBP_SIMANSpecified {
            get {
                return this.nIL_PNBP_SIMANFieldSpecified;
            }
            set {
                this.nIL_PNBP_SIMANFieldSpecified = value;
                this.RaisePropertyChanged("NIL_PNBP_SIMANSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=6)]
        public System.Nullable<decimal> NIL_PNBP_SPAN {
            get {
                return this.nIL_PNBP_SPANField;
            }
            set {
                this.nIL_PNBP_SPANField = value;
                this.RaisePropertyChanged("NIL_PNBP_SPAN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NIL_PNBP_SPANSpecified {
            get {
                return this.nIL_PNBP_SPANFieldSpecified;
            }
            set {
                this.nIL_PNBP_SPANFieldSpecified = value;
                this.RaisePropertyChanged("NIL_PNBP_SPANSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=7)]
        public System.Nullable<decimal> SELISIH {
            get {
                return this.sELISIHField;
            }
            set {
                this.sELISIHField = value;
                this.RaisePropertyChanged("SELISIH");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool SELISIHSpecified {
            get {
                return this.sELISIHFieldSpecified;
            }
            set {
                this.sELISIHFieldSpecified = value;
                this.RaisePropertyChanged("SELISIHSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=8)]
        public string KET {
            get {
                return this.kETField;
            }
            set {
                this.kETField = value;
                this.RaisePropertyChanged("KET");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=9)]
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2102.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_GRID_REKAP_PNBP_SPAN")]
    public partial class OutputParameters : object, System.ComponentModel.INotifyPropertyChanged {
        
        private WASDALSROW_GRID_REKAP_PNBP_SPAN[] sF_GRID_REKAP_PNBP_SPANField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("SF_GRID_REKAP_PNBP_SPAN_ITEM")]
        public WASDALSROW_GRID_REKAP_PNBP_SPAN[] SF_GRID_REKAP_PNBP_SPAN {
            get {
                return this.sF_GRID_REKAP_PNBP_SPANField;
            }
            set {
                this.sF_GRID_REKAP_PNBP_SPANField = value;
                this.RaisePropertyChanged("SF_GRID_REKAP_PNBP_SPAN");
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
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_GRID_REKAP_PNBP_SPAN", Order=0)]
        public AppPengguna.SvcGridPnbpRekap.InputParameters InputParameters;
        
        public executeRequest() {
        }
        
        public executeRequest(AppPengguna.SvcGridPnbpRekap.InputParameters InputParameters) {
            this.InputParameters = InputParameters;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class executeResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_GRID_REKAP_PNBP_SPAN", Order=0)]
        public AppPengguna.SvcGridPnbpRekap.OutputParameters OutputParameters;
        
        public executeResponse() {
        }
        
        public executeResponse(AppPengguna.SvcGridPnbpRekap.OutputParameters OutputParameters) {
            this.OutputParameters = OutputParameters;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface execute_pttChannel : AppPengguna.SvcGridPnbpRekap.execute_ptt, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class executeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public executeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public AppPengguna.SvcGridPnbpRekap.OutputParameters Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((AppPengguna.SvcGridPnbpRekap.OutputParameters)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class execute_pttClient : System.ServiceModel.ClientBase<AppPengguna.SvcGridPnbpRekap.execute_ptt>, AppPengguna.SvcGridPnbpRekap.execute_ptt {
        
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
        AppPengguna.SvcGridPnbpRekap.executeResponse AppPengguna.SvcGridPnbpRekap.execute_ptt.execute(AppPengguna.SvcGridPnbpRekap.executeRequest request) {
            return base.Channel.execute(request);
        }
        
        public AppPengguna.SvcGridPnbpRekap.OutputParameters execute(AppPengguna.SvcGridPnbpRekap.InputParameters InputParameters) {
            AppPengguna.SvcGridPnbpRekap.executeRequest inValue = new AppPengguna.SvcGridPnbpRekap.executeRequest();
            inValue.InputParameters = InputParameters;
            AppPengguna.SvcGridPnbpRekap.executeResponse retVal = ((AppPengguna.SvcGridPnbpRekap.execute_ptt)(this)).execute(inValue);
            return retVal.OutputParameters;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult AppPengguna.SvcGridPnbpRekap.execute_ptt.Beginexecute(AppPengguna.SvcGridPnbpRekap.executeRequest request, System.AsyncCallback callback, object asyncState) {
            return base.Channel.Beginexecute(request, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult Beginexecute(AppPengguna.SvcGridPnbpRekap.InputParameters InputParameters, System.AsyncCallback callback, object asyncState) {
            AppPengguna.SvcGridPnbpRekap.executeRequest inValue = new AppPengguna.SvcGridPnbpRekap.executeRequest();
            inValue.InputParameters = InputParameters;
            return ((AppPengguna.SvcGridPnbpRekap.execute_ptt)(this)).Beginexecute(inValue, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        AppPengguna.SvcGridPnbpRekap.executeResponse AppPengguna.SvcGridPnbpRekap.execute_ptt.Endexecute(System.IAsyncResult result) {
            return base.Channel.Endexecute(result);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public AppPengguna.SvcGridPnbpRekap.OutputParameters Endexecute(System.IAsyncResult result) {
            AppPengguna.SvcGridPnbpRekap.executeResponse retVal = ((AppPengguna.SvcGridPnbpRekap.execute_ptt)(this)).Endexecute(result);
            return retVal.OutputParameters;
        }
        
        private System.IAsyncResult OnBeginexecute(object[] inValues, System.AsyncCallback callback, object asyncState) {
            AppPengguna.SvcGridPnbpRekap.InputParameters InputParameters = ((AppPengguna.SvcGridPnbpRekap.InputParameters)(inValues[0]));
            return this.Beginexecute(InputParameters, callback, asyncState);
        }
        
        private object[] OnEndexecute(System.IAsyncResult result) {
            AppPengguna.SvcGridPnbpRekap.OutputParameters retVal = this.Endexecute(result);
            return new object[] {
                    retVal};
        }
        
        private void OnexecuteCompleted(object state) {
            if ((this.executeCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.executeCompleted(this, new executeCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void executeAsync(AppPengguna.SvcGridPnbpRekap.InputParameters InputParameters) {
            this.executeAsync(InputParameters, null);
        }
        
        public void executeAsync(AppPengguna.SvcGridPnbpRekap.InputParameters InputParameters, object userState) {
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
