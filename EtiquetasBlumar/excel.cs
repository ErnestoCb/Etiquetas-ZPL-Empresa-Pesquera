using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Collections.ObjectModel;

namespace EtiquetasBlumar
{
    class Dato
    {
        public String dato1 { get; set; }
        public String dato2 { get; set; }
        public String dato3 { get; set; }


    }

    class excel
    {
        OleDbConnection Conn;
        OleDbCommand Cmd;

        public excel()
        {
            Conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\riria\\Desktop\\excelTest.xlsx;Extended Properties=\"Excel 12.0 Xml;HDR=YES;\"");
        }

        // Method to Get All the Records from Excel
        public async Task<ObservableCollection<Dato>> GetDataFormExcelAsync()
        {
            ObservableCollection<Dato> Datos = new ObservableCollection<Dato>();
            await Conn.OpenAsync();
            Cmd = new OleDbCommand();
            Cmd.Connection = Conn;
            Cmd.CommandText = "Select * from [Sheet1$]";
            var Reader = await Cmd.ExecuteReaderAsync();
            while (Reader.Read())
            {
                Datos.Add(new Dato()
                {
                    dato1 = Reader["dato1"].ToString(),
                    dato2 = Reader["dato2"].ToString(),
                    dato3 = Reader["dato3"].ToString()
                });
            }
            Reader.Close();
            Conn.Close();
            return Datos;
        }

        
        /// The method to check if the record is already available 
        /// in the workgroup
        private async Task<bool> CheckIfRecordExistAsync(Dato datox)
        {
            bool IsRecordExist = false;
            Cmd.CommandText = "Select * from [Sheet1$] where dato1=@datox";
            var Reader = await Cmd.ExecuteReaderAsync();
            if (Reader.HasRows)
            {
                IsRecordExist = true;
            }

            Reader.Close();
            return IsRecordExist;
        }
    }
}
