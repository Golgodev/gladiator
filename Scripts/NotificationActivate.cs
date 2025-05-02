using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationActivate : MonoBehaviour
{
    private void OnEnable() {
        StartCoroutine(Disable());
    }
    IEnumerator Disable() {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
   }
}
