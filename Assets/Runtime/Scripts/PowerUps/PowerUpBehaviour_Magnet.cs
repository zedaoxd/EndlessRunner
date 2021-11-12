using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviour_Magnet : PowerUpBehaviour
{
    [SerializeField] private float attractSpeed = 10;
    [SerializeField] private float scaleSpeed = 2;
    [SerializeField] private float finalScaleMultipler = 0.3f;
    [SerializeField] private Vector3 attractionBox = Vector3.one * 10;

    private List<AbstractPickup> pickupsToAttract = new List<AbstractPickup>();
    private Collider[] overlapResults = new Collider[20];


    public void Activate(float duration)
    {
        ActivateForDuration(duration);
    }

    protected override void EndBehaviour()
    {
        pickupsToAttract.Clear();
    }

    protected override void StartBehaviour()
    {
        pickupsToAttract.Clear();
    }

    protected override void UpdateBehaviour()
    {
        GatherPickupsInRange();
        foreach (AbstractPickup pickup in pickupsToAttract)   
        {
            if (pickup != null)
            {
                Vector3 startPos = pickup.transform.position;
                Vector3 endPos = transform.position;
                pickup.transform.position = Vector3.MoveTowards(startPos, endPos, Time.deltaTime * attractSpeed);

                Vector3 startScale = pickup.transform.localScale;
                Vector3 endScale = Vector3.one * finalScaleMultipler;
                pickup.transform.localScale = Vector3.MoveTowards(startScale, endScale, Time.deltaTime * scaleSpeed);
            }
        }
    }

    private void GatherPickupsInRange()
    {
        int overlapCount = Physics.OverlapBoxNonAlloc(transform.position, attractionBox, overlapResults);
        for (int i = 0; i < overlapCount; i++)
        {
            AbstractPickup pickup = overlapResults[i].GetComponent<AbstractPickup>();
            if (pickup != null && !(pickup is AbstractPowerUp) && !pickupsToAttract.Contains(pickup))
            {
                pickupsToAttract.Add(pickup);
            }
        }
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, attractionBox);
    }
}
