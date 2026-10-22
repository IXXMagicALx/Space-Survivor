using JetBrains.Annotations;
using UnityEngine;

public class SettingPanel : BasePanel<SettingPanel>
{
    #region 按键相关
    public UIButton CloseBtn;
    #endregion
    #region 滑条相关
    public UISlider MusicSlider;
    public UISlider SoundSlider;
    #endregion
    #region 多选框相关
    public UIToggle MusicToggle;
    public UIToggle SoundToggle;
    #endregion
    public override void Init()
    {
        CloseBtn.onClick.Add(new EventDelegate(() =>
        {
            //关闭设置面板
            HidePanel();
        }));

        #region 音乐相关
        MusicSlider.onChange.Add(new EventDelegate(() =>
        {
            //设置音乐音量
            GameDataManager.Instance.ChangeMusicVolume(MusicSlider.value);
            Music.Instance.SetMusicVolume(MusicSlider.value);

        }));

        MusicToggle.onChange.Add(new EventDelegate(() =>
        {
            //设置音乐是否开启
            GameDataManager.Instance.OpenOrCloseMusic(MusicToggle.value);
            Music.Instance.SetMusicMute(MusicToggle.value);

        }));
        #endregion

        #region 音效相关
        SoundSlider.onChange.Add(new EventDelegate(() =>
        {
            //设置音效音量
            GameDataManager.Instance.ChangeSoundVolume(SoundSlider.value);
        }));

        SoundToggle.onChange.Add(new EventDelegate(() =>
        {
            //设置音效是否开启
            GameDataManager.Instance.OpenOrCloseSound(SoundToggle.value);

        }));
        #endregion

        HidePanel();

    }

    public override void ShowPanel()
    {
        base.ShowPanel();
        //更新面板
        UpdatePanel();
    }

    public override void HidePanel()
    {
        base.HidePanel();
        //存储面板数据
        GameDataManager.Instance.SaveVoiceData();
    }
    /// <summary>
    /// 更新设置面板数据
    /// </summary>
    public void UpdatePanel()
    {
        #region 音乐相关
        MusicSlider.value = GameDataManager.Instance.VoiceData.MusicVolume;
        MusicToggle.value = GameDataManager.Instance.VoiceData.MusicIsOpen;
        #endregion

        #region 音效相关
        SoundSlider.value = GameDataManager.Instance.VoiceData.SoundVolume;
        SoundToggle.value = GameDataManager.Instance.VoiceData.SoundIsOpen;
        #endregion
    }
}
