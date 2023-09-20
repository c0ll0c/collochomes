using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player -> Infect, The panel is inactive
// 단서 모음집 판넬이 켜진 상태로 감염됐으면 그 즉시 

public class CollectionPanelInavtive : MonoBehaviour
{
    static public bool IsInfected = false;
    public GameObject CollectionPanel;        // 단서 모음집 판넬

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsInfected)
        {
            CollectionPanel.SetActive(false);
            GameManager.instance.isAlert = false;
        }
    }
}
