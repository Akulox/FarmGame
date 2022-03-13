using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;


public class Player : MonoBehaviour
{
    public Animator animator;
    public bool isWalking;
    
    public TextMeshProUGUI wheatCounterText;
    public CoinManager coinManager;

    public PlayerAnimationManager playerAnimationManager;
    
    public GameObject stackObject;
    public GameObject barn;

    public int wheatCost;
    public int stackSize;
    public int wheatCount;
    public float sellCD;
    public bool selling;

    private void OnTriggerEnter(Collider other)
    {
        var wheatItem = other.gameObject.GetComponent<WheatItem>();
        if (wheatItem)
        {
            if (IsFree())
            {
                AddWheat();
                wheatItem.Deactivate();
                wheatItem.transform.parent = stackObject.transform;
                wheatItem.transform.DOLocalMove(Vector3.zero, 0.5f);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == barn.name && !isWalking && !selling)
        {
            selling = true;
            SellWheat();
        }
    }

    public void StartWalk()
    {
        isWalking = true;
        selling = false;
        animator.SetBool("IsWalking", isWalking);
    }

    public void UpdateWheatCounter()
    {
        wheatCounterText.text = wheatCount + "/" + stackSize;
    }
    
    public bool IsFree()
    {
        return stackSize - wheatCount > 0;
    }

    public IEnumerator SellItem()
    {
        var wheatItem = stackObject.transform.GetChild(0);
        wheatItem.SetParent(barn.transform);
        wheatItem.DOLocalMove(Vector3.zero, 1f).OnComplete(delegate
        {
            coinManager.CoinFromTargetToCoinUI(barn.transform.GetChild(0));
            wheatItem.GetComponent<WheatItem>().Destroy();
            coinManager.AddCoins(wheatCost);
        });
        
        yield return new WaitForSeconds(sellCD);
        SellWheat();
    }
    
    public void SellWheat()
    {
        if (wheatCount > 0 && selling)
        {
            wheatCount--;
            UpdateWheatCounter();
            StartCoroutine(SellItem());
        }
        else
        {
            selling = false;
        }
    }
    public void AddWheat()
    {
        wheatCount++;
        UpdateWheatCounter();
    }
    public void Swing()
    {
        if (!playerAnimationManager.isSwinging)
        {
            animator.SetTrigger("Swing");
        }
    }
    
    public void WalkInDir(float angle)
    {
        transform.DOMove(new Vector3(
            Mathf.Clamp(transform.position.x + (float)Math.Cos((angle - 45f) * Math.PI / 180) * 0.1f, -9f, 4.5f), 
            transform.position.y, 
            Mathf.Clamp(transform.position.z + (float)Math.Sin((angle - 45f) * Math.PI / 180) * 0.1f, -2.5f, 9.5f)), 0.1f, false);
        transform.GetChild(0).rotation = Quaternion.AngleAxis(angle - 135f, Vector3.down);
    }
    public void StopWalk()
    {
        isWalking = false;
        animator.SetBool("IsWalking", isWalking);
    }
}
