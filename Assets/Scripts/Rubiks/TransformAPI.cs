using CustomMath;
using UnityEngine;

public class CustomTransformBridge : MonoBehaviour
{
    public MyTransform customTransform = new MyTransform();

    private void Awake()
    {
        customTransform.localPosition = new Vec3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        customTransform.localRotation = new Quat(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z, transform.localRotation.w);
        customTransform.localScale = new Vec3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void Start()
    {
        if (transform.parent != null)
        {
            CustomTransformBridge parentBridge = transform.parent.GetComponent<CustomTransformBridge>();
            if (parentBridge != null)
            {
                customTransform.SetParent(parentBridge.customTransform);
            }
        }
    }

    private void LateUpdate()
    {
        transform.localPosition = new Vector3(customTransform.localPosition.x, customTransform.localPosition.y, customTransform.localPosition.z);
        transform.localRotation = new Quaternion(customTransform.localRotation.x, customTransform.localRotation.y, customTransform.localRotation.z, customTransform.localRotation.w);
        transform.localScale = new Vector3(customTransform.localScale.x, customTransform.localScale.y, customTransform.localScale.z);
    }
}