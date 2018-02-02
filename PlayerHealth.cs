using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    [SerializeField] Image healthbar;
    private float fillAmount;

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "EnemyBullet")
        {
            loseHealth(34);
        }
    }
    void loseHealth(int damage)
    {
        fillAmount = health / 100;
        healthbar.fillAmount = fillAmount;
        if (health <= 0)
        {
            Destroy(gameObject);
            //GameOver
        }
    }
}
