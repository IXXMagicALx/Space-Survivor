using UnityEngine;

public class GameSceneManagerObject : MonoBehaviour
{
    public Transform AirplaneStartPosition;
    private SingleAirplaneData airplaneData;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //记录玩家选择的飞船信息
        airplaneData = GameDataManager.Instance.GetNowSelectAirplaneData();
        //实例化一个飞船对象
        GameObject playerObj = Instantiate(Resources.Load<GameObject>(airplaneData.ModelPath), AirplaneStartPosition);
        //为对象挂载脚本
        PlayerObject player = playerObj.AddComponent<PlayerObject>();
        //根据属性设置
        player.Init(airplaneData);
        //更新血条
        GamePanel.Instance.UpdateHealthBar(airplaneData.Hp);

    }

}
