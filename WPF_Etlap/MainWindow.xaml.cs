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

namespace WPF_Etlap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FoodService service;
        public MainWindow()
        {
            InitializeComponent();
            this.service = new FoodService();
            Read();
        }

        private void Read()
        {
            dataGridMenu.ItemsSource = service.GetAll();
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            FoodForm form = new FoodForm(service);
            form.Closed += (_, _) => Read();
            form.ShowDialog();
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            Food selectedFood = dataGridMenu.SelectedItem as Food;
            if (selectedFood == null)
            {
                MessageBox.Show("Válasszon ki egy elemet a törléshez!");
                return;
            }

            MessageBoxResult result = MessageBox.Show($"Törölni szeretné az alábbi ételt: {selectedFood.Name}?",
                "Törlés", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (this.service.Delete(selectedFood.Id))
                {
                    MessageBox.Show("Sikeres törlés!");
                }
                else
                {
                    MessageBox.Show("Sikertelen törlés!");
                }
                Read();
            }
        }

        private void buttonPercentIncrease_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxPercentIncrease.Text.Trim() == "")
            {
                MessageBox.Show("Nem adott meg százalékot!");
                return;
            }

            Food selectedFood = dataGridMenu.SelectedItem as Food;
            double percent = double.Parse(textBoxPercentIncrease.Text.Trim());
            if (selectedFood == null)
            {
                service.UpdateByPercent(percent);
            } else
            {
                service.UpdateByPercent(selectedFood.Id, percent, selectedFood);
            }
            textBoxPercentIncrease.Text = "";
            Read();
        }

        private void buttonFtIncrease_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxFtIncrease.Text.Trim() == "")
            {
                MessageBox.Show("Nem adott meg értéket az emeléshez!");
                return;
            }

            Food selectedFood = dataGridMenu.SelectedItem as Food;
            int forint = int.Parse(textBoxFtIncrease.Text.Trim());
            if (selectedFood == null)
            {
                service.UpdateByForint(forint);
            }
            else
            {
                service.UpdateByForint(selectedFood.Id, forint, selectedFood);
            }
            textBoxFtIncrease.Text = "";
            Read();
        }
    }
}