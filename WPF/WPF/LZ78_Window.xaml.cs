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
using WPF.LZ78;

namespace WPF
{
    /// <summary>
    /// Логика взаимодействия для LZ78_Window.xaml
    /// </summary>
    public partial class LZ78_Window : Window
    {
        LZ78_Controller controller = new LZ78_Controller();

        public LZ78_Window()
        {
            InitializeComponent();
        }

        private void CreateString(LZ78_Coding_String data)
        {
            WrapPanel wp = new WrapPanel();
            wp.Orientation = Orientation.Horizontal;

            TextBox dictionary = new TextBox();
            dictionary.Text = data.Dictionary;
            dictionary.Width = 120;
            dictionary.TextAlignment = TextAlignment.Center;
            TextBox remainingMessage = new TextBox();
            remainingMessage.Text = data.RemainingMessage.ToString();
            remainingMessage.Width = 120;
            remainingMessage.TextAlignment = TextAlignment.Center;
            TextBox Prefix = new TextBox();
            Prefix.Text = data.FoundPrefix.ToString();
            Prefix.Width = 35;
            Prefix.TextAlignment = TextAlignment.Center;
            TextBox Code = new TextBox();
            Code.Text = data.Code.ToString();
            Code.Width = 35;
            Code.TextAlignment = TextAlignment.Center;
            //TextBox bits = new TextBox();
            //bits.Width = 50;
            //bits.TextAlignment = TextAlignment.Center;
            //bits.Text = data.Bytes;
            //TextBox dictionary = new TextBox();
            //dictionary.Width = 70;
            //dictionary.TextAlignment = TextAlignment.Center;
            //dictionary.Text = data.Dictionary;

            wp.Children.Add(dictionary);
            wp.Children.Add(remainingMessage);
            wp.Children.Add(Prefix);
            wp.Children.Add(Code);
            //wp.Children.Add(bits);
            //wp.Children.Add(dictionary);

            compressContainer.Children.Add(wp);
        }

        private void CreateDecodingString(LZ78_Coding_String data)
        {
            WrapPanel wp = new WrapPanel();
            wp.Orientation = Orientation.Horizontal;

            TextBox dictionary = new TextBox();
            dictionary.Text = data.Dictionary;
            dictionary.Width = 120;
            dictionary.TextAlignment = TextAlignment.Center;
            TextBox remainingMessage = new TextBox();
            remainingMessage.Text = data.RemainingMessage.ToString();
            remainingMessage.Width = 120;
            remainingMessage.TextAlignment = TextAlignment.Center;
            TextBox Prefix = new TextBox();
            Prefix.Text = data.FoundPrefix;
            Prefix.Width = 35;
            Prefix.TextAlignment = TextAlignment.Center;
            TextBox Code = new TextBox();
            Code.Text = data.Code.ToString();
            Code.Width = 35;
            Code.TextAlignment = TextAlignment.Center;
            //TextBox bits = new TextBox();
            //bits.Width = 50;
            //bits.TextAlignment = TextAlignment.Center;
            //bits.Text = data.Bytes;
            //TextBox dictionary = new TextBox();
            //dictionary.Width = 70;
            //dictionary.TextAlignment = TextAlignment.Center;
            //dictionary.Text = data.Dictionary;

            wp.Children.Add(dictionary);
            wp.Children.Add(remainingMessage);
            wp.Children.Add(Prefix);
            wp.Children.Add(Code);
            //wp.Children.Add(bits);
            //wp.Children.Add(dictionary);

            decompressContainer.Children.Add(wp);

            outputTB.Text += data.RemainingMessage;
        }

        private void StartCmprBtn_Click(object sender, RoutedEventArgs e)
        {
            controller.InitCompress(sourceTB.Text);
            iterationCmprBtn.IsEnabled = true;
            allCmprBtn.IsEnabled = true;
            startCmprBtn.IsEnabled = false;
        }

        private void IterationCmprBtn_Click(object sender, RoutedEventArgs e)
        {
            CreateString(controller.GetCodingString());

            if (controller.IsCompressionEnd)
            {
                iterationCmprBtn.IsEnabled = false;
                allCmprBtn.IsEnabled = false;
                startDecmprBtn.IsEnabled = true;
            }
        }

        private void AllCmprBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (var str in controller.GetRemainingCodingStrings())
            {
                CreateString(str);
            }

            iterationCmprBtn.IsEnabled = false;
            allCmprBtn.IsEnabled = false;
            startDecmprBtn.IsEnabled = true;
        }

        private void StartDecmprBtn_Click(object sender, RoutedEventArgs e)
        {
            controller.InitDecompress();

            startDecmprBtn.IsEnabled = false;
            iterationDecmprBtn.IsEnabled = true;
            allDecmprBtn.IsEnabled = true;
        }

        private void IterationDecmprBtn_Click(object sender, RoutedEventArgs e)
        {
            CreateDecodingString(controller.GetDecodingString());

            if (controller.IsDecompressionEnd)
            {
                iterationDecmprBtn.IsEnabled = false;
                allDecmprBtn.IsEnabled = false;
            }
        }

        private void AllDecmprBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (var str in controller.GetRemainingDecodingStrings())
            {
                CreateDecodingString(str);
            }

            iterationDecmprBtn.IsEnabled = false;
            allDecmprBtn.IsEnabled = false;
        }
    }
}
