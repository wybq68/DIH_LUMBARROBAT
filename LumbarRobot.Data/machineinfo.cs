using System;
using System.Collections;

namespace LumbarRobot.Data
{
	#region Machineinfo

	/// <summary>
	/// Machineinfo object for NHibernate mapped table 'machineinfo'.
	/// </summary>
	public class Machineinfo
	{
		#region Member Variables
		
		protected int _id;

		#endregion


		#region Public Properties

		public virtual int Id
		{
			get {return _id;}
			set {_id = value;}
		}

		

		#endregion
	}
	#endregion
}