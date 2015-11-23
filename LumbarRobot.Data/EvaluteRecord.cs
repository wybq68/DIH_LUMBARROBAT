using System;
using System.Collections;

namespace LumbarRobot.Data
{
	#region EvaluteRecord

	/// <summary>
	/// EvaluteRecord object for NHibernate mapped table 'EvaluteRecord'.
	/// </summary>
	public class EvaluteRecord
	{
		#region Member Variables
		
		protected int _evaluteID;
		protected string _evaluteName;
		protected DateTime? _evaluteDate;
        protected string _patientID;
		#endregion


		#region Public Properties

		public virtual int EvaluteID
		{
			get {return _evaluteID;}
			set {_evaluteID = value;}
		}

		public virtual string EvaluteName
		{
			get { return _evaluteName; }
			set
			{
				if ( value != null && value.Length > 150)
					throw new ArgumentOutOfRangeException("Invalid value for EvaluteName", value, value.ToString());
				_evaluteName = value;
			}
		}

		public virtual DateTime? EvaluteDate
		{
			get { return _evaluteDate; }
			set { _evaluteDate = value; }
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

		#endregion
	}
	#endregion
}