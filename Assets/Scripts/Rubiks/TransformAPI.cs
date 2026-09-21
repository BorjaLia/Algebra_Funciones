using CustomMath;
using UnityEngine;

public class CustomTransformBridge : MonoBehaviour
{
    public MyTransform customTransform = new MyTransform();

    [SerializeField] private Vector3 debugPosition;
    [SerializeField] private Vector3 debugEulerAngles;
    [SerializeField] private Vector3 debugScale;

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
        Vec3 pos = customTransform.localPosition;
        Quat rot = customTransform.localRotation;
        Vec3 scale = customTransform.localScale;
        Vec3 euler = customTransform.localEulerAngles;

        transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
        transform.localRotation = new Quaternion(rot.x, rot.y, rot.z, rot.w);
        transform.localScale = new Vector3(scale.x, scale.y, scale.z);

        debugPosition = new Vector3(pos.x, pos.y, pos.z);
        debugEulerAngles = new Vector3(euler.x, euler.y, euler.z);
        debugScale = new Vector3(scale.x, scale.y, scale.z);
    }

    public void SetDualParent(CustomTransformBridge newParentBridge)
    {
        if (newParentBridge == null) return;

        customTransform.SetParent(newParentBridge.customTransform);

        transform.SetParent(newParentBridge.transform, true);

        transform.localPosition = new Vec3(customTransform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        transform.localRotation = new Quat(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z, transform.localRotation.w);
        transform.localScale = new Vec3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}