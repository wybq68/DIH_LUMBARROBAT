using System;
using System.Collections;

namespace LumbarRobot.Data
{
	#region Gameresult

	/// <summary>
	/// Gameresult object for NHibernate mapped table 'gameresult'.
	/// </summary>
	public class Gameresult
	{
		#region Member Variables
		
		protected int _id;
		protected int _gameId;
		protected string _patientId;
		protected int? _hardLevel;
		protected int? _forceLevel;
		protected int _handType;
		protected int? _setTime;
		protected int? _score;
		protected int? _completeTime;
		protected int? _percentage;
		protected int? _correctlyNum;
		protected string _temp1;
		protected DateTime? _temp2;
		protected string _temp3;
		protected string _temp4;
		protected string _temp5;

		#endregion


		#region Public Properties

		public virtual int Id
		{
			get {return _id;}
			set {_id = value;}
		}

		public virtual int GameId
		{
			get { return _gameId; }
			set { _gameId = value; }
		}

		public virtual string PatientId
		{
			get { return _patientId; }
			set
			{
				if ( value != null && value.Length > 135)
					throw new ArgumentOutOfRangeException("Invalid value for PatientId", value, value.ToString());
				_patientId = value;
			}
		}

		public virtual int? HardLevel
		{
			get { return _hardLevel; }
			set { _hardLevel = value; }
		}

		public virtual int? ForceLevel
		{
			get { return _forceLevel; }
			set { _forceLevel = value; }
		}

		public virtual int HandType
		{
			get { return _handType; }
			set { _handType = value; }
		}

		public virtual int? SetTime
		{
			get { return _setTime; }
			set { _setTime = value; }
		}

		public virtual int? Score
		{
			get { return _score; }
			set { _score = value; }
		}

		public virtual int? CompleteTime
		{
			get { return _completeTime; }
			set { _completeTime = value; }
		}

		public virtual int? Percentage
		{
			get { return _percentage; }
			set { _percentage = value; }
		}

		public virtual int? CorrectlyNum
		{
			get { return _correctlyNum; }
			set { _correctlyNum = value; }
		}

		public virtual string Temp1
		{
			get { return _temp1; }
			set
			{
				if ( value != null && value.Length > 135)
					throw new ArgumentOutOfRangeException("Invalid value for Temp1", value, value.ToString());
				_temp1 = value;
			}
		}

		public virtual DateTime? Temp2
		{
			get { return _temp2; }
			set { _temp2 = value; }
		}

		public virtual string Temp3
		{
			get { return _temp3; }
			set
			{
				if ( value != null && value.Length > 135)
					throw new ArgumentOutOfRangeException("Invalid value for Temp3", value, value.ToString());
				_temp3 = value;
			}
		}

		public virtual string Temp4
		{
			get { return _temp4; }
			set
			{
				if ( value != null && value.Length > 135)
					throw new ArgumentOutOfRangeException("Invalid value for Temp4", value, value.ToString());
				_temp4 = value;
			}
		}

		public virtual string Temp5
		{
			get { return _temp5; }
			set
			{
				if ( value != null && value.Length > 135)
					throw new ArgumentOutOfRangeException("Invalid value for Temp5", value, value.ToString());
				_temp5 = value;
			}
		}

		

		#endregion
	}
	#endregion
}