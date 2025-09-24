using UnityEngine;

public class FlashlightStun : MonoBehaviour
{
    [SerializeField] private FlashlightController flashlightController;
    [SerializeField] private float stunDuration = 2f;
    [SerializeField] private float detectionDistance = 10f;
    [SerializeField] private float detectionAngle = 30f; // Ángulo del cono de visión

    void Update()
    {
        if (!flashlightController.IsFlashlightOn() || flashlightController.currentIntensity <= flashlightController.LowIntensity)
            return;

        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionDistance);
        foreach (Collider col in colliders)
        {
            Vector3 dirToTarget = (col.transform.position - transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, dirToTarget);

            if (angle < detectionAngle / 2f)
            {
                IStunnable stunnable = col.GetComponent<IStunnable>();
                if (stunnable != null)
                {
                    stunnable.Stun(stunDuration);
                }
            }
        }
    }
}
