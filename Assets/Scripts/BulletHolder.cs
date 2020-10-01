using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletHolder : MonoBehaviour
{
    const int DefalutBulletAmount = 6;//全弾数
    int bullet = DefalutBulletAmount;//現在の残り弾数

    public BulletPanel panel;

    void Update()
    {
        panel.UpdateBulletPanel(bullet);//BulletPanelに弾数反映
    }

    public void ConsumeBullet()//弾数の消費
    {
        if (bullet > 0)
        {
            bullet--;
        }
    }

    public int GetDefalutBulletAmount()//全弾数確認
    {
        return DefalutBulletAmount;
    }

    public int GetBulletAmount()//残り弾数確認
    {
        return bullet;
    }

    public void AddBullet(int amount)//弾数補充
    {
        bullet += amount;
    }



}
