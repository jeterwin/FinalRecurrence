using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Cameras;

public class IsVisible : MonoBehaviour
{
    /*    public Camera camera;
        public Renderer r;
        public LookatTarget target;
        private void Start()
        {
            target = GetComponent<LookatTarget>();
        }
        private void Update()
        {
            if(Function(r, camera) == true)
            {
                target.enabled = false;
            }
            else
                target.enabled = true;
        }
        public bool Function(Renderer r, Camera camera)
        {
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
            return GeometryUtility.TestPlanesAABB(planes, r.bounds);
        }*/
    public Transform other;
    public LookatTarget target;
    void Update()
    {
        if (other)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 toOther = other.position - transform.position;

            if (Vector3.Dot(forward, toOther) < 0)
            {
                target.enabled = true;
            }
            else
                target.enabled = false;
        }
    }
}
