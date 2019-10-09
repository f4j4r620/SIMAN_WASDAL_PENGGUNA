using System;
using System.Linq;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using System.Data;
using System.Reflection;
using System.Diagnostics;

namespace AppPengguna
{
    class DataLokal
    {
        private bool selesaiPeriksaBeda = false;
        public string koneksiSqlServer = String.Format(@"Data Source=localhost\SQLBPSIMAN;Initial Catalog=LOCALBPSIMAN;User Id=sa;Password=SHTbpD*1");
        string koneksiSqlServerMaster = String.Format(@"Data Source=localhost\SQLBPSIMAN;Initial Catalog=master;User Id=sa;Password=SHTbpD*1");
        SqlConnection connection = null;
        string fileteks;
        public static DataTable dt;
        private string appPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\";

        #region database & table name
        public static string nmFolder = "Penggunaan";
        public static string nmDB = "master";
        public static string namaLaporan = "";
        private static string namaTabelPenggunaan = "RPT_WASDAL_GUNA_SATKER";
        private static string namaTabelPenggunaanRpmk = "RPT_WASDAL_GUNA_PMK_SATKER";
        private static string namaTabelPemanfaatan = "RPT_WASDAL_MANFAAT_SATKER";
        private static string namaTabelPemindahtanganan = "RPT_WASDAL_PINDAHTANGAN_SATKER";
        private static string namaTabelPenghapusan = "";
        private static string namaTabelPenertiban = "RPT_WASDAL_PENERTIBAN";
        private static string namaTabelinvestigasi = "";
        private static string namaTabelMonLapWasdal = "RPT_MON_LAP_WASDAL";
        static string namaTabel = "";
        #endregion

        #region SQL
        static string sqldata = "SELECT * FROM ";
        //static string strCari = "WHERE " + strCari;
        //static string syntaxSql = DataLokal.sqldata + strCari;
        static string syntaxSql;
        #endregion

        #region 1. pilih Laporan
        public void pilihLaporan(String namaLaporan)
        {
            if (namaLaporan != null && namaLaporan != "")
            {
                if (namaLaporan.Equals("penggunaan"))
                    namaTabel = namaTabelPenggunaan;
                else if (namaLaporan.Equals("pemanfaatan"))
                    namaTabel = namaTabelPemanfaatan;
                else if (namaLaporan.Equals("pemindahtanganan"))
                    namaTabel = namaTabelPemindahtanganan;
                else if (namaLaporan.Equals("penertiban"))
                    namaTabel = namaTabelPenertiban;
                else if (namaLaporan.Equals("investigasi"))
                    namaTabel = namaTabelinvestigasi;
                else if (namaLaporan.Equals("penggunaanRpmk"))
                    namaTabel = namaTabelPenggunaanRpmk;
                else if (namaLaporan.Equals("monLapWasdal"))
                    namaTabel = namaTabelMonLapWasdal;
                syntaxSql = DataLokal.sqldata + namaTabel;
                eksekusiFileSql();
            }
        }
        #endregion

        #region 2. decompress file

