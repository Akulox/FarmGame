using System.Collections;
using UnityEngine;

public class Wheat : MonoBehaviour
{
    public GameObject wheatTop;
    public GameObject wheatItem;
    public float growingTime;
    public bool isGrown;

    public void Cut()
    {
        if (isGrown)
        {
            wheatTop.SetActive(false);
            isGrown = false;
            StartCoroutine(StartGrowing());
            var wheatClone = Instantiate(wheatItem, wheatTop.transform.position + Vector3.up, Quaternion.identity);
            wheatClone.GetComponent<WheatItem>().Drop();
        }
    }
    
    IEnumerator StartGrowing()
    {
        yield return new WaitForSeconds(growingTime);
        isGrown = true;
        wheatTop.SetActive(true);
    }
}
