using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LumbarRobot.Common
{
    public class TrainRecord
    {
        public virtual string PatientId
        {
            get;
            set;
        }

        public int ModeId
        {
            get;
            set;
        }

        public int ActionId
        {
            get;
            set;
        }

        public int IsFit
        {
            get;
            set;
        }

        public virtual DateTime ExerciseDate
        {
            get;
            set;
        }

        public virtual DateTime StartTime
        {
            get;
            set;
        }

        public virtual DateTime EndTime
        {
            get;
            set;
        }

        public int Speed
        {
            get;
            set;
        }

        public int Force
        {
            get;
            set;
        }

        public virtual int MinAngle
        {
            get;
            set;
        }

        public virtual int MaxAngle
        {
            get;
            set;
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

        public int GroupNum
        {
            get;
            set;
        }

        public virtual int Times
        {
            get;
            set;
        }

        public virtual int FactTimes
        {
            get;
            set;
        }

        public long ExMinutes
        {
            get;
            set;
        }

        public float AvgForce
        {
            get;
            set;
        }

        public float MaxForce
        {
            get;
            set;
        }

        public float PushRodValue
        {
            get;
            set;
        }

        public double[] TargetLine
        {
            get;
            set;
        }

        public float[] RealLine
        {
            get;
            set;
        }

        public float[] ForceLine
        {
            get;
            set;
        }

        public GroupRecord[] GroupRecords
        {
            set;
            get;
        }
    }

    public class GroupRecord
    {
        public int GroupNum { get; set; }
        public float Min { get; set; }
        public float Max { get; set; }
    }
}