        public static void Decompress(FileInfo fileToDecompress)
        {
            using (FileStream originalFileStream = fileToDecompress.OpenRead())
            {
                string currentFileName = fileToDecompress.FullName;
                string newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);

                using (FileStream decompressedFileStream = File.Create(newFileName))
                {
                    using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(decompressedFileStream);
                        Console.WriteLine("Decompressed: {0}", fileToDecompress.Name);
                    }

                }
            }
        }

        #endregion

        #region 3. cacah file

        public string CacahFile(string SintaksSQL)
        {
            string[] cacahRecord = SintaksSQL.Split('|');
            string tempSQL = "";
            for (int i = 0; i < cacahRecord.Count(); i++)
            {
                if (cacahRecord[i] != "")
                {
                    int j = 0; int k = 0;
                    int temp = 0;
                    while ((j = cacahRecord[i].IndexOf("'", j)) != -1)
                    {
                        if (k == 1)
                        {
                            temp = j;
                        }
                        if (k > 1)
                        {
                            cacahRecord[i] = cacahRecord[i].Substring(1, temp) + cacahRecord[i].Substring(temp + 1);
                            break;
                        }
                        k = k + 1;
                        j++;
                    }
                    if (i > 0)
                    { tempSQL = tempSQL + "," + cacahRecord[i]; }
                    else { tempSQL = cacahRecord[i]; }
                }
            }
            return tempSQL;
        }

        #endregion

        #region 4. kosongkan table

        private void KosongkanTabel()
        {
            try
            {
                using (connection = new SqlConnection(koneksiSqlServer))
                {
                    connection.Open();
                    SqlCommand cmd = connection.CreateCommand();
                    SqlTransaction transaksi = connection.BeginTransaction();
                    cmd.Connection = connection;
                    cmd.Transaction = transaksi;
                    try
                    {
                        string perintahSql = "truncate table " + namaTabel + ";";
                        cmd.CommandText = perintahSql;
                        cmd.ExecuteNonQuery();
                        transaksi.Commit();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Eksekusi Query SQL Gagal dilakukan", konfigApp.judulGagalSimpan);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Eksekusi Query SQL Gagal dilakukan", konfigApp.judulGagalSimpan);
            }
            finally
            {
                connection.Close();

            }
        }

        #endregion

        #region 5. insert data ke Database lokal

        private void eksekusiFileSql()
        {
            string FolderDiFile = String.Format("{0}\\" + nmFolder + "\\", System.IO.Directory.GetCurrentDirectory());
            string lokasiDiFile = String.Format("{0}\\" + nmFolder + "\\{1}", System.IO.Directory.GetCurrentDirectory(), namaTabel);
            DirectoryInfo directorySelected = new DirectoryInfo(FolderDiFile);
            foreach (FileInfo fileToDecompress in directorySelected.GetFiles("*.zip")) //dekompresi result dari service
            {
                Decompress(fileToDecompress);
                lokasiDiFile = FolderDiFile + "\\" + fileToDecompress.Name.Replace(".zip", ""); 
            }
            bool adaGagal = false;
            string daftarBarisGagal = "";
            int noBaris = 0;
            string barisFile;
            StreamReader fileDibaca = new StreamReader(lokasiDiFile);
            fileteks = "";
            fileteks = fileDibaca.ReadToEnd();
            string[] cacahBaris = fileteks.Split(';');
            KosongkanTabel(); //mengosongkan isi table lokal
            int j = 0;
            
                    try
                    {
                        using (connection = new SqlConnection(koneksiSqlServer))
                        {
                            connection.Open();
                            SqlCommand cmd = connection.CreateCommand();
                            SqlTransaction transaksi = connection.BeginTransaction();
                            cmd.Connection = connection;
                            cmd.Transaction = transaksi;
                            string perintahSql;
                            //perintahSql = CacahFile(syntaxSql);
                            for (int i = 0; i < cacahBaris.Count(); i++)
                            {
                                if (cacahBaris[i] != "")
                                {
                                    perintahSql = cacahBaris[i];
                                    perintahSql = CacahFile(perintahSql);
                                    perintahSql = perintahSql.Replace("\n", "").Trim().Replace("\r", "").Replace("\t", "");
                                    if (perintahSql != "")
                                    {
                                        try
                                        {

                                            cmd.CommandText = perintahSql;
                                            cmd.ExecuteNonQuery();
                                            j = j + 1;

                                            if (j == 1000)
                                            {
                                                j = 0;
                                                transaksi.Commit();
                                            }


                                        }
                                        catch (Exception ex)
                                        {
                                            transaksi.Rollback();
                                            MessageBox.Show("File Patch Gagal");
                                        }

                                        //transaksi.Commit();
                                        //transaksi = connection.BeginTransaction();
                                    }
                                }
                            }
                            transaksi.Commit();
                        }
                    }
                    catch
                    {
                        adaGagal = true;
                        daftarBarisGagal = noBaris.ToString() + "; ";
                        
                    }
                    finally
                    {
                        connection.Close();
                    }
                
            //CetakDataLokal();
            if (adaGagal == true)
            {
                MessageBox.Show("Daftar record (baris) data yang gagal: " + daftarBarisGagal, "Perhatian");
            }
            //eksekusiDone = true;
            fileDibaca.Close();
            File.Delete(lokasiDiFile);
        }
        #endregion

        #region 6. Cetak data lokal ke datatable
        public void CetakDataLokal()
        {
            try
            {
                dt = new DataTable();
                using (connection = new SqlConnection(koneksiSqlServer))
                {
                    connection.Open();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = syntaxSql;
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{ 
                        
                    //}
                    dataAdapter.Fill(dt);
                }
            }
            catch 
            {
                MessageBox.Show(konfigApp.teksGagalAmbil, konfigApp.judulKonfirmasi);
            }
            finally
            {
                connection.Close();
            }
        }
        #endregion

        #region UPDATE DATABASE
        private string serviceName = "MSSQL$SQLBPSIMAN";
        private bool openConnection(string catalog="")
        {
            try
            {
                if (catalog == "master")
                {
                    connection = new SqlConnection(koneksiSqlServerMaster);

                }
                else {
                    connection = new SqlConnection(koneksiSqlServer);
                }
                connection.Open();
                return true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Koneksi ke Database Lokal gagal");
                return false;
            }
        }
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("CLOSE CONNECTION FAILED : " + ex.Message);
                return false;
            }
        }
        
        string dest_localbpsiman = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Common Files", "DBLocal", "LOCALBPSIMAN.mdf");
        string dest_localbpsiman_log = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Common Files", "DBLocal", "LOCALBPSIMAN_Log.ldf");
        string localbpsiman = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath) + @"\", "", "LOCALBPSIMAN.mdf");
        string localbpsiman_log = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath) + @"\", "", "LOCALBPSIMAN_Log.ldf");

        string localbpsiman_txt = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath) + @"\", "", "LOCALBPSIMAN.dll");
        string localbpsiman_log_txt = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath) + @"\", "", "LOCALBPSIMAN_Log.dll");

        public void CheckDb()
        {
            Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.CompanyName.Substring(1,10);
            DataTable dtInfoWasdal = new DataTable();
            DataLokal dl = new DataLokal();

            if (openConnection("sa") == true)
            {
                try
                {
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT ID_INFO,TGL_INFO FROM T_INFO_WASDAL WHERE ID_INFO=1";
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    dataAdapter.Fill(dtInfoWasdal);

                    if (dtInfoWasdal.Rows.Count == 1)
                    {

                        foreach (DataRow dr in dtInfoWasdal.Rows)
                        {
                            bool result = dr[1].ToString().Equals(version, StringComparison.Ordinal);
                            if (result == false)
                            {
                                //CreateStartStopService();
                                this.CopyDatabase();

                            }
                        }
                    }
                    else
                    {
                        this.CopyDatabase();
                    }
                }
                catch(SqlException ex) {
                    //MessageBox.Show("error");
                    this.CopyDatabase();
                }
            }
        }

        private string DetachDb()
        {
            string output = "";
            string query = "";
            try
            {
                if (openConnection("master"))
                {
                    SqlCommand cmd = connection.CreateCommand();

                    query = @"
                         USE master;
                        alter database LOCALBPSIMAN
                        set offline with rollback immediate;
                        alter database LOCALBPSIMAN
                        set SINGLE_USER;
                        EXEC sp_detach_db @dbname = N'LOCALBPSIMAN';
                        ";



                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                    output = "Y";
                    CloseConnection();
                }
            }
            catch(SqlException e) {

                output = "N";
            }
            return output;
        }

        private string AttachDb()
        {
            string output = "";
            string query = "";
            try
            {
                if (openConnection("master"))
                {
                    SqlCommand cmd = connection.CreateCommand();

                    query = @"
                   USE master;
                    CREATE DATABASE LOCALBPSIMAN
                    ON (FILENAME = '"+dest_localbpsiman+@"'),
                    (FILENAME = '"+dest_localbpsiman_log+ @"')
                    FOR ATTACH;
                    ALTER DATABASE [LOCALBPSIMAN]
                    SET READ_WRITE;
                    EXEC xp_instance_regwrite N'HKEY_LOCAL_MACHINE', N'Software\Microsoft\MSSQLServer\MSSQLServer', N'NumErrorLogs', REG_DWORD, 6;
                  
                    ";

                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                    output = "Y";
                    CloseConnection();
                }
            }
            catch (SqlException e)
            {

                output = "N";
            }
            return output;
        }

        

        private void CopyDatabase()
        {
            string outPar = "";
            try
            {
                
                if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Common Files", "DBLocal"))) Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Common Files", "DBLocal"));

                outPar = this.DetachDb();

                if (File.Exists(localbpsiman_txt))
                    File.Copy(localbpsiman_txt, dest_localbpsiman, true);

                if (File.Exists(localbpsiman_log_txt))
                    File.Copy(localbpsiman_log_txt, dest_localbpsiman_log, true);

                if (File.Exists(localbpsiman_txt) && File.Exists(localbpsiman_log_txt))
                {
                    outPar = this.AttachDb();
                }
                

                switch (outPar)
                {
                    case "Y":
                        MessageBox.Show("Proses Update Database Lokal Berhasil", konfigApp.judulGagalLain);

                        break;
                    case "N":
                        MessageBox.Show("Proses Update Database Lokal Gagal! \nDatabase Tidak Di Temukan", konfigApp.judulGagalLain);
                        break;
                    default:
                        MessageBox.Show("Proses Update Database Lokal Gagal", konfigApp.judulGagalLain);
                        break;
                }





            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, konfigApp.judulGagalAmbil);
            }
        }
        #endregion
    }
}
