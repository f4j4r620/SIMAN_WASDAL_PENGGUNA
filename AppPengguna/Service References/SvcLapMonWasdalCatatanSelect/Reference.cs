﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppPengguna.SvcLapMonWasdalCatatanSelect {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://oracle.com/sca/soapservice/siman_wasdal/monLapWasdal/selectCatatan", ConfigurationName="SvcLapMonWasdalCatatanSelect.execute_ptt")]
    public interface execute_ptt {
        
        // CODEGEN: Generating message contract since the operation execute is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="execute", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        AppPengguna.SvcLapMonWasdalCatatanSelect.executeResponse execute(AppPengguna.SvcLapMonWasdalCatatanSelect.executeRequest request);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="execute", ReplyAction="*")]
        System.IAsyncResult Beginexecute(AppPengguna.SvcLapMonWasdalCatatanSelect.executeRequest request, System.AsyncCallback callback, object asyncState);
        
        AppPengguna.SvcLapMonWasdalCatatanSelect.executeResponse Endexecute(System.IAsyncResult result);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_MON_LAP_WASDAL_CATATAN")]
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="WASDAL.SROW_MON_LAP_WASDAL_CATATAN", Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_MON_LAP_WASDAL_CATATAN")]
    public partial class WASDALSROW_MON_LAP_WASDAL_CATATAN : object, System.ComponentModel.INotifyPropertyChanged {
        
        private System.Nullable<decimal> nUMField;
        
        private bool nUMFieldSpecified;
        
        private System.Nullable<decimal> iD_MON_LAP_CATField;
        
        private bool iD_MON_LAP_CATFieldSpecified;
        
        private string cATATANField;
        
        private System.Nullable<decimal> iD_MON_LAPField;
        
        private bool iD_MON_LAPFieldSpecified;
        
        private System.Nullable<decimal> iD_SATKERField;
        
        private bool iD_SATKERFieldSpecified;
        
        private string kD_SATKERField;
        
        private string uR_SATKERField;
        
        private System.Nullable<decimal> iD_KLField;
        
        private bool iD_KLFieldSpecified;
        
        private string kD_KLField;
        
        private string uR_KLField;
        
        private System.Nullable<decimal> iD_KPKNLField;
        
        private bool iD_KPKNLFieldSpecified;
        
        private string kD_KPKNLField;
        
        private string uR_KPKNLField;
        
        private System.Nullable<decimal> iD_KANWILField;
        
        private bool iD_KANWILFieldSpecified;
        
        private string kD_KANWILField;
        
        private string uR_KANWILField;
        
        private System.Nullable<System.DateTime> tGL_REKAMField;
        
        private bool tGL_REKAMFieldSpecified;
        
        private string tHN_ANGField;
        
        private string nO_SURATField;
        
        private System.Nullable<System.DateTime> tGL_SURATField;
        
        private bool tGL_SURATFieldSpecified;
        
        private string sTATUS_KIRIMField;
        
        private System.Nullable<System.DateTime> tGL_KIRIMField;
        
        private bool tGL_KIRIMFieldSpecified;
        
        private System.Nullable<System.DateTime> tGL_DITERIMAField;
        
        private bool tGL_DITERIMAFieldSpecified;
        
        private string iS_TEPAT_WAKTUField;
        
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
        public System.Nullable<decimal> ID_MON_LAP_CAT {
            get {
                return this.iD_MON_LAP_CATField;
            }
            set {
                this.iD_MON_LAP_CATField = value;
                this.RaisePropertyChanged("ID_MON_LAP_CAT");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ID_MON_LAP_CATSpecified {
            get {
                return this.iD_MON_LAP_CATFieldSpecified;
            }
            set {
                this.iD_MON_LAP_CATFieldSpecified = value;
                this.RaisePropertyChanged("ID_MON_LAP_CATSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=2)]
        public string CATATAN {
            get {
                return this.cATATANField;
            }
            set {
                this.cATATANField = value;
                this.RaisePropertyChanged("CATATAN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=3)]
        public System.Nullable<decimal> ID_MON_LAP {
            get {
                return this.iD_MON_LAPField;
            }
            set {
                this.iD_MON_LAPField = value;
                this.RaisePropertyChanged("ID_MON_LAP");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ID_MON_LAPSpecified {
            get {
                return this.iD_MON_LAPFieldSpecified;
            }
            set {
                this.iD_MON_LAPFieldSpecified = value;
                this.RaisePropertyChanged("ID_MON_LAPSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=4)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=5)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=6)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=7)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=8)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=9)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=10)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=11)]
        public string KD_KPKNL {
            get {
                return this.kD_KPKNLField;
            }
            set {
                this.kD_KPKNLField = value;
                this.RaisePropertyChanged("KD_KPKNL");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=12)]
        public string UR_KPKNL {
            get {
                return this.uR_KPKNLField;
            }
            set {
                this.uR_KPKNLField = value;
                this.RaisePropertyChanged("UR_KPKNL");
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
        public string KD_KANWIL {
            get {
                return this.kD_KANWILField;
            }
            set {
                this.kD_KANWILField = value;
                this.RaisePropertyChanged("KD_KANWIL");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=15)]
        public string UR_KANWIL {
            get {
                return this.uR_KANWILField;
            }
            set {
                this.uR_KANWILField = value;
                this.RaisePropertyChanged("UR_KANWIL");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=16)]
        public System.Nullable<System.DateTime> TGL_REKAM {
            get {
                return this.tGL_REKAMField;
            }
            set {
                this.tGL_REKAMField = value;
                this.RaisePropertyChanged("TGL_REKAM");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TGL_REKAMSpecified {
            get {
                return this.tGL_REKAMFieldSpecified;
            }
            set {
                this.tGL_REKAMFieldSpecified = value;
                this.RaisePropertyChanged("TGL_REKAMSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=17)]
        public string THN_ANG {
            get {
                return this.tHN_ANGField;
            }
            set {
                this.tHN_ANGField = value;
                this.RaisePropertyChanged("THN_ANG");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=18)]
        public string NO_SURAT {
            get {
                return this.nO_SURATField;
            }
            set {
                this.nO_SURATField = value;
                this.RaisePropertyChanged("NO_SURAT");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=19)]
        public System.Nullable<System.DateTime> TGL_SURAT {
            get {
                return this.tGL_SURATField;
            }
            set {
                this.tGL_SURATField = value;
                this.RaisePropertyChanged("TGL_SURAT");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TGL_SURATSpecified {
            get {
                return this.tGL_SURATFieldSpecified;
            }
            set {
                this.tGL_SURATFieldSpecified = value;
                this.RaisePropertyChanged("TGL_SURATSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=20)]
        public string STATUS_KIRIM {
            get {
                return this.sTATUS_KIRIMField;
            }
            set {
                this.sTATUS_KIRIMField = value;
                this.RaisePropertyChanged("STATUS_KIRIM");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=21)]
        public System.Nullable<System.DateTime> TGL_KIRIM {
            get {
                return this.tGL_KIRIMField;
            }
            set {
                this.tGL_KIRIMField = value;
                this.RaisePropertyChanged("TGL_KIRIM");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TGL_KIRIMSpecified {
            get {
                return this.tGL_KIRIMFieldSpecified;
            }
            set {
                this.tGL_KIRIMFieldSpecified = value;
                this.RaisePropertyChanged("TGL_KIRIMSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=22)]
        public System.Nullable<System.DateTime> TGL_DITERIMA {
            get {
                return this.tGL_DITERIMAField;
            }
            set {
                this.tGL_DITERIMAField = value;
                this.RaisePropertyChanged("TGL_DITERIMA");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TGL_DITERIMASpecified {
            get {
                return this.tGL_DITERIMAFieldSpecified;
            }
            set {
                this.tGL_DITERIMAFieldSpecified = value;
                this.RaisePropertyChanged("TGL_DITERIMASpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=23)]
        public string IS_TEPAT_WAKTU {
            get {
                return this.iS_TEPAT_WAKTUField;
            }
            set {
                this.iS_TEPAT_WAKTUField = value;
                this.RaisePropertyChanged("IS_TEPAT_WAKTU");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=24)]
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_MON_LAP_WASDAL_CATATAN")]
    public partial class OutputParameters : object, System.ComponentModel.INotifyPropertyChanged {
        
        private WASDALSROW_MON_LAP_WASDAL_CATATAN[] sF_MON_LAP_WASDAL_CATATANField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("SF_MON_LAP_WASDAL_CATATAN_ITEM")]
        public WASDALSROW_MON_LAP_WASDAL_CATATAN[] SF_MON_LAP_WASDAL_CATATAN {
            get {
                return this.sF_MON_LAP_WASDAL_CATATANField;
            }
            set {
                this.sF_MON_LAP_WASDAL_CATATANField = value;
                this.RaisePropertyChanged("SF_MON_LAP_WASDAL_CATATAN");
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
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_MON_LAP_WASDAL_CATATAN", Order=0)]
        public AppPengguna.SvcLapMonWasdalCatatanSelect.InputParameters InputParameters;
        
        public executeRequest() {
        }
        
        public executeRequest(AppPengguna.SvcLapMonWasdalCatatanSelect.InputParameters InputParameters) {
            this.InputParameters = InputParameters;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class executeResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_MON_LAP_WASDAL_CATATAN", Order=0)]
        public AppPengguna.SvcLapMonWasdalCatatanSelect.OutputParameters OutputParameters;
        
        public executeResponse() {
        }
        
        public executeResponse(AppPengguna.SvcLapMonWasdalCatatanSelect.OutputParameters OutputParameters) {
            this.OutputParameters = OutputParameters;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface execute_pttChannel : AppPengguna.SvcLapMonWasdalCatatanSelect.execute_ptt, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class executeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public executeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public AppPengguna.SvcLapMonWasdalCatatanSelect.OutputParameters Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((AppPengguna.SvcLapMonWasdalCatatanSelect.OutputParameters)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class execute_pttClient : System.ServiceModel.ClientBase<AppPengguna.SvcLapMonWasdalCatatanSelect.execute_ptt>, AppPengguna.SvcLapMonWasdalCatatanSelect.execute_ptt {
        
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
        AppPengguna.SvcLapMonWasdalCatatanSelect.executeResponse AppPengguna.SvcLapMonWasdalCatatanSelect.execute_ptt.execute(AppPengguna.SvcLapMonWasdalCatatanSelect.executeRequest request) {
            return base.Channel.execute(request);
        }
        
        public AppPengguna.SvcLapMonWasdalCatatanSelect.OutputParameters execute(AppPengguna.SvcLapMonWasdalCatatanSelect.InputParameters InputParameters) {
            AppPengguna.SvcLapMonWasdalCatatanSelect.executeRequest inValue = new AppPengguna.SvcLapMonWasdalCatatanSelect.executeRequest();
            inValue.InputParameters = InputParameters;
            AppPengguna.SvcLapMonWasdalCatatanSelect.executeResponse retVal = ((AppPengguna.SvcLapMonWasdalCatatanSelect.execute_ptt)(this)).execute(inValue);
            return retVal.OutputParameters;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult AppPengguna.SvcLapMonWasdalCatatanSelect.execute_ptt.Beginexecute(AppPengguna.SvcLapMonWasdalCatatanSelect.executeRequest request, System.AsyncCallback callback, object asyncState) {
            return base.Channel.Beginexecute(request, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult Beginexecute(AppPengguna.SvcLapMonWasdalCatatanSelect.InputParameters InputParameters, System.AsyncCallback callback, object asyncState) {
            AppPengguna.SvcLapMonWasdalCatatanSelect.executeRequest inValue = new AppPengguna.SvcLapMonWasdalCatatanSelect.executeRequest();
            inValue.InputParameters = InputParameters;
            return ((AppPengguna.SvcLapMonWasdalCatatanSelect.execute_ptt)(this)).Beginexecute(inValue, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        AppPengguna.SvcLapMonWasdalCatatanSelect.executeResponse AppPengguna.SvcLapMonWasdalCatatanSelect.execute_ptt.Endexecute(System.IAsyncResult result) {
            return base.Channel.Endexecute(result);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public AppPengguna.SvcLapMonWasdalCatatanSelect.OutputParameters Endexecute(System.IAsyncResult result) {
            AppPengguna.SvcLapMonWasdalCatatanSelect.executeResponse retVal = ((AppPengguna.SvcLapMonWasdalCatatanSelect.execute_ptt)(this)).Endexecute(result);
            return retVal.OutputParameters;
        }
        
        private System.IAsyncResult OnBeginexecute(object[] inValues, System.AsyncCallback callback, object asyncState) {
            AppPengguna.SvcLapMonWasdalCatatanSelect.InputParameters InputParameters = ((AppPengguna.SvcLapMonWasdalCatatanSelect.InputParameters)(inValues[0]));
            return this.Beginexecute(InputParameters, callback, asyncState);
        }
        
        private object[] OnEndexecute(System.IAsyncResult result) {
            AppPengguna.SvcLapMonWasdalCatatanSelect.OutputParameters retVal = this.Endexecute(result);
            return new object[] {
                    retVal};
        }
        
        private void OnexecuteCompleted(object state) {
            if ((this.executeCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.executeCompleted(this, new executeCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void executeAsync(AppPengguna.SvcLapMonWasdalCatatanSelect.InputParameters InputParameters) {
            this.executeAsync(InputParameters, null);
        }
        
        public void executeAsync(AppPengguna.SvcLapMonWasdalCatatanSelect.InputParameters InputParameters, object userState) {
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
