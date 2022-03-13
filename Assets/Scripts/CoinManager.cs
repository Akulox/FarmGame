using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int moneyCount;
    public TextMeshProUGUI moneyCounterText;
    public Animator animator;
    public GameObject coinPref;
    public RectTransform uiCoin;
    public Camera cam;


    public void CoinFromTargetToCoinUI(Transform target)
    {
        var coin = Instantiate(coinPref, cam.WorldToScreenPoint(target.position), Quaternion.identity);
        coin.transform.SetParent(uiCoin);
        coin.transform.DOLocalMove(Vector3.zero, 0.5f).OnComplete(delegate
        {
            Destroy(coin);
        });
        
    }

    public void AddCoins(int coins)
    {
        moneyCount += coins;
        UpdateMoneyCounter();
        Shake();
    }
    private void UpdateMoneyCounter()
    {
        moneyCounterText.text = moneyCount.ToString();
    }
    private void Shake()
    {
        animator.SetTrigger("Shake");
    }
}
