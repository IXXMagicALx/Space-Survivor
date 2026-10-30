using System.Collections.Generic;
using UnityEngine;

public enum E_TurretPosType
{
    TopLeft,
    Top,
    TopRight,

    Left,
    Right,

    BottomLeft,
    Bottom,
    BottomRight,

}


public class TurretObject : MonoBehaviour
{
    /// <summary>
    /// 炮台所在位置类型
    /// </summary>
    public E_TurretPosType PosType;
    /// <summary>
    /// 炮台在屏幕中的位置
    /// </summary>
    private Vector3 screenPos;
    /// <summary>
    /// 用于设置多发子弹的默认方向
    /// </summary>
    private Vector3 initDir;
    /// <summary>
    /// 用于多发时记录上一次偏转的方向
    /// </summary>
    private Vector3 nowDir;
    /// <summary>
    /// 该炮台的配置信息
    /// </summary>
    private SingleTurretData nowTurretData;

    /// <summary>
    /// 记录已射出子弹数量
    /// </summary>
    private int nowNum;
    /// <summary>
    /// 记录射出子弹间隔
    /// </summary>
    private float nowCD;
    /// <summary>
    /// 记录每组子弹延时
    /// </summary>
    private float nowDelay;
    /// <summary>
    /// 记录当前组子弹的子弹信息
    /// </summary>
    private SingleBulletData nowBulletData;
    /// <summary>
    /// 多发时每次偏移的角度
    /// </summary>
    private float offsetAngle;

    // Update is called once per frame
    void Update()
    {
        //帧更新
        //更新炮台位置
        UpdateTurretPos();
        //更新炮台配置
        ResetTurretData();
        //发射子弹
        Fire();
    }


    /// <summary>
    /// 分辨率自适应更新炮台位置
    /// </summary>
    void UpdateTurretPos()
    {
        screenPos.z = 200F;//玩家在屏幕坐标系中的深度为200

        switch (PosType)
        {
            case E_TurretPosType.TopLeft:
                screenPos.x = 0;
                screenPos.y = Screen.height;
                initDir = Vector3.right;
                break;
            case E_TurretPosType.Top:
                screenPos.x = Screen.width / 2;
                screenPos.y = Screen.height;
                initDir = Vector3.right;
                break;
            case E_TurretPosType.TopRight:
                screenPos.x = Screen.width;
                screenPos.y = Screen.height;
                initDir = Vector3.back;
                break;
            case E_TurretPosType.Left:
                screenPos.x = 0;
                screenPos.y = Screen.height / 2;
                initDir = Vector3.forward;
                break;
            case E_TurretPosType.Right:
                screenPos.x = Screen.width;
                screenPos.y = Screen.height / 2;
                initDir = Vector3.back;
                break;
            case E_TurretPosType.BottomLeft:
                screenPos.x = 0;
                screenPos.y = 0;
                initDir = Vector3.forward;
                break;
            case E_TurretPosType.Bottom:
                screenPos.x = Screen.width / 2;
                screenPos.y = 0;
                initDir = Vector3.left;
                break;
            case E_TurretPosType.BottomRight:
                screenPos.x = Screen.width;
                screenPos.y = 0;
                initDir = Vector3.left;
                break;
        }

        //将屏幕坐标系转成世界坐标系
        this.transform.position = Camera.main.ScreenToWorldPoint(screenPos);

    }

