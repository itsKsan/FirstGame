using System;
using UnityEngine;
using UnityEngine.UI;
public class Healthbar : MonoBehaviour
{
    [SerializeField] private Image healthbarSprite;
    [SerializeField] private float reduseSpeed = 2f;
    private float _target = 1;
    private Camera _cam;

    private void Awake()
    {
        _cam = Camera.main;
    }

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        _target = currentHealth / maxHealth;
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - _cam.transform.position);
        healthbarSprite.fillAmount =
            Mathf.MoveTowards(healthbarSprite.fillAmount, _target, reduseSpeed * Time.deltaTime);
    }
}
