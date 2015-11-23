using System;
using System.Collections;

namespace LumbarRobot.Data
{
	#region Games

	/// <summary>
	/// Games object for NHibernate mapped table 'games'.
	/// </summary>
	public class Games
	{
		#region Member Variables
		
		protected int _id;
		protected string _gameName;
		protected string _gameIco;

		#endregion


		#region Public Properties

		public virtual int Id
		{
			get {return _id;}
			set {_id = value;}
		}

		public virtual string GameName
		{
			get { return _gameName; }
			set
			{
				if ( value != null && value.Length > 135)
					throw new ArgumentOutOfRangeException("Invalid value for GameName", value, value.ToString());
				_gameName = value;
			}
		}

		public virtual string GameIco
		{
			get { return _gameIco; }
			set
			{
				if ( value != null && value.Length > 135)
					throw new ArgumentOutOfRangeException("Invalid value for GameIco", value, value.ToString());
				_gameIco = value;
			}
		}

		

		#endregion
	}
	#endregion
}