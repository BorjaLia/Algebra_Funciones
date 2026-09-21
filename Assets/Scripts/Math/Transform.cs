using CustomMath;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomMath
{
    public class MyTransform : IEnumerable, IEquatable<MyTransform>
    {
        #region Variables

        public string name = "Transform";

        private Vec3 _LocalPosition = new Vec3(0, 0, 0);
        private Quat _LocalRotation = Quat.identity;
        private Vec3 _LocalScale = new Vec3(1, 1, 1);

        private MyTransform _Parent = null;
        private List<MyTransform> _Children = new List<MyTransform>();

        private bool _HasChanged = false;
        private int _HierarchyCapacity = 0;

        private Mat4x4 _LocalToWorldMatrix = Mat4x4.identity;
        private Mat4x4 _WorldToLocalMatrix = Mat4x4.identity;
        private bool _IsDirty = true;

        #endregion

        #region Properties
        //Properties

        #region Hierarchy

        //childCount    The number of children the parent Transform has.
        public int childCount { get { return _Children.Count; } }

        //hasChanged    Has the transform changed since the last time the flag was set to 'false'?
        public bool hasChanged { get { return _HasChanged; } set { _HasChanged = value; } }

        //hierarchyCapacity The transform capacity of the transform's hierarchy data structure.
        public int hierarchyCapacity { get { return _HierarchyCapacity; } set { _HierarchyCapacity = value; } }

        //hierarchyCount    The number of transforms in the transform's hierarchy data structure.
        public int hierarchyCount
        {
            get
            {
                int count = 1;
                for (int i = 0; i < _Children.Count; i++)
                {
                    count += _Children[i].hierarchyCount;
                }
                return count;
            }
        }

        //parent    The parent of the transform.
        public MyTransform parent { get { return _Parent; } set { SetParent(value); } }

        //root  Returns the topmost transform in the hierarchy.
        public MyTransform root
        {
            get
            {
                MyTransform current = this;
                while (current.parent != null)
                {
                    current = current.parent;
                }
                return current;
            }
        }

        #endregion

        #region Matrix

        //localToWorldMatrix    Matrix that transforms a point from local space into world space (Read Only).
        public Mat4x4 localToWorldMatrix 
        { 
            get 
            { 
                UpdateMatrices(); 
                return _LocalToWorldMatrix; 
            } 
        }

        //worldToLocalMatrix    Matrix that transforms a point from world space into local space (Read Only).
        public Mat4x4 worldToLocalMatrix 
        { 
            get 
            { 
                UpdateMatrices(); 
                return _WorldToLocalMatrix; 
            } 
        }

        #endregion

        #region Directions

        //forward   Returns a normalized vector representing the blue axis of the transform in world space.
        public Vec3 forward 
        { 
            get { return rotation * new Vec3(0, 0, 1); } 
            set { LookAt(position + value, up); } 
        }

        //right The red axis of the transform in world space.
        public Vec3 right 
        { 
            get { return rotation * new Vec3(1, 0, 0); } 
            set { rotation = Quat.LookRotation(forward, new Vec3(0, 1, 0)) * Quat.Euler(new Vec3(0, 90, 0)); } 
        }

        //up    The green axis of the transform in world space.
        public Vec3 up 
        { 
            get { return rotation * new Vec3(0, 1, 0); } 
            set { LookAt(position + forward, value); } 
        }

        #endregion

        #region Position

        //position  The world space position of the Transform.
        public Vec3 position 
        { 
            get 
            { 
                UpdateMatrices();
                return new Vec3(_LocalToWorldMatrix.m03, _LocalToWorldMatrix.m13, _LocalToWorldMatrix.m23);
            } 
            set 
            { 
                if (_Parent != null)
                {
                    localPosition = _Parent.InverseTransformPoint(value);
                }
                else
                {
                    localPosition = value;
                }
            } 
        }

        //localPosition Position of the transform relative to the parent transform.
        public Vec3 localPosition 
        { 
            get { return _LocalPosition; } 
            set 
            { 
                _LocalPosition = value; 
                SetDirty(); 
            } 
        }

        #endregion

        #region Rotation

        //eulerAngles   The rotation as Euler angles in degrees.
        public Vec3 eulerAngles 
        { 
            get { return rotation.eulerAngles; } 
            set { rotation = Quat.Euler(new Vec3(value.x, value.y, value.z)); } 
        }

        //localEulerAngles  The rotation as Euler angles in degrees relative to the parent transform's rotation.
        public Vec3 localEulerAngles 
        { 
            get { return _LocalRotation.eulerAngles; } 
            set { localRotation = Quat.Euler(new Vec3(value.x, value.y, value.z)); } 
        }

        //localRotation The rotation of the transform relative to the transform rotation of the parent.
        public Quat localRotation 
        { 
            get { return _LocalRotation; } 
            set 
            { 
                _LocalRotation = value; 
                SetDirty(); 
            } 
        }

        //rotation  A Quaternion that stores the rotation of the Transform in world space.
        public Quat rotation 
        { 
            get 
            { 
                if (_Parent != null)
                {
                    return _Parent.rotation * _LocalRotation;
                }
                return _LocalRotation;
            } 
            set 
            { 
                if (_Parent != null)
                {
                    localRotation = Quat.Inverse(_Parent.rotation) * value;
                }
                else
                {
                    localRotation = value;
                }
            } 
        }

        #endregion

        #region Scale

        //localScale    The scale of the transform relative to the GameObjects parent.
        public Vec3 localScale 
        { 
            get { return _LocalScale; } 
            set 
            { 
                _LocalScale = value; 
                SetDirty(); 
            } 
        }

        //lossyScale    The global scale of the object (Read Only).
        public Vec3 lossyScale 
        { 
            get 
            { 
                UpdateMatrices();
                float x = MathF.Sqrt(_LocalToWorldMatrix.m00 * _LocalToWorldMatrix.m00 + _LocalToWorldMatrix.m10 * _LocalToWorldMatrix.m10 + _LocalToWorldMatrix.m20 * _LocalToWorldMatrix.m20);
                float y = MathF.Sqrt(_LocalToWorldMatrix.m01 * _LocalToWorldMatrix.m01 + _LocalToWorldMatrix.m11 * _LocalToWorldMatrix.m11 + _LocalToWorldMatrix.m21 * _LocalToWorldMatrix.m21);
                float z = MathF.Sqrt(_LocalToWorldMatrix.m02 * _LocalToWorldMatrix.m02 + _LocalToWorldMatrix.m12 * _LocalToWorldMatrix.m12 + _LocalToWorldMatrix.m22 * _LocalToWorldMatrix.m22);
                return new Vec3(x, y, z);
            } 
        }

        #endregion

        #endregion

        #region Constructors
        #endregion

        #region Methods

        private void SetDirty()
        {
            if (!_IsDirty)
            {
                _IsDirty = true;
                _HasChanged = true;
                for (int i = 0; i < _Children.Count; i++)
                {
                    _Children[i].SetDirty();
                }
            }
        }

        private void UpdateMatrices()
        {
            if (_IsDirty)
            {
                Mat4x4 localMat = Mat4x4.TRS(_LocalPosition, _LocalRotation, _LocalScale);
                
                if (_Parent != null)
                {
                    _LocalToWorldMatrix = _Parent.localToWorldMatrix * localMat;
                }
                else
                {
                    _LocalToWorldMatrix = localMat;
                }
                
                _WorldToLocalMatrix = Mat4x4.Inverse(_LocalToWorldMatrix);
                _IsDirty = false;
            }
        }

        //Public Methods

        #region Hierarchy

        //DetachChildren Unparents all children.
        public void DetachChildren()
        {
            for (int i = _Children.Count - 1; i >= 0; i--)
            {
                _Children[i].SetParent(null, true);
            }
        }

        //Find    Finds a child by n and returns it.
        public MyTransform Find(string n)
        {
            if (name == n) return this;
            
            for (int i = 0; i < _Children.Count; i++)
            {
                MyTransform result = _Children[i].Find(n);
                if (result != null) return result;
            }
            
            return null;
        }

        //GetChild Returns a transform child by index.
        public MyTransform GetChild(int index)
        {
            if (index < 0 || index >= _Children.Count) throw new ArgumentOutOfRangeException(nameof(index), "GetChild out of range!");
            return _Children[index];
        }

        //GetSiblingIndex Gets the sibling index.
        public int GetSiblingIndex()
        {
            if (_Parent == null) return 0;
            return _Parent._Children.IndexOf(this);
        }

        //IsChildOf Is this transform a child of parent?
        public bool IsChildOf(MyTransform parent)
        {
            if (parent == null) return false;
            
            MyTransform curr = this;
            while (curr != null)
            {
                if (curr == parent) return true;
                curr = curr.parent;
            }
            return false;
        }

        //SetAsFirstSibling Move the transform to the start of the local transform list.
        public void SetAsFirstSibling()
        {
            if (_Parent == null) return;
            _Parent._Children.Remove(this);
            _Parent._Children.Insert(0, this);
        }

        //SetAsLastSibling Move the transform to the end of the local transform list.
        public void SetAsLastSibling()
        {
            if (_Parent == null) return;
            _Parent._Children.Remove(this);
            _Parent._Children.Add(this);
        }

        //SetParent Set the parent of the transform.
        public void SetParent(MyTransform p)
        {
            SetParent(p, true);
        }

        public void SetParent(MyTransform parent, bool worldPositionStays)
        {
            if (parent == this) return;

            MyTransform curr = parent;
            while (curr != null)
            {
                if (curr == this) return; 
                curr = curr.parent;
            }

            if (worldPositionStays)
            {
                Vec3 wPos = position;
                Quat wRot = rotation;
                Vec3 wScale = lossyScale;

                if (_Parent != null)
                {
                    _Parent._Children.Remove(this);
                }

                _Parent = parent;

                if (_Parent != null)
                {
                    _Parent._Children.Add(this);
                    
                    Vec3 pScale = _Parent.lossyScale;
                    if (pScale.x != 0 && pScale.y != 0 && pScale.z != 0)
                    {
                        localScale = new Vec3(wScale.x / pScale.x, wScale.y / pScale.y, wScale.z / pScale.z);
                    }
                    else
                    {
                        localScale = wScale;
                    }
                }
                else
                {
                    localScale = wScale;
                }

                position = wPos;
                rotation = wRot;
            }
            else
            {
                if (_Parent != null)
                {
                    _Parent._Children.Remove(this);
                }

                _Parent = parent;

                if (_Parent != null)
                {
                    _Parent._Children.Add(this);
                }
                SetDirty();
            }
        }

        //SetSiblingIndex Sets the sibling index.
        public void SetSiblingIndex(int index)
        {
            if (_Parent == null) return;
            
            _Parent._Children.Remove(this);
            
            if (index < 0) index = 0;
            if (index > _Parent._Children.Count) index = _Parent._Children.Count;
            
            _Parent._Children.Insert(index, this);
        }

        #endregion

        #region Position & Rotation

        //GetLocalPositionAndRotation Gets the local space position and rotation of the Transform component.
        public void GetLocalPositionAndRotation(out Vec3 localPosition, out Quat localRotation)
        {
            localPosition = this.localPosition;
            localRotation = this.localRotation;
        }

        //GetPositionAndRotation Gets the world space position and rotation of the Transform component.
        public void GetPositionAndRotation(out Vec3 position, out Quat rotation)
        {
            position = this.position;
            rotation = this.rotation;
        }

        //SetPositionAndRotation Sets the world space position and rotation of the Transform component.
        public void SetPositionAndRotation(Vec3 position, Quat rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }

        //SetLocalPositionAndRotation Sets the local space position and rotation of the Transform component.
        public void SetLocalPositionAndRotation(Vec3 localPosition, Quat localRotation)
        {
            this.localPosition = localPosition;
            this.localRotation = localRotation;
        }

        #endregion

        #region Rotation

        //LookAt  Rotates the transform so the forward vector points at /target/'s current position.
        public void LookAt(MyTransform target)
        {
            if (target == null) return;
            LookAt(target.position, new Vec3(0, 1, 0));
        }

        public void LookAt(MyTransform target, Vec3 worldUp)
        {
            if (target == null) return;
            LookAt(target.position, worldUp);
        }

        public void LookAt(Vec3 worldPosition)
        {
            LookAt(worldPosition, new Vec3(0, 1, 0));
        }

        public void LookAt(Vec3 worldPosition, Vec3 worldUp)
        {
            Vec3 dir = worldPosition - position;
            if (dir.sqrMagnitude > 0.0001f)
            {
                rotation = Quat.LookRotation(dir, worldUp);
            }
        }

        //Rotate Use Transform.Rotate to rotate GameObjects in a variety of ways. The rotation is often provided as an Euler angle and not a Quaternion.
        public void Rotate(Vec3 eulers)
        {
            Rotate(eulers, Space.Self);
        }

        public void Rotate(Vec3 eulers, Space relativeTo)
        {
            Quat q = Quat.Euler(new Vec3(eulers.x, eulers.y, eulers.z));
            if (relativeTo == Space.Self)
            {
                localRotation = localRotation * q;
            }
            else
            {
                rotation = rotation * Quat.Inverse(rotation) * q * rotation; 
            }
        }

        public void Rotate(float xAngle, float yAngle, float zAngle)
        {
            Rotate(new Vec3(xAngle, yAngle, zAngle), Space.Self);
        }

        public void Rotate(float xAngle, float yAngle, float zAngle, Space relativeTo)
        {
            Rotate(new Vec3(xAngle, yAngle, zAngle), relativeTo);
        }

        public void Rotate(Vec3 axis, float angle)
        {
            Rotate(axis, angle, Space.Self);
        }

        public void Rotate(Vec3 axis, float angle, Space relativeTo)
        {
            Quat q = Quat.AngleAxis(angle, axis);
            if (relativeTo == Space.Self)
            {
                localRotation = localRotation * q;
            }
            else
            {
                rotation = rotation * Quat.Inverse(rotation) * q * rotation;
            }
        }

        //RotateAround Rotates the transform about axis passing through point in world coordinates by angle degrees.
        public void RotateAround(Vec3 point, Vec3 axis, float angle)
        {
            Quat q = Quat.AngleAxis(angle, axis);
            Vec3 dir = position - point;
            dir = q * dir;
            position = point + dir;
            
            rotation = q * rotation;
        }

        #endregion

        #region Translation

        //Translate Moves the transform in the direction and distance of translation.
        public void Translate(Vec3 translation)
        {
            Translate(translation, Space.Self);
        }

        public void Translate(Vec3 translation, Space relativeTo)
        {
            if (relativeTo == Space.World)
            {
                position += translation;
            }
            else
            {
                position += TransformDirection(translation);
            }
        }

        public void Translate(float x, float y, float z)
        {
            Translate(new Vec3(x, y, z), Space.Self);
        }

        public void Translate(float x, float y, float z, Space relativeTo)
        {
            Translate(new Vec3(x, y, z), relativeTo);
        }

        public void Translate(Vec3 translation, MyTransform relativeTo)
        {
            if (relativeTo != null)
            {
                position += relativeTo.TransformDirection(translation);
            }
            else
            {
                position += translation;
            }
        }

        public void Translate(float x, float y, float z, MyTransform relativeTo)
        {
            Translate(new Vec3(x, y, z), relativeTo);
        }

        #endregion

        #region Coordinate Transformations

        #region Transformations

        //TransformDirection Transforms direction from local space to world space.
        public Vec3 TransformDirection(Vec3 direction)
        {
            return rotation * direction;
        }

        public Vec3 TransformDirection(float x, float y, float z)
        {
            return TransformDirection(new Vec3(x, y, z));
        }

        public void TransformDirections(ReadOnlySpan<Vec3> directions, Span<Vec3> transformedDirections)
        {
            for (int i = 0; i < directions.Length; i++)
            {
                transformedDirections[i] = TransformDirection(directions[i]);
            }
        }

        public void TransformDirections(Span<Vec3> directions)
        {
            for (int i = 0; i < directions.Length; i++)
            {
                directions[i] = TransformDirection(directions[i]);
            }
        }

        //TransformPoint Transforms position from local space to world space.
        public Vec3 TransformPoint(Vec3 position)
        {
            Mat4x4 m = localToWorldMatrix;
            return new Vec3(
                m.m00 * position.x + m.m01 * position.y + m.m02 * position.z + m.m03,
                m.m10 * position.x + m.m11 * position.y + m.m12 * position.z + m.m13,
                m.m20 * position.x + m.m21 * position.y + m.m22 * position.z + m.m23
            );
        }

        public Vec3 TransformPoint(float x, float y, float z)
        {
            return TransformPoint(new Vec3(x, y, z));
        }

        public void TransformPoints(ReadOnlySpan<Vec3> positions, Span<Vec3> transformedPositions)
        {
            for (int i = 0; i < positions.Length; i++)
            {
                transformedPositions[i] = TransformPoint(positions[i]);
            }
        }

        public void TransformPoints(Span<Vec3> positions)
        {
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = TransformPoint(positions[i]);
            }
        }

        //TransformVector Transforms vector from local space to world space.
        public Vec3 TransformVector(Vec3 vector)
        {
            Mat4x4 m = localToWorldMatrix;
            return new Vec3(
                m.m00 * vector.x + m.m01 * vector.y + m.m02 * vector.z,
                m.m10 * vector.x + m.m11 * vector.y + m.m12 * vector.z,
                m.m20 * vector.x + m.m21 * vector.y + m.m22 * vector.z
            );
        }

        public Vec3 TransformVector(float x, float y, float z)
        {
            return TransformVector(new Vec3(x, y, z));
        }

        public void TransformVectors(ReadOnlySpan<Vec3> vectors, Span<Vec3> transformedVectors)
        {
            for (int i = 0; i < vectors.Length; i++)
            {
                transformedVectors[i] = TransformVector(vectors[i]);
            }
        }

        public void TransformVectors(Span<Vec3> vectors)
        {
            for (int i = 0; i < vectors.Length; i++)
            {
                vectors[i] = TransformVector(vectors[i]);
            }
        }

        #endregion

        #region Inverse Transformations

        //InverseTransformDirection Transforms a direction from world space to local space. The opposite of Transform.TransformDirection.
        public Vec3 InverseTransformDirection(Vec3 direction)
        {
            return Quat.Inverse(rotation) * direction;
        }

        public Vec3 InverseTransformDirection(float x, float y, float z)
        {
            return InverseTransformDirection(new Vec3(x, y, z));
        }

        public void InverseTransformDirections(ReadOnlySpan<Vec3> directions, Span<Vec3> transformedDirections)
        {
            for (int i = 0; i < directions.Length; i++)
            {
                transformedDirections[i] = InverseTransformDirection(directions[i]);
            }
        }

        public void InverseTransformDirections(Span<Vec3> directions)
        {
            for (int i = 0; i < directions.Length; i++)
            {
                directions[i] = InverseTransformDirection(directions[i]);
            }
        }

        //InverseTransformPoint Transforms position from world space to local space.
        public Vec3 InverseTransformPoint(Vec3 position)
        {
            Mat4x4 m = worldToLocalMatrix;
            return new Vec3(
                m.m00 * position.x + m.m01 * position.y + m.m02 * position.z + m.m03,
                m.m10 * position.x + m.m11 * position.y + m.m12 * position.z + m.m13,
                m.m20 * position.x + m.m21 * position.y + m.m22 * position.z + m.m23
            );
        }

        public Vec3 InverseTransformPoint(float x, float y, float z)
        {
            return InverseTransformPoint(new Vec3(x, y, z));
        }

        public void InverseTransformPoints(ReadOnlySpan<Vec3> positions, Span<Vec3> transformedPositions)
        {
            for (int i = 0; i < positions.Length; i++)
            {
                transformedPositions[i] = InverseTransformPoint(positions[i]);
            }
        }

        public void InverseTransformPoints(Span<Vec3> positions)
        {
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = InverseTransformPoint(positions[i]);
            }
        }

        //InverseTransformVector Transforms a vector from world space to local space. The opposite of Transform.TransformVector.
        public Vec3 InverseTransformVector(Vec3 vector)
        {
            Mat4x4 m = worldToLocalMatrix;
            return new Vec3(
                m.m00 * vector.x + m.m01 * vector.y + m.m02 * vector.z,
                m.m10 * vector.x + m.m11 * vector.y + m.m12 * vector.z,
                m.m20 * vector.x + m.m21 * vector.y + m.m22 * vector.z
            );
        }

        public Vec3 InverseTransformVector(float x, float y, float z)
        {
            return InverseTransformVector(new Vec3(x, y, z));
        }

        public void InverseTransformVectors(ReadOnlySpan<Vec3> vectors, Span<Vec3> transformedVectors)
        {
            for (int i = 0; i < vectors.Length; i++)
            {
                transformedVectors[i] = InverseTransformVector(vectors[i]);
            }
        }

        public void InverseTransformVectors(Span<Vec3> vectors)
        {
            for (int i = 0; i < vectors.Length; i++)
            {
                vectors[i] = InverseTransformVector(vectors[i]);
            }
        }

        #endregion

        #endregion

        #endregion

        #region Operators
        //Operators

        //operator ==   Compares two object references to see if they refer to the same object.
        public static bool operator ==(MyTransform lhs, MyTransform rhs)
        {
            if (ReferenceEquals(lhs, rhs)) return true;
            if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null)) return false;

            return ReferenceEquals(lhs.parent, rhs.parent) &&
                   lhs.localPosition == rhs.localPosition &&
                   lhs.localRotation == rhs.localRotation &&
                   lhs.localScale == rhs.localScale;
        }

        //operator !=   Compares if two objects refer to a different object.
        public static bool operator !=(MyTransform lhs, MyTransform rhs)
        {
            return !(lhs == rhs);
        }

        public static implicit operator MyTransform(UnityEngine.Transform unityTransform)
        {
            if (unityTransform == null) return null;

            MyTransform newTransform = new MyTransform();

            newTransform.localPosition = new Vec3(unityTransform.localPosition.x, unityTransform.localPosition.y, unityTransform.localPosition.z);
            newTransform.localRotation = new Quat(unityTransform.localRotation.x, unityTransform.localRotation.y, unityTransform.localRotation.z, unityTransform.localRotation.w);
            newTransform.localScale = new Vec3(unityTransform.localScale.x, unityTransform.localScale.y, unityTransform.localScale.z);

            return newTransform;
        }

        #endregion

        #region Interfaces & Overrides

        public bool Equals(MyTransform other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as MyTransform);
        }

        public override int GetHashCode()
        {
            return localPosition.GetHashCode() ^ localRotation.GetHashCode() ^ localScale.GetHashCode();
        }

        public IEnumerator GetEnumerator()
        {
            return _Children.GetEnumerator();
        }

        #endregion
    }
}