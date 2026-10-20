using UnityEngine;

public abstract class BasePanel<T> : MonoBehaviour where T : class
{
    #region 单例模式
    private static T instance;
    public static T Instance => instance;

    protected virtual void Awake()
    {
        instance = this as T;
    }
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Init();
    }
    /// <summary>
    /// 面板初始化
    /// </summary>
    public abstract void Init();

    /// <summary>
    /// 显示面板
    /// </summary>
    public virtual void ShowPanel()
    {
        this.gameObject.SetActive(true);
    }

    /// <summary>
    /// 隐藏面板
    /// </summary>
    public virtual void HidePanel()
    {
        this.gameObject.SetActive(false);
    }
}
