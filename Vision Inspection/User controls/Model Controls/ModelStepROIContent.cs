using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vision_Inspection.Core;

namespace Vision_Inspection.User_controls.Model_Controls
{
    public class ModelStepROIContent:ObseriableObject
    {
		private bool _IsReadOnly = true;

		public bool IsReadOnly
		{
			get { return _IsReadOnly; }
			set
			{
				if (_IsReadOnly != value)
				{
					_IsReadOnly = value;
					NotifyPropertyChanged(nameof(IsReadOnly));
				}
			}
		}

	}
}
