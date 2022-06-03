using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Image backGround;
    private float updateInSec = 0.2f;


    public void SubscribeToHealthChanged()
    {
        GetComponentInParent<Model>().Hero.OnHealthPercentageChanged += HandleHealthChanged;
    }

    private void HandleHealthChanged(float percentage)
    {
        StartCoroutine(ChangeHealthSmoothly(percentage));
    }

    IEnumerator ChangeHealthSmoothly(float percentage)
    {
        float preChangePercentage = healthBar.fillAmount;
        float elapsed = 0.0f;
        while(elapsed < updateInSec)
        {
            elapsed += Time.deltaTime;
            healthBar.fillAmount = Mathf.Lerp(preChangePercentage, percentage, elapsed / updateInSec);
            yield return null;
        }
        Debug.Log(percentage);
        healthBar.fillAmount = percentage;
    }

    private void LateUpdate()
    {
        //transform.LookAt(Camera.main.transform);
        //backGround.transform.LookAt(Camera.main.transform);
        //transform.Rotate(0, 180, 0);
    }
}
