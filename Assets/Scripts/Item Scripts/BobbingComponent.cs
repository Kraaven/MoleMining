using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobbingComponent : MonoBehaviour
{
    private Vector3 InitPosition;

    public float SinDivide;

    public float Multiplier;
    // Start is called before the first frame update
    void Start()
    {
        InitPosition = transform.position;
        SinDivide = 1;
        Multiplier = 1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(0, Mathf.Sin(Time.time * 2f)/10, 0) + InitPosition;
    }
}
