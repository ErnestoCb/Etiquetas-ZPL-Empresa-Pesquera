using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Data;
using System.Drawing.Printing;
using System.Configuration;
using System.Net;
using Newtonsoft.Json;
using System.Collections;

namespace EtiquetasBlumar
{
    /// <summary>
    /// Lógica de interacción para tablaExcel.xaml
    /// </summary>
    public partial class tablaExcel : Window
    {
        List<DataRow> listaExcelFilas = new List<DataRow>();
        public tablaExcel()
        {
            InitializeComponent();
            
        }

        private List<DataRow> testExcel()
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.DefaultExt = ".xlsx";
            openfile.Filter = "(.xlsx)|*.xlsx";
            //openfile.ShowDialog();

            var browsefile = openfile.ShowDialog();

            if (browsefile == true)
            {
                txtFilePath.Text = openfile.FileName;

                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                //Static File From Base Path...........
                //Microsoft.Office.Interop.Excel.Workbook excelBook = excelApp.Workbooks.Open(AppDomain.CurrentDomain.BaseDirectory + "TestExcel.xlsx", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                //Dynamic File Using Uploader...........
                Microsoft.Office.Interop.Excel.Workbook excelBook = excelApp.Workbooks.Open(txtFilePath.Text.ToString(), 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                Microsoft.Office.Interop.Excel.Worksheet excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelBook.Worksheets.get_Item(1); ;
                Microsoft.Office.Interop.Excel.Range excelRange = excelSheet.UsedRange;

                string strCellData = "";
                double douCellData;
                int rowCnt = 0;
                int colCnt = 0;

                DataTable dt = new DataTable();
                for (colCnt = 1; colCnt <= excelRange.Columns.Count; colCnt++)
                {
                    string strColumn = "";
                    strColumn = (string)(excelRange.Cells[1, colCnt] as Microsoft.Office.Interop.Excel.Range).Value2;
                    dt.Columns.Add(strColumn, typeof(string));
                }

                for (rowCnt = 2; rowCnt <= excelRange.Rows.Count; rowCnt++)
                {
                    String strData = "";
                    for (colCnt = 1; colCnt <= excelRange.Columns.Count; colCnt++)
                    {
                        try
                        {
                            strCellData = (string)(excelRange.Cells[rowCnt, colCnt] as Microsoft.Office.Interop.Excel.Range).Value2;
                            strData += strCellData + "|";
                        }
                        catch (Exception ex)
                        {
                            douCellData = (excelRange.Cells[rowCnt, colCnt] as Microsoft.Office.Interop.Excel.Range).Value2;
                            strData += douCellData.ToString() + "|";
                        }
                    }
                    strData = strData.Remove(strData.Length - 1, 1);
                    dt.Rows.Add(strData.Split('|'));
                }

                dtGrid.ItemsSource = dt.DefaultView;



                excelBook.Close(true, null, null);
                excelApp.Quit();

                List<DataRow> list = dt.AsEnumerable().ToList();

                return list;
            }
            else
                return null;
        }

