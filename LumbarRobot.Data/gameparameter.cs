using System;
using System.Collections;

namespace LumbarRobot.Data
{
	#region Gameparameter

	/// <summary>
	/// Gameparameter object for NHibernate mapped table 'gameparameter'.
	/// </summary>
	public class Gameparameter
	{
		#region Member Variables
		
		protected string _id;
		protected string _patientId;
		protected string _gameId;
		protected int? _speed;
		protected int? _force;
		protected string _hardLevel;
		protected string _leftMaxPoint;
		protected string _rightMaxPoint;
		protected string _frontMaxPoint;
		protected string _backMaxPoint;
		protected string _upMaxPoint;
		protected string _downMaxPoint;
		protected string _minTiggerForce;
		protected string _maxTiggerForce;

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

		public virtual string GameId
		{
			get { return _gameId; }
			set
			{
				if ( value != null && value.Length > 96)
					throw new ArgumentOutOfRangeException("Invalid value for GameId", value, value.ToString());
				_gameId = value;
			}
		}

		public virtual int? Speed
		{
			get { return _speed; }
			set { _speed = value; }
		}

		public virtual int? Force
		{
			get { return _force; }
			set { _force = value; }
		}

		public virtual string HardLevel
		{
			get { return _hardLevel; }
			set
			{
				if ( value != null && value.Length > 135)
					throw new ArgumentOutOfRangeException("Invalid value for HardLevel", value, value.ToString());
				_hardLevel = value;
			}
		}

		public virtual string LeftMaxPoint
		{
			get { return _leftMaxPoint; }
			set
			{
				if ( value != null && value.Length > 135)
					throw new ArgumentOutOfRangeException("Invalid value for LeftMaxPoint", value, value.ToString());
				_leftMaxPoint = value;
			}
		}

		public virtual string RightMaxPoint
		{
			get { return _rightMaxPoint; }
			set
			{
				if ( value != null && value.Length > 135)
					throw new ArgumentOutOfRangeException("Invalid value for RightMaxPoint", value, value.ToString());
				_rightMaxPoint = value;
			}
		}

		public virtual string FrontMaxPoint
		{
			get { return _frontMaxPoint; }
			set
			{
				if ( value != null && value.Length > 135)
					throw new ArgumentOutOfRangeException("Invalid value for FrontMaxPoint", value, value.ToString());
				_frontMaxPoint = value;
			}
		}

		public virtual string BackMaxPoint
		{
			get { return _backMaxPoint; }
			set
			{
				if ( value != null && value.Length > 135)
					throw new ArgumentOutOfRangeException("Invalid value for BackMaxPoint", value, value.ToString());
				_backMaxPoint = value;
			}
		}

		public virtual string UpMaxPoint
		{
			get { return _upMaxPoint; }
			set
			{
				if ( value != null && value.Length > 135)
					throw new ArgumentOutOfRangeException("Invalid value for UpMaxPoint", value, value.ToString());
				_upMaxPoint = value;
			}
		}

		public virtual string DownMaxPoint
		{
			get { return _downMaxPoint; }
			set
			{
				if ( value != null && value.Length > 135)
					throw new ArgumentOutOfRangeException("Invalid value for DownMaxPoint", value, value.ToString());
				_downMaxPoint = value;
			}
		}

		public virtual string MinTiggerForce
		{
			get { return _minTiggerForce; }
			set
			{
				if ( value != null && value.Length > 135)
					throw new ArgumentOutOfRangeException("Invalid value for MinTiggerForce", value, value.ToString());
				_minTiggerForce = value;
			}
		}

		public virtual string MaxTiggerForce
		{
			get { return _maxTiggerForce; }
			set
			{
				if ( value != null && value.Length > 135)
					throw new ArgumentOutOfRangeException("Invalid value for MaxTiggerForce", value, value.ToString());
				_maxTiggerForce = value;
			}
		}

		

		#endregion
	}
	#endregion
}