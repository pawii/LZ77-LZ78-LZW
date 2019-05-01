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

namespace WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { 
        private byte[] archiveBytes;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ArchiveBtn_Click(object sender, RoutedEventArgs e)
        {
            archiveBytes = LZ77.Lz77Compress(sourseTextTB.Text.GetBytes());
            var unarchiveBytesString = sourseTextTB.Text.GetBytes().ToBinaryString();
            var archiveBytesString = archiveBytes.ToBinaryString();
            archiveTextTB.Text = "Unarchive bytes: \n" + FormatString(unarchiveBytesString) + 
                "\nArchive bytes: \n" + FormatString(archiveBytesString);
        }

        private void UnarchiveBtn_Click(object sender, RoutedEventArgs e)
        {
            sourseTextTB.Text = LZ77.Lz77Decompress(archiveBytes).ToSingleString();
        }

        private string FormatString(string source)
        {
            StringBuilder result = new StringBuilder(source.Length);

            for (int i = 0; i < source.Length; i++)
            {
                result.Append(source[i]);
                if (i % 40 == 39)
                {
                    result.Append('\n');
                }
            }

            return result.ToString();
        }

        private void LzwBtn_Click(object sender, RoutedEventArgs e)
        {
            var wind = new LZW_Window();
            wind.Show();
        }

        private void Lz78Btn_Click(object sender, RoutedEventArgs e)
        {
            var wind = new LZ78_Window();
            wind.Show();
        }
    }
}