        private void BtonExcel_Click(object sender, RoutedEventArgs e)
        {
            listaExcelFilas = testExcel();
        }

        
        public String print(String[] datosArray)
        {
            string etiqueta = System.IO.Directory.GetCurrentDirectory() + @"\zpl\etiqueta.zpl";
            string tempoEtiqueta = System.IO.Directory.GetCurrentDirectory() + @"\zpl\tempoEtiqueta.zpl";
            var lines = File.ReadAllLines(etiqueta);
            string salida = "";

            foreach (var line in lines)
            {
                salida += line;
            }

            String tipos = "";
            if (datosArray[8] == "5")
                tipos = "x";
            else
                tipos = datosArray[8];
            
            String qrcode = "{" +
                "\"lt\":\"" + datosArray[0] + "\"," +
                "\"mt\":\"" + datosArray[5] + "\"," +
                "\"pr\":\"" + datosArray[2] + "\"," +
                "\"cr\":\"" + datosArray[1] + "\"," +
                "\"f\":\"" + datosArray[6] + "\"," +
                "\"ev\":\"" + datosArray[7] + "\"," +
                "\"jl\":\"" + datosArray[3] + "\"," +
                "\"an\":\"" + datosArray[4] + "\"," +
                "\"rz\":\"" + tipos + "\"," +
                "\"ct\":\"" + datosArray[9] + "\"" +
                "}";

            salida = salida.Replace("[lote]", datosArray[0]);
            salida = salida.Replace("[correlativo]", datosArray[1]);
            salida = salida.Replace("[porcion]", datosArray[2]);
            salida = salida.Replace("[juliana]", datosArray[3]);
            salida = salida.Replace("[anno]", datosArray[4]);
            salida = salida.Replace("[qrCode]", qrcode);

            try
            {
                if (File.Exists(tempoEtiqueta))
                {
                    File.Delete(tempoEtiqueta);
                }

                using (FileStream fs = File.Create(tempoEtiqueta))
                {
                    if (salida != null)
                    {
                        Byte[] info = new UTF8Encoding(true).GetBytes(salida);
                        fs.Write(info, 0, info.Length);
                        fs.Dispose();
                        fs.Close();
                    }
                    else
                    {
                        Console.WriteLine("Debe ingresar ZPL.");
                    }
                }

                PrintDialog dlgSettings = new PrintDialog();
                PrinterSettings ps = new PrinterSettings();

                RawPrinterHelper.SendFileToPrinter(ps.PrinterName, tempoEtiqueta);

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return salida;
        }



        private void BtnVolver_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PrintExcel_Click(object sender, RoutedEventArgs e)
        {
            DataRow last = listaExcelFilas.Last();

            //List<DataRow> list = dt.AsEnumerable().ToList();
            foreach (DataRow row in listaExcelFilas)
            {
                String[] datosArray = new String[10];

                try
                {
                    datosArray[0] = row["lote"].ToString();
                    datosArray[1] = (row["correlativo"].ToString()).PadLeft(3, '0');
                    datosArray[2] = (row["porcion"].ToString()).PadLeft(2, '0');
                    datosArray[3] = (row["juliana"].ToString()).PadLeft(3, '0');
                    datosArray[4] = row["anno"].ToString();
                    datosArray[5] = row["material"].ToString();
                    datosArray[6] = row["fecha"].ToString();
                    datosArray[7] = row["envase"].ToString();
                    datosArray[8] = row["tipo"].ToString();
                    datosArray[9] = row["cantidad"].ToString();

                    print(datosArray);

                    if (row.Equals(last))
                    {
                        if (reimpresion.IsChecked ?? false)
                        {
                            MessageBox.Show("no guardando");
                        }
                        else
                        {
                            MessageBox.Show("guardando");
                            insertarNuevoCorrelativoWB(datosArray[0], datosArray[2], DateTime.Parse(datosArray[6]).ToString("yyyyMMdd"), datosArray[1]);
                        }
                    }

                }
                catch
                {
                    MessageBox.Show("Formato de excel incrorrecto.");
                }
                
            }
        }
        
        public Boolean insertarNuevoCorrelativoWB(String lote, String porcion, String fecha, String correlativo)
        {
            Boolean resp = false;
            String jsonInsert = "{ \"lote\": " + "\"" + lote + "\"" + ", \"porcion\": " + "\"" + porcion + "\"" + ", \"fecha\": " + "\"" + fecha + "\"" + ", \"correlativo\": " + "\"" + correlativo + "\"" + " }";
            //testeo.Content = jsonInsert;
            //MessageBox.Show(jsonInsert);

            try
            {
                string insertarNuevoCorrelativo = ConfigurationManager.AppSettings["insertarNuevoCorrelativo"];
                MessageBox.Show("" + insertarNuevoCorrelativo + jsonInsert + "");
                WebRequest request = WebRequest.Create("" + insertarNuevoCorrelativo + jsonInsert + "");
                WebResponse response = request.GetResponse();

                Stream dataStream = response.GetResponseStream();

                StreamReader reader = new StreamReader(dataStream);

                string responseFromServer = reader.ReadToEnd();

                String respuesta = responseFromServer.ToString();


                reader.Close();
                response.Close();

                dynamic json = JsonConvert.DeserializeObject(respuesta);

                ArrayList retorno = new ArrayList();
                //retorno.Add(json.id);
                retorno.Add(json.estado);
                retorno.Add(json.mensaje);

                String responder = retorno[0].ToString();
                
                if (responder == "true")
                {
                    resp = true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("error:  " + e);
                resp = false;
            }
            return resp;
        }

    }
}
