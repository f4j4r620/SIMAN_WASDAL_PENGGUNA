﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppPengguna.SvcMonPropSusSelect {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://oracle.com/sca/soapservice/siman_wasdal/kprokPsp/monBMN2", ConfigurationName="SvcMonPropSusSelect.execute_ptt")]
    public interface execute_ptt {
        
        // CODEGEN: Generating message contract since the operation execute is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="execute", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        AppPengguna.SvcMonPropSusSelect.executeResponse execute(AppPengguna.SvcMonPropSusSelect.executeRequest request);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="execute", ReplyAction="*")]
        System.IAsyncResult Beginexecute(AppPengguna.SvcMonPropSusSelect.executeRequest request, System.AsyncCallback callback, object asyncState);
        
        AppPengguna.SvcMonPropSusSelect.executeResponse Endexecute(System.IAsyncResult result);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_MON_KPROK_PSP2")]
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
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="WASDAL.SROW_MON_BMN_PSP2", Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_MON_KPROK_PSP2")]
    public partial class WASDALSROW_MON_BMN_PSP2 : object, System.ComponentModel.INotifyPropertyChanged {
        
        private System.Nullable<decimal> nUMField;
        
        private bool nUMFieldSpecified;
        
        private System.Nullable<decimal> iD_BMNField;
        
        private bool iD_BMNFieldSpecified;
        
        private string nO_SURATField;
        
        private System.Nullable<decimal> iD_ASETField;
        
        private bool iD_ASETFieldSpecified;
        
        private string kD_BRGField;
        
        private string uR_SSKELField;
        
        private System.Nullable<decimal> nUPField;
        
        private bool nUPFieldSpecified;
        
        private string nOREGField;
        
        private string mERKField;
        
        private string aLAMATField;
        
        private System.Nullable<decimal> kUANTITASField;
        
        private bool kUANTITASFieldSpecified;
        
        private System.Nullable<decimal> nILAI_BUKUField;
        
        private bool nILAI_BUKUFieldSpecified;
        
        private System.Nullable<decimal> nILAI_BUKU_SBLM_SUSUTField;
        
        private bool nILAI_BUKU_SBLM_SUSUTFieldSpecified;
        
        private System.Nullable<decimal> nILAI_PEROLEHANField;
        
        private bool nILAI_PEROLEHANFieldSpecified;
        
        private System.Nullable<System.DateTime> tGL_PEROLEHANField;
        
        private bool tGL_PEROLEHANFieldSpecified;
        
        private System.Nullable<decimal> nILAI_PEROLEHAN_PERTAMAField;
        
        private bool nILAI_PEROLEHAN_PERTAMAFieldSpecified;
        
        private System.Nullable<decimal> iD_SATKERField;
        
        private bool iD_SATKERFieldSpecified;
        
        private string kD_SATKERField;
        
        private string uR_SATKERField;
        
        private System.Nullable<decimal> iD_KPKNLField;
        
        private bool iD_KPKNLFieldSpecified;
        
        private string kD_KPKNLField;
        
        private string uR_KPKNLField;
        
        private System.Nullable<decimal> iD_KANWILField;
        
        private bool iD_KANWILFieldSpecified;
        
        private string kD_KANWILField;
        
        private string uR_KANWILField;
        
        private System.Nullable<decimal> iD_KORWILField;
        
        private bool iD_KORWILFieldSpecified;
        
        private System.Nullable<decimal> iD_ESELON1Field;
        
        private bool iD_ESELON1FieldSpecified;
        
        private System.Nullable<decimal> iD_KLField;
        
        private bool iD_KLFieldSpecified;
        
        private string sTATUS_BMN_YNField;
        
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
        public System.Nullable<decimal> ID_BMN {
            get {
                return this.iD_BMNField;
            }
            set {
                this.iD_BMNField = value;
                this.RaisePropertyChanged("ID_BMN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ID_BMNSpecified {
            get {
                return this.iD_BMNFieldSpecified;
            }
            set {
                this.iD_BMNFieldSpecified = value;
                this.RaisePropertyChanged("ID_BMNSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=2)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=3)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=4)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=5)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=6)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=7)]
        public string NOREG {
            get {
                return this.nOREGField;
            }
            set {
                this.nOREGField = value;
                this.RaisePropertyChanged("NOREG");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=8)]
        public string MERK {
            get {
                return this.mERKField;
            }
            set {
                this.mERKField = value;
                this.RaisePropertyChanged("MERK");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=9)]
        public string ALAMAT {
            get {
                return this.aLAMATField;
            }
            set {
                this.aLAMATField = value;
                this.RaisePropertyChanged("ALAMAT");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=10)]
        public System.Nullable<decimal> KUANTITAS {
            get {
                return this.kUANTITASField;
            }
            set {
                this.kUANTITASField = value;
                this.RaisePropertyChanged("KUANTITAS");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool KUANTITASSpecified {
            get {
                return this.kUANTITASFieldSpecified;
            }
            set {
                this.kUANTITASFieldSpecified = value;
                this.RaisePropertyChanged("KUANTITASSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=11)]
        public System.Nullable<decimal> NILAI_BUKU {
            get {
                return this.nILAI_BUKUField;
            }
            set {
                this.nILAI_BUKUField = value;
                this.RaisePropertyChanged("NILAI_BUKU");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NILAI_BUKUSpecified {
            get {
                return this.nILAI_BUKUFieldSpecified;
            }
            set {
                this.nILAI_BUKUFieldSpecified = value;
                this.RaisePropertyChanged("NILAI_BUKUSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=12)]
        public System.Nullable<decimal> NILAI_BUKU_SBLM_SUSUT {
            get {
                return this.nILAI_BUKU_SBLM_SUSUTField;
            }
            set {
                this.nILAI_BUKU_SBLM_SUSUTField = value;
                this.RaisePropertyChanged("NILAI_BUKU_SBLM_SUSUT");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NILAI_BUKU_SBLM_SUSUTSpecified {
            get {
                return this.nILAI_BUKU_SBLM_SUSUTFieldSpecified;
            }
            set {
                this.nILAI_BUKU_SBLM_SUSUTFieldSpecified = value;
                this.RaisePropertyChanged("NILAI_BUKU_SBLM_SUSUTSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=13)]
        public System.Nullable<decimal> NILAI_PEROLEHAN {
            get {
                return this.nILAI_PEROLEHANField;
            }
            set {
                this.nILAI_PEROLEHANField = value;
                this.RaisePropertyChanged("NILAI_PEROLEHAN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NILAI_PEROLEHANSpecified {
            get {
                return this.nILAI_PEROLEHANFieldSpecified;
            }
            set {
                this.nILAI_PEROLEHANFieldSpecified = value;
                this.RaisePropertyChanged("NILAI_PEROLEHANSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=14)]
        public System.Nullable<System.DateTime> TGL_PEROLEHAN {
            get {
                return this.tGL_PEROLEHANField;
            }
            set {
                this.tGL_PEROLEHANField = value;
                this.RaisePropertyChanged("TGL_PEROLEHAN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TGL_PEROLEHANSpecified {
            get {
                return this.tGL_PEROLEHANFieldSpecified;
            }
            set {
                this.tGL_PEROLEHANFieldSpecified = value;
                this.RaisePropertyChanged("TGL_PEROLEHANSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=15)]
        public System.Nullable<decimal> NILAI_PEROLEHAN_PERTAMA {
            get {
                return this.nILAI_PEROLEHAN_PERTAMAField;
            }
            set {
                this.nILAI_PEROLEHAN_PERTAMAField = value;
                this.RaisePropertyChanged("NILAI_PEROLEHAN_PERTAMA");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NILAI_PEROLEHAN_PERTAMASpecified {
            get {
                return this.nILAI_PEROLEHAN_PERTAMAFieldSpecified;
            }
            set {
                this.nILAI_PEROLEHAN_PERTAMAFieldSpecified = value;
                this.RaisePropertyChanged("NILAI_PEROLEHAN_PERTAMASpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=16)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=17)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=18)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=19)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=20)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=21)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=22)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=23)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=24)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=25)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=26)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=27)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=28)]
        public string STATUS_BMN_YN {
            get {
                return this.sTATUS_BMN_YNField;
            }
            set {
                this.sTATUS_BMN_YNField = value;
                this.RaisePropertyChanged("STATUS_BMN_YN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=29)]
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_MON_KPROK_PSP2")]
    public partial class OutputParameters : object, System.ComponentModel.INotifyPropertyChanged {
        
        private WASDALSROW_MON_BMN_PSP2[] sF_MON_KPROK_PSP2Field;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("SF_MON_KPROK_PSP2_ITEM")]
        public WASDALSROW_MON_BMN_PSP2[] SF_MON_KPROK_PSP2 {
            get {
                return this.sF_MON_KPROK_PSP2Field;
            }
            set {
                this.sF_MON_KPROK_PSP2Field = value;
                this.RaisePropertyChanged("SF_MON_KPROK_PSP2");
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
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_MON_KPROK_PSP2", Order=0)]
        public AppPengguna.SvcMonPropSusSelect.InputParameters InputParameters;
        
        public executeRequest() {
        }
        
        public executeRequest(AppPengguna.SvcMonPropSusSelect.InputParameters InputParameters) {
            this.InputParameters = InputParameters;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class executeResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_MON_KPROK_PSP2", Order=0)]
        public AppPengguna.SvcMonPropSusSelect.OutputParameters OutputParameters;
        
        public executeResponse() {
        }
        
        public executeResponse(AppPengguna.SvcMonPropSusSelect.OutputParameters OutputParameters) {
            this.OutputParameters = OutputParameters;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface execute_pttChannel : AppPengguna.SvcMonPropSusSelect.execute_ptt, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class executeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public executeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public AppPengguna.SvcMonPropSusSelect.OutputParameters Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((AppPengguna.SvcMonPropSusSelect.OutputParameters)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class execute_pttClient : System.ServiceModel.ClientBase<AppPengguna.SvcMonPropSusSelect.execute_ptt>, AppPengguna.SvcMonPropSusSelect.execute_ptt {
        
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
        AppPengguna.SvcMonPropSusSelect.executeResponse AppPengguna.SvcMonPropSusSelect.execute_ptt.execute(AppPengguna.SvcMonPropSusSelect.executeRequest request) {
            return base.Channel.execute(request);
        }
        
        public AppPengguna.SvcMonPropSusSelect.OutputParameters execute(AppPengguna.SvcMonPropSusSelect.InputParameters InputParameters) {
            AppPengguna.SvcMonPropSusSelect.executeRequest inValue = new AppPengguna.SvcMonPropSusSelect.executeRequest();
            inValue.InputParameters = InputParameters;
            AppPengguna.SvcMonPropSusSelect.executeResponse retVal = ((AppPengguna.SvcMonPropSusSelect.execute_ptt)(this)).execute(inValue);
            return retVal.OutputParameters;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult AppPengguna.SvcMonPropSusSelect.execute_ptt.Beginexecute(AppPengguna.SvcMonPropSusSelect.executeRequest request, System.AsyncCallback callback, object asyncState) {
            return base.Channel.Beginexecute(request, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult Beginexecute(AppPengguna.SvcMonPropSusSelect.InputParameters InputParameters, System.AsyncCallback callback, object asyncState) {
            AppPengguna.SvcMonPropSusSelect.executeRequest inValue = new AppPengguna.SvcMonPropSusSelect.executeRequest();
            inValue.InputParameters = InputParameters;
            return ((AppPengguna.SvcMonPropSusSelect.execute_ptt)(this)).Beginexecute(inValue, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        AppPengguna.SvcMonPropSusSelect.executeResponse AppPengguna.SvcMonPropSusSelect.execute_ptt.Endexecute(System.IAsyncResult result) {
            return base.Channel.Endexecute(result);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public AppPengguna.SvcMonPropSusSelect.OutputParameters Endexecute(System.IAsyncResult result) {
            AppPengguna.SvcMonPropSusSelect.executeResponse retVal = ((AppPengguna.SvcMonPropSusSelect.execute_ptt)(this)).Endexecute(result);
            return retVal.OutputParameters;
        }
        
        private System.IAsyncResult OnBeginexecute(object[] inValues, System.AsyncCallback callback, object asyncState) {
            AppPengguna.SvcMonPropSusSelect.InputParameters InputParameters = ((AppPengguna.SvcMonPropSusSelect.InputParameters)(inValues[0]));
            return this.Beginexecute(InputParameters, callback, asyncState);
        }
        
        private object[] OnEndexecute(System.IAsyncResult result) {
            AppPengguna.SvcMonPropSusSelect.OutputParameters retVal = this.Endexecute(result);
            return new object[] {
                    retVal};
        }
        
        private void OnexecuteCompleted(object state) {
            if ((this.executeCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.executeCompleted(this, new executeCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void executeAsync(AppPengguna.SvcMonPropSusSelect.InputParameters InputParameters) {
            this.executeAsync(InputParameters, null);
        }
        
        public void executeAsync(AppPengguna.SvcMonPropSusSelect.InputParameters InputParameters, object userState) {
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
