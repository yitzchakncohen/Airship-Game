using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedBurstUI : MonoBehaviour
{
    [SerializeField] GameObject overlay;
    Image circle;
    float timer;
    float endTime;

    void Awake()
    {
        circle = overlay.GetComponent<Image>();
    }

    void OnEnable()
    {
        circle.fillAmount = 0;
        timer = 0f;
    }

    private void Update() 
    {
        timer += Time.deltaTime;
        circle.fillAmount = timer / endTime ;
    }

        void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;
    }

    public void UpdateCircle(float endTimeAmount) 
    {
        endTime = endTimeAmount;
    }


}
