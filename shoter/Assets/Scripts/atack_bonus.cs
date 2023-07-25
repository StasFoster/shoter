using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atack_bonus : MonoBehaviour, IDeath_from_player
{
    public static bool flag = false;
    private void Update()
    {
        Death();
    }
    private void OnTriggerEnter(Collider other)
    {
        flag = true;
        gameObject.SetActive(false);
    }
    private void OnTriggerExit(Collider other)
    {
        flag = false;
    }
    public void Death()
    {
        if (!mananger.gamemode)
        {
            Destroy(gameObject);
        }
    }
}
