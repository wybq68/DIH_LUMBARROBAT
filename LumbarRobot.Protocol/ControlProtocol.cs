using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using LumbarRobot.Communication;
using LumbarRobot.Common;

namespace LumbarRobot.Protocol
{
    public class ControlProtocol
    {
        //object _lock = new object();

        public event DataPackReceivedHandler DataPackRecieved;

        MyThreadPool myThreadPool = new MyThreadPool(5);

        IDataCommunication dataCommunication;

        public ControlProtocol(IDataCommunication dataCommunication)
        {
            this.dataCommunication = dataCommunication;
            dataCommunication.DataReceived += new DataRecievedHandler(dataCommunication_DataRecieved);
        }

        #region read

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="e"></param>
        void dataCommunication_DataRecieved(DataRecievedArgs e)
        {
            var buffer = e.Buffer.GetBytes(0xFA, 9);

            while (buffer != null)
            {
                ResponsePackage responsePackage = null;
                //判断数据类型
                int dataType = buffer[1];
                switch (dataType)
                {
                    case ResponseCodes.BendStretchData:
                        responsePackage = GetBendStretchData(buffer, 2);
                        break;
                    case ResponseCodes.RotationData:
                        responsePackage = GetRotationData(buffer, 2);
                        break;
                    case ResponseCodes.OtherData:
                        responsePackage = GetOtherData(buffer, 2);
                        break;
                    case ResponseCodes.Alarm:
                        responsePackage = GetAlarm(buffer, 2);
                        break;
                    case ResponseCodes.Version:
                        responsePackage = GetVersion(buffer, 2);
                        break;
                }

                if (responsePackage != null)
                {
                    if (DataPackRecieved != null)
                    {
                        DataPackReceivedEventArgs args = new DataPackReceivedEventArgs();
                        args.ResponsePackageData = responsePackage;

                        myThreadPool.QueueUserWorkItem((x) => { DataPackRecieved(args); }, null);

                    }
                }

                buffer = e.Buffer.GetBytes(0xFA, 9);
            }
        }

        ResponsePackage GetBendStretchData(byte[] buffer, int offset)
        {
            ResponsePackage responsePackage = new ResponsePackage();
            responsePackage.Code = ResponseCodes.BendStretchData;

            responsePackage.BendStretchAngle = (float)BitConverter.ToInt16(buffer , offset)  / 10;
            responsePackage.BendStretchForce = (float)BitConverter.ToInt16(buffer, offset + 2);
            responsePackage.BendStretchSpeed = (float)BitConverter.ToInt16(buffer, offset + 4) / 10;
            //responsePackage.BendStretchVoltage = Convert.ToSingle(Convert.ToInt32(buffer[offset + 6])<128?Convert.ToInt32(buffer[offset + 6]):(256-Convert.ToInt32(buffer[offset + 6])) * -1) / 10;
            unchecked
            {
                responsePackage.BendStretchVoltage = (SByte)buffer[offset + 6];
            }
            return responsePackage;
        }

        ResponsePackage GetRotationData(byte[] buffer, int offset)
        {
            ResponsePackage responsePackage = new ResponsePackage();
            responsePackage.Code = ResponseCodes.RotationData;

            responsePackage.RotationAngle = (float)BitConverter.ToInt16(buffer, offset) / 10;
            responsePackage.RotationForce = (float)BitConverter.ToInt16(buffer, offset + 2);
            responsePackage.RotationSpeed = (float)BitConverter.ToInt16(buffer, offset + 4) / 10;
            //responsePackage.RotationVoltage = Convert.ToSingle(Convert.ToInt32(buffer[offset + 6]) < 128 ? Convert.ToInt32(buffer[offset + 6]) : (256 - Convert.ToInt32(buffer[offset + 6])) * -1) / 10;
            unchecked
            {
                responsePackage.RotationVoltage = (SByte)buffer[offset + 6];
            }
            return responsePackage;
        }

        ResponsePackage GetOtherData(byte[] buffer, int offset)
        {
            ResponsePackage responsePackage = new ResponsePackage();
            responsePackage.Code = ResponseCodes.OtherData;

            //推杆角度
            unchecked
            {
                responsePackage.PushRodAngle = (SByte)buffer[offset];
                responsePackage.HomingAngle = (SByte)buffer[offset + 1];
            }
            //限位开关
            responsePackage.LimitSwitch = BitConverter.ToInt32(new byte[] { buffer[offset + 4], buffer[offset + 3], buffer[offset + 2], 0 }, 0);

            return responsePackage;
        }

