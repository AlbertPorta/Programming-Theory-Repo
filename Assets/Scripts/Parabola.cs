using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parabola : MonoBehaviour
{
    public Transform tr1;
    public Transform tr2;
    public GameObject go;

    public float speed = 10;
    public float a = 10;

    void Start()
    {
        Reset();
        InvokeRepeating("Reset", 2, 5);
    }

    float deltaTime = 0;
    void Update()
    {
        ParabolaMovement();
    }

    private void ParabolaMovement()
    {
        a = -Mathf.Abs(a);
        speed = Mathf.Abs(speed);
        deltaTime += Time.deltaTime;

        Vector3 vec = (tr2.position - tr1.position).normalized;
        float dis = Vector3.Distance(tr1.position, tr2.position);

        float x = deltaTime * speed;
        float y = a * deltaTime * deltaTime - a * (dis / speed) * deltaTime;
        go.transform.position = vec * x + Vector3.up * y;
    }

    void Reset()
    {
        deltaTime = 0;
        go.transform.position = tr1.position;
    }
}
