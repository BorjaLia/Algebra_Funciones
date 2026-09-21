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

            List<Vec3> initialPositions = new List<Vec3>();
            List<Quat> initialRotations = new List<Quat>();
            
            foreach (var piece in loosePieces)
            {
                float dot = Vec3.Dot(piece.customTransform.localPosition, cmd.axis);
                if ((cmd.slice > 0 && dot > 0.5f) || (cmd.slice < 0 && dot < -0.5f))
                {
                    activePieces.Add(piece);
                    initialPositions.Add(piece.customTransform.localPosition);
                    initialRotations.Add(piece.customTransform.localRotation);
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

                float currentAngle = Mathf.Lerp(0, cmd.angle, t);
                Quat currentOrbitRot = Quat.AngleAxis(currentAngle, cmd.axis);

                for (int i = 0; i < activePieces.Count; i++)
                {
                    Vec3 newPos = currentOrbitRot * initialPositions[i];
                    Quat newRot = currentOrbitRot * initialRotations[i];

                    activePieces[i].customTransform.localPosition = newPos;
                    activePieces[i].customTransform.localRotation = newRot;
                }

                yield return null;
            }

            activeAxis.customTransform.localRotation = targetRot;

            Quat finalOrbitRot = Quat.AngleAxis(cmd.angle, cmd.axis);

            for (int i = 0; i < activePieces.Count; i++)
            {
                Vec3 finalPos = finalOrbitRot * initialPositions[i];
                Quat finalRot = finalOrbitRot * initialRotations[i];

                activePieces[i].customTransform.localPosition = new Vec3(Mathf.Round(finalPos.x), Mathf.Round(finalPos.y), Mathf.Round(finalPos.z));

                activePieces[i].customTransform.localRotation = finalRot;

                Vec3 e = activePieces[i].customTransform.localEulerAngles;
                activePieces[i].customTransform.localEulerAngles = new Vec3(
                    Mathf.Round(e.x / 90f) * 90f,
                    Mathf.Round(e.y / 90f) * 90f,
                    Mathf.Round(e.z / 90f) * 90f
                );
            }
        }
        isAnimating = false;
    }
}