using System;
using System.Collections;

namespace LumbarRobot.Data
{
	#region Exerciserecord

	/// <summary>
	/// Exerciserecord object for NHibernate mapped table 'exerciserecord'.
	/// </summary>
	public class Exerciserecord
	{
		#region Member Variables
		
		protected string _id;
		protected string _seeionId;
		protected string _patientId;
		protected int _actionId;
		protected int _modeId;
		protected int _isFit;
		protected DateTime _exerciseDate;
		protected DateTime _startTime;
		protected DateTime _endTime;
		protected int? _speed;
        protected int? _robotForce;
		protected int _minAngle;
		protected int _maxAngle;
        protected int _groupNum;
		protected int _times;
		protected int _factTimes;
		protected long? _exMinutes;
		protected float? _maxforce;
		protected string _record1;
		protected string _record2;
        protected string _record3;
        protected string _groupRecord;
		protected string _temp1;
		protected string _temp2;
		protected string _temp3;
		protected string _temp4;

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

		public virtual string SeeionId
		{
			get { return _seeionId; }
			set
			{
				if ( value != null && value.Length > 96)
					throw new ArgumentOutOfRangeException("Invalid value for SeeionId", value, value.ToString());
				_seeionId = value;
			}
		}

		public virtual string PatientId
		{
			get { return _patientId; }
			set
			{
				if ( value != null && value.Length > 96)
					throw new ArgumentOutOfRangeException("Invalid value for PatientId", value, value.ToString());
				_patientId = value;
			}
		}

		public virtual int ActionId
		{
			get { return _actionId; }
			set { _actionId = value; }
		}

		public virtual int ModeId
		{
			get { return _modeId; }
			set { _modeId = value; }
		}

		public virtual int IsFit
		{
			get { return _isFit; }
			set { _isFit = value; }
		}

		public virtual DateTime ExerciseDate
		{
			get { return _exerciseDate; }
			set { _exerciseDate = value; }
		}

		public virtual DateTime StartTime
		{
			get { return _startTime; }
			set { _startTime = value; }
		}

		public virtual DateTime EndTime
		{
			get { return _endTime; }
			set { _endTime = value; }
		}

		public virtual int? Speed
		{
			get { return _speed; }
			set { _speed = value; }
		}

        public virtual int? RobotForce
		{
            get { return _robotForce; }
            set { _robotForce = value; }
		}

		public virtual int MinAngle
		{
			get { return _minAngle; }
			set { _minAngle = value; }
		}

		public virtual int MaxAngle
		{
			get { return _maxAngle; }
			set { _maxAngle = value; }
		}

        public virtual float RealMinAngle
        {
            get;
            set;
        }

        public virtual float RealMaxAngle
        {
            get;
            set;
        }

        public virtual int GroupNum
        {
            get { return _groupNum; }
            set { _groupNum = value; }
        }

		public virtual int Times
		{
			get { return _times; }
			set { _times = value; }
		}

		public virtual int FactTimes
		{
			get { return _factTimes; }
			set { _factTimes = value; }
		}

		public virtual long? ExMinutes
		{
			get { return _exMinutes; }
			set { _exMinutes = value; }
		}

        public virtual float? AvgForce
        {
            set;
            get;
        }

		public virtual float? Maxforce
		{
			get { return _maxforce; }
			set { _maxforce = value; }
		}

		public virtual string Record1
		{
			get { return _record1; }
			set
			{
                if (value != null && value.Length > 16777215)
                    _record1 = value.Substring(0, 16777215);
                else
				_record1 = value;
			}
		}

		public virtual string Record2
		{
			get { return _record2; }
			set
			{
                if (value != null && value.Length > 16777215)
                    _record2 = value.Substring(0, 16777215);
                else
				_record2 = value;
			}
		}

        public virtual string Record3
        {
            get { return _record3; }
            set
            {
                if (value != null && value.Length > 16777215)
                    _record3 = value.Substring(0, 16777215);
                else
                _record3 = value;
            }
        }

        public virtual string GroupRecord
        {
            get { return _groupRecord; }
            set
            {
                if (value != null && value.Length > 16777215)
                    _groupRecord = value.Substring(0, 16777215);
                else
                    _groupRecord = value;
            }
        }

		public virtual string Temp1
		{
			get { return _temp1; }
			set
			{
				if ( value != null && value.Length > 150)
					throw new ArgumentOutOfRangeException("Invalid value for Temp1", value, value.ToString());
				_temp1 = value;
			}
		}

		public virtual string Temp2
		{
			get { return _temp2; }
			set
			{
				if ( value != null && value.Length > 150)
					throw new ArgumentOutOfRangeException("Invalid value for Temp2", value, value.ToString());
				_temp2 = value;
			}
		}

		public virtual string Temp3
		{
			get { return _temp3; }
			set
			{
				if ( value != null && value.Length > 65535)
					throw new ArgumentOutOfRangeException("Invalid value for Temp3", value, value.ToString());
				_temp3 = value;
			}
		}

		public virtual string Temp4
		{
			get { return _temp4; }
			set
			{
				if ( value != null && value.Length > 65535)
					throw new ArgumentOutOfRangeException("Invalid value for Temp4", value, value.ToString());
				_temp4 = value;
			}
		}

        public virtual float PushRodValue
        {
            get;
            set;
        }

		#endregion
	}
	#endregion
}