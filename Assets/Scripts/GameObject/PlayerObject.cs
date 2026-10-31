using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    #region 单例模式
    private static PlayerObject instance;
    public static PlayerObject Instance => instance;
    void Awake()
    {
        instance = this;
    }
    #endregion

    #region 基本属性

    #region 血量相关
    [HideInInspector]
    public int NowHp;
    private int MaxHp;
    #endregion

    #region 移动旋转相关
    private float MoveSpeed = 5;
    private Vector3 lastPosition;
    private Vector3 nextPosition;
    private Vector2 playerWorldToScreen;
    private float RotateSpeed = 5;
    private Quaternion targetQ;
    private Vector3 MaxRotateQ = new Vector3(0,0,20);
    #endregion

    #endregion

    #region 流程属性
    private bool isDead = false;
    private float hInput;
    private float vInput;
    #endregion

    void Update()
    {
        if(isDead) return;

        MoveAndRotate();
        ClickToDestroyBullet();
    }
    /// <summary>
    /// 创建玩家时用于初始化
    /// </summary>
    /// <param name="singleAirplaneData">要初始化的数据</param>
    public void Init(SingleAirplaneData singleAirplaneData)
    {
        this.MaxHp = singleAirplaneData.Hp;
        this.NowHp = singleAirplaneData.Hp;
        this.MoveSpeed *= singleAirplaneData.MoveSpeed;
        this.RotateSpeed *= singleAirplaneData.RotateSpeed;
        this.transform.localScale = Vector3.one;
    }

    /// <summary>
    /// 死亡
    /// </summary>
    private void Dead()
    {
        //死亡标识符置为真
        this.isDead = true;
        EndPanel.Instance.ShowPanel();

    }
    
    /// <summary>
    /// 移动与左右旋转
    /// </summary>
    private void MoveAndRotate()
    {
        //得到输入量
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");

        #region 移动
        if (hInput != 0 || vInput != 0)
        {
            //记下移动前的位置
            lastPosition = this.transform.position;
            //移动
            this.transform.Translate(hInput * MoveSpeed * Time.deltaTime * Vector3.right, Space.World);
            this.transform.Translate(vInput * MoveSpeed * Time.deltaTime * Vector3.forward);
            //记下移动后的位置
            nextPosition = this.transform.position;
            //将玩家的世界坐标转为屏幕坐标系
            playerWorldToScreen = Camera.main.WorldToScreenPoint(this.transform.position);
            //判断是否出界
            if (playerWorldToScreen.x <= 0 || playerWorldToScreen.x >= Screen.width)
            {
                this.transform.position = new Vector3(lastPosition.x, nextPosition.y, nextPosition.z);
            }
            if (playerWorldToScreen.y <= 0 || playerWorldToScreen.y >= Screen.height)
            {
                this.transform.position = new Vector3(nextPosition.x, nextPosition.y, lastPosition.z);
            }
        }

        #endregion

        #region 旋转
        //得到目标旋转四元数
        if (hInput == 0)
        {
            targetQ = Quaternion.identity;
        }
        else
        {
            targetQ = hInput < 0 ? Quaternion.AngleAxis(MaxRotateQ.z, Vector3.forward) :
                                   Quaternion.AngleAxis(-MaxRotateQ.z, Vector3.forward);
        }

        //四元数插值运算进行旋转
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                                   targetQ,
                                                   RotateSpeed * Time.deltaTime);
        #endregion

    }
    /// <summary>
    /// 受到伤害
    /// </summary>
    public void TakeDamage()
    {
        //如果已死亡就不去扣血
        if (isDead) return;
     
        //减血
        this.NowHp--;
        //更新血条
        GamePanel.Instance.UpdateHealthBar(NowHp);
        
        if(this.NowHp <= 0)
        {
            Dead();
        }

    }
    /// <summary>
    /// 点击消除子弹
    /// </summary>
    public void ClickToDestroyBullet()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;

            bool isClickBullet = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),
                                                 out hitInfo, 1000,
                                                 1 << LayerMask.NameToLayer("Bullet"));
            if (isClickBullet)
            {
                BulletObject bullet = hitInfo.transform.GetComponent<BulletObject>();
                //销毁这一子弹
                bullet.Despawn();
            }
        }
    }
    

    
}
