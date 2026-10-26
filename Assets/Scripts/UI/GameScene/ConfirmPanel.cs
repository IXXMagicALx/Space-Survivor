using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfirmPanel : BasePanel<ConfirmPanel>
{
    #region 按键相关
    public UIButton YesBtn;
    public UIButton NoBtn;
    #endregion
    public override void Init()
    {
        YesBtn.onClick.Add(new EventDelegate(() =>
        {
            //切换到开始场景
            SceneManager.LoadScene("BeginScene");
            //游戏恢复
            Time.timeScale = 1.0F;

        }));

        NoBtn.onClick.Add(new EventDelegate(() =>
        {
            //关闭确认面板
            HidePanel();

        }));

        HidePanel();
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
        //游戏暂停
        Time.timeScale = 0;
    }

    public override void HidePanel()
    {
        base.HidePanel();
        //游戏恢复
        Time.timeScale = 1.0F;
    }
}
