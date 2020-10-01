using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rotateSpeed;

    bool isStan = false;
    bool touchEnemy = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (touchEnemy == true)
        {
            return;
        }

        transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal") * rotateSpeed, 0));

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Zombie")
        {
            touchEnemy = true;
            transform.LookAt(collision.transform);
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;//回転不可
            GetComponentInChildren<Shooter>().enabled = false;
        }
    }

    public bool GetIsStan()
    {
        return isStan;
    }

    public void SetIsStan()
    {
        isStan = true;
    }

    public bool GetTouchEnemy()
    {
        return touchEnemy;
    }
}
