using System;
using System.Collections;
using UnityEngine;
using Random = System.Random;


public class WheatItem : MonoBehaviour
{
    public void Drop()
    {
        var rand = new Random();
        GetComponent<Rigidbody>().AddForce(new Vector3(((float)rand.NextDouble()-0.5f)*200f,(float)rand.NextDouble()*200f,((float)rand.NextDouble()-0.5f)*200f));
        StartCoroutine(DespawnTimer());
    }

    public void Deactivate()
    {
        StopAllCoroutines();
        Destroy(GetComponent<Rigidbody>());
        Destroy(GetComponent<BoxCollider>());
    }

    IEnumerator DespawnTimer()
    {
        yield return new WaitForSeconds(20f);
        Destroy();
    }
    
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
