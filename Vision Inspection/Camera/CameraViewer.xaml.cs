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

namespace Vision_Inspection.Camera
{
    /// <summary>
    /// Interaction logic for CameraViewer.xaml
    /// </summary>
    public partial class CameraViewer : UserControl
    {
        Camera Camera { get; set; } = Camera.GetCamera();
        public CameraViewer()
        {
            InitializeComponent();
            this.DataContext = Camera;
        }
        public void StartCamera()
        {
            Camera = Camera.GetCamera();
            Camera.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Camera.GetBackground();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Camera.CreateForegroundMark();
        }
    }
}
