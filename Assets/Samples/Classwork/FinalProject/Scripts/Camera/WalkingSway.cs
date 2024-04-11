using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingSway : MonoBehaviour
{
    //walking sway points
    [SerializeField] private List<Transform> _walkingSwayTransforms;
    [SerializeField] private int _currentSwayTransformIndex = 0;
    [SerializeField] private float _walkSwaySpeed = 4f;

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponentInParent<Rigidbody>().velocity.x != 0)
        {
            CallWalkingSway();
        }
    }

    private void CallWalkingSway()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(this.transform.position.x, _walkingSwayTransforms[_currentSwayTransformIndex].position.y, this.transform.position.z), (Time.deltaTime * _walkSwaySpeed));
        float distanceBetweenPoints = Vector3.Distance(this.transform.position, _walkingSwayTransforms[_currentSwayTransformIndex].position);

        //get pretty close (doesn't need to be exact)
        if (distanceBetweenPoints <= 0.01f)
            _currentSwayTransformIndex = (_currentSwayTransformIndex + 1) % _walkingSwayTransforms.Count;
    }
}
