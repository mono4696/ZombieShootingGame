using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPanel : MonoBehaviour
{
    public GameObject[] icons;

    public void UpdateBulletPanel(int bullet)
    {
        for(int i = 0; i < icons.Length; i++)
        {
            if (i < bullet)
            {
                icons[i].SetActive(true);
            }
            else
            {
                icons[i].SetActive(false);
            }
        }
    }
}
