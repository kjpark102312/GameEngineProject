using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitleUI : MonoBehaviour
{
    [SerializeField]
    private Image Title;
    [SerializeField]
    private CanvasGroup CG;

    private bool isStart;

    void Start()
    { 
        Title.DOFade(0 , 1f)
        .SetLoops(-1, LoopType.Yoyo)
        .SetEase(Ease.Linear);
    }


    private void Update()
    {
        if(isStart)
        {
            CG.alpha -= Time.deltaTime * 5f;
        }
    }

    public void ClickStartPanel()
    {
        isStart = true;
        CG.blocksRaycasts = false;
        CG.interactable = false;
    }
}
