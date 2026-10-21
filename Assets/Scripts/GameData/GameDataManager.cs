using UnityEngine;

public class GameDataManager
{
	#region 单例模式
	private static GameDataManager instance = new GameDataManager();
	public static GameDataManager Instance => instance;
	private GameDataManager()
	{
		//每次启动时加载数据
		SingleVoiceData = XmlDataManager.Instance.LoadXmlData(typeof(VoiceData), "VoiceData") as VoiceData;
	}

	#endregion

	public VoiceData SingleVoiceData;

	#region 声音相关方法
	/// <summary>
	/// 数据持久化声音数据
	/// </summary>
	public void SaveVoiceData()
	{
		XmlDataManager.Instance.SaveXmlData(SingleVoiceData, "VoiceData");
	}
	/// <summary>
	/// 改变音乐音量大小
	/// </summary>
	/// <param name="value">新的音量</param>
	public void ChangeMusicVolume(float value)
	{
		SingleVoiceData.MusicVolume = value;
	}
	/// <summary>
	/// 改变音效音量大小
	/// </summary>
	/// <param name="value">新的音量</param>
    public void ChangeSoundVolume(float value)
    {
        SingleVoiceData.SoundVolume = value;
    }
	/// <summary>
	/// 开/关音乐
	/// </summary>
	/// <param name="isOpen">是否开启</param>
	public void OpenOrCloseMusic(bool isOpen)
	{
		SingleVoiceData.MusicIsOpen = isOpen;
	}
	/// <summary>
	/// 开/关音效
	/// </summary>
	/// <param name="isOpen">是否开启</param>
	public void OpenOrCloseSound(bool isOpen)
	{
		SingleVoiceData.SoundIsOpen = isOpen;
	}
    #endregion
}
