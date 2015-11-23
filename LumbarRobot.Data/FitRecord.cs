using System;
using System.Collections;

namespace LumbarRobot.Data
{
	#region FitRecord

	/// <summary>
	/// FitRecord object for NHibernate mapped table 'FitRecord'.
	/// </summary>
	public class FitRecord
	{
		#region Member Variables
		
		protected string _id;
		protected int _modeID;
		protected float _pushRodValue;
		protected string _patientID;
		protected int _maxAngle;
		protected int _minAngle;
		protected DateTime _createTime;
        private DateTime _exerciseDate;

      

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

		public virtual int ModeID
		{
			get { return _modeID; }
			set { _modeID = value; }
		}

		public virtual float PushRodValue
		{
			get { return _pushRodValue; }
			set { _pushRodValue = value; }
		}

		public virtual string PatientID
		{
			get { return _patientID; }
			set
			{
				if ( value != null && value.Length > 96)
					throw new ArgumentOutOfRangeException("Invalid value for PatientID", value, value.ToString());
				_patientID = value;
			}
		}

		public virtual int MaxAngle
		{
			get { return _maxAngle; }
			set { _maxAngle = value; }
		}

		public virtual int MinAngle
		{
			get { return _minAngle; }
			set { _minAngle = value; }
		}

		public virtual DateTime CreateTime
		{
			get { return _createTime; }
			set { _createTime = value; }
		}

        public virtual DateTime ExerciseDate
        {
            get { return _exerciseDate; }
            set { _exerciseDate = value; }
        }

		#endregion
	}
	#endregion
}