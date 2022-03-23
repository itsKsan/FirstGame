using System;
using UnityEngine;

public class ShellPrediction : MonoBehaviour
{
    [SerializeField] private float scaleTime = 3;
    private Vector3 _maxScale;
    private void Start()
    {
        _maxScale = new Vector3(0.5f, 1, 0.5f);
        
        Destroy(gameObject, 3f);
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, _maxScale, Time.deltaTime);
    }
}
