using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoosePanel : BasePanel<ChoosePanel>
{
    #region 按键相关
    /// <summary>
    /// 关闭按键
    /// </summary>
    public UIButton CloseBtn;
    /// <summary>
    /// 切换上一个按键
    /// </summary>
    public UIButton LastBtn;
    /// <summary>
    /// 切换下一个按键
    /// </summary>
    public UIButton NextBtn;
    /// <summary>
    /// 开始游戏按键
    /// </summary>
    public UIButton StartBtn;
    #endregion

    #region 展示飞船相关
    /// <summary>
    /// 展示飞船位置
    /// </summary>
    public Transform AirplanePosition;
    /// <summary>
    /// 实例化飞船对象
    /// </summary>
    private GameObject nowShowAirplane;
    /// <summary>
    /// 当前飞船对象ID
    /// </summary>
    private int nowShowAirplaneID;
    /// <summary>
    /// 上下浮动时间
    /// </summary>
    private float time;
    /// <summary>
    /// 是否选中
    /// </summary>
    private bool isSelect;
    /// <summary>
    /// 飞船旋转速度
    /// </summary>
    public float RotateSpeed = 20;

    #endregion

    #region 属性相关
    public int AttributesCount = 10;
    public List<GameObject> HpAttributes;
    public List<GameObject> SpeedAttributes;
    public List<GameObject> VolumeAttributes;
    #endregion  

    void Update()
    {
        #region 飞船上下浮动
        time += Time.deltaTime;
        
        AirplanePosition.Translate(Vector3.up * Mathf.Sin(time) * 0.0001F, Space.World);
        #endregion

        #region 飞船左右旋转
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            //左键按下并且射线检测到对象
            if(Physics.Raycast( Camera.main.ScreenPointToRay(Input.mousePosition),
                                out hitInfo,100,
                                1 << LayerMask.NameToLayer("UI") ))
            {
                isSelect = true;
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            isSelect = false;
        }

        if(Input.GetMouseButton(0)&&isSelect)
        {
            AirplanePosition.rotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse X") * RotateSpeed, Vector3.up);
        }

        #endregion
    }
    public override void Init()
    {
        CloseBtn.onClick.Add(new EventDelegate(() =>
        {
            //关闭选角面板
            HidePanel();
            //打开主界面
            MenuPanel.Instance.ShowPanel();
        }));

        LastBtn.onClick.Add(new EventDelegate(() =>
        {
            //切换成上一个(处于ID=1的飞船时切换到上一个会切换到ID=5的飞船)
            GameDataManager.Instance.ChangeToLastAirplane();
            //切换后更新面板
            UpdatePanel();

        }));

        NextBtn.onClick.Add(new EventDelegate(() =>
        {
            //切换成下一个(处于ID=5的飞船时切换到下一个会切换到ID=1的飞船)
            GameDataManager.Instance.ChangeToNextAirplane();
            //切换后更新面板
            UpdatePanel();

        }));

        StartBtn.onClick.Add(new EventDelegate(() =>
        {
            //进入游戏场景
            SceneManager.LoadScene("GameScene");

        }));

        HidePanel();
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
        UpdatePanel();
    }
    /// <summary>
    /// 更新选角面板信息
    /// </summary>
    public void UpdatePanel()
    {
        //更新展示的模型
        int index = GameDataManager.Instance.NowSelectAirplaneIndex;
        SingleAirplaneData nowShowSingleAirplaneData = GameDataManager.Instance.AirplaneData.AirplaneDatas[index];
        //加载出Prefab用来实例化
        GameObject nowShowAirplaneModel = Resources.Load(nowShowSingleAirplaneData.ModelPath) as GameObject;
        //如果是同一个模型就不用重新实例化
        if (nowShowAirplaneID == nowShowSingleAirplaneData.ID && nowShowAirplane != null) return;
        //如果已有模型就先清除后实例化
        if (nowShowAirplane != null)
        {
            GameObject.Destroy(nowShowAirplane);
        }
        //实例化对象
        nowShowAirplane = GameObject.Instantiate(nowShowAirplaneModel);
        nowShowAirplane.transform.SetParent(AirplanePosition, false);
        float scale = nowShowSingleAirplaneData.Scale;
        nowShowAirplane.transform.localScale = new Vector3(scale, scale, scale);
        //设置层级到UI层
        nowShowAirplane.layer = LayerMask.NameToLayer("UI");

        
        //更新展示的属性
        int hp = nowShowSingleAirplaneData.Hp;
        int speed = nowShowSingleAirplaneData.MoveSpeed;
        int volume = nowShowSingleAirplaneData.Volume;
        for (int i = 0; i < AttributesCount; i++)
        {
            //判断语句的结果设置属性
            HpAttributes[i].SetActive(i < hp);
            SpeedAttributes[i].SetActive(i < speed);
            VolumeAttributes[i].SetActive(i < volume);
        }
        

    }
    
}
