﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppPengguna.SvcKelPilihAsetSum {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://oracle.com/sca/soapservice/siman_pengelolaan/PengelolaStatGuna/wewenangPen" +
        "gajuan", ConfigurationName="SvcKelPilihAsetSum.wewenangPengajuan_ptt")]
    public interface wewenangPengajuan_ptt {
        
        // CODEGEN: Generating message contract since the operation execute is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="execute", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        AppPengguna.SvcKelPilihAsetSum.executeResponse execute(AppPengguna.SvcKelPilihAsetSum.executeRequest request);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="execute", ReplyAction="*")]
        System.IAsyncResult Beginexecute(AppPengguna.SvcKelPilihAsetSum.executeRequest request, System.AsyncCallback callback, object asyncState);
        
        AppPengguna.SvcKelPilihAsetSum.executeResponse Endexecute(System.IAsyncResult result);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2102.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SP_TIKET_KELOLA_PERIKSA")]
    public partial class InputParameters : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string p_KODE_PELAYANANField;
        
        private string p_IS_TBField;
        
        private System.Nullable<decimal> p_ID_SATKERField;
        
        private bool p_ID_SATKERFieldSpecified;
        
        private System.Nullable<decimal> p_ID_KLField;
        
        private bool p_ID_KLFieldSpecified;
        
        private string p_NO_TIKET_KELOLAField;
        
        private string p_LEVEL_AKTIFField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=0)]
        public string P_KODE_PELAYANAN {
            get {
                return this.p_KODE_PELAYANANField;
            }
            set {
                this.p_KODE_PELAYANANField = value;
                this.RaisePropertyChanged("P_KODE_PELAYANAN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=1)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=2)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=3)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=4)]
        public string P_NO_TIKET_KELOLA {
            get {
                return this.p_NO_TIKET_KELOLAField;
            }
            set {
                this.p_NO_TIKET_KELOLAField = value;
                this.RaisePropertyChanged("P_NO_TIKET_KELOLA");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=5)]
        public string P_LEVEL_AKTIF {
            get {
                return this.p_LEVEL_AKTIFField;
            }
            set {
                this.p_LEVEL_AKTIFField = value;
                this.RaisePropertyChanged("P_LEVEL_AKTIF");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SP_TIKET_KELOLA_PERIKSA")]
    public partial class OutputParameters : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string pO_KET_PEMOHONField;
        
        private string pO_KET_PENGELOLAField;
        
        private string pO_TUJUAN_TIKETField;
        
        private System.Nullable<decimal> pO_NILAI_TOTAL_BUKUField;
        
        private bool pO_NILAI_TOTAL_BUKUFieldSpecified;
        
        private string pO_RESULTField;
        
        private string pO_RESULT_MESSAGEField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=0)]
        public string PO_KET_PEMOHON {
            get {
                return this.pO_KET_PEMOHONField;
            }
            set {
                this.pO_KET_PEMOHONField = value;
                this.RaisePropertyChanged("PO_KET_PEMOHON");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=1)]
        public string PO_KET_PENGELOLA {
            get {
                return this.pO_KET_PENGELOLAField;
            }
            set {
                this.pO_KET_PENGELOLAField = value;
                this.RaisePropertyChanged("PO_KET_PENGELOLA");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=2)]
        public string PO_TUJUAN_TIKET {
            get {
                return this.pO_TUJUAN_TIKETField;
            }
            set {
                this.pO_TUJUAN_TIKETField = value;
                this.RaisePropertyChanged("PO_TUJUAN_TIKET");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=3)]
        public System.Nullable<decimal> PO_NILAI_TOTAL_BUKU {
            get {
                return this.pO_NILAI_TOTAL_BUKUField;
            }
            set {
                this.pO_NILAI_TOTAL_BUKUField = value;
                this.RaisePropertyChanged("PO_NILAI_TOTAL_BUKU");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PO_NILAI_TOTAL_BUKUSpecified {
            get {
                return this.pO_NILAI_TOTAL_BUKUFieldSpecified;
            }
            set {
                this.pO_NILAI_TOTAL_BUKUFieldSpecified = value;
                this.RaisePropertyChanged("PO_NILAI_TOTAL_BUKUSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=4)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=5)]
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
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SP_TIKET_KELOLA_PERIKSA", Order=0)]
        public AppPengguna.SvcKelPilihAsetSum.InputParameters InputParameters;
        
        public executeRequest() {
        }
        
        public executeRequest(AppPengguna.SvcKelPilihAsetSum.InputParameters InputParameters) {
            this.InputParameters = InputParameters;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class executeResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SP_TIKET_KELOLA_PERIKSA", Order=0)]
        public AppPengguna.SvcKelPilihAsetSum.OutputParameters OutputParameters;
        
        public executeResponse() {
        }
        
        public executeResponse(AppPengguna.SvcKelPilihAsetSum.OutputParameters OutputParameters) {
            this.OutputParameters = OutputParameters;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface wewenangPengajuan_pttChannel : AppPengguna.SvcKelPilihAsetSum.wewenangPengajuan_ptt, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class executeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public executeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public AppPengguna.SvcKelPilihAsetSum.OutputParameters Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((AppPengguna.SvcKelPilihAsetSum.OutputParameters)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class wewenangPengajuan_pttClient : System.ServiceModel.ClientBase<AppPengguna.SvcKelPilihAsetSum.wewenangPengajuan_ptt>, AppPengguna.SvcKelPilihAsetSum.wewenangPengajuan_ptt {
        
        private BeginOperationDelegate onBeginexecuteDelegate;
        
        private EndOperationDelegate onEndexecuteDelegate;
        
        private System.Threading.SendOrPostCallback onexecuteCompletedDelegate;
        
        public wewenangPengajuan_pttClient() {
        }
        
        public wewenangPengajuan_pttClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public wewenangPengajuan_pttClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public wewenangPengajuan_pttClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public wewenangPengajuan_pttClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public event System.EventHandler<executeCompletedEventArgs> executeCompleted;
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        AppPengguna.SvcKelPilihAsetSum.executeResponse AppPengguna.SvcKelPilihAsetSum.wewenangPengajuan_ptt.execute(AppPengguna.SvcKelPilihAsetSum.executeRequest request) {
            return base.Channel.execute(request);
        }
        
        public AppPengguna.SvcKelPilihAsetSum.OutputParameters execute(AppPengguna.SvcKelPilihAsetSum.InputParameters InputParameters) {
            AppPengguna.SvcKelPilihAsetSum.executeRequest inValue = new AppPengguna.SvcKelPilihAsetSum.executeRequest();
            inValue.InputParameters = InputParameters;
            AppPengguna.SvcKelPilihAsetSum.executeResponse retVal = ((AppPengguna.SvcKelPilihAsetSum.wewenangPengajuan_ptt)(this)).execute(inValue);
            return retVal.OutputParameters;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult AppPengguna.SvcKelPilihAsetSum.wewenangPengajuan_ptt.Beginexecute(AppPengguna.SvcKelPilihAsetSum.executeRequest request, System.AsyncCallback callback, object asyncState) {
            return base.Channel.Beginexecute(request, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult Beginexecute(AppPengguna.SvcKelPilihAsetSum.InputParameters InputParameters, System.AsyncCallback callback, object asyncState) {
            AppPengguna.SvcKelPilihAsetSum.executeRequest inValue = new AppPengguna.SvcKelPilihAsetSum.executeRequest();
            inValue.InputParameters = InputParameters;
            return ((AppPengguna.SvcKelPilihAsetSum.wewenangPengajuan_ptt)(this)).Beginexecute(inValue, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        AppPengguna.SvcKelPilihAsetSum.executeResponse AppPengguna.SvcKelPilihAsetSum.wewenangPengajuan_ptt.Endexecute(System.IAsyncResult result) {
            return base.Channel.Endexecute(result);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public AppPengguna.SvcKelPilihAsetSum.OutputParameters Endexecute(System.IAsyncResult result) {
            AppPengguna.SvcKelPilihAsetSum.executeResponse retVal = ((AppPengguna.SvcKelPilihAsetSum.wewenangPengajuan_ptt)(this)).Endexecute(result);
            return retVal.OutputParameters;
        }
        
        private System.IAsyncResult OnBeginexecute(object[] inValues, System.AsyncCallback callback, object asyncState) {
            AppPengguna.SvcKelPilihAsetSum.InputParameters InputParameters = ((AppPengguna.SvcKelPilihAsetSum.InputParameters)(inValues[0]));
            return this.Beginexecute(InputParameters, callback, asyncState);
        }
        
        private object[] OnEndexecute(System.IAsyncResult result) {
            AppPengguna.SvcKelPilihAsetSum.OutputParameters retVal = this.Endexecute(result);
            return new object[] {
                    retVal};
        }
        
        private void OnexecuteCompleted(object state) {
            if ((this.executeCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.executeCompleted(this, new executeCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void executeAsync(AppPengguna.SvcKelPilihAsetSum.InputParameters InputParameters) {
            this.executeAsync(InputParameters, null);
        }
        
        public void executeAsync(AppPengguna.SvcKelPilihAsetSum.InputParameters InputParameters, object userState) {
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
