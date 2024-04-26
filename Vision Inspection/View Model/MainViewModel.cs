using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Vision_Inspection.Core;
using Vision_Inspection.View.Screens;

namespace Vision_Inspection.View_Model
{
    public class MainViewModel:ObseriableObject
    {
        // Views 
        public InspectionPage Inspection { get; set; } = new InspectionPage();
        public ModelPage Model { get; set; } = new ModelPage();


        // Main view content
        public ContentControl Content { get; set; } = new ContentControl();


        //Relay commands
        ////Select page commands
        private ICommand _SelectPage;

        public ICommand SelectPage
        {
            get { return _SelectPage; }
            set
            {
                if (_SelectPage != value)
                {
                    _SelectPage = value;
                    NotifyPropertyChanged(nameof(SelectPage));
                }
            }
        }

        // Main view model init
        public MainViewModel()
        {
            RelayCommandSetup();
        }

        //Relay Command init
        private void RelayCommandSetup()
        {
            SelectPage = new RelayCommand<UserControl>(SetContent, CanSetContent);
        }


        //Command Excuter
        private void SetContent(object obj)
        {
            if (Content.Content != obj) Content.Content = obj;
        }
        private bool CanSetContent(object obj)
        {
            if(obj == null) return false;
            return true;
        }
    }
}