        ResponsePackage GetAlarm(byte[] buffer, int offset)
        {
            ResponsePackage responsePackage = new ResponsePackage();
            responsePackage.Code = ResponseCodes.Alarm;
            responsePackage.Alarm = Convert.ToInt32(buffer[offset]);
            return responsePackage;
        }

        ResponsePackage GetVersion(byte[] buffer, int offset)
        {
            ResponsePackage responsePackage = new ResponsePackage();
            responsePackage.Code = ResponseCodes.Version;
            responsePackage.Version1 = Convert.ToInt32(buffer[offset]);
            responsePackage.Version2 = Convert.ToInt32(buffer[offset + 1]);
            responsePackage.Version3 = Convert.ToInt32(buffer[offset + 2]);

            return responsePackage;
        }

        #endregion

        #region write
        private void WriteData(byte[] data)
        {
            if (data != null)
            {
                byte[] buffer = new byte[data.Length + 4];
                buffer[0] = 0x00;
                buffer[1] = 0x00;
                buffer[2] = 0x06;
                buffer[3] = 0x02;
                for (int i = 0; i < data.Length; i++)
                {
                    buffer[i + 4] = data[i];
                }
                dataCommunication.SendData(buffer);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="package"></param>
        /// <param name="type">0 屈伸，1旋转</param>
        /// <returns></returns>
        private byte[] CreateParamBuffer(ControlParamPackage package, ControlParamType type)
        {
            byte[] result = new byte[8];
            if (type == ControlParamType.BendStretch)
            {
                result[0] = 0x13;

                var temp = BitConverter.GetBytes((short)package.BendStretchForceOrSpeed);
                result[1] = temp[0];
                result[2] = temp[1];

                temp = BitConverter.GetBytes((short)package.BendStretchTarget);
                result[3] = temp[0];
                result[4] = temp[1];

                result[5] = BitConverter.GetBytes(package.BendAngleLimit)[0];
                result[6] = BitConverter.GetBytes(package.StretchAngleLimit)[0];
            }
            else
            {
                result[0] = 0x14;

                var temp = BitConverter.GetBytes((short)package.RotationaForceOrSpeed);
                result[1] = temp[0];
                result[2] = temp[1];

                temp = BitConverter.GetBytes((short)package.RotationTarget);
                result[3] = temp[0];
                result[4] = temp[1];

                result[5] = BitConverter.GetBytes(package.LeftRotationAngleLimit)[0];
                result[6] = BitConverter.GetBytes(package.RightRotationAngleLimit)[0];
            }
            return result;            
        }

        /// <summary>
        /// 发送字节***2015.11.17
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        private byte[] CreateCommandBuffer(ControlCommandPackage package)
        {
            byte[] result = new byte[8];
            if (package.CommandType == CommandType.Version)
            {
                result[0] = 0x81;
                result[1] = BitConverter.GetBytes(Math.Abs((int)package.CommandType))[0];
            }
            else if (package.CommandType == CommandType.PushRodUp || package.CommandType == CommandType.PushRodDown || package.CommandType == CommandType.PushRodStop)
            {
                result[0] = 0x62;
                result[1] = BitConverter.GetBytes((int)package.CommandType)[0];
            }
            else
            {
                result[0] = 0x61;
                result[1] = BitConverter.GetBytes((int)package.CommandType)[0];
                switch (package.CommandType)
                {
                    case CommandType.Assist:
                        result[2] = BitConverter.GetBytes((int)package.CommandParam)[0];
                        break;
                    case CommandType.ErrorReset:
                        break;
                    case CommandType.Free:
                        result[2] = BitConverter.GetBytes((int)package.CommandParam)[0];
                        break;
                    case CommandType.Free2:
                        result[2] = BitConverter.GetBytes((int)package.CommandParam)[0];
                        break;
                    case CommandType.FreeConstantResistance:
                        result[2] = BitConverter.GetBytes((int)package.CommandParam)[0];
                        break;
                    case CommandType.FreeCounterWeight:
                        result[2] = BitConverter.GetBytes((int)package.CommandParam)[0];
                        break;
                    case CommandType.Guided:
                        result[2] = BitConverter.GetBytes((int)package.CommandParam)[0];
                        break;
                    case CommandType.IsotonicA:
                        result[2] = BitConverter.GetBytes((int)package.CommandParam)[0];
                        break;
                    case CommandType.IsotonicB:
                        result[2] = BitConverter.GetBytes((int)package.CommandParam)[0];
                        break;
                    case CommandType.Reset:
                        break;
                    case CommandType.SensorInit:
                        break;
                    case CommandType.Pause:
                        break;
                }
            }

            return result;
        }
        #endregion

        #region 命令

        public void Free2(CommandParam param)
        {
            ControlCommandPackage package = new ControlCommandPackage();
            package.CommandType = CommandType.Free2;
            package.CommandParam = param;
            WriteData(CreateCommandBuffer(package));
        }

        public void PauseCmd()
        {
            ControlCommandPackage package = new ControlCommandPackage();
            package.CommandType = CommandType.Pause;
            WriteData(CreateCommandBuffer(package));
        }

        public void QuickStopCmd()
        {
            ControlCommandPackage package = new ControlCommandPackage();
            package.CommandType = CommandType.QuickStop;
            WriteData(CreateCommandBuffer(package));
        }

        public void SensorInit()
        {
            ControlCommandPackage package = new ControlCommandPackage();
            package.CommandType = CommandType.SensorInit;
            WriteData(CreateCommandBuffer(package));
        }

        public void ErrorReset()
        {
            ControlCommandPackage package = new ControlCommandPackage();
            package.CommandType = CommandType.ErrorReset;
            WriteData(CreateCommandBuffer(package));
        }

        public void Reset()
        {
            ControlCommandPackage package = new ControlCommandPackage();
            package.CommandType = CommandType.Reset;
            WriteData(CreateCommandBuffer(package));
        }

        public void Homing()
        {
            ControlCommandPackage package = new ControlCommandPackage();
            package.CommandType = CommandType.SetZero;
            WriteData(CreateCommandBuffer(package));
        }

        public void GetVersion()
        {
            ControlCommandPackage package = new ControlCommandPackage();
            package.CommandType = CommandType.Version;
            WriteData(CreateCommandBuffer(package));
        }

        public void BendStretchFree(int forceOrSpeed, int target, int bendAngle, int stretchAngle)
        {
            ControlParamPackage paramPackage = new ControlParamPackage();
            paramPackage.BendStretchForceOrSpeed = forceOrSpeed;
            paramPackage.BendStretchTarget = target;
            //paramPackage.BendStretchForce = force;
            //paramPackage.BendStretchSpeed = speed;
            paramPackage.BendAngleLimit = bendAngle;
            paramPackage.StretchAngleLimit = stretchAngle;
            WriteData(CreateParamBuffer(paramPackage,ControlParamType.BendStretch));
            Thread.Sleep(10);
            ControlCommandPackage package = new ControlCommandPackage();
            package.CommandType = CommandType.Free;
            package.CommandParam = CommandParam.BendStretch;
            WriteData(CreateCommandBuffer(package));            
        }

        public void BendStretchLixin(int forceOrSpeed, int target, int bendAngle, int stretchAngle)
        {
            ControlParamPackage paramPackage = new ControlParamPackage();
            paramPackage.BendStretchForceOrSpeed = forceOrSpeed;
            paramPackage.BendStretchTarget = target;
            //paramPackage.BendStretchForce = force;
            //paramPackage.BendStretchSpeed = speed;
            paramPackage.BendAngleLimit = bendAngle;
            paramPackage.StretchAngleLimit = stretchAngle;
            WriteData(CreateParamBuffer(paramPackage, ControlParamType.BendStretch));
            Thread.Sleep(10);
            ControlCommandPackage package = new ControlCommandPackage();
            package.CommandType = CommandType.Lixin;
            package.CommandParam = CommandParam.BendStretch;
            WriteData(CreateCommandBuffer(package));
        }

        public void BendStretchDensu(int forceOrSpeed, int target, int bendAngle, int stretchAngle)
        {
            ControlParamPackage paramPackage = new ControlParamPackage();
            paramPackage.BendStretchForceOrSpeed = forceOrSpeed;
            paramPackage.BendStretchTarget = target;
            //paramPackage.BendStretchForce = force;
            //paramPackage.BendStretchSpeed = speed;
            paramPackage.BendAngleLimit = bendAngle;
            paramPackage.StretchAngleLimit = stretchAngle;
            WriteData(CreateParamBuffer(paramPackage, ControlParamType.BendStretch));
            Thread.Sleep(10);
            ControlCommandPackage package = new ControlCommandPackage();
            package.CommandType = CommandType.Dengsu;
            package.CommandParam = CommandParam.BendStretch;
            WriteData(CreateCommandBuffer(package));
        }

        public void BendStretchFreeCounterWeight(int forceOrSpeed, int target, int bendAngle, int stretchAngle)
        {
            ControlParamPackage paramPackage = new ControlParamPackage();
            paramPackage.BendStretchForceOrSpeed = forceOrSpeed;
            paramPackage.BendStretchTarget = target;
            //paramPackage.BendStretchForce = force;
            //paramPackage.BendStretchSpeed = speed;
            paramPackage.BendAngleLimit = bendAngle;
            paramPackage.StretchAngleLimit = stretchAngle;
            WriteData(CreateParamBuffer(paramPackage, ControlParamType.BendStretch));
            Thread.Sleep(10);
            ControlCommandPackage package = new ControlCommandPackage();
            package.CommandType = CommandType.FreeCounterWeight;
            package.CommandParam = CommandParam.BendStretch;
            WriteData(CreateCommandBuffer(package));
        }

        public void BendStretchFreeConstantResistance(int forceOrSpeed, int target, int bendAngle, int stretchAngle)
        {
            ControlParamPackage paramPackage = new ControlParamPackage();
            paramPackage.BendStretchForceOrSpeed = forceOrSpeed;
            paramPackage.BendStretchTarget = target;
            //paramPackage.BendStretchForce = force;
            //paramPackage.BendStretchSpeed = speed;
            paramPackage.BendAngleLimit = bendAngle;
            paramPackage.StretchAngleLimit = stretchAngle;
            WriteData(CreateParamBuffer(paramPackage, ControlParamType.BendStretch));
            Thread.Sleep(10);
            ControlCommandPackage package = new ControlCommandPackage();
            package.CommandType = CommandType.FreeConstantResistance;
            package.CommandParam = CommandParam.BendStretch;
            WriteData(CreateCommandBuffer(package));
        }

        public void BendStretchGuided(int forceOrSpeed,int target, int bendAngle, int stretchAngle)
        {
            ControlParamPackage paramPackage = new ControlParamPackage();
            paramPackage.BendStretchForceOrSpeed = forceOrSpeed;
            paramPackage.BendStretchTarget = target;
            //paramPackage.BendStretchForce = force;
            //paramPackage.BendStretchSpeed = speed;
            paramPackage.BendAngleLimit = bendAngle;
            paramPackage.StretchAngleLimit = stretchAngle;
            WriteData(CreateParamBuffer(paramPackage, ControlParamType.BendStretch));
            Thread.Sleep(10);
            ControlCommandPackage package = new ControlCommandPackage();
            package.CommandType = CommandType.Guided;
            package.CommandParam = CommandParam.BendStretch;
            WriteData(CreateCommandBuffer(package));
        }

        public void BendStretchAssist(int forceOrSpeed, int target, int bendAngle, int stretchAngle)
        {
            ControlParamPackage paramPackage = new ControlParamPackage();
            paramPackage.BendStretchForceOrSpeed = forceOrSpeed;
            paramPackage.BendStretchTarget = target;
            //paramPackage.BendStretchForce = force;
            //paramPackage.BendStretchSpeed = speed;
            paramPackage.BendAngleLimit = bendAngle;
            paramPackage.StretchAngleLimit = stretchAngle;
            WriteData(CreateParamBuffer(paramPackage, ControlParamType.BendStretch));
            Thread.Sleep(10);
            ControlCommandPackage package = new ControlCommandPackage();
            package.CommandType = CommandType.Assist;
            package.CommandParam = CommandParam.BendStretch;
            WriteData(CreateCommandBuffer(package));
        }

        public void BendStretchIsotonicA()
        {
            ControlParamPackage paramPackage = new ControlParamPackage();
            WriteData(CreateParamBuffer(paramPackage, ControlParamType.BendStretch));
            Thread.Sleep(10);
            ControlCommandPackage package = new ControlCommandPackage();
            package.CommandType = CommandType.IsotonicA2;
            package.CommandParam = CommandParam.BendStretch;
            WriteData(CreateCommandBuffer(package));
        }

        public void BendStretchIsotonicB(int forceOrSpeed, int target, int bendAngle, int stretchAngle,bool isControl)
        {
            ControlParamPackage paramPackage = new ControlParamPackage();
            paramPackage.BendStretchForceOrSpeed = forceOrSpeed;
            paramPackage.BendStretchTarget = target * -1;
            //paramPackage.BendStretchForce = force;
            //paramPackage.BendStretchSpeed = speed;
            paramPackage.BendAngleLimit = bendAngle;
            paramPackage.StretchAngleLimit = stretchAngle;
            WriteData(CreateParamBuffer(paramPackage, ControlParamType.BendStretch));
            if (isControl)
            {
                Thread.Sleep(10);
                ControlCommandPackage package = new ControlCommandPackage();
                package.CommandType = CommandType.IsotonicB;
                package.CommandParam = CommandParam.BendStretch;
                WriteData(CreateCommandBuffer(package));
            }
        }

        public void RotationFree(int forceOrSpeed, int target, int leftAngle, int rightAngle)
        {
            ControlParamPackage paramPackage = new ControlParamPackage();
            paramPackage.RotationaForceOrSpeed = forceOrSpeed;
            paramPackage.RotationTarget = target;
            //paramPackage.RotationaForce = force;
            //paramPackage.RotationSpeed = speed;
            paramPackage.LeftRotationAngleLimit = leftAngle;
            paramPackage.RightRotationAngleLimit = rightAngle;
            WriteData(CreateParamBuffer(paramPackage, ControlParamType.Rotationa));
            Thread.Sleep(10);
            ControlCommandPackage package = new ControlCommandPackage();
            package.CommandType = CommandType.Free;
            package.CommandParam = CommandParam.Rotation;
            WriteData(CreateCommandBuffer(package));
        }

        public void RotationLixin(int forceOrSpeed, int target, int leftAngle, int rightAngle)
        {
            ControlParamPackage paramPackage = new ControlParamPackage();
            paramPackage.RotationaForceOrSpeed = forceOrSpeed;
            paramPackage.RotationTarget = target;
            //paramPackage.RotationaForce = force;
            //paramPackage.RotationSpeed = speed;
            paramPackage.LeftRotationAngleLimit = leftAngle;
            paramPackage.RightRotationAngleLimit = rightAngle;
            WriteData(CreateParamBuffer(paramPackage, ControlParamType.Rotationa));
            Thread.Sleep(10);
            ControlCommandPackage package = new ControlCommandPackage();
            package.CommandType = CommandType.Lixin;
            package.CommandParam = CommandParam.Rotation;
            WriteData(CreateCommandBuffer(package));
        }

        public void RotationDengsu(int forceOrSpeed, int target, int leftAngle, int rightAngle)
        {
            ControlParamPackage paramPackage = new ControlParamPackage();
            paramPackage.RotationaForceOrSpeed = forceOrSpeed;
            paramPackage.RotationTarget = target;
            //paramPackage.RotationaForce = force;
            //paramPackage.RotationSpeed = speed;
            paramPackage.LeftRotationAngleLimit = leftAngle;
            paramPackage.RightRotationAngleLimit = rightAngle;
            WriteData(CreateParamBuffer(paramPackage, ControlParamType.Rotationa));
            Thread.Sleep(10);
            ControlCommandPackage package = new ControlCommandPackage();
            package.CommandType = CommandType.Dengsu;
            package.CommandParam = CommandParam.Rotation;
            WriteData(CreateCommandBuffer(package));
        }

        public void RotationCounterWeight(int forceOrSpeed, int target, int leftAngle, int rightAngle)
        {
            ControlParamPackage paramPackage = new ControlParamPackage();
            paramPackage.RotationaForceOrSpeed = forceOrSpeed;
            paramPackage.RotationTarget = target;
            //paramPackage.RotationaForce = force;
            //paramPackage.RotationSpeed = speed;
            paramPackage.LeftRotationAngleLimit = leftAngle;
            paramPackage.RightRotationAngleLimit = rightAngle;
            WriteData(CreateParamBuffer(paramPackage, ControlParamType.Rotationa));
            Thread.Sleep(10);
            ControlCommandPackage package = new ControlCommandPackage();
            package.CommandType = CommandType.FreeCounterWeight;
            package.CommandParam = CommandParam.Rotation;
            WriteData(CreateCommandBuffer(package));
        }

        public void RotationFreeConstantResistance(int forceOrSpeed, int target, int leftAngle, int rightAngle)
        {
            ControlParamPackage paramPackage = new ControlParamPackage();
            paramPackage.RotationaForceOrSpeed = forceOrSpeed;
            paramPackage.RotationTarget = target;
            //paramPackage.RotationaForce = force;
            //paramPackage.RotationSpeed = speed;
            paramPackage.LeftRotationAngleLimit = leftAngle;
            paramPackage.RightRotationAngleLimit = rightAngle;
            WriteData(CreateParamBuffer(paramPackage, ControlParamType.Rotationa));
            Thread.Sleep(10);
            ControlCommandPackage package = new ControlCommandPackage();
            package.CommandType = CommandType.FreeConstantResistance;
            package.CommandParam = CommandParam.Rotation;
            WriteData(CreateCommandBuffer(package));
        }

        public void RotationIsotonicA()
        {
            ControlParamPackage paramPackage = new ControlParamPackage();
            WriteData(CreateParamBuffer(paramPackage, ControlParamType.Rotationa));
            Thread.Sleep(10);
            ControlCommandPackage package = new ControlCommandPackage();
            package.CommandType = CommandType.IsotonicA2;
            package.CommandParam = CommandParam.Rotation;
            WriteData(CreateCommandBuffer(package));
        }

        public void RotationIsotonicB(int forceOrSpeed, int target, int bendAngle, int stretchAngle,bool isControl)
        {
            ControlParamPackage paramPackage = new ControlParamPackage();
            paramPackage.RotationaForceOrSpeed = forceOrSpeed;
            paramPackage.RotationTarget = target * -1;
            paramPackage.LeftRotationAngleLimit = bendAngle;
            paramPackage.RightRotationAngleLimit = stretchAngle;
            WriteData(CreateParamBuffer(paramPackage, ControlParamType.Rotationa));
            if (isControl)
            {
                Thread.Sleep(10);
                ControlCommandPackage package = new ControlCommandPackage();
                package.CommandType = CommandType.IsotonicB;
                package.CommandParam = CommandParam.Rotation;
                WriteData(CreateCommandBuffer(package));
            }
        }

        public void RotationGuided(int forceOrSpeed, int target, int leftAngle, int rightAngle)
        {
            ControlParamPackage paramPackage = new ControlParamPackage();
            //paramPackage.RotationaForce = force;
            //paramPackage.RotationSpeed = speed;
            paramPackage.RotationaForceOrSpeed = forceOrSpeed;
            paramPackage.RotationTarget = target;
            paramPackage.LeftRotationAngleLimit = leftAngle;
            paramPackage.RightRotationAngleLimit = rightAngle;
            WriteData(CreateParamBuffer(paramPackage, ControlParamType.Rotationa));
            Thread.Sleep(10);
            ControlCommandPackage package = new ControlCommandPackage();
            package.CommandType = CommandType.Guided;
            package.CommandParam = CommandParam.Rotation;
            WriteData(CreateCommandBuffer(package));
        }

        public void RotationAssist(int forceOrSpeed, int target, int leftAngle, int rightAngle)
        {
            ControlParamPackage paramPackage = new ControlParamPackage();
            //paramPackage.RotationaForce = force;
            //paramPackage.RotationSpeed = speed;
            paramPackage.RotationaForceOrSpeed = forceOrSpeed;
            paramPackage.RotationTarget = target;
            paramPackage.LeftRotationAngleLimit = leftAngle;
            paramPackage.RightRotationAngleLimit = rightAngle;
            WriteData(CreateParamBuffer(paramPackage, ControlParamType.Rotationa));
            Thread.Sleep(10);
            ControlCommandPackage package = new ControlCommandPackage();
            package.CommandType = CommandType.Assist;
            package.CommandParam = CommandParam.Rotation;
            WriteData(CreateCommandBuffer(package));
        }

        public void PushRodUp()
        {
            ControlCommandPackage package = new ControlCommandPackage();
            package.CommandType = CommandType.PushRodUp;
            WriteData(CreateCommandBuffer(package));
        }

        public void PushRodDowm()
        {
            ControlCommandPackage package = new ControlCommandPackage();
            package.CommandType = CommandType.PushRodDown;
            WriteData(CreateCommandBuffer(package));
        }

        public void PushRodStop()
        {
            ControlCommandPackage package = new ControlCommandPackage();
            package.CommandType = CommandType.PushRodStop;
            WriteData(CreateCommandBuffer(package));
        }

        #endregion
    }
}
