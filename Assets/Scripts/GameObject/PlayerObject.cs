using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    #region 基本属性

    #region 血量相关
    private int nowHp;
    public int MaxHp;
    #endregion

    #region 移动旋转相关
    public float MoveSpeed = 20;
    private Vector3 lastPosition;
    private Vector3 nextPosition;
    private Vector2 playerWorldToScreen;
    public float RotateSpeed;
    private Quaternion targetQ;
    public Quaternion MaxRotateQ = Quaternion.Euler(0,0,20);
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
    }

    public void TakeDamage()
    {
        //如果已死亡就不去扣血
        if (isDead) return;
     
        //减血
        this.nowHp--;
        //更新血条
        GamePanel.Instance.UpdateHealthBar(nowHp);
        
        if(this.nowHp == 0)
        {
            Dead();
        }

    }

    private void Dead()
    {
        //死亡标识符置为真
        this.isDead = true;
        EndPanel.Instance.ShowPanel();

    }

    private void MoveAndRotate()
    {
        //得到输入量
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");

        #region 移动
        if(hInput != 0 || vInput != 0)
        {
            //记下移动前的位置
            lastPosition = this.transform.position;
            //移动
            this.transform.Translate(hInput * MoveSpeed * Time.deltaTime * Vector3.right,Space.World);
            this.transform.Translate(vInput * MoveSpeed * Time.deltaTime * Vector3.forward);
            //记下移动后的位置
            nextPosition = this.transform.position;
            //将玩家的世界坐标转为屏幕坐标系
            playerWorldToScreen = Camera.main.WorldToScreenPoint(this.transform.position);
            //判断是否出界
            if(playerWorldToScreen.x <= 0||playerWorldToScreen.x >= Screen.width)
            {
                this.transform.position = new Vector3(lastPosition.x,nextPosition.y,nextPosition.z);
            }
            if(playerWorldToScreen.y <= 0||playerWorldToScreen.y >= Screen.height)
            {
                this.transform.position = new Vector3(nextPosition.x, nextPosition.y, lastPosition.z);
            }
        }

        #endregion

        #region 旋转
        //得到目标旋转四元数
        if(hInput == 0)
        {
            targetQ = Quaternion.identity;
        }
        else
        {
            targetQ = hInput < 0 ? Quaternion.AngleAxis(20,Vector3.forward) : 
                                   Quaternion.AngleAxis(-20, Vector3.forward);
        }

        //四元数插值运算进行旋转
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, 
                                                   targetQ, 
                                                   RotateSpeed * Time.deltaTime);
        #endregion

    }
}
