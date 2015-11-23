using System;
using System.Collections.Generic;
using System.Text;

namespace LumbarRobot.ModulesCommon
{
    /// <summary>
    /// The type of commands that you can sent to the server.(Note : These are just some comman types.You should do the desired actions when a command received to the client yourself.)
    /// </summary>
    public enum CommandType
    {
        /// <summary>
        /// 游戏初始化    
        /// </summary>
        GameInitializeCommand,
        /// <summary>
        /// 游戏控制指令
        /// </summary>
        GameControlCommand,
        /// <summary>
        /// 实时坐标数据
        /// </summary>
        GameRunCommand,

        GameResultData,

        /// <summary>
        /// Force the target PC to RESTART without prompt.Pass null or "" as command's Metadata.
        /// </summary>
        PCRestart,

        /// <summary>
        /// Force the target PC to RESTART with prompt.Pass the timer interval of RESTART action as command's Metadata in miliseconds.For example "20000".
        /// </summary>
        PCRestartWithTimer,

        /// <summary>
        /// Force the target PC to LOGOFF without prompt.Pass null or "" as command's Metadata.
        /// </summary>
        PCLogOFF,
        /// <summary>
        /// Force the target PC to LOGOFF with prompt.Pass the timer interval of LOGOFF action as command's Metadata in miliseconds.For example "20000".
        /// </summary>
        PCLogOFFWithTimer,
        /// <summary>
        /// Force the target PC to SHUTDOWN without prompt.Pass null or "" as command's Metadata.
        /// </summary>
        PCShutDown,
        /// <summary>
        /// Force the target PC to SHUTDOWN with prompt.Pass the timer interval of SHUTDOWN action as command's Metadata in miliseconds.For example "20000".
        /// </summary>
        PCShutDownWithTimer,
        /// <summary>
        /// Send a text message to the server.Pass the body of text message as command's Metadata.
        /// </summary>
        Message,
        /// <summary>
        /// This command will sent to all clients when an specific client is had been logged in to the server.The metadata of this command is in this format : "ClientIP:ClientNetworkName"
        /// </summary>
        ClientLoginInform,
        /// <summary>
        /// This command will sent to all clients when an specific client is had been logged off from the server.You can get the disconnected client information from SenderIP and SenderName properties of command event args.
        /// </summary>
        ClientLogOffInform,
        /// <summary>
        /// To ask from the server pass the name that you want check it's existance as meta data of command.The server will replay to you a command with the same type and MetaData of 'True' or 'False' that specifies the Network name is exists on the server or not.
        /// </summary>
        IsNameExists,
        /// <summary>
        /// To get a list of current connected clients to the server,Send this type of command to it.The server will replay to you one same command for each client with the metadata in this format : "ClientIP:ClientNetworkName".
        /// </summary>
        SendClientList,
        /// <summary>
        /// This is a free command that you can sent to the server.
        /// </summary>
        FreeCommand,
        /// <summary>
        /// 创建轨迹
        /// </summary>
        CreateTrajectory,

        /// <summary>
        /// 移动指令
        /// </summary>
        MoveCommand,
        /// <summary>
        /// FIT命令
        /// </summary>
        FitCommand,
        /// <summary>
        /// 录制命令
        /// </summary>
        RecordCommand,
        /// <summary>
        /// 力量命令
        /// </summary>
        ForceCommand,

        /// <summary>
        /// 视角调整命令
        /// </summary>
        CameraCommand,
        /// <summary>
        /// 消息及警告指令
        /// </summary>
        AlarmCommand,
        /// <summary>
        /// 参数调整指令
        /// </summary>
        ParamCommand,
        /// <summary>
        /// 下一个点
        /// </summary>
        NextCommand,
        /// <summary>
        /// 到达点
        /// </summary>
        ArrivedCommand,

        /// <summary>
        /// 到达确认
        /// </summary>
        ConfirmCommand,

        /// <summary>
        /// 音乐播放控制
        /// </summary>
        MusicCommand,

        /// <summary>
        /// fit 控制
        /// </summary>
        FitPlayCommand,

        /// <summary>
        /// 眼动 控制
        /// </summary>
        EyesPlayCommand,

        /// <summary>
        /// 显示实时轨迹命令
        /// </summary>
        DisPlayCommand



    }
}
