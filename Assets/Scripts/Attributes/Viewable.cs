using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viewable : MonoBehaviour
{
    public float radius; 
    [Range(0,360)]
    public int angle;
    public GameObject targetRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeeTarget;

    public void Start () {
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine() {
        float timeDelay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(timeDelay);

        while (true) {
            yield return wait;
            CheckInRange();
        }

    }

    private void CheckInRange() {
        Collider[] targets = Physics.OverlapSphere(transform.position, radius, targetMask);
        if (targets.Length > 0) {
            Transform target = targets[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2) {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask)) {
                    canSeeTarget = true;
                } else 
                    canSeeTarget = false;
            }
            else 
                canSeeTarget = false;
        } else if (canSeeTarget) 
            canSeeTarget = false;
    }

}