    /// <summary>
    /// 配置炮台数据
    /// </summary>
    void ResetTurretData()
    {
        //当每组子弹数量和每个子弹的CD都为0时才认为需要创建新的一组子弹
        if (nowNum  != 0 && nowCD != 0) return;

        //组间延时判定
        if(nowTurretData != null)
        {
            nowDelay -= Time.deltaTime;

            //如果延时还没有结束就先不配置新的数据
            if (nowDelay > 0) return;
        }

        //从配置的数据中随机取出一条来配置到这个炮塔
        List<SingleTurretData> list = GameDataManager.Instance.TurretData.TurretDatas;
        nowTurretData = list[Random.Range(0, list.Count)];

        //记录需要变化的数据，不直接修改数据本身
        if(nowTurretData != null)
        {
            nowNum = nowTurretData.Num;
            nowCD = nowTurretData.CD;
            nowDelay = nowTurretData.Delay;

            //根据炮台数据里的设定，随机取出子弹数据
            string[] strs = nowTurretData.BulletsID.Split(',');
            int beginID = int.Parse(strs[0]);
            int endID = int.Parse(strs[strs.Length - 1]);
            int randomID = Random.Range(beginID, endID + 1);
            nowBulletData = GameDataManager.Instance.BulletData.BulletDatas[randomID - 1];
        }

        //如果多发就计算偏转角度
        if(nowTurretData.Type == E_TurretType.Multi)
        {

            switch (PosType)
            {
                case E_TurretPosType.TopLeft:
                case E_TurretPosType.TopRight:
                case E_TurretPosType.BottomLeft:
                case E_TurretPosType.BottomRight:
                    offsetAngle = 90F / (nowNum + 1);
                    break;
                case E_TurretPosType.Top:
                case E_TurretPosType.Left:
                case E_TurretPosType.Right:
                case E_TurretPosType.Bottom:
                    offsetAngle = 180F / (nowNum + 1);
                    break;
            
            }

        }

    }
    
    /// <summary>
    /// 发射子弹
    /// </summary>
    void Fire()
    {
        //一组打完去切换下一组
        if (nowCD == 0 && nowNum == 0) return;

        //CD更新
        nowCD -= Time.deltaTime;
        if (nowCD > 0) return;

        GameObject bulletObj;
        BulletObject bullet;

        //根据多发和单发两种类型来分别实现各自逻辑
        switch (nowTurretData.Type)
        {
            #region 单发
            case E_TurretType.Single:
                //动态创建子弹对象
                bulletObj = Instantiate(Resources.Load<GameObject>(nowBulletData.ResourcePath));
                //动态添加子弹脚本
                bullet = bulletObj.AddComponent<BulletObject>();
                //初始化子弹信息
                bullet.Spawn(nowBulletData);
                //设置子弹位置和朝向
                bulletObj.transform.position = this.transform.position;
                bulletObj.transform.rotation = Quaternion.LookRotation(PlayerObject.Instance.transform.position - bulletObj.transform.position);
                //实例化后减少更新记录
                nowNum--;
                //一组子弹没有打完时重置CD，打完了就将CD置0
                nowCD = nowNum == 0 ? 0 : nowCD = nowTurretData.CD;

                break;
            #endregion

            #region 多发
            case E_TurretType.Multi:
                //同时
                if(nowCD == 0)
                {
                    for(int i = 0; i < nowNum; i++)
                    {
                        //实例化对象
                        //动态创建子弹对象
                        bulletObj = Instantiate(Resources.Load<GameObject>(nowBulletData.ResourcePath));
                        //动态添加子弹脚本
                        bullet = bulletObj.AddComponent<BulletObject>();
                        //初始化子弹信息
                        bullet.Spawn(nowBulletData);
                        //设置子弹位置和朝向
                        bulletObj.transform.position = this.transform.position;
                        nowDir = Quaternion.AngleAxis(offsetAngle * (i + 1), Vector3.up) * initDir;
                        bulletObj.transform.rotation = Quaternion.LookRotation(nowDir);
                    }

                    //因为是同时发射所以遍历完要将子弹数和CD都置零
                    nowCD = nowNum = 0;
                }
                //延时
                else
                {
                    //实例化对象
                    //动态创建子弹对象
                    bulletObj = Instantiate(Resources.Load<GameObject>(nowBulletData.ResourcePath));
                    //动态添加子弹脚本
                    bullet = bulletObj.AddComponent<BulletObject>();
                    //初始化子弹信息
                    bullet.Spawn(nowBulletData);
                    //设置子弹位置和朝向(朝向根据当前所要发射的是第几个子弹计算)
                    bulletObj.transform.position = this.transform.position;
                    nowDir = Quaternion.AngleAxis(offsetAngle * (nowTurretData.Num - nowNum + 1), Vector3.up) * initDir;
                    bulletObj.transform.rotation = Quaternion.LookRotation(nowDir);
                    
                    //实例化后减少更新记录
                    nowNum--;

                    //重置CD
                    nowCD = nowNum == 0 ? 0 : nowTurretData.CD;

                }

                break;
            #endregion

        }


    }
}
