using CustomMath;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubikController : MonoBehaviour
{
    [SerializeField] private RubikInput inputManager;
    [SerializeField] private CustomTransformBridge rootCube;

    [SerializeField] private List<AxisData> faceAxes = new List<AxisData>();

    [SerializeField] private List<CustomTransformBridge> loosePieces = new List<CustomTransformBridge>();

    [SerializeField] private float rotationSpeed = 270f;

    private bool isAnimating = false;

    void Update()
    {
        if (!isAnimating && inputManager.commandQueue.Count > 0)
        {
            RubikCommand cmd = inputManager.commandQueue.Dequeue();
            StartCoroutine(AnimateRotation(cmd));
        }
    }

    private IEnumerator AnimateRotation(RubikCommand cmd)
    {
        isAnimating = true;

        Vec3 faceNormal = cmd.axis * cmd.slice;
        CustomTransformBridge activeAxis = null;

        //buscar eje
        foreach (var axis in faceAxes)
        {
            if (Vec3.Dot(axis.normalDir, faceNormal) > 0.9f)
            {
                activeAxis = axis.pivotBridge;
                break;
            }
        }

        if (activeAxis != null)
        {
            List<CustomTransformBridge> activePieces = new List<CustomTransformBridge>();

            //buscar caras activas
            foreach (var piece in loosePieces)
            {
                float dot = Vec3.Dot(piece.customTransform.localPosition, cmd.axis);
                if ((cmd.slice > 0 && dot > 0.5f) || (cmd.slice < 0 && dot < -0.5f))
                {
                    activePieces.Add(piece);

                    piece.SetDualParent(activeAxis);
                }
            }

            Quat startRot = activeAxis.customTransform.localRotation;
            Quat deltaRot = Quat.AngleAxis(cmd.angle, cmd.axis);
            Quat targetRot = deltaRot * startRot;

            float duration = Mathf.Abs(cmd.angle) / rotationSpeed;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / duration;

                activeAxis.customTransform.localRotation = Quat.Slerp(startRot, targetRot, t);

                yield return null;
            }

            activeAxis.customTransform.localRotation = targetRot;

            foreach (var piece in activePieces)
            {
                piece.SetDualParent(rootCube);

                Vec3 p = piece.customTransform.localPosition;
                piece.customTransform.localPosition = new Vec3(Mathf.Round(p.x), Mathf.Round(p.y), Mathf.Round(p.z));

                Vec3 e = piece.customTransform.localEulerAngles;
                piece.customTransform.localEulerAngles = new Vec3(
                    Mathf.Round(e.x / 90f) * 90f,
                    Mathf.Round(e.y / 90f) * 90f,
                    Mathf.Round(e.z / 90f) * 90f
                );

                piece.customTransform.localRotation = Quat.Euler( new Vec3(
                    piece.customTransform.localEulerAngles.x,
                    piece.customTransform.localEulerAngles.y,
                    piece.customTransform.localEulerAngles.z
                ));
            }
        }

        isAnimating = false;
    }
}