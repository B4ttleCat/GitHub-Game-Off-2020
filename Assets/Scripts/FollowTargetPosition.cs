using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTargetPosition : MonoBehaviour
{
    [SerializeField]
    private Transform _targetTransform;

    void Update()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        transform.position = new Vector2(_targetTransform.position.x, transform.position.y);
    }
}
