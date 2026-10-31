using UnityEngine;

public class BulletObject : MonoBehaviour
{
    //子弹基本数据
    private SingleBulletData data;
    //运动时间
    private float time;

    // Update is called once per frame
    void Update()
    {
        Move(); 
    }
    /// <summary>
    /// 子弹移动
    /// </summary>
    private void Move()
    {
        //前方分位移
        this.transform.Translate(data.ForwardSpeed * Time.deltaTime * Vector3.forward);

        //左右分位移
        switch (data.Type)
        {
            case E_BulletType.Straight:
                //没有水平分位移
                break;
            case E_BulletType.Wave:
                //水平分位移为正弦函数y轴大小
                time += Time.deltaTime;
                //在波浪形运动中 RightSpeed是左右位移大小，RotateSpeed是运动左右位移速度
                this.transform.Translate(Mathf.Sin(time * data.RotateSpeed) * data.RightSpeed * Time.deltaTime * Vector3.right);
                break;
            case E_BulletType.LeftArc:
                //左抛物线
                this.transform.rotation *= Quaternion.AngleAxis(data.RotateSpeed * Time.deltaTime, Vector3.up);
                break;
            case E_BulletType.RightArc:
                //右抛物线
                this.transform.rotation *= Quaternion.AngleAxis(data.RotateSpeed * Time.deltaTime, Vector3.up);
                break;
            case E_BulletType.Homing:
                //追踪玩家
                time += Time.deltaTime;
                //在追踪时间内才会追踪
                if(time < data.HomingTime)
                {
                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                 Quaternion.LookRotation(PlayerObject.Instance.transform.position - this.transform.position),
                                 data.RotateSpeed * Time.deltaTime);
                }
                //超过时间就无法追踪，保持直线
                
                break;
        }

    }
    /// <summary>
    /// 与其他对象碰撞
    /// </summary>
    /// <param name="other">碰撞</param>
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            CollideWithPlayer(other);
        }
    }
    /// <summary>
    /// 与玩家碰撞逻辑
    /// </summary>
    /// <param name="playerCollider">玩家碰撞器</param>
    private void CollideWithPlayer(Collider playerCollider)
    {
        //玩家受伤
        PlayerObject player = playerCollider.gameObject.GetComponent<PlayerObject>();
        player.TakeDamage();
        //子弹销毁
        Despawn();
    }
    /// <summary>
    /// 初始化生成子弹
    /// </summary>
    /// <param name="singleData"></param>
    public void Spawn(SingleBulletData singleData)
    {
        this.data = singleData;
        //延时自动销毁子弹
        Invoke("Despawn", data.LifeTime);
    }
    /// <summary>
    /// 销毁子弹
    /// </summary>
    public void Despawn()
    {
        //销毁子弹
        Destroy(this.gameObject);
        //创建销毁特效
        GameObject deadEffect = Instantiate<GameObject>(Resources.Load(data.DeadEffect) as GameObject);
        deadEffect.transform.position = this.transform.position;
        //延时1s销毁特效
        Destroy(deadEffect.gameObject,2);
    }
}
