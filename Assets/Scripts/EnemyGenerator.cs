using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    //円卓状にEnemyを生成する!
    //時間経過によって段々と発生する敵の数が増える！
    //倒した敵の数をレコードする！

    //※prefab化したenemyには直接hierarchyのゲームオブジェクトをアタッチできない！！
    //※Instansiateした時にセットする方法で対処。

    public GameObject enemyPrefab;//生成物
    int makeEnemyCount = 3;//生成数
    public GameObject centerTarget;//中心点となるオブジェクト
    public float distance;//距離

    int onEnemyCount;//現在ステージ上にいるEnemy数
    int MakedEnemyCount;//作ったenemyの合計数
    int AttackEnemyCount;//倒したenemyの合計数

    int eventCount = 10;//イベント発生条件数

    EnemyController enemyController;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //クリア条件数を超えたら製造中止
        if (MakedEnemyCount >= gameManager.GetClearScore())
        {
            return;
        }

        //GameGeneratorの子オブジェクト数でステージ上のenemy数を把握
        onEnemyCount = transform.childCount;

        //eventCount毎に敵増殖(enemy生成数を増やす)
        if (MakedEnemyCount > eventCount)
        {
            eventCount *= 2;
            makeEnemyCount += 2;
        }

        //もしステージ上のenemyが指定生成数より多ければ作らない、そうでなければ作る
        if (onEnemyCount > makeEnemyCount)
        {
            return;
        }
        else
        {
            MakeEnemy();
        }
    }

    void MakeEnemy()
    {
        for (int i = 0; i < makeEnemyCount-onEnemyCount; i++)
        {
            Vector3 position = centerTarget.transform.position + Quaternion.Euler(0f, Random.Range(0, 360f), 0f) * centerTarget.transform.forward * distance;
            GameObject enemy = Instantiate(
                enemyPrefab,
                position,
                Quaternion.identity
                );
            //生成したenemyの親にEnemyGeneratorを指定
            enemy.transform.parent = transform;
            //生成したenemyのEnemycontrollerにsceneのplayerとGameManagerをセットする
            enemy.GetComponent<EnemyController>().SetPlayer(centerTarget);
            enemy.GetComponent<EnemyController>().SetGameManager(gameManager);
            //Playerの方向に向かせる
            enemy.transform.LookAt(centerTarget.transform.position);
            //MakedEnemyCount追加
            MakedEnemyCount++;
        }
    }

    public void AddAttackEnemyCount()
    {
        AttackEnemyCount++;
    }

    public int GetAttackEnemyCount()
    {
        return AttackEnemyCount;
    }

    public int GetMakedEnemyCount()
    {
        return MakedEnemyCount;
    }
}
