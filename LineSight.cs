using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSight : MonoBehaviour
{

    public enum SightSensitivity { STRICT, LOOSE };

    public SightSensitivity Sensitity = SightSensitivity.STRICT;

    public float FieldofView = 45f;

    public Transform Eyepoint = null;

    public Transform Target = null;

    public bool CanSeeTarget = false;

    private Transform ThisTransform = null;

    private SphereCollider ThisCollider = null;

    public Vector3 LastKnowSighting = Vector3.zero;


    void Awake()
    {
        ThisTransform = GetComponent<Transform>();

        ThisCollider = GetComponent<SphereCollider>();

        LastKnowSighting = ThisTransform.position;

    }

    void OnTriggerStay(Collider Other)
    {
        UpdateSight();

        if (CanSeeTarget)
        {
            LastKnowSighting = Target.position;
        }

    }

    bool InFOV()
    {
        Vector3 DirToTarget = Target.position - Eyepoint.position;

        float Angle = Vector3.Angle(Eyepoint.forward, DirToTarget);

        if (Angle <= FieldofView)

        {
            return true;
        }
        return false;
    }

    bool ClearLineofSight()
    {
        RaycastHit Info;

        if (Physics.Raycast(Eyepoint.position, (Target.position - Eyepoint.position).normalized, out Info, ThisCollider.radius))
        {
            Debug.DrawLine(Eyepoint.position, Target.position, Color.green);

            if (Info.transform.CompareTag("Snek"))
            {

                return true;
            }
        }
        return false;

    }
    void UpdateSight()
    {
        switch (Sensitity)
        {
            case SightSensitivity.STRICT:

                CanSeeTarget = InFOV() && ClearLineofSight();

                break;

            case SightSensitivity.LOOSE:

                CanSeeTarget = InFOV() || ClearLineofSight();

                break;
        }
    }
}

