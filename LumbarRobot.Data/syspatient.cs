using System;
using System.Collections;

namespace LumbarRobot.Data
{
	#region Syspatient

	/// <summary>
	/// Syspatient object for NHibernate mapped table 'syspatient'.
	/// </summary>
	public class Syspatient
	{
		#region Member Variables
		
		protected string _id;
		protected string _userId;
		protected string _diagnoseTypeId;
		protected string _userName;
		protected string _sex;
		protected int? _weight;
		protected DateTime _birthDay;
		protected string _afftectedHand;
		protected DateTime _opDate;
		protected string _cardNo;
		protected string _patientCarNo;
		protected string _doctorID;
		protected int? _bodyHeight;
		protected string _note;
		protected DateTime? _lastTime;
        protected string _pinYin;

     

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

		public virtual string UserId
		{
			get { return _userId; }
			set
			{
				if ( value != null && value.Length > 96)
					throw new ArgumentOutOfRangeException("Invalid value for UserId", value, value.ToString());
				_userId = value;
			}
		}

		public virtual string DiagnoseTypeId
		{
			get { return _diagnoseTypeId; }
			set
			{
				if ( value != null && value.Length > 30)
					throw new ArgumentOutOfRangeException("Invalid value for DiagnoseTypeId", value, value.ToString());
				_diagnoseTypeId = value;
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

		public virtual string Sex
		{
			get { return _sex; }
			set
			{
				if ( value != null && value.Length > 3)
					throw new ArgumentOutOfRangeException("Invalid value for Sex", value, value.ToString());
				_sex = value;
			}
		}

		public virtual int? Weight
		{
			get { return _weight; }
			set { _weight = value; }
		}

		public virtual DateTime BirthDay
		{
			get { return _birthDay; }
			set { _birthDay = value; }
		}

		public virtual string AfftectedHand
		{
			get { return _afftectedHand; }
			set
			{
				if ( value != null && value.Length > 6)
					throw new ArgumentOutOfRangeException("Invalid value for AfftectedHand", value, value.ToString());
				_afftectedHand = value;
			}
		}

		public virtual DateTime OpDate
		{
			get { return _opDate; }
			set { _opDate = value; }
		}

		public virtual string CardNo
		{
			get { return _cardNo; }
			set
			{
				if ( value != null && value.Length > 54)
					throw new ArgumentOutOfRangeException("Invalid value for CardNo", value, value.ToString());
				_cardNo = value;
			}
		}

		public virtual string PatientCarNo
		{
			get { return _patientCarNo; }
			set
			{
				if ( value != null && value.Length > 150)
					throw new ArgumentOutOfRangeException("Invalid value for PatientCarNo", value, value.ToString());
				_patientCarNo = value;
			}
		}

		public virtual string DoctorID
		{
			get { return _doctorID; }
			set
			{
				if ( value != null && value.Length > 150)
					throw new ArgumentOutOfRangeException("Invalid value for DoctorID", value, value.ToString());
				_doctorID = value;
			}
		}

		public virtual int? BodyHeight
		{
			get { return _bodyHeight; }
			set { _bodyHeight = value; }
		}

		public virtual string Note
		{
			get { return _note; }
			set
			{
				if ( value != null && value.Length > 65535)
					throw new ArgumentOutOfRangeException("Invalid value for Note", value, value.ToString());
				_note = value;
			}
		}

		public virtual DateTime? LastTime
		{
			get { return _lastTime; }
			set { _lastTime = value; }
		}


        public virtual string PinYin
        {
            get { return _pinYin; }
            set { _pinYin = value; }
        }

		#endregion
	}
	#endregion
}