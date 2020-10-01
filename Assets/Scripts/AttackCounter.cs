using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackCounter : MonoBehaviour
{
    public EnemyGenerator enemyGenerator;
    Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "AttackEnemy : " + enemyGenerator.GetAttackEnemyCount();
    }
}
