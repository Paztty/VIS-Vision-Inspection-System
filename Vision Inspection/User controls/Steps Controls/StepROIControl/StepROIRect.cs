using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vision_Inspection.Core;

namespace Vision_Inspection.User_controls.Steps_Controls.StepROIControl
{
    public class StepROIRect:ObseriableObject
    {
		private bool _IsFocused;

		public bool IsFocused
		{
			get { return _IsFocused; }
			set
			{
				if (_IsFocused != value)
				{
					_IsFocused = value;
					NotifyPropertyChanged(nameof(IsFocused));
				}
			}
		}

	}
}
