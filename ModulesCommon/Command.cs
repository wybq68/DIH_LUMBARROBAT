using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace LumbarRobot.ModulesCommon
{
    /// <summary>
    /// A class that implements a command object.
    /// </summary>
    public class Command
    {
        private IPAddress senderIP;
        /// <summary>
        /// 客户端IP
        /// </summary>
        public IPAddress SenderIP
        {
            get { return senderIP; }
            set { senderIP = value; }
        }

        private string senderName;
        /// <summary>
        /// 客户端名程
        /// </summary>
        public string SenderName
        {
            get { return senderName; }
            set { senderName = value; }
        }
        
        private CommandType cmdType;
        /// <summary>
        /// 命令包类型
        /// </summary>
        public CommandType CommandType
        {
            get { return cmdType; }
            set { cmdType = value; }
        }

        private IPAddress target;
        /// <summary>
        /// 目标IP
        /// </summary>
        public IPAddress Target
        {
            get { return target; }
            set { target = value; }
        }
        private string commandBody;
        /// <summary>
        /// 控制体命令数据包体 Json
        /// </summary>
        public string MetaData
        {
            get { return commandBody; }

            set { commandBody = value; }

        }

        /// <summary>
        /// Creates an instance of command object to send over the network.
        /// </summary>
        /// <param name="type">The type of command.If you wanna use the Message command type,create a Message class instead of command.</param>
        /// <param name="targetMachine">The targer machine that will receive the command.Set this property to IPAddress.Broadcast if you want send the command to all connected clients.</param>
        /// <param name="metaData">
        /// The body of the command.This string is different in various commands.
        /// <para>Message : The text of the message.</para>
        /// <para>ClientLoginInform : "RemoteClientIP:RemoteClientName".</para>
        /// <para>***WithTimer : The interval of timer in miliseconds..The default value is 60000 equal to 1 min.</para>
        /// <para>IsNameExists : The name of client you want to check it's existance.</para>
        /// <para>Otherwise pass the "" or null or use the next overriden constructor.</para>
        /// </param>
        public Command(CommandType type , IPAddress targetMachine , string metaData)
        {
            this.cmdType = type;
            this.target = targetMachine;
            this.commandBody = metaData;
        }

        /// <summary>
        /// Creates an instance of command object to send over the network.
        /// </summary>
        /// <param name="type">The type of command.If you wanna use the Message command type,create a Message class instead of command.</param>
        /// <param name="targetMachine">The targer machine that will receive the command.Set this property to IPAddress.Broadcast if you want send the command to all connected clients.</param>
        public Command(CommandType type , IPAddress targetMachine)
        {
            this.cmdType = type;
            this.target = targetMachine;
            this.commandBody = "";
        }
    }
}
