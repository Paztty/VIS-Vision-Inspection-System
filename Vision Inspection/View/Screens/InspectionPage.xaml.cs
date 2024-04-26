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
using Vision_Inspection.Model.Steps_data;

namespace Vision_Inspection.View.Screens
{
    /// <summary>
    /// Interaction logic for InspectionPage.xaml
    /// </summary>
    public partial class InspectionPage : UserControl
    {
		public List<Step> Steps { get; set; } = new List<Step> { };

		public InspectionPage()
        {
            InitializeComponent();
            this.DataContext = this;
            Random random = new Random();
            for (int i = 0; i < 200; i++)
            {
                Steps.Add(new Step()
                {
                    No = i,
                    Name = "Component " + i.ToString(),
                    Location = random.Next(1920) + " : "+ random.Next(1080),
                    Result = "PASS",
                });
            }
        }
    }
}
