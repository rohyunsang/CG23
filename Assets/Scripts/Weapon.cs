using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public BoxCollider swordArea;
    public void Use(){
        StopCoroutine("Swing");
        StartCoroutine("Swing");
    }

    IEnumerator Swing(){
        
        yield return new WaitForSeconds(0.1f);
        swordArea.enabled = true;
        
        yield return new WaitForSeconds(0.8f);
        swordArea.enabled = false;
    }
}
