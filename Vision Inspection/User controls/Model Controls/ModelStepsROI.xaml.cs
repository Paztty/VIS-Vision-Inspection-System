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
using Vision_Inspection.Core;

namespace Vision_Inspection.User_controls.Model_Controls
{
    /// <summary>
    /// Interaction logic for ModelStepsROI.xaml
    /// </summary>
    public partial class ModelStepsROI : UserControl
    {
        private ModelStepROIContent _Content { get;} = new ModelStepROIContent();
        public ModelStepsROI()
        {
            InitializeComponent();
            this.DataContext = _Content;
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_Content.IsReadOnly)
            {
                Point location = e.GetPosition(this);
                VerticalLine.X1 = 0;
                VerticalLine.X2 = this.ActualWidth;
                VerticalLine.Y1 = location.Y;
                VerticalLine.Y2 = location.Y;

                HorizontalLine.X1 = location.X;
                HorizontalLine.X2 = location.X;
                HorizontalLine.Y1 = 0;
                HorizontalLine.Y2 = this.ActualHeight;
            }
        }

        public void SetPoint(Point globalLocation)
        {
           var location = GlobalSize.GetDisplayLocation((globalLocation.X, globalLocation.Y), (this.ActualWidth, this.ActualHeight));

            VerticalLine.X1 = 0;
            VerticalLine.X2 = this.ActualWidth;
            VerticalLine.Y1 = location.Y;
            VerticalLine.Y2 = location.Y;

            HorizontalLine.X1 = location.X;
            HorizontalLine.X2 = location.X;
            HorizontalLine.Y1 = 0;
            HorizontalLine.Y2 = this.ActualHeight;
        }
    }
}
