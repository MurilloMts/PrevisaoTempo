/************************************************************
 * Simples app de previsão do tem para 4 dias               *
 * Fonte site do INPE  (http://servicos.cptec.inpe.br/XML/) *
 *              Desenvolvido por- Murillo Matos             *
 ************************************************************/              

using System;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml;

namespace Previsão_do_tempo
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        XmlDocument docXml = new XmlDocument();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ComboBox1.Items.Clear();
                string cidade = RemoverAcentos(TxtCidade.Text.Split(',')[0].Replace(" ", "%20").ToLower()); 
                 
                docXml.Load("http://servicos.cptec.inpe.br/XML/listaCidades?city="+ cidade);
                foreach (XmlNode node in docXml.GetElementsByTagName("cidade"))
                {
                    ComboBox1.Items.Add(node["nome"].InnerText + "," + node["uf"].InnerText + "," + node["id"].InnerText);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }

        private void ComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                TxtCidade.Text = "";
                docXml.Load("http://servicos.cptec.inpe.br/XML/cidade/" + ComboBox1.SelectedItem.ToString().Split(',')[2] + "/previsao.xml");
                LblDia1.Content = "";
                LblDia2.Content = "";
                LblDia3.Content = "";
                LblDia4.Content = "";
                foreach (XmlNode node in docXml.GetElementsByTagName("previsao"))
                {
                    string valores = "Dia: " + node["dia"].InnerText.Split('-')[2] + " Máxima: " + node["maxima"].InnerText + "° Graus " + " Mínima: " + node["minima"].InnerText + "° Graus";

                    if (LblDia1.Content.Equals("")) { LblDia1.Content = valores; valores = ""; }
                    if (LblDia2.Content.Equals("")) { LblDia2.Content = valores; valores = ""; }
                    if (LblDia3.Content.Equals("")) { LblDia3.Content = valores; valores = ""; }
                    if (LblDia4.Content.Equals("")) { LblDia4.Content = valores; valores = ""; }

                }
                foreach (XmlNode node in docXml.GetElementsByTagName("cidade"))
                {
                    LblTituloCidade.Content = node["nome"].InnerText;
                    //LblInfo.Content = node["tempo"].InnerText;
                    break;
                }
                foreach (XmlNode node in docXml.GetElementsByTagName("previsao"))
                {
                    LblInfo.Content = node["tempo"].InnerText;
                    string info = LblInfo.Content.ToString();
                    
                    switch (info)
                    {
                        case "ec": LblInfo.Content = "Encoberto com Chuvas Isoladas"; ImageTempo.Source = new BitmapImage(new Uri("pack://application:,,,/Img/ec.png"));  break;
                        case "ci": LblInfo.Content = "Chuvas Isoladas"; ImageTempo.Source = new BitmapImage(new Uri("pack://application:,,,/Img/ci.png")); break;
                        case "c": LblInfo.Content = "Chuva"; ImageTempo.Source = new BitmapImage(new Uri("pack://application:,,,/Img/c.png")); break;
                        case "in": LblInfo.Content = "Instável"; ImageTempo.Source = new BitmapImage(new Uri("pack://application:,,,/Img/in.png")); break;
                        case "pp": LblInfo.Content = "Possibilidade de Pancadas de Chuva"; ImageTempo.Source = new BitmapImage(new Uri("pack://application:,,,/Img/pp.png")); break;
                        case "cm": LblInfo.Content = "Chuva pela Manhã"; ImageTempo.Source = new BitmapImage(new Uri("pack://application:,,,/Img/cm.png")); break;
                        case "cn": LblInfo.Content = "Chuva a Noite"; ImageTempo.Source = new BitmapImage(new Uri("pack://application:,,,/Img/cn.png")); break;
                        case "pt": LblInfo.Content = "Pancadas de Chuva a Tarde"; ImageTempo.Source = new BitmapImage(new Uri("pack://application:,,,/Img/pt.png")); break;
                        case "pm": LblInfo.Content = "Pancadas de Chuva pela Manhã"; ImageTempo.Source = new BitmapImage(new Uri("pack://application:,,,/Img/pm.png")); break;
                        case "np": LblInfo.Content = "Nublado e Pancadas de Chuva"; ImageTempo.Source = new BitmapImage(new Uri("pack://application:,,,/Img/np.png")); break;
                        case "pc": LblInfo.Content = "Pancadas de Chuva"; ImageTempo.Source = new BitmapImage(new Uri("pack://application:,,,/Img/pc.png")); break;
                        case "pn": LblInfo.Content = "Parcialmente Nublado"; ImageTempo.Source = new BitmapImage(new Uri("pack://application:,,,/Img/pn.png")); break;
                        case "cv": LblInfo.Content = "Chuvisco"; ImageTempo.Source = new BitmapImage(new Uri("pack://application:,,,/Img/cv.png")); break;
                        case "ch": LblInfo.Content = "Chuvoso"; ImageTempo.Source = new BitmapImage(new Uri("pack://application:,,,/Img/ch.png")); break;
                        case "t": LblInfo.Content = "Tempestade"; ImageTempo.Source = new BitmapImage(new Uri("pack://application:,,,/Img/t.png")); break;
                        case "ps": LblInfo.Content = "Predomínio de Sol"; ImageTempo.Source = new BitmapImage(new Uri("pack://application:,,,/Img/ps.png")); break;
                        case "e": LblInfo.Content = "Encoberto"; ImageTempo.Source = new BitmapImage(new Uri("pack://application:,,,/Img/e.png")); break;
                        case "n": LblInfo.Content = "Nublado"; ImageTempo.Source = new BitmapImage(new Uri("pack://application:,,,/Img/n.png")); break;
                        case "cl": LblInfo.Content = "Céu Claro"; ImageTempo.Source = new BitmapImage(new Uri("pack://application:,,,/Img/cl.png")); break;
                        case "nv": LblInfo.Content = "Nevoeiro"; ImageTempo.Source = new BitmapImage(new Uri("pack://application:,,,/Img/nv.png")); break;
                        case "g": LblInfo.Content = "Geada"; ImageTempo.Source = new BitmapImage(new Uri("pack://application:,,,/Img/g.png")); break;
                        case "ne": LblInfo.Content = "Neve"; ImageTempo.Source = new BitmapImage(new Uri("pack://application:,,,/Img/ne.png")); break;
                        case "nd": LblInfo.Content = "Não Definido"; ImageTempo.Source = new BitmapImage(new Uri("pack://application:,,,/Img/nd.png")); break;
                        case "pnt": LblInfo.Content = "Pancadas de Chuva a Noite"; ImageTempo.Source = new BitmapImage(new Uri("pack://application:,,,/Img/pnt.png")); break;
                        case "psc": LblInfo.Content = "Possibilidade de Chuva"; ImageTempo.Source = new BitmapImage(new Uri("pack://application:,,,/Img/psc.png")); break;
                        case "pcm": LblInfo.Content = "Possibilidade de Chuva pela Manhã"; ImageTempo.Source = new BitmapImage(new Uri("pack://application:,,,/Img/pcm.png")); break;
                        case "pct": LblInfo.Content = "Possibilidade de Chuva a Tarde"; ImageTempo.Source = new BitmapImage(new Uri("pack://application:,,,/Img/pct.png")); break;
                        case "pcn": LblInfo.Content = "Possibilidade de Chuva a Noite"; ImageTempo.Source = new BitmapImage(new Uri("pack://application:,,,/Img/pcn.png")); break;
                        case "npt": LblInfo.Content = "Nublado com Pancadas a Tarde"; ImageTempo.Source = new BitmapImage(new Uri("pack://application:,,,/Img/npt.png")); break;
                        case "npn": LblInfo.Content = "Nublado com Pancadas a Noite"; ImageTempo.Source = new BitmapImage(new Uri("pack://application:,,,/Img/npn.png")); break;
                        case "ncn": LblInfo.Content = "Nublado com Possibilidade de Chuva a Noite"; ImageTempo.Source = new BitmapImage(new Uri("pack://application:,,,/Img/ncn.png")); break;
                        case "nct": LblInfo.Content = "Nublado com Possibilidade de Chuva a Tarde"; ImageTempo.Source = new BitmapImage(new Uri("pack://application:,,,/Img/nct.png")); break;
                        case "ncm": LblInfo.Content = "Nublado com Possibilidade de Chuva pela Manhã"; ImageTempo.Source = new BitmapImage(new Uri("pack://application:,,,/Img/ncm.png")); break;
                        case "npm": LblInfo.Content = "Nublado com Pancadas pela Manhã"; ImageTempo.Source = new BitmapImage(new Uri("pack://application:,,,/Img/npm.png")); break;
                        case "npp": LblInfo.Content = "Nublado com Possibilidade de Chuva"; ImageTempo.Source = new BitmapImage(new Uri("pack://application:,,,/Img/npp.png")); break;
                        case "vn": LblInfo.Content = "Variação de Nebulosidade"; ImageTempo.Source = new BitmapImage(new Uri("pack://application:,,,/Img/vn.png")); break;
                        case "ct": LblInfo.Content = "Chuva a Tarde"; ImageTempo.Source = new BitmapImage(new Uri("pack://application:,,,/Img/ct.png")); break;
                        case "ppn": LblInfo.Content = "Possibilidade de Pancadas de Chuva a Noite"; ImageTempo.Source = new BitmapImage(new Uri("pack://application:,,,/Img/ppn.png")); break;
                        case "ppt": LblInfo.Content = "Possibilidade de Pancadas de Chuva a Tarde"; ImageTempo.Source = new BitmapImage(new Uri("pack://application:,,,/Img/ppt.png")); break;
                        case "ppm": LblInfo.Content = "Possibilidade de Pancadas de Chuva pela Manhã"; ImageTempo.Source = new BitmapImage(new Uri("pack://application:,,,/Img/ppm.png")); break;
                        default: LblInfo.Content = "Não Definido";  break;
                            
                    }
                    break;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        public static string RemoverAcentos(string texto) // Remove acentos 
        {

            string s = texto.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            for (int k = 0; k < s.Length; k++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(s[k]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(s[k]);
                }
            }
            return sb.ToString();
        }
    }
}
