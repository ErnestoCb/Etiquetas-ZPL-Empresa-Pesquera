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
using System.Windows.Navigation;
using System.Windows.Shapes;
using iTextSharp;
using iTextSharp.text.pdf;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.IO;
using System.Drawing.Printing;
using System.Net;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Xml;
using System.Collections;
using System.Net.Http;
using System.Net.Http.Headers;

namespace EtiquetasBlumar
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml 
    /// </summary>
    public partial class MainWindow : Window
    {
        //XmlDOCUMENT
        XmlDocument doc = new XmlDocument();
        //XmlDocument

        private MainWindow instance;
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            instance = this;
            
            fechaPicker.SelectedDate = DateTime.Today;
            cbxTipos.Text = "Recursos";
            //txtCorrelativo.Text = cantEtiquetas.Value.ToString();
            //testeo.Content = fechaPicker.SelectedDate.Value;

            //XmlDOCUMENT Y CENTROS
            doc.Load(System.IO.Directory.GetCurrentDirectory() + @"\centrosBlumar.xml");
            XmlNodeList nodoCentros = doc.GetElementsByTagName("centros");
            foreach(XmlNode node in nodoCentros[0].ChildNodes)
            {
                ComboBoxItem cbxitem = new ComboBoxItem();

                cbxitem.Tag = node.Attributes["id"].Value;
                cbxitem.Content = node.InnerText;

                comboCentro.Items.Add(cbxitem);
            }

            llenarCentro();

            //XmlDocument Y CENTROS

            //Conexion con SAP-------------------------------------
            WebClient client = new WebClient();
            string url = ConfigurationManager.AppSettings["JsonMateriales"];
            string json = client.DownloadString(url);
            
            dynamic asj = JsonConvert.DeserializeObject(json);

            JArray a = JArray.Parse(asj.LT_DETALLE.ToString());

            foreach (dynamic item in a)
            {
                ComboBoxItem cbxitem = new ComboBoxItem();

                cbxitem.Content = item.MAKTX;
                cbxitem.Tag = item.MATNR;

                cbxMaterial.Items.Add(cbxitem);
            }
            //Conexion con SAP-------------------------------------

            //Carga del Logo segun Planta

            cambiarLogo();

            //Carga del Logo segun Planta
        }
        public void cambiarLogo()
        {
            doc.Load(System.IO.Directory.GetCurrentDirectory() + @"\centrosBlumar.xml");

            XmlNodeList nodoValor = doc.GetElementsByTagName("seleccion");
            int valorCentro = 001;
            foreach (XmlNode node in nodoValor[0].FirstChild)
            {
                valorCentro = Convert.ToInt32(node.InnerText);
            }
            String auxili = valorCentro.ToString().PadLeft(3, '0');
            //testeo.Content = auxili;
            imgLogo.Source = new BitmapImage(new Uri(@"/img/" + auxili + ".jpg", UriKind.Relative));
        }

        public Boolean buscarExisteCorreWB(String lotesito, String porcionsita)
        {
            string buscarExisteCorre = ConfigurationManager.AppSettings["buscarExisteCorre"];
            WebRequest request = WebRequest.Create("" + buscarExisteCorre + lotesito + "&porcion=" + porcionsita + "");
            WebResponse response = request.GetResponse();

            Stream dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);

            string responseFromServer = reader.ReadToEnd();
            String respuesta = responseFromServer.ToString();
            //testeo.Content = respuesta;

            reader.Close();
            response.Close();

            if (respuesta == "true")
                return true;
            else
                return false;
        }

        public int siExisteCoWB(String lotesito, String porcionsita)
        {
            string siExisteCo = ConfigurationManager.AppSettings["siExisteCo"];
            WebRequest request = WebRequest.Create("" + siExisteCo + lotesito + "&porcion=" + porcionsita + "");
            WebResponse response = request.GetResponse();

            Stream dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);

            string responseFromServer = reader.ReadToEnd();

            int respuesta = Convert.ToInt32(responseFromServer.ToString());
            //testeo.Content = respuesta.ToString();

            reader.Close();
            response.Close();
            return respuesta;
        }

        public ArrayList comprobLotFechWB(String lote)
        {
            string comprobLotFech = ConfigurationManager.AppSettings["comprobLotFech"];
            WebRequest request = WebRequest.Create("" + comprobLotFech + lote + "");
            WebResponse response = request.GetResponse();

            Stream dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);

            string responseFromServer = reader.ReadToEnd();

            String respuesta = responseFromServer.ToString();
            //testeo.Content = respuesta;

            reader.Close();
            response.Close();

            dynamic json = JsonConvert.DeserializeObject(respuesta);

            ArrayList retorno = new ArrayList();
            //retorno.Add(json.id);
            retorno.Add(json.lote);
            retorno.Add(json.porcion);
            retorno.Add(json.fecha);
            retorno.Add(json.correlativo);

            return retorno;
        }

        

        public Boolean insertarNuevoCorrelativoWB(String lote, String porcion, String fecha, String correlativo)
        {
            Boolean resp = false;
            String jsonInsert = "{ \"lote\": " + "\"" + lote + "\"" + ", \"porcion\": " + "\"" + porcion + "\"" + ", \"fecha\": " + "\"" + fecha + "\"" + ", \"correlativo\": " + "\"" + correlativo + "\"" + " }";
            testeo.Content = jsonInsert;

            try
            {
                string insertarNuevoCorrelativo = ConfigurationManager.AppSettings["insertarNuevoCorrelativo"];
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

        //DateTime fechaCreacion = DateTime.Parse("02/04/2019 12:00:00 AM");
        public Boolean llenarCampos()
        {
            BlumarWS.rfcNetSoapClient cliente = new BlumarWS.rfcNetSoapClient();

            BlumarWS.request_ZMOV_10000 imp = new BlumarWS.request_ZMOV_10000();

            imp.CHARG = txtLote.Text;
            imp.MATNR = cbxMaterial.SelectedValue.ToString();

            BlumarWS.responce_ZMOV_10000 respuesta = new BlumarWS.responce_ZMOV_10000();
            BlumarWS.ZMOV_10002_IR_CHARG[] lotes = new BlumarWS.ZMOV_10002_IR_CHARG[1];

            lotes[0] = new BlumarWS.ZMOV_10002_IR_CHARG();
            lotes[0].SIGN = "I";
            lotes[0].OPTION = "EQ";
            lotes[0].LOW = txtLote.Text;
            
            BlumarWS.request_ZMOV_10002 imp2 = new BlumarWS.request_ZMOV_10002();

            imp2.IV_SPRAS = "ES";
            imp2.IV_PROC = "01";
            imp2.IV_MATNR = cbxMaterial.SelectedValue.ToString();
            imp2.IR_CHARG = lotes;

            BlumarWS.responce_ZMOV_10002 respuesta2 = new BlumarWS.responce_ZMOV_10002();

            try
            {
                respuesta = cliente.ZMOV_10000(imp);
            }
            catch (Exception e)
            {
                MessageBox.Show("" + e);
            }
            

            try
            {
                /*
                try
                {
                    var rechazito = Array.FindIndex(respuesta.CHAR_OF_BATCH, row => row.ATNAM == "ZRECHAZO");
                    lblRechazo.Content = respuesta.CHAR_OF_BATCH[rechazito].ATWTB;
                }
                catch (Exception)
                {
                    lblRechazo.Content = "";
                }
                */

                try
                {
                    var envase = Array.FindIndex(respuesta.CHAR_OF_BATCH, row => row.ATNAM == "ZTIPHU");
                    //MessageBox.Show(respuesta.CHAR_OF_BATCH[envase].ATWTB);
                    
                    cbxEnvase.Text = respuesta.CHAR_OF_BATCH[envase].ATWTB;
                    
                }
                catch (Exception)
                {
                    
                    if(respuesta.CHAR_OF_BATCH.Length == 0)
                    {
                        MessageBox.Show("Lote no existe");
                        cbxMaterial.SelectedValue = 0;
                    }
                    else
                    {
                        //MessageBox.Show("Lote no contiene envase seleccionado");
                    }
                }

                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("No se encuentra el Lote");
                cbxMaterial.SelectedValue = 0;
                return false;
            }
            
        }

        public DateTime validar10002()
        {
            BlumarWS.rfcNetSoapClient cliente = new BlumarWS.rfcNetSoapClient();
            
            BlumarWS.ZMOV_10002_IR_CHARG[] lotes = new BlumarWS.ZMOV_10002_IR_CHARG[1];

            lotes[0] = new BlumarWS.ZMOV_10002_IR_CHARG();
            lotes[0].SIGN = "I";
            lotes[0].OPTION = "EQ";
            lotes[0].LOW = txtLote.Text;

            BlumarWS.request_ZMOV_10002 imp2 = new BlumarWS.request_ZMOV_10002();

            imp2.IV_SPRAS = "ES";
            imp2.IV_PROC = "01";
            imp2.IV_MATNR = cbxMaterial.SelectedValue.ToString();
            imp2.IR_CHARG = lotes;

            BlumarWS.responce_ZMOV_10002 respuesta2 = new BlumarWS.responce_ZMOV_10002();

            DateTime fechaCreacion = DateTime.Now;
            try
            {
                respuesta2 = cliente.ZMOV_10002(imp2);
            }
            catch (Exception e)
            {
                MessageBox.Show("" + e);
            }

            try
            {
                try
                {
                    fechaCreacion = DateTime.Parse(respuesta2.LT_DATOS_LOTES[0].FABRICACION);
                    return fechaCreacion;
                }
                catch (Exception e )
                {
                    MessageBox.Show("No se encuentra la fecha de creacion del Lote " + e);
                    return fechaCreacion;
                }

            }
            catch(Exception e)
            {
                MessageBox.Show(" No se encuentra la fecha de creacion ----- " + e);
                return fechaCreacion;
            }
        }
        
        public Boolean validaFecha()
        {
            var fechaCr = validar10002();
            if (fechaPicker.SelectedDate.Value >= fechaCr)
            {
                //testeo.Content = "Fecha mayor";
                return true;
            }
            else
            {
                //testeo.Content = "Fecha menor";
                return false;
            }
        }
        
        private void BtnTest_Click(object sender, RoutedEventArgs e)
        {
            tablaExcel ventanaExcel = new tablaExcel();
            ventanaExcel.Visibility = Visibility.Visible;
        }

        public int crearCodQr()
        {
            
            if (!validaCampos("codigoQr"))
            {
                return 0;
            }
            /*
            String rechazado = "";
            rechazado = lblRechazo.Content.ToString();
            if(rechazado != "")
            {
                rechazado = "x";
            }
            */
            String tipos = "";
            dynamic asd = cbxTipos;
            if (cbxTipos.SelectedValue.ToString() == "1")
                tipos = "x";
            else
                tipos = cbxTipos.SelectedValue.ToString();
            int correlativo = Convert.ToInt16(cantEtiquetas.Value.ToString());
            String codigoQr = "";
            codigoQr = "{" +
                "\"lt\":\"" + txtLote.Text + "\"," +
                "\"mt\":\"" + cbxMaterial.SelectedValue + "\"," +
                "\"pr\":\"" + txtPorcion.Text + "\"," +
                "\"cr\":\"" + txtCorrelativo.Text + "\"," +
                "\"f\":\"" + fechaPicker.SelectedDate.Value.ToString("dd-MM-yyyy") + "\"," +
                "\"ev\":\"" + cbxEnvase.SelectedValue + "\"," +
                //"\"almacen\":\"" + cbxAlmacen.Text + "\"," +
                "\"jl\":\"" + lblJuliano.Content.ToString() + "\"," +
                "\"an\":\"" + lblAño.Content.ToString() + "\"," +
                "\"or\":\"" + cbxOrigenes.SelectedValue.ToString() + "\"," +
                "\"rz\":\"" + tipos + "\"," +
                "\"ct\":\"" + cantToneladas.Value.ToString() + "\"" +
                "}";
            //or
            //testeo.Content = tipos;


            QrEncoder encoder = new QrEncoder(ErrorCorrectionLevel.M);
            QrCode qrCode;
            encoder.TryEncode(codigoQr, out qrCode);
            WriteableBitmapRenderer wRenderer = new WriteableBitmapRenderer(new FixedModuleSize(2, QuietZoneModules.Two), Colors.Black, Colors.White);
            WriteableBitmap wBitmap = new WriteableBitmap(104, 104, 170, 170, PixelFormats.Gray8, null);
            wRenderer.Draw(wBitmap, qrCode.Matrix);

            imgQrCode.Source = wBitmap;
            
            return 0;
        }

        private bool validaCampos(String cod)
        {
            if (cod == "codigoQr")
            {
                if(
                    cbxMaterial.SelectedIndex != 0 &&
                    txtLote.Text != "" &&
                    txtLote.Text.Length >= 5 &&
                    txtLote.Text.Length <= 6 &&
                    txtPorcion.Text != "00" &&
                    txtPorcion.Text != "" &&
                    txtCorrelativo.Text != "000" &&
                    fechaPicker.SelectedDate.Value != null &&
                    cbxTipos.SelectedValue != null &&
                    cbxOrigenes.SelectedValue != null
                    )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            else if (cod == "imprimir")
            {
                if(
                    cbxMaterial.SelectedIndex != 0 &&
                    txtLote.Text != "" &&
                    txtLote.Text.Length >= 5 &&
                    txtLote.Text.Length <= 6 &&
                    txtPorcion.Text != "00" &&
                    txtPorcion.Text != "" &&
                    txtCorrelativo.Text != "000" &&
                    //cbxEnvase.SelectedIndex != null &&
                    cantEtiquetas.Value != 0 &&
                    fechaPicker.SelectedDate.Value != null &&
                    //cbxAlmacen.SelectedIndex + 1 != 0
                    cbxTipos.SelectedValue != null &&
                    cbxOrigenes.SelectedValue != null
                    )
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Ingrese todo los datos solicitados para Imprimir");
                }
            }
            return false;
        }

        public string print(int correlativoEtiq)
        {
            //application startuppath ejemplo en wpf
            string etiqueta = System.IO.Directory.GetCurrentDirectory() + @"\zpl\etiqueta.zpl";
            string tempoEtiqueta = System.IO.Directory.GetCurrentDirectory() + @"\zpl\tempoEtiqueta.zpl";
            var lines = File.ReadAllLines(etiqueta);
            string salida = "";

            foreach(var line in lines)
            {
                salida += line;
            }
            crearCodQr();
            /*
            String rechazado = "";
            rechazado = lblRechazo.Content.ToString();
            if (rechazado != "")
            {
                rechazado = "x";
            }
            */
            String tipos = "";
            if (cbxTipos.SelectedValue.ToString() == "1")
                tipos = "x";
            else
                tipos = cbxTipos.SelectedValue.ToString();

            String qrcode = "{" +
                "\"lt\":\"" + txtLote.Text + "\"," +
                "\"mt\":\"" + cbxMaterial.SelectedValue + "\"," +
                "\"pr\":\"" + txtPorcion.Text + "\"," +
                "\"cr\":\"" + correlativoEtiq.ToString().PadLeft(3, '0') + "\"," +
                "\"f\":\"" + fechaPicker.SelectedDate.Value.ToString("dd-MM-yyyy") + "\"," +
                "\"ev\":\"" + cbxEnvase.SelectedValue + "\"," +
                //"\"almacen\":\"" + cbxAlmacen.Text + "\"," +
                "\"jl\":\"" + lblJuliano.Content.ToString() + "\"," +
                "\"an\":\"" + lblAño.Content.ToString() + "\"," +
                "\"or\":\"" + cbxOrigenes.SelectedValue.ToString() + "\"," +
                "\"rz\":\"" + tipos + "\"," +
                "\"ct\":\"" + cantToneladas.Value.ToString() + "\"" +
                "}";

            //string qrcode = "" + txtLote.Text + "-" + txtPorcion.Text + "-" + correlativoEtiq.ToString() + "-" + lblJuliano.Content + "-" + lblAño.Content + "-" + (cbxMaterial.SelectedIndex + 1).ToString() + "-" + (cbxEnvase.SelectedIndex + 1).ToString() + "-" + (cbxAlmacen.SelectedIndex + 1).ToString() + "";

            salida = salida.Replace("[lote]", txtLote.Text);
            salida = salida.Replace("[correlativo]", correlativoEtiq.ToString().PadLeft(3, '0'));
            salida = salida.Replace("[porcion]", txtPorcion.Text.PadLeft(2, '0'));
            salida = salida.Replace("[juliana]", lblJuliano.Content.ToString());
            salida = salida.Replace("[anno]", lblAño.Content.ToString());
            salida = salida.Replace("[qrCode]", qrcode);

            
            try
            {
                if (File.Exists(tempoEtiqueta))
                {
                    File.Delete(tempoEtiqueta);
                }

                using(FileStream fs = File.Create(tempoEtiqueta))
                {
                    if(salida != null)
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

        private void BtnImprimir_Click(object sender, RoutedEventArgs e)
        {
            txtPorcion.Text = txtPorcion.Text.PadLeft(2, '0');
            txtCorrelativo.Text = txtCorrelativo.Text.PadLeft(3, '0');

            if (!validaFecha())
            {
                MessageBox.Show("La fecha debe ser mayor a la fecha de creacion del Lote");
                fechaPicker.SelectedDate = DateTime.Today;
                return;
            }

            if (!validaCampos("imprimir"))
            {
                return;
            }

            int cant = Convert.ToInt16(cantEtiquetas.Value.ToString());
            int correlativ = Convert.ToInt32(txtCorrelativo.Text);
            int copias = Convert.ToInt32(copiasEtiquetas.Value.ToString());

            for (int i=cant; i>0; i--)
            {
                for (int j=copias; j>0; j--)
                {
                    print(correlativ);
                }
                correlativ++;
            }
            if(reimpresion.IsChecked ?? false)
            {
                txtLote.RaiseEvent(new RoutedEventArgs(LostFocusEvent, txtLote));
            }
            else
            {
                insertarNuevoCorrelativoWB(txtLote.Text, txtPorcion.Text, fechaPicker.SelectedDate.Value.ToString("yyyyMMdd"), (Convert.ToInt32(txtCorrelativo.Text) + cant - 1).ToString());
                txtLote.RaiseEvent(new RoutedEventArgs(LostFocusEvent, txtLote));
            }
            
        }

        private void FechaPicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            txtPorcion.Text = txtPorcion.Text.PadLeft(2, '0');
            lblAño.Content = fechaPicker.SelectedDate.Value.ToString("yy");
            lblJuliano.Content = Convert.ToDateTime(fechaPicker.SelectedDate.Value).DayOfYear.ToString().PadLeft(3, '0');
            txtCorrelativo.Text = txtCorrelativo.Text.PadLeft(3, '0');
            Boolean rell = true;
            rell = rellenarPorcion();
            llenarCorrelativo();
            crearCodQr();
        }

        private void TxtLote_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtPorcion.Text = txtPorcion.Text.PadLeft(2, '0');
            lblLote.Content = txtLote.Text;
            txtCorrelativo.Text = txtCorrelativo.Text.PadLeft(3, '0');
            crearCodQr();
        }

        private void CbxEnvase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtPorcion.Text = txtPorcion.Text.PadLeft(2, '0');
            txtCorrelativo.Text = txtCorrelativo.Text.PadLeft(3, '0');
            crearCodQr();
        }

        private void CbxMaterial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtPorcion.Text = txtPorcion.Text.PadLeft(2, '0');
            txtCorrelativo.Text = txtCorrelativo.Text.PadLeft(3, '0');

            if(txtLote.Text != "" && cbxMaterial.SelectedIndex != 0)
            {
                llenarCampos();
            }
            crearCodQr();
        }

        private void TxtPorcion_TextChanged(object sender, TextChangedEventArgs e)
        {
            lblPorcion.Content = txtPorcion.Text;
            txtCorrelativo.Text = txtCorrelativo.Text.PadLeft(3, '0');
            crearCodQr();
        }

        private void CantEtiquetas_ValueChanged(object sender, RoutedPropertyChangedEventArgs<decimal> e)
        {
            txtPorcion.Text = txtPorcion.Text.PadLeft(2, '0');
            txtCorrelativo.Text = txtCorrelativo.Text.PadLeft(3, '0');
            lblCorrelativo.Content = txtCorrelativo.Text;
            crearCodQr();
        }

        private void TxtLote_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtLote.Text != "" && cbxMaterial.SelectedValue != null && cbxMaterial.SelectedIndex != 0)
            {
                llenarCampos();
            }
            rellenarPorcion();
            llenarCorrelativo();
            crearCodQr();
        }

        private void BtnAdmin_Click(object sender, RoutedEventArgs e)
        {
            if(adminPassword.Visibility == Visibility.Hidden)
            {
                adminPassword.Visibility = Visibility.Visible;
            }
            else
            {
                adminPassword.Password = "";
                adminPassword.Visibility = Visibility.Hidden;
                unlockCentroCkb.Visibility = Visibility.Hidden;
                unlockCentroCkb.IsChecked = false;
            }
            //ingresoAdmin ventanaAdmin = new ingresoAdmin();
            //ventanaAdmin.Visibility = Visibility.Visible;
        }

        private void AdminPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (adminPassword.Password.ToString() == "bm2019*.")
                unlockCentroCkb.Visibility = Visibility.Visible;
        }
        
        private void UnlockCentroCkb_Checked(object sender, RoutedEventArgs e)
        {
            comboCentro.IsHitTestVisible = true;
            comboCentro.Focusable = true;
        }

        private void UnlockCentroCkb_Unchecked(object sender, RoutedEventArgs e)
        {
            comboCentro.IsHitTestVisible = false;
            comboCentro.Focusable = false;
        }

        private void ComboCentro_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String valor = comboCentro.SelectedValue.ToString();
            doc.Load(System.IO.Directory.GetCurrentDirectory() + @"\centrosBlumar.xml");
            foreach (XmlElement element in doc.SelectNodes("//seleccion"))
            {
                foreach (XmlElement element1 in element)
                {
                    XmlNode newValue = doc.CreateElement("idCentro");
                    newValue.InnerText = valor;
                    element.ReplaceChild(newValue, element1);

                    doc.Save(System.IO.Directory.GetCurrentDirectory() + @"\centrosBlumar.xml");
                }
            }
            llenarCentro();
            cambiarLogo();
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            llenarCentro();
            llenarCorrelativo();
            cambiarLogo();
        }

        public void refreshTest()
        {
            llenarCentro();
            llenarCorrelativo();
            cambiarLogo();
        }

        public void llenarCentro()
        {
            doc.Load(System.IO.Directory.GetCurrentDirectory() + @"\centrosBlumar.xml");

            XmlNodeList nodoValor = doc.GetElementsByTagName("seleccion");
            //testeo.Content = nodoValor.ToString();
            foreach (XmlNode node in nodoValor[0].FirstChild)
            {
                int valorCentro = Convert.ToInt32(node.InnerText);
                //MessageBox.Show(valorCentro.ToString());
                comboCentro.SelectedIndex = valorCentro - 1;
            }
        }

        public void llenarCorrelativo()
        {
            //Boolean existCorre = false;
            if (txtLote.Text != "" && txtPorcion.Text != "00" && txtLote.Text.Length >= 5 && txtPorcion.Text != "")
            {
                //cambio2503
                if (buscarExisteCorreWB(txtLote.Text, txtPorcion.Text))
                {
                    int numPor = Convert.ToInt32(txtPorcion.Text);
                    //cambio2503
                    int numCon = (siExisteCoWB(txtLote.Text, numPor.ToString())) + 1;
                    txtCorrelativo.Text = (numCon).ToString().PadLeft(3, '0');
                }
                else
                {
                    /*
                    txtCorrelativo.Text = "001";
                    MessageBox.Show("fui yo");
                    */
                } 
            }
        }
        
        public Boolean rellenarPorcion()
        {
            Boolean resp = false;
            Boolean auxibo = false;
            try
            {
                
                if (txtLote.Text != "")
                {
                    
                    //comprobar el lote con la fecha
                    //cambio2503
                    ArrayList auxiliar = comprobLotFechWB(txtLote.Text);
                    //MessageBox.Show(auxiliar.Count.ToString());

                    if(auxiliar.Count > 0)
                    {
                        if (fechaPicker.SelectedDate == DateTime.MinValue)
                        {
                            fechaPicker.SelectedDate = DateTime.Parse(auxiliar[2].ToString());
                        }


                        //si existe trae la misma porcion
                        if (txtLote.Text == auxiliar[0].ToString() && fechaPicker.SelectedDate.Value == DateTime.Parse(auxiliar[2].ToString()))
                        {
                            txtPorcion.Text = auxiliar[1].ToString().PadLeft(2, '0');
                            txtCorrelativo.Text = (Convert.ToInt32(auxiliar[3].ToString()) + 1).ToString().PadLeft(3, '0');
                        }
                        else
                        {
                            txtPorcion.Text = "01";
                        }

                        //si existe con otra fecha aumentar porcion
                        if (txtLote.Text == auxiliar[0].ToString() && fechaPicker.SelectedDate.Value > DateTime.Parse(auxiliar[2].ToString()))
                        {
                            txtPorcion.Text = (Convert.ToInt32(auxiliar[1].ToString()) + 1).ToString().PadLeft(2, '0');
                            txtCorrelativo.Text = (Convert.ToInt32(auxiliar[3].ToString()) + 1).ToString().PadLeft(3, '0');
                        }

                        //imprimir comprobar porcion / comprobar que la fecha no sea anterior para un mismo lote

                        if (txtLote.Text == auxiliar[0].ToString() && fechaPicker.SelectedDate.Value < DateTime.Parse(auxiliar[2].ToString()))
                        {
                            txtPorcion.Text = "00";
                            MessageBox.Show("Fecha ingresada anterior a la porcion del Lote.");
                            fechaPicker.SelectedDate = DateTime.Today;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Lote no Encontrado");
                        auxibo = true;
                    }
                    
                }
                resp = true;
            }
            catch (Exception)
            {
                //if(auxibo != true)
                    //MessageBox.Show("Nuevo Lote");
                
                txtPorcion.Text = "01";
                txtCorrelativo.Text = "001";
                resp = false;
            }
            return resp;
        }

        private void TxtCorrelativo_SelectionChanged(object sender, RoutedEventArgs e)
        {
            lblCorrelativo.Content = txtCorrelativo.Text;
            crearCodQr();
            int maximo = 0;
            Int32.TryParse(txtCorrelativo.Text, out maximo);
            if (maximo > 366)
                txtCorrelativo.Text = "366";
        }

        private void CbxMaterial_LostFocus(object sender, RoutedEventArgs e)
        {
            txtLote.RaiseEvent(new RoutedEventArgs(LostFocusEvent, txtLote));
        }

        private void CbxTipos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtPorcion.Text = txtPorcion.Text.PadLeft(2, '0');
            txtCorrelativo.Text = txtCorrelativo.Text.PadLeft(3, '0');
            crearCodQr();
        }

        private void CbxOrigenes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtPorcion.Text = txtPorcion.Text.PadLeft(2, '0');
            txtCorrelativo.Text = txtCorrelativo.Text.PadLeft(3, '0');
            crearCodQr();
        }
    }
}
