﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppPengguna.SvcLapPnbpBlmRekamSiman {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://oracle.com/sca/soapservice/siman_wasdal/wasSpan2/lapPnbpBlmRekamSiman", ConfigurationName="SvcLapPnbpBlmRekamSiman.execute_ptt")]
    public interface execute_ptt {
        
        // CODEGEN: Generating message contract since the operation execute is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="execute", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        AppPengguna.SvcLapPnbpBlmRekamSiman.executeResponse execute(AppPengguna.SvcLapPnbpBlmRekamSiman.executeRequest request);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="execute", ReplyAction="*")]
        System.IAsyncResult Beginexecute(AppPengguna.SvcLapPnbpBlmRekamSiman.executeRequest request, System.AsyncCallback callback, object asyncState);
        
        AppPengguna.SvcLapPnbpBlmRekamSiman.executeResponse Endexecute(System.IAsyncResult result);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2102.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_LAP_SPAN_BLMREKAM_SIMAN")]
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
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="WASDAL.SROW_LAP_SPAN_BLMREKAM_SIMAN", Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_LAP_SPAN_BLMREKAM_SIMAN")]
    public partial class WASDALSROW_LAP_SPAN_BLMREKAM_SIMAN : object, System.ComponentModel.INotifyPropertyChanged {
        
        private System.Nullable<decimal> nUMField;
        
        private bool nUMFieldSpecified;
        
        private string tAHUNField;
        
        private string nTPNField;
        
        private string nTBField;
        
        private string kD_BILLINGField;
        
        private string pERIODEField;
        
        private System.Nullable<System.DateTime> tANGGALField;
        
        private bool tANGGALFieldSpecified;
        
        private string kD_SATKER_SPANField;
        
        private System.Nullable<decimal> iD_ASETField;
        
        private bool iD_ASETFieldSpecified;
        
        private string kD_BRGField;
        
        private string uR_SSKELField;
        
        private System.Nullable<decimal> nUPField;
        
        private bool nUPFieldSpecified;
        
        private System.Nullable<decimal> nIL_PNBP_SIMANField;
        
        private bool nIL_PNBP_SIMANFieldSpecified;
        
        private System.Nullable<decimal> iD_SATKERField;
        
        private bool iD_SATKERFieldSpecified;
        
        private string kD_SATKERField;
        
        private string uR_SATKERField;
        
        private string kD_AKUNField;
        
        private string sTATUSField;
        
        private string jNS_PENGELOLAANField;
        
        private string sK_KEPUTUSANField;
        
        private System.Nullable<System.DateTime> tGL_SKField;
        
        private bool tGL_SKFieldSpecified;
        
        private string uR_PENDAPATANField;
        
        private string nM_PENYETORField;
        
        private System.Nullable<decimal> iD_KORWILField;
        
        private bool iD_KORWILFieldSpecified;
        
        private System.Nullable<decimal> iD_ESELON1Field;
        
        private bool iD_ESELON1FieldSpecified;
        
        private System.Nullable<decimal> iD_KLField;
        
        private bool iD_KLFieldSpecified;
        
        private System.Nullable<decimal> iD_KPKNLField;
        
        private bool iD_KPKNLFieldSpecified;
        
        private System.Nullable<decimal> iD_KANWILField;
        
        private bool iD_KANWILFieldSpecified;
        
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
        public string TAHUN {
            get {
                return this.tAHUNField;
            }
            set {
                this.tAHUNField = value;
                this.RaisePropertyChanged("TAHUN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=2)]
        public string NTPN {
            get {
                return this.nTPNField;
            }
            set {
                this.nTPNField = value;
                this.RaisePropertyChanged("NTPN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=3)]
        public string NTB {
            get {
                return this.nTBField;
            }
            set {
                this.nTBField = value;
                this.RaisePropertyChanged("NTB");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=4)]
        public string KD_BILLING {
            get {
                return this.kD_BILLINGField;
            }
            set {
                this.kD_BILLINGField = value;
                this.RaisePropertyChanged("KD_BILLING");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=5)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=6)]
        public System.Nullable<System.DateTime> TANGGAL {
            get {
                return this.tANGGALField;
            }
            set {
                this.tANGGALField = value;
                this.RaisePropertyChanged("TANGGAL");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TANGGALSpecified {
            get {
                return this.tANGGALFieldSpecified;
            }
            set {
                this.tANGGALFieldSpecified = value;
                this.RaisePropertyChanged("TANGGALSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=7)]
        public string KD_SATKER_SPAN {
            get {
                return this.kD_SATKER_SPANField;
            }
            set {
                this.kD_SATKER_SPANField = value;
                this.RaisePropertyChanged("KD_SATKER_SPAN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=8)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=9)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=10)]
        public string UR_SSKEL {
            get {
                return this.uR_SSKELField;
            }
            set {
                this.uR_SSKELField = value;
                this.RaisePropertyChanged("UR_SSKEL");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=11)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=12)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=13)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=14)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=15)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=16)]
        public string KD_AKUN {
            get {
                return this.kD_AKUNField;
            }
            set {
                this.kD_AKUNField = value;
                this.RaisePropertyChanged("KD_AKUN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=17)]
        public string STATUS {
            get {
                return this.sTATUSField;
            }
            set {
                this.sTATUSField = value;
                this.RaisePropertyChanged("STATUS");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=18)]
        public string JNS_PENGELOLAAN {
            get {
                return this.jNS_PENGELOLAANField;
            }
            set {
                this.jNS_PENGELOLAANField = value;
                this.RaisePropertyChanged("JNS_PENGELOLAAN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=19)]
        public string SK_KEPUTUSAN {
            get {
                return this.sK_KEPUTUSANField;
            }
            set {
                this.sK_KEPUTUSANField = value;
                this.RaisePropertyChanged("SK_KEPUTUSAN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=20)]
        public System.Nullable<System.DateTime> TGL_SK {
            get {
                return this.tGL_SKField;
            }
            set {
                this.tGL_SKField = value;
                this.RaisePropertyChanged("TGL_SK");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TGL_SKSpecified {
            get {
                return this.tGL_SKFieldSpecified;
            }
            set {
                this.tGL_SKFieldSpecified = value;
                this.RaisePropertyChanged("TGL_SKSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=21)]
        public string UR_PENDAPATAN {
            get {
                return this.uR_PENDAPATANField;
            }
            set {
                this.uR_PENDAPATANField = value;
                this.RaisePropertyChanged("UR_PENDAPATAN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=22)]
        public string NM_PENYETOR {
            get {
                return this.nM_PENYETORField;
            }
            set {
                this.nM_PENYETORField = value;
                this.RaisePropertyChanged("NM_PENYETOR");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=23)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=24)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=25)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=26)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=27)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=28)]
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_LAP_SPAN_BLMREKAM_SIMAN")]
    public partial class OutputParameters : object, System.ComponentModel.INotifyPropertyChanged {
        
        private WASDALSROW_LAP_SPAN_BLMREKAM_SIMAN[] sF_LAP_SPAN_BLMREKAM_SIMANField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("SF_LAP_SPAN_BLMREKAM_SIMAN_ITEM")]
        public WASDALSROW_LAP_SPAN_BLMREKAM_SIMAN[] SF_LAP_SPAN_BLMREKAM_SIMAN {
            get {
                return this.sF_LAP_SPAN_BLMREKAM_SIMANField;
            }
            set {
                this.sF_LAP_SPAN_BLMREKAM_SIMANField = value;
                this.RaisePropertyChanged("SF_LAP_SPAN_BLMREKAM_SIMAN");
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
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_LAP_SPAN_BLMREKAM_SIMAN", Order=0)]
        public AppPengguna.SvcLapPnbpBlmRekamSiman.InputParameters InputParameters;
        
        public executeRequest() {
        }
        
        public executeRequest(AppPengguna.SvcLapPnbpBlmRekamSiman.InputParameters InputParameters) {
            this.InputParameters = InputParameters;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class executeResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_LAP_SPAN_BLMREKAM_SIMAN", Order=0)]
        public AppPengguna.SvcLapPnbpBlmRekamSiman.OutputParameters OutputParameters;
        
        public executeResponse() {
        }
        
        public executeResponse(AppPengguna.SvcLapPnbpBlmRekamSiman.OutputParameters OutputParameters) {
            this.OutputParameters = OutputParameters;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface execute_pttChannel : AppPengguna.SvcLapPnbpBlmRekamSiman.execute_ptt, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class executeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public executeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public AppPengguna.SvcLapPnbpBlmRekamSiman.OutputParameters Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((AppPengguna.SvcLapPnbpBlmRekamSiman.OutputParameters)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class execute_pttClient : System.ServiceModel.ClientBase<AppPengguna.SvcLapPnbpBlmRekamSiman.execute_ptt>, AppPengguna.SvcLapPnbpBlmRekamSiman.execute_ptt {
        
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
        AppPengguna.SvcLapPnbpBlmRekamSiman.executeResponse AppPengguna.SvcLapPnbpBlmRekamSiman.execute_ptt.execute(AppPengguna.SvcLapPnbpBlmRekamSiman.executeRequest request) {
            return base.Channel.execute(request);
        }
        
        public AppPengguna.SvcLapPnbpBlmRekamSiman.OutputParameters execute(AppPengguna.SvcLapPnbpBlmRekamSiman.InputParameters InputParameters) {
            AppPengguna.SvcLapPnbpBlmRekamSiman.executeRequest inValue = new AppPengguna.SvcLapPnbpBlmRekamSiman.executeRequest();
            inValue.InputParameters = InputParameters;
            AppPengguna.SvcLapPnbpBlmRekamSiman.executeResponse retVal = ((AppPengguna.SvcLapPnbpBlmRekamSiman.execute_ptt)(this)).execute(inValue);
            return retVal.OutputParameters;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult AppPengguna.SvcLapPnbpBlmRekamSiman.execute_ptt.Beginexecute(AppPengguna.SvcLapPnbpBlmRekamSiman.executeRequest request, System.AsyncCallback callback, object asyncState) {
            return base.Channel.Beginexecute(request, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult Beginexecute(AppPengguna.SvcLapPnbpBlmRekamSiman.InputParameters InputParameters, System.AsyncCallback callback, object asyncState) {
            AppPengguna.SvcLapPnbpBlmRekamSiman.executeRequest inValue = new AppPengguna.SvcLapPnbpBlmRekamSiman.executeRequest();
            inValue.InputParameters = InputParameters;
            return ((AppPengguna.SvcLapPnbpBlmRekamSiman.execute_ptt)(this)).Beginexecute(inValue, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        AppPengguna.SvcLapPnbpBlmRekamSiman.executeResponse AppPengguna.SvcLapPnbpBlmRekamSiman.execute_ptt.Endexecute(System.IAsyncResult result) {
            return base.Channel.Endexecute(result);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public AppPengguna.SvcLapPnbpBlmRekamSiman.OutputParameters Endexecute(System.IAsyncResult result) {
            AppPengguna.SvcLapPnbpBlmRekamSiman.executeResponse retVal = ((AppPengguna.SvcLapPnbpBlmRekamSiman.execute_ptt)(this)).Endexecute(result);
            return retVal.OutputParameters;
        }
        
        private System.IAsyncResult OnBeginexecute(object[] inValues, System.AsyncCallback callback, object asyncState) {
            AppPengguna.SvcLapPnbpBlmRekamSiman.InputParameters InputParameters = ((AppPengguna.SvcLapPnbpBlmRekamSiman.InputParameters)(inValues[0]));
            return this.Beginexecute(InputParameters, callback, asyncState);
        }
        
        private object[] OnEndexecute(System.IAsyncResult result) {
            AppPengguna.SvcLapPnbpBlmRekamSiman.OutputParameters retVal = this.Endexecute(result);
            return new object[] {
                    retVal};
        }
        
        private void OnexecuteCompleted(object state) {
            if ((this.executeCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.executeCompleted(this, new executeCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void executeAsync(AppPengguna.SvcLapPnbpBlmRekamSiman.InputParameters InputParameters) {
            this.executeAsync(InputParameters, null);
        }
        
        public void executeAsync(AppPengguna.SvcLapPnbpBlmRekamSiman.InputParameters InputParameters, object userState) {
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
