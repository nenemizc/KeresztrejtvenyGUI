using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace KeresztrejtvenyGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            for (int i = 6; i < 16; i++)
            {
                cbSorokSzama.Items.Add(i);
                cbOszlopokSzama.Items.Add(i);
            }
            for (int i = 1; i < 11; i++)
            {
                cbAllomanyIndexe.Items.Add(i);
            }
            cbSorokSzama.SelectedItem = 15;
            cbOszlopokSzama.SelectedItem = 15;
            cbAllomanyIndexe.SelectedItem = 3;
        }

        private void btnLetrehoz_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < Relytveny.Children.Count;)
            {
                if (Relytveny.Children[i] is TextBox)
                {
                    Relytveny.Children.Remove(Relytveny.Children[i] as TextBox);
                }
                else
                {
                    i++;
                }
            }
            int sorokSzama = (int)cbSorokSzama.SelectedItem;
            int oszlopokSzama = (int)cbOszlopokSzama.SelectedItem;
            for (int r = 0; r < sorokSzama; r++)
            {
                for (int c = 0; c < oszlopokSzama; c++)
                {
                    TextBox aktualisTextBox = new TextBox();
                    aktualisTextBox.Margin = new Thickness(20 + c * 22, 90 + r * 22, 0, 0);
                    aktualisTextBox.Text = "-";
                    aktualisTextBox.Width = 20;
                    aktualisTextBox.TextAlignment = TextAlignment.Center;
                    Relytveny.Children.Add(aktualisTextBox);
                }
            }
        }

        private void btnMentes_Click(object sender, RoutedEventArgs e)
        {
            List<string> ki = new();
            int allomanyIndex = (int)cbAllomanyIndexe.SelectedItem;
            int oszlopokSzama = (int)cbOszlopokSzama.SelectedItem;
            string sor = "";
            int sz = 0;
            foreach (var item in Relytveny.Children)
            {
                if (item is TextBox)
                {
                    TextBox aktualisTB = (TextBox)item;
                    sor += aktualisTB.Text;
                    sz++;
                }
                if (sz == oszlopokSzama)
                {
                    ki.Add(sor);
                    sz = 0;
                    sor = "";
                }
            }
            try
            {
                File.WriteAllLines($"kr{allomanyIndex}.txt", ki);
                MessageBox.Show("A keresztrejtvény mentése sikeres");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        
        }
    }
}