using UnityEngine;

public class CameraRig : MonoBehaviour
{
    [Header("Transition")]
    [Range(0f, 1f)]
    public float transition;

    [Header("Hands")]
    public Transform head;
    public Transform handLeft;
    public Transform handRight;
    [Range(0f, 3f)]
    public float handsSmoothTime = 0.3f;
    public float handsDistanceMultiplier = 1f;
    public Vector3 handsCamOffset;

    private Vector3 velocityHandsMidPoint = Vector3.zero;
    private Vector3 lastHandsMidPoint;

    [Header("Orbit")]
    public SkinnedMeshRenderer avatarRenderer;
    public Transform orbitCenterTarget;

    public float radius = 5f;
    [Range(0f, 1f)]
    public float orbitalSpeed = 0.1f;
    [Range(0f, 1f)]
    public float orbitalSpeedWeight = 0f;
    [Range(0f, 3f)]
    public float orbitalSmoothTime = 0.3f;
    private float angle = 0f;
    private Vector3 lastCenter;

    private Vector3 velocityOrbit = Vector3.zero;

    [Header("FOV")]

    [Header("Offset")]
    public bool useOffset = false;
    public Vector3 offset;

    private Quaternion rotationOriginal;

    private void Awake()
    {
        //lastCenter = avatarRenderer.bounds.center;
        //if(orbitCenterTarget)
        //{
            lastCenter = orbitCenterTarget.position;
        //}
        lastHandsMidPoint = Vector3.Lerp(handLeft.position, handRight.position, 0.5f);
        //lastHead = head.position;

        rotationOriginal = transform.rotation;
    }

    private void Update()
    {
        float handsDistance = Vector3.Distance(handLeft.position, handRight.position);
        Vector3 handsMidPoint = Vector3.Lerp(handLeft.position, handRight.position, 0.5f);
        Vector3 handsCenter = Vector3.SmoothDamp(lastHandsMidPoint, handsMidPoint, ref velocityHandsMidPoint, handsSmoothTime);
        lastHandsMidPoint = handsCenter;

        Vector3 handsPosition = handsCenter + new Vector3(0, handsDistanceMultiplier, 0) + handsCamOffset;

        angle += ((2 * Mathf.PI) / (1f / (orbitalSpeed * orbitalSpeedWeight))) * Time.deltaTime * transition;
        Vector3 orbitCenter;

        //if (orbitCenterTarget)
        //{
            orbitCenter = Vector3.SmoothDamp(orbitCenterTarget.position, lastCenter, ref velocityOrbit, orbitalSmoothTime);
            lastCenter = orbitCenterTarget.position;
        //}
        //else
        //{
        //    orbitCenter = Vector3.SmoothDamp(avatarRenderer.bounds.center, lastCenter, ref velocityOrbit, orbitalSmoothTime);
        //    lastCenter = avatarRenderer.bounds.center;
        //}

        //Vector3 orbitPosition = new Vector3(Mathf.Cos(angle) * radius, orbitCenter.y, Mathf.Sin(angle) * radius);
        Vector3 orbitPosition = new Vector3(orbitCenter.x + Mathf.Cos(angle) * radius, orbitCenterTarget.position.y, orbitCenter.z + Mathf.Sin(angle) * radius);

        transform.position = Vector3.Lerp(handsPosition, orbitPosition, transition);
        transform.LookAt(Vector3.Lerp(handsCenter, orbitCenter, transition));

        if (useOffset)
        {
            transform.Translate(offset);
        }
    }

    public void SetTransitionValue(float value)
    {
        transition = value;
    }

    public void SetOrbitalSpeedWeight(float value)
    {
        orbitalSpeedWeight = value;
    }

    public void SetDistance(float value)
    {
        radius = 0.1f + value * 2f;
    }
}