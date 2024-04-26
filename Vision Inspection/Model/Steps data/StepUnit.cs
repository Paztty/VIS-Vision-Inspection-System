using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vision_Inspection.Core;

namespace Vision_Inspection.Model.Steps_data
{
    public class StepUnit : ObseriableObject
    {
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
		private string _Mode;

		public string Mode
		{
			get { return _Mode; }
			set
			{
				if (_Mode != value)
				{
					_Mode = value;
					NotifyPropertyChanged(nameof(Mode));
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


	}
}
