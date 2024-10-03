using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Viewable : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public int angle;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public List<GameObject> entitiesInView { get; set; } = new List<GameObject>();
    public Collider[] entitiesInRadius { get; set; } = new Collider[10];

    public void Start()
    {
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        float timeDelay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(timeDelay);

        while (true)
        {
            yield return wait;
            CheckInRange();
            if (entitiesInView.Count > 0)
            {
                Debug.Log(transform.name + ": " + entitiesInView.Count);
            }
        }

    }

    private void CheckInRange()
    {
        entitiesInView.Clear();
        for (int i = 0; i < entitiesInRadius.Length; i++)
        {
            entitiesInRadius[i] = null; // Clear the array
        }

        int count = Physics.OverlapSphereNonAlloc(transform.position, radius, entitiesInRadius, targetMask);
        for (int i = 0; i < count; i++)
        {
            Transform entityTransform = entitiesInRadius[i].transform;
            Vector3 directionToTarget = (entityTransform.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, entityTransform.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    GameObject entityGameObject = entitiesInRadius[i].gameObject;
                    if (entityGameObject != null)
                    {
                        entitiesInView.Add(entityGameObject);
                    }
                }
            }

        }
    }
}
