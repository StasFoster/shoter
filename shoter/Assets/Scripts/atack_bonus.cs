using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atack_bonus : MonoBehaviour
{
    public static bool flag = false;
    private void Start()
    {
        StartCoroutine(Time_active());
    }

    private void OnTriggerEnter(Collider other)
    {
        flag = true;
        this.gameObject.SetActive(false);
    }
    private void OnTriggerExit(Collider other)
    {
        flag = false;
    }
    public IEnumerator Time_active()
    {
        int n = 0;
        while (true)
        {
            if(n % 10 == 0)
            {
                this.gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(1);
        }
    }
}
