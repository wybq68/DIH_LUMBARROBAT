using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LumbarRobot.Data
{
    #region Prescription

    /// <summary>
    /// Prescription object for NHibernate mapped table 'prescription'.
    /// </summary>
    public class Prescription
    {
        #region Member Variables
        private string _id;
        private string _prescriptionName;
        private string _traingoal;
        private string _timesAll;
        private string _patientId;
        private string _userId;
        private DateTime? _opTime;
        private string _note;
        private DateTime? _lastTime;
      
        #endregion

        #region Public Properties
        public virtual string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public virtual string PrescriptionName
        {
            get { return _prescriptionName; }
            set { _prescriptionName = value; }
        }
        public virtual string Traingoal
        {
            get { return _traingoal; }
            set { _traingoal = value; }
        }

        public virtual string TimesAll
        {
            get { return _timesAll; }
            set { _timesAll = value; }
        }

        public virtual string PatientId
        {
            get { return _patientId; }
            set { _patientId = value; }
        }

        public virtual string UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        public virtual DateTime? OpTime
        {
            get { return _opTime; }
            set { _opTime = value; }
        }

        public virtual string Note
        {
            get { return _note; }
            set { _note = value; }
        }

        public virtual DateTime? LastTime
        {
            get { return _lastTime; }
            set { _lastTime = value; }
        }
        #endregion
    }
    #endregion 
}
