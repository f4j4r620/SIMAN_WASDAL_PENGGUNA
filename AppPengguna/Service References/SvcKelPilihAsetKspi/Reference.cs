﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppPengguna.SvcKelPilihAsetKspi {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://oracle.com/sca/soapservice/siman_pengelolaan/BsgAsetSatker/selectItemAset", ConfigurationName="SvcKelPilihAsetKspi.execute_ptt")]
    public interface execute_ptt {
        
        // CODEGEN: Generating message contract since the operation execute is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="execute", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        AppPengguna.SvcKelPilihAsetKspi.executeResponse execute(AppPengguna.SvcKelPilihAsetKspi.executeRequest request);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="execute", ReplyAction="*")]
        System.IAsyncResult Beginexecute(AppPengguna.SvcKelPilihAsetKspi.executeRequest request, System.AsyncCallback callback, object asyncState);
        
        AppPengguna.SvcKelPilihAsetKspi.executeResponse Endexecute(System.IAsyncResult result);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2556.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_ROW_ASET_BSG")]
    public partial class InputParameters : object, System.ComponentModel.INotifyPropertyChanged {
        
        private System.Nullable<decimal> p_MINField;
        
        private bool p_MINFieldSpecified;
        
        private System.Nullable<decimal> p_MAXField;
        
        private bool p_MAXFieldSpecified;
        
        private string p_COUNTField;
        
        private string p_NO_TIKET_KELOLAField;
        
        private string sTR_WHEREField;
        
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=4)]
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2556.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="DBKELOLA.SROW_ASET_BSG", Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_ROW_ASET_BSG")]
    public partial class DBKELOLASROW_ASET_BSG : object, System.ComponentModel.INotifyPropertyChanged {
        
        private System.Nullable<decimal> nUMField;
        
        private bool nUMFieldSpecified;
        
        private System.Nullable<decimal> iD_KEL_BSGField;
        
        private bool iD_KEL_BSGFieldSpecified;
        
        private string nO_TIKET_KELOLAField;
        
        private System.Nullable<decimal> iD_ASETField;
        
        private bool iD_ASETFieldSpecified;
        
        private string iS_CHECKEDField;
        
        private string iS_DELETEDField;
        
        private System.Nullable<decimal> nILAI_PERSETUJUANField;
        
        private bool nILAI_PERSETUJUANFieldSpecified;
        
        private System.Nullable<decimal> kUANTITAS_PERSETUJUANField;
        
        private bool kUANTITAS_PERSETUJUANFieldSpecified;
        
        private string nO_PSPField;
        
        private System.Nullable<System.DateTime> tGL_PSPField;
        
        private bool tGL_PSPFieldSpecified;
        
        private string cATATANField;
        
        private System.Nullable<decimal> nO_ASETField;
        
        private bool nO_ASETFieldSpecified;
        
        private string kD_BRGField;
        
        private string uR_SSKELField;
        
        private System.Nullable<decimal> kD_JNS_BMNField;
        
        private bool kD_JNS_BMNFieldSpecified;
        
        private string kD_KONDISIField;
        
        private string uR_KONDISIField;
        
        private System.Nullable<decimal> pEROLEHAN_PERTAMAField;
        
        private bool pEROLEHAN_PERTAMAFieldSpecified;
        
        private System.Nullable<decimal> pENYUSUTANField;
        
        private bool pENYUSUTANFieldSpecified;
        
        private System.Nullable<decimal> pEROLEHANField;
        
        private bool pEROLEHANFieldSpecified;
        
        private System.Nullable<decimal> nILAI_BUKUField;
        
        private bool nILAI_BUKUFieldSpecified;
        
        private string mERK_TIPEField;
        
        private string aLAMAT_LOKASIField;
        
        private string kD_SATKERField;
        
        private string uR_SATKERField;
        
        private System.Nullable<System.DateTime> tGL_PERLHField;
        
        private bool tGL_PERLHFieldSpecified;
        
        private string jNS_DOKField;
        
        private string nO_DOKField;
        
        private string nO_KIBField;
        
        private System.Nullable<System.DateTime> tGL_KIBField;
        
        private bool tGL_KIBFieldSpecified;
        
        private System.Nullable<decimal> jML_PHOTOField;
        
        private bool jML_PHOTOFieldSpecified;
        
        private System.Nullable<decimal> lUASField;
        
        private bool lUASFieldSpecified;
        
        private string sK_KEPUTUSANField;
        
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
        public System.Nullable<decimal> ID_KEL_BSG {
            get {
                return this.iD_KEL_BSGField;
            }
            set {
                this.iD_KEL_BSGField = value;
                this.RaisePropertyChanged("ID_KEL_BSG");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ID_KEL_BSGSpecified {
            get {
                return this.iD_KEL_BSGFieldSpecified;
            }
            set {
                this.iD_KEL_BSGFieldSpecified = value;
                this.RaisePropertyChanged("ID_KEL_BSGSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=2)]
        public string NO_TIKET_KELOLA {
            get {
                return this.nO_TIKET_KELOLAField;
            }
            set {
                this.nO_TIKET_KELOLAField = value;
                this.RaisePropertyChanged("NO_TIKET_KELOLA");
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
        public string IS_CHECKED {
            get {
                return this.iS_CHECKEDField;
            }
            set {
                this.iS_CHECKEDField = value;
                this.RaisePropertyChanged("IS_CHECKED");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=5)]
        public string IS_DELETED {
            get {
                return this.iS_DELETEDField;
            }
            set {
                this.iS_DELETEDField = value;
                this.RaisePropertyChanged("IS_DELETED");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=6)]
        public System.Nullable<decimal> NILAI_PERSETUJUAN {
            get {
                return this.nILAI_PERSETUJUANField;
            }
            set {
                this.nILAI_PERSETUJUANField = value;
                this.RaisePropertyChanged("NILAI_PERSETUJUAN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NILAI_PERSETUJUANSpecified {
            get {
                return this.nILAI_PERSETUJUANFieldSpecified;
            }
            set {
                this.nILAI_PERSETUJUANFieldSpecified = value;
                this.RaisePropertyChanged("NILAI_PERSETUJUANSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=7)]
        public System.Nullable<decimal> KUANTITAS_PERSETUJUAN {
            get {
                return this.kUANTITAS_PERSETUJUANField;
            }
            set {
                this.kUANTITAS_PERSETUJUANField = value;
                this.RaisePropertyChanged("KUANTITAS_PERSETUJUAN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool KUANTITAS_PERSETUJUANSpecified {
            get {
                return this.kUANTITAS_PERSETUJUANFieldSpecified;
            }
            set {
                this.kUANTITAS_PERSETUJUANFieldSpecified = value;
                this.RaisePropertyChanged("KUANTITAS_PERSETUJUANSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=8)]
        public string NO_PSP {
            get {
                return this.nO_PSPField;
            }
            set {
                this.nO_PSPField = value;
                this.RaisePropertyChanged("NO_PSP");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=9)]
        public System.Nullable<System.DateTime> TGL_PSP {
            get {
                return this.tGL_PSPField;
            }
            set {
                this.tGL_PSPField = value;
                this.RaisePropertyChanged("TGL_PSP");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TGL_PSPSpecified {
            get {
                return this.tGL_PSPFieldSpecified;
            }
            set {
                this.tGL_PSPFieldSpecified = value;
                this.RaisePropertyChanged("TGL_PSPSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=10)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=11)]
        public System.Nullable<decimal> NO_ASET {
            get {
                return this.nO_ASETField;
            }
            set {
                this.nO_ASETField = value;
                this.RaisePropertyChanged("NO_ASET");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NO_ASETSpecified {
            get {
                return this.nO_ASETFieldSpecified;
            }
            set {
                this.nO_ASETFieldSpecified = value;
                this.RaisePropertyChanged("NO_ASETSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=12)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=13)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=14)]
        public System.Nullable<decimal> KD_JNS_BMN {
            get {
                return this.kD_JNS_BMNField;
            }
            set {
                this.kD_JNS_BMNField = value;
                this.RaisePropertyChanged("KD_JNS_BMN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool KD_JNS_BMNSpecified {
            get {
                return this.kD_JNS_BMNFieldSpecified;
            }
            set {
                this.kD_JNS_BMNFieldSpecified = value;
                this.RaisePropertyChanged("KD_JNS_BMNSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=15)]
        public string KD_KONDISI {
            get {
                return this.kD_KONDISIField;
            }
            set {
                this.kD_KONDISIField = value;
                this.RaisePropertyChanged("KD_KONDISI");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=16)]
        public string UR_KONDISI {
            get {
                return this.uR_KONDISIField;
            }
            set {
                this.uR_KONDISIField = value;
                this.RaisePropertyChanged("UR_KONDISI");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=17)]
        public System.Nullable<decimal> PEROLEHAN_PERTAMA {
            get {
                return this.pEROLEHAN_PERTAMAField;
            }
            set {
                this.pEROLEHAN_PERTAMAField = value;
                this.RaisePropertyChanged("PEROLEHAN_PERTAMA");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PEROLEHAN_PERTAMASpecified {
            get {
                return this.pEROLEHAN_PERTAMAFieldSpecified;
            }
            set {
                this.pEROLEHAN_PERTAMAFieldSpecified = value;
                this.RaisePropertyChanged("PEROLEHAN_PERTAMASpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=18)]
        public System.Nullable<decimal> PENYUSUTAN {
            get {
                return this.pENYUSUTANField;
            }
            set {
                this.pENYUSUTANField = value;
                this.RaisePropertyChanged("PENYUSUTAN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PENYUSUTANSpecified {
            get {
                return this.pENYUSUTANFieldSpecified;
            }
            set {
                this.pENYUSUTANFieldSpecified = value;
                this.RaisePropertyChanged("PENYUSUTANSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=19)]
        public System.Nullable<decimal> PEROLEHAN {
            get {
                return this.pEROLEHANField;
            }
            set {
                this.pEROLEHANField = value;
                this.RaisePropertyChanged("PEROLEHAN");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PEROLEHANSpecified {
            get {
                return this.pEROLEHANFieldSpecified;
            }
            set {
                this.pEROLEHANFieldSpecified = value;
                this.RaisePropertyChanged("PEROLEHANSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=20)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=21)]
        public string MERK_TIPE {
            get {
                return this.mERK_TIPEField;
            }
            set {
                this.mERK_TIPEField = value;
                this.RaisePropertyChanged("MERK_TIPE");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=22)]
        public string ALAMAT_LOKASI {
            get {
                return this.aLAMAT_LOKASIField;
            }
            set {
                this.aLAMAT_LOKASIField = value;
                this.RaisePropertyChanged("ALAMAT_LOKASI");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=23)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=24)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=25)]
        public System.Nullable<System.DateTime> TGL_PERLH {
            get {
                return this.tGL_PERLHField;
            }
            set {
                this.tGL_PERLHField = value;
                this.RaisePropertyChanged("TGL_PERLH");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TGL_PERLHSpecified {
            get {
                return this.tGL_PERLHFieldSpecified;
            }
            set {
                this.tGL_PERLHFieldSpecified = value;
                this.RaisePropertyChanged("TGL_PERLHSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=26)]
        public string JNS_DOK {
            get {
                return this.jNS_DOKField;
            }
            set {
                this.jNS_DOKField = value;
                this.RaisePropertyChanged("JNS_DOK");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=27)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=28)]
        public string NO_KIB {
            get {
                return this.nO_KIBField;
            }
            set {
                this.nO_KIBField = value;
                this.RaisePropertyChanged("NO_KIB");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=29)]
        public System.Nullable<System.DateTime> TGL_KIB {
            get {
                return this.tGL_KIBField;
            }
            set {
                this.tGL_KIBField = value;
                this.RaisePropertyChanged("TGL_KIB");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TGL_KIBSpecified {
            get {
                return this.tGL_KIBFieldSpecified;
            }
            set {
                this.tGL_KIBFieldSpecified = value;
                this.RaisePropertyChanged("TGL_KIBSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=30)]
        public System.Nullable<decimal> JML_PHOTO {
            get {
                return this.jML_PHOTOField;
            }
            set {
                this.jML_PHOTOField = value;
                this.RaisePropertyChanged("JML_PHOTO");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool JML_PHOTOSpecified {
            get {
                return this.jML_PHOTOFieldSpecified;
            }
            set {
                this.jML_PHOTOFieldSpecified = value;
                this.RaisePropertyChanged("JML_PHOTOSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=31)]
        public System.Nullable<decimal> LUAS {
            get {
                return this.lUASField;
            }
            set {
                this.lUASField = value;
                this.RaisePropertyChanged("LUAS");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool LUASSpecified {
            get {
                return this.lUASFieldSpecified;
            }
            set {
                this.lUASFieldSpecified = value;
                this.RaisePropertyChanged("LUASSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=32)]
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
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=33)]
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2556.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_ROW_ASET_BSG")]
    public partial class OutputParameters : object, System.ComponentModel.INotifyPropertyChanged {
        
        private DBKELOLASROW_ASET_BSG[] sF_ROW_ASET_BSGField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(IsNullable=true, Order=0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("SF_ROW_ASET_BSG_ITEM")]
        public DBKELOLASROW_ASET_BSG[] SF_ROW_ASET_BSG {
            get {
                return this.sF_ROW_ASET_BSGField;
            }
            set {
                this.sF_ROW_ASET_BSGField = value;
                this.RaisePropertyChanged("SF_ROW_ASET_BSG");
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
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_ROW_ASET_BSG", Order=0)]
        public AppPengguna.SvcKelPilihAsetKspi.InputParameters InputParameters;
        
        public executeRequest() {
        }
        
        public executeRequest(AppPengguna.SvcKelPilihAsetKspi.InputParameters InputParameters) {
            this.InputParameters = InputParameters;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class executeResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.oracle.com/pcbpel/adapter/db/sp/SF_ROW_ASET_BSG", Order=0)]
        public AppPengguna.SvcKelPilihAsetKspi.OutputParameters OutputParameters;
        
        public executeResponse() {
        }
        
        public executeResponse(AppPengguna.SvcKelPilihAsetKspi.OutputParameters OutputParameters) {
            this.OutputParameters = OutputParameters;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface execute_pttChannel : AppPengguna.SvcKelPilihAsetKspi.execute_ptt, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class executeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public executeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public AppPengguna.SvcKelPilihAsetKspi.OutputParameters Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((AppPengguna.SvcKelPilihAsetKspi.OutputParameters)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class execute_pttClient : System.ServiceModel.ClientBase<AppPengguna.SvcKelPilihAsetKspi.execute_ptt>, AppPengguna.SvcKelPilihAsetKspi.execute_ptt {
        
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
        AppPengguna.SvcKelPilihAsetKspi.executeResponse AppPengguna.SvcKelPilihAsetKspi.execute_ptt.execute(AppPengguna.SvcKelPilihAsetKspi.executeRequest request) {
            return base.Channel.execute(request);
        }
        
        public AppPengguna.SvcKelPilihAsetKspi.OutputParameters execute(AppPengguna.SvcKelPilihAsetKspi.InputParameters InputParameters) {
            AppPengguna.SvcKelPilihAsetKspi.executeRequest inValue = new AppPengguna.SvcKelPilihAsetKspi.executeRequest();
            inValue.InputParameters = InputParameters;
            AppPengguna.SvcKelPilihAsetKspi.executeResponse retVal = ((AppPengguna.SvcKelPilihAsetKspi.execute_ptt)(this)).execute(inValue);
            return retVal.OutputParameters;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult AppPengguna.SvcKelPilihAsetKspi.execute_ptt.Beginexecute(AppPengguna.SvcKelPilihAsetKspi.executeRequest request, System.AsyncCallback callback, object asyncState) {
            return base.Channel.Beginexecute(request, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult Beginexecute(AppPengguna.SvcKelPilihAsetKspi.InputParameters InputParameters, System.AsyncCallback callback, object asyncState) {
            AppPengguna.SvcKelPilihAsetKspi.executeRequest inValue = new AppPengguna.SvcKelPilihAsetKspi.executeRequest();
            inValue.InputParameters = InputParameters;
            return ((AppPengguna.SvcKelPilihAsetKspi.execute_ptt)(this)).Beginexecute(inValue, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        AppPengguna.SvcKelPilihAsetKspi.executeResponse AppPengguna.SvcKelPilihAsetKspi.execute_ptt.Endexecute(System.IAsyncResult result) {
            return base.Channel.Endexecute(result);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public AppPengguna.SvcKelPilihAsetKspi.OutputParameters Endexecute(System.IAsyncResult result) {
            AppPengguna.SvcKelPilihAsetKspi.executeResponse retVal = ((AppPengguna.SvcKelPilihAsetKspi.execute_ptt)(this)).Endexecute(result);
            return retVal.OutputParameters;
        }
        
        private System.IAsyncResult OnBeginexecute(object[] inValues, System.AsyncCallback callback, object asyncState) {
            AppPengguna.SvcKelPilihAsetKspi.InputParameters InputParameters = ((AppPengguna.SvcKelPilihAsetKspi.InputParameters)(inValues[0]));
            return this.Beginexecute(InputParameters, callback, asyncState);
        }
        
        private object[] OnEndexecute(System.IAsyncResult result) {
            AppPengguna.SvcKelPilihAsetKspi.OutputParameters retVal = this.Endexecute(result);
            return new object[] {
                    retVal};
        }
        
        private void OnexecuteCompleted(object state) {
            if ((this.executeCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.executeCompleted(this, new executeCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void executeAsync(AppPengguna.SvcKelPilihAsetKspi.InputParameters InputParameters) {
            this.executeAsync(InputParameters, null);
        }
        
        public void executeAsync(AppPengguna.SvcKelPilihAsetKspi.InputParameters InputParameters, object userState) {
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
