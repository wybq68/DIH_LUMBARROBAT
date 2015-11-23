using System;
using System.Collections;

namespace LumbarRobot.Data
{
	#region Sysuserinfo

	/// <summary>
	/// Sysuserinfo object for NHibernate mapped table 'sysuserinfo'.
	/// </summary>
	public class Sysuserinfo
	{
		#region Member Variables
		
		protected string _id;
		protected string _userCode;
		protected string _userName;
		protected string _userType;
		protected string _sysPassWord;
		protected DateTime _lastTime;

		#endregion


		#region Public Properties

		public virtual string Id
		{
			get {return _id;}
			set
			{
				if ( value != null && value.Length > 96)
					throw new ArgumentOutOfRangeException("Invalid value for Id", value, value.ToString());
				_id = value;
			}
		}

		public virtual string UserCode
		{
			get { return _userCode; }
			set
			{
				if ( value != null && value.Length > 90)
					throw new ArgumentOutOfRangeException("Invalid value for UserCode", value, value.ToString());
				_userCode = value;
			}
		}

		public virtual string UserName
		{
			get { return _userName; }
			set
			{
				if ( value != null && value.Length > 150)
					throw new ArgumentOutOfRangeException("Invalid value for UserName", value, value.ToString());
				_userName = value;
			}
		}

		public virtual string UserType
		{
			get { return _userType; }
			set
			{
				if ( value != null && value.Length > 6)
					throw new ArgumentOutOfRangeException("Invalid value for UserType", value, value.ToString());
				_userType = value;
			}
		}

		public virtual string SysPassWord
		{
			get { return _sysPassWord; }
			set
			{
				if ( value != null && value.Length > 60)
					throw new ArgumentOutOfRangeException("Invalid value for SysPassWord", value, value.ToString());
				_sysPassWord = value;
			}
		}

		public virtual DateTime LastTime
		{
			get { return _lastTime; }
			set { _lastTime = value; }
		}

		

		#endregion
	}
	#endregion
}