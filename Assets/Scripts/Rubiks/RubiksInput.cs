using CustomMath;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RubikInput : MonoBehaviour
{
    [SerializeField] private bool isPositiveRotation = true;
    [SerializeField] private int scrambleMoves = 20;

    [SerializeField] private CustomTransformBridge rootBridge;
    [SerializeField] private float rootMoveSpeed = 3f;
    [SerializeField] private float rootRotSpeed = 90f;
    [SerializeField] private float rootScaleSpeed = 2f;

    public Queue<RubikCommand> commandQueue { get; private set; } = new Queue<RubikCommand>();

    void Update()
    {
        HandleRubikInputs();
        HandleRootTransformInputs();
    }

    private void HandleRubikInputs()
    {
        Keyboard kb = Keyboard.current;

        if (kb.tabKey.wasPressedThisFrame)
        {
            isPositiveRotation = !isPositiveRotation;
            Debug.Log($"{(isPositiveRotation ? "positive" : "negative")}");
        }

        bool isShiftDown = kb.leftShiftKey.isPressed || kb.rightShiftKey.isPressed;
        float currentSlice = isShiftDown ? -1f : 1f;
        float currentAngle = isPositiveRotation ? 90f : -90f;

        if (kb.rKey.wasPressedThisFrame) EnqueueCommand(new Vec3(1, 0, 0), currentSlice, currentAngle);
        else if (kb.uKey.wasPressedThisFrame) EnqueueCommand(new Vec3(0, 1, 0), currentSlice, currentAngle);
        else if (kb.fKey.wasPressedThisFrame) EnqueueCommand(new Vec3(0, 0, 1), currentSlice, currentAngle);

        if (kb.tKey.wasPressedThisFrame)
        {
            ScrambleCube();
        }
    }

    private void HandleRootTransformInputs()
    {
        if (rootBridge == null) return;
        var t = rootBridge.customTransform;

        Keyboard kb = Keyboard.current;

        Vec3 pos = t.localPosition;
        if (kb.upArrowKey.isPressed) pos.y += rootMoveSpeed * Time.deltaTime;
        if (kb.downArrowKey.isPressed) pos.y -= rootMoveSpeed * Time.deltaTime;
        if (kb.rightArrowKey.isPressed) pos.x += rootMoveSpeed * Time.deltaTime;
        if (kb.leftArrowKey.isPressed) pos.x -= rootMoveSpeed * Time.deltaTime;
        if (kb.pageUpKey.isPressed) pos.z += rootMoveSpeed * Time.deltaTime;
        if (kb.pageDownKey.isPressed) pos.z -= rootMoveSpeed * Time.deltaTime;
        t.localPosition = pos;

        Quat deltaRot = Quat.identity;
        if (kb.wKey.isPressed) deltaRot = Quat.AngleAxis(rootRotSpeed * Time.deltaTime, new Vec3(1, 0, 0)) * deltaRot;
        if (kb.sKey.isPressed) deltaRot = Quat.AngleAxis(-rootRotSpeed * Time.deltaTime, new Vec3(1, 0, 0)) * deltaRot;
        if (kb.dKey.isPressed) deltaRot = Quat.AngleAxis(rootRotSpeed * Time.deltaTime, new Vec3(0, 1, 0)) * deltaRot;
        if (kb.aKey.isPressed) deltaRot = Quat.AngleAxis(-rootRotSpeed * Time.deltaTime, new Vec3(0, 1, 0)) * deltaRot;
        if (kb.qKey.isPressed) deltaRot = Quat.AngleAxis(rootRotSpeed * Time.deltaTime, new Vec3(0, 0, 1)) * deltaRot;
        if (kb.eKey.isPressed) deltaRot = Quat.AngleAxis(-rootRotSpeed * Time.deltaTime, new Vec3(0, 0, 1)) * deltaRot;

        t.localRotation = deltaRot * t.localRotation;

        Vec3 scale = t.localScale;

        if (kb.mKey.isPressed) scale += new Vec3(1, 1, 1) * rootScaleSpeed * Time.deltaTime;
        if (kb.nKey.isPressed) scale -= new Vec3(1, 1, 1) * rootScaleSpeed * Time.deltaTime;
        t.localScale = scale;
    }

    private void ScrambleCube()
    {
        Vec3[] axes = new Vec3[] { new Vec3(1, 0, 0), new Vec3(0, 1, 0), new Vec3(0, 0, 1) };
        float[] slices = new float[] { 1f, -1f };
        float[] angles = new float[] { 90f, -90f };

        for (int i = 0; i < scrambleMoves; i++)
        {
            Vec3 randomAxis = axes[Random.Range(0, axes.Length)];
            float randomSlice = slices[Random.Range(0, slices.Length)];
            float randomAngle = angles[Random.Range(0, angles.Length)];

            EnqueueCommand(randomAxis, randomSlice, randomAngle);
        }
    }

    private void EnqueueCommand(Vec3 axis, float slice, float angle)
    {
        RubikCommand newCommand = new RubikCommand { axis = axis, slice = slice, angle = angle };
        commandQueue.Enqueue(newCommand);
        newCommand.Print();
    }
}

public struct RubikCommand
{
    public Vec3 axis;
    public float slice;
    public float angle;

    public override string ToString()
    {
        string face = "";
        bool isClockwise = angle > 0;

        if (axis.x != 0)
        {
            face = slice > 0 ? "R" : "L";
            if (slice < 0) isClockwise = !isClockwise;
        }
        else if (axis.y != 0)
        {
            face = slice > 0 ? "U" : "D";
            if (slice < 0) isClockwise = !isClockwise;
        }
        else if (axis.z != 0)
        {
            face = slice > 0 ? "F" : "B";
            if (slice < 0) isClockwise = !isClockwise;
        }

        return $"{face}{(isClockwise ? "" : "'")}";
    }

    public void Print()
    {
        Debug.Log(ToString());
    }
}


[System.Serializable]
public class AxisData
{
    public string faceName;
    public Vec3 normalDir;
    public CustomTransformBridge pivotBridge;
}