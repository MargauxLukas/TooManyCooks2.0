using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningTable : MonoBehaviour
{
    public static TurningTable instance;
    public float speed;

    [Header("Slots Placement")]
    public Transform slotsGroup;
    public GameObject prefab;
    public int numSlots;
    public float circleRadius;
    public int slotSpacing;

    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        instance = this;
    }

    [ContextMenu("SpawnSlots")]
    void SpawnSlots()
    {
        List<GameObject> childs = new List<GameObject>();
        if (slotsGroup.childCount > 0)
        {
            childs = UtilityFunctions.instance.GetAllChildren(slotsGroup.gameObject);
            foreach(GameObject _go in childs)
            {
                DestroyImmediate(_go);
            }

            childs.Clear();
        }

        float ang = 0;
        Vector3 center = transform.position;

        for (int i = 0; i < numSlots; i++)
        {
            ang += slotSpacing;
            Vector3 pos = Circle(center, circleRadius, ang);
            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
            Instantiate(prefab, pos, rot, slotsGroup);
        }
    }

    Vector3 Circle(Vector3 center, float radius, float angle)
    {
        Vector3 _pos;
        _pos.x = center.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        _pos.y = center.y + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        _pos.z = center.z;
        return _pos;
    }
}