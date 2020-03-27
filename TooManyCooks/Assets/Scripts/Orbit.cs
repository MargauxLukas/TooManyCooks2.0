using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    private TurningTable table;
    private void Start()
    {
        table = TurningTable.instance;
    }
    void Update()
    {
        OrbitAround();
    }

    void OrbitAround()
    {
        transform.RotateAround(table.transform.position, Vector3.forward, table.speed * Time.fixedDeltaTime);
    }
}
