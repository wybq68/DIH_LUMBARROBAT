using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LumbarRobot.DAL;
using LumbarRobot.ViewModels;
using LumbarRobot.Data;
using LumbarRobot.Common.Enums;

namespace LumbarRobot.MyUserControl
{
    /// <summary>
    /// GameBtn.xaml 的交互逻辑
    /// </summary>
    public partial class GameBtn : UserControl
    {

        #region 变量
        private GameDialog GameDialog;
        #endregion

        #region 构造
        public GameBtn()
        {
            InitializeComponent();
        }
        #endregion
        GamePram gp = new GamePram();
        #region 游戏
        private void btnGame_Click(object sender, RoutedEventArgs e)
        {
            if (Init())
            {
                GameDialog = new GameDialog();
                GameDialog.Parent = this;
                GameView child = new GameView();
                child.gp = this.gp;
                child.Close += new EventHandler(child_Close);
                GameDialog.Content = child;
                GameDialog.Show();
            }
        }
        #endregion

        public bool Init()
        {
            if (ModuleConstant.PatientId != null)
            {
                var LeftRang = from eva in MySession.Query<EvaluteDetail>() where eva.PatientID == ModuleConstant.PatientId && eva.ActionId == (int)EvaluateActionEnum.RotationRangeLeft orderby eva.EvaluteDate descending select eva;
                if (LeftRang.Count() > 0)
                {
                    gp.RotationMax = LeftRang.First().MaxV;
                }
                else
                {
                    MessageBox.Show("该患者没有评测记录，请返回评测");

                    return false;
                }
                var RightRang = from eva in MySession.Query<EvaluteDetail>() where eva.PatientID == ModuleConstant.PatientId && eva.ActionId == (int)EvaluateActionEnum.RotationRangeRight orderby eva.EvaluteDate descending select eva;
                if (LeftRang.Count() > 0)
                {
                    gp.BendStretchMin = RightRang.FirstOrDefault().MaxV;
                }
                else
                {
                    MessageBox.Show("该患者没有评测记录，请返回评测");

                    return false;
                }
                var BendRang = from eva in MySession.Query<EvaluteDetail>() where eva.PatientID == ModuleConstant.PatientId && eva.ActionId == (int)EvaluateActionEnum.RangeBend orderby eva.EvaluteDate descending select eva;
                if (BendRang.Count() > 0)
                {
                    gp.RotationMax = BendRang.FirstOrDefault().MaxV;
                }
                else
                {
                    MessageBox.Show("该患者没有评测记录，请返回评测");

                    return false;
                }
                var ProtrusiveRange = from eva in MySession.Query<EvaluteDetail>() where eva.PatientID == ModuleConstant.PatientId && eva.ActionId == (int)EvaluateActionEnum.RangeProtrusive orderby eva.EvaluteDate descending select eva;
                if (ProtrusiveRange.Count() > 0)
                {
                    gp.RotationMin = ProtrusiveRange.FirstOrDefault().MaxV;
                }
                else
                {
                    MessageBox.Show("该患者没有评测记录，请返回评测");

                    return false;
                }
               

            }
            else
            {

                MessageBox.Show("请选择患者！");

                //return;
            }
            return true;
        }

        #region 关闭
        public void child_Close(object sender, EventArgs e)
        {
            GameDialog.Close();
        }
        #endregion
    }
}
