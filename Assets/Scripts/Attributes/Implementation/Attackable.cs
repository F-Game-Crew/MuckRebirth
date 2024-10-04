using System.Collections.Generic;
using UnityEngine;

public class Attackable : MonoBehaviour
{
    public float damageValue;

    private Viewable view;
    private List<GameObject> targets;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<Viewable>();
        targets = view.entitiesInView;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            foreach (GameObject target in targets)
            {
                Hitable hitableEntity = target.GetComponent<Hitable>();

                if (hitableEntity != null)
                {
                    hitableEntity.TakeDamage(damageValue);
                }
            }
        }
    }
}
