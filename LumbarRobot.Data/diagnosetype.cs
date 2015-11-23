using System;
using System.Collections;

namespace LumbarRobot.Data
{
	#region Diagnosetype

	/// <summary>
	/// Diagnosetype object for NHibernate mapped table 'diagnosetype'.
	/// </summary>
	public class Diagnosetype
	{
		#region Member Variables
		
		protected string _id;
		protected string _diagnoseTypeName;

		#endregion


		#region Public Properties

		public virtual string Id
		{
			get {return _id;}
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for Id", value, value.ToString());
				_id = value;
			}
		}

		public virtual string DiagnoseTypeName
		{
			get { return _diagnoseTypeName; }
			set
			{
				if ( value != null && value.Length > 150)
					throw new ArgumentOutOfRangeException("Invalid value for DiagnoseTypeName", value, value.ToString());
				_diagnoseTypeName = value;
			}
		}

		

		#endregion
	}
	#endregion
}