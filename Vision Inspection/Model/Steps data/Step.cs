using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vision_Inspection.Core;

namespace Vision_Inspection.Model.Steps_data
{
    public class Step:ObseriableObject
    {
		private int _No;

		public int No
		{
			get { return _No; }
			set
			{
				if (_No != value)
				{
					_No = value;
					NotifyPropertyChanged(nameof(No));
				}
			}
		}
		private string _Name;

		public string Name
		{
			get { return _Name; }
			set
			{
				if (_Name != value)
				{
					_Name = value;
					NotifyPropertyChanged(nameof(Name));
				}
			}
		}
		private string _CMD;

		public string CMD
		{
			get { return _CMD; }
			set
			{
				if (_CMD != value)
				{
					_CMD = value;
					NotifyPropertyChanged(nameof(CMD));
				}
			}
		}
		private string _Location;

		public string Location
		{
			get { return _Location; }
			set
			{
				if (_Location != value)
				{
					_Location = value;
					NotifyPropertyChanged(nameof(Location));
				}
			}
		}
		private string _Result;

		public string Result
		{
			get { return _Result; }
			set
			{
				if (_Result != value)
				{
					_Result = value;
					NotifyPropertyChanged(nameof(Result));
				}
			}
		}
		private ObservableCollection<StepUnit> _Units = new ObservableCollection<StepUnit>();

		public ObservableCollection<StepUnit> Units
		{
			get { return _Units; }
			set
			{
				if (_Units != value)
				{
					_Units = value;
					NotifyPropertyChanged(nameof(Units));
				}
			}
		}

		public Step()
		{
			for (int i = 0; i < 10; i++)
			{
				this.Units.Add(new StepUnit()
				{
					Name = i.ToString(),
                    Mode = "MODE DEVICE",
                    Result = "PASS",

                });
			}
		}
	}
}
