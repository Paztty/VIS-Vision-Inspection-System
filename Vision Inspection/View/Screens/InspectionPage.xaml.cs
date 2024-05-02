using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
                    Location = new Point(random.Next(1920),random.Next(1080)),
                    Result = "PASS",
                });
            }
        }
        System.Timers.Timer updatePostimer = new()
        {
            AutoReset = false,
            Interval = 100,
        };
        private void dgrSteps_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updatePostimer.Elapsed -= UpdatePostimer_Elapsed;
            updatePostimer.Stop();
            updatePostimer.Start();
            updatePostimer.Elapsed += UpdatePostimer_Elapsed;
        }

        private void UpdatePostimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                var grid = dgrSteps;
                if (grid != null)
                {
                    try
                    {
                        var step = (Step)grid.SelectedItem;
                        if (step != null)
                        {
                            modelViewer.SetPoint(step.Location);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }));
            updatePostimer.Stop();
        }
    }
}
