using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDeath : MonoBehaviour
{
    //[SerializeField] float timeDelay = 0;
     private void OnTriggerEnter2D(Collider2D other)
{
    if (other.GetComponent<PlayerMovement>())
    {
        DestroySlime();
        //Invoke("DestroySlime", timeDelay);
    }
}
    void DestroySlime(){
        Destroy(transform.parent.gameObject);
    }

}
