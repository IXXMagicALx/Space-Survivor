using UnityEngine;

public class Music : MonoBehaviour
{
    #region 单例模式
    private static Music instance;
    public static Music Instance => instance;

    void Awake()
    {
        instance = this;
        //获取音源并初始化声音设置
        audioSource = this.gameObject.GetComponent<AudioSource>();
        SetMusicVolume(GameDataManager.Instance.SingleVoiceData.MusicVolume);
        SetMusicMute(GameDataManager.Instance.SingleVoiceData.MusicIsOpen);
    }
    #endregion

    private AudioSource audioSource;
    /// <summary>
    /// 设置音源音量大小
    /// </summary>
    /// <param name="value">音量</param>
    public void SetMusicVolume(float value)
    {
        audioSource.volume = value;
    }
    /// <summary>
    /// 设置音源是否静音
    /// </summary>
    /// <param name="isOpen">是否不静音</param>
    public void SetMusicMute(bool isOpen)
    {
        audioSource.mute = !isOpen;
    }
}
