using System;
using System.Collections;

namespace LumbarRobot.Data
{
	#region EvaluteDetail

	/// <summary>
	/// EvaluteDetail object for NHibernate mapped table 'EvaluteDetail'.
	/// </summary>
	public class EvaluteDetail
	{
		#region Member Variables
		
		protected int _evaluteDetailId;
		protected int? _evaluteId;
		protected float _maxValue;
		protected float _lastValue;
		protected float _fatigueIndex;
		protected DateTime _evaluteDetailDate;
		protected string _record;
		protected string _record2;
        protected string _patientID;
        protected DateTime _evaluteDate;

       
		#endregion


		#region Public Properties

		public virtual int EvaluteDetailId
		{
			get {return _evaluteDetailId;}
			set {_evaluteDetailId = value;}
		}

		public virtual int? EvaluteId
		{
			get { return _evaluteId; }
			set { _evaluteId = value; }
		}

		public virtual float MaxV
		{
			get { return _maxValue; }
			set { _maxValue = value; }
		}

		public virtual float LastValue
		{
			get { return _lastValue; }
			set { _lastValue = value; }
		}

		public virtual float FatigueIndex
		{
			get { return _fatigueIndex; }
			set { _fatigueIndex = value; }
		}

		public virtual DateTime EvaluteDetailDate
		{
			get { return _evaluteDetailDate; }
			set { _evaluteDetailDate = value; }
		}

		public virtual string Record
		{
			get { return _record; }
			set
			{
				if ( value != null && value.Length > 2147483647)
					throw new ArgumentOutOfRangeException("Invalid value for Record", value, value.ToString());
				_record = value;
			}
		}

		public virtual string Record2
		{
			get { return _record2; }
			set
			{
				if ( value != null && value.Length > 2147483647)
					throw new ArgumentOutOfRangeException("Invalid value for Record2", value, value.ToString());
				_record2 = value;
			}
		}

        public virtual string PatientID
        {
            get { return _patientID; }
            set
            {
                if (value != null && value.Length > 96)
                    throw new ArgumentOutOfRangeException("Invalid value for PatientID", value, value.ToString());
                _patientID = value;
            }
        }

        public virtual int ModeId { get; set; }

        public virtual int ActionId { get; set; }

        public virtual int Interval { get; set; }

        public virtual int TargetValue { get; set; }

        public virtual DateTime EvaluteDate
        {
            get { return _evaluteDate; }
            set { _evaluteDate = value; }
        }

		#endregion
	}
	#endregion
}