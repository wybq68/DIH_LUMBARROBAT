using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LumbarRobot.Data
{
    public class AlarmMsgInfo
    {

        private bool _isDisplay;
        /// <summary>
        /// 是否显示力量对话框
        /// </summary>
        public bool IsDisplay
        {
            get { return _isDisplay; }
            set { _isDisplay = value; }
        }

        private string _caption;
        /// <summary>
        /// 消息框标题
        /// </summary>
        public string Caption
        {
            get { return _caption; }
            set { _caption = value; }
        }

        private string _message;
        /// <summary>
        /// 消息
        /// </summary>
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        private MsgBtnType _messageBtton;
        /// <summary>
        /// 按键类型
        /// </summary>
        public MsgBtnType MessageBtton
        {
            get { return _messageBtton; }
            set { _messageBtton = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private DialogResult _result;
        /// <summary>
        /// 触发结果
        /// </summary>
        public DialogResult Result
        {
            get { return _result; }
            set { _result = value; }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum MsgBtnType
    {
        // 摘要:
        //     消息框显示“确定”按钮。
        OK = 0,
        //
        // 摘要:
        //     消息框显示“确定”和“取消”按钮。
        OKCancel = 1,
        //
        // 摘要:
        //     消息框显示“是”、“否”和“取消”按钮。
        YesNoCancel = 3,
        //
        // 摘要:
        //     消息框显示“是”和“否”按钮。
        YesNo = 4,

    }

    public enum DialogResult
    {
        // 摘要:
        /// <summary>
        /// 消息框显示“确定”按钮。
        /// </summary>
        OK = 0,
        /// <summary>
        /// 取消
        /// </summary>
        Cancel = 1,
        /// <summary>
        /// 是
        /// </summary>
        Yes = 2,
        /// <summary>
        /// 否
        /// </summary>
        No = 4
    }

}
