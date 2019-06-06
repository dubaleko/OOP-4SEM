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

namespace Lab12
{
    public enum ObjectType { Square, Circle };

    public partial class FactoryDialog : Window
    {
        int objectType;
        int count;

        MainWindow mainWindow;
        CircleFactory cfactory;
        SquareFactory sfactory;
        IGeometryObject currentObject;

        public FactoryDialog(MainWindow mainWindow)
        {
            InitializeComponent();
            InitFactories();
            this.mainWindow = mainWindow;
        }

        private void InitFactories()
        {
            cfactory = new CircleFactory();
            sfactory = new SquareFactory();
        }

        private void OnCreateObject(object sender, RoutedEventArgs e)
        {
            try
            { 
                count = Convert.ToInt32(countTextBox.Text.ToString());

                for (int i = 0; i < count; i++)
                {
                    switch (objectType)
                    {
                        case (int)ObjectType.Square:
                            {
                                currentObject = sfactory.CreateGeometryObject();
                                mainWindow.flat.Children.Add(CreateSquare());
                                break;
                            }
                        case (int)ObjectType.Circle:
                            {
                                currentObject = cfactory.CreateGeometryObject();
                                mainWindow.flat.Children.Add(CreateCircle());
                                break;
                            }
                    }
                    mainWindow.factory.Items.Add(currentObject.Type);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private UIElement CreateSquare()
        {
            return  new Rectangle
            {
                Fill = new SolidColorBrush(Color.FromRgb(255, 0, 0)),
                Stroke = new SolidColorBrush(Color.FromRgb(128, 56, 128)),
                Width = 20,
                Height = 20
            };
        }

        private UIElement CreateCircle()
        {
            return new Ellipse
            {
                Fill = new SolidColorBrush(Color.FromRgb(100, 255, 65)),
                Stroke = new SolidColorBrush(Color.FromRgb(128, 56, 128)),
                Width = 20,
                Height = 20
            };
        }

        private void OnObjectSelected(object sender, RoutedEventArgs e)
        {
            try
            {
                RadioButton radioButton = (RadioButton)sender;
                objectType = Convert.ToInt32(radioButton.Tag.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CountTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;
        }
    }
}
