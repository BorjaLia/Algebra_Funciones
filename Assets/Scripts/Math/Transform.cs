using CustomMath;
using System;
using System.Collections;
using UnityEngine;

namespace CustomMath
{
    public struct MyTransform : IEnumerable, IEquatable<MyTransform>
    {
        #region Properties
        //Properties

            #region Hierarchy
        
        //childCount	The number of children the parent Transform has.
        public int childCount { get; private set; }
        
        //hasChanged	Has the transform changed since the last time the flag was set to 'false'?
        public bool hasChanged;
        
        //hierarchyCapacity	The transform capacity of the transform's hierarchy data structure.
        public int hierarchyCapacity { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        
        //hierarchyCount	The number of transforms in the transform's hierarchy data structure.
        public int hierarchyCount { get { throw new NotImplementedException(); } }
        
        //parent	The parent of the transform.
        public MyTransform parent { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }

        //root	Returns the topmost transform in the hierarchy.
        public MyTransform root { get { throw new NotImplementedException(); } }

            #endregion
            
            #region Matrix
        
        //localToWorldMatrix	Matrix that transforms a point from local space into world space (Read Only).
        public Mat4x4 localToWorldMatrix { get { throw new NotImplementedException(); } }
        
        //worldToLocalMatrix	Matrix that transforms a point from world space into local space (Read Only).
        public Mat4x4 worldToLocalMatrix { get { throw new NotImplementedException(); } }
            
            #endregion

            #region Directions
        
        //forward	Returns a normalized vector representing the blue axis of the transform in world space.
        public Vec3 forward { get; set; }
        
        //right	The red axis of the transform in world space.
        public Vec3 right { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        
        //up	The green axis of the transform in world space.
        public Vec3 up { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        
            #endregion

            #region Position
        
        //position	The world space position of the Transform.
        public Vec3 position { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }

        //localPosition	Position of the transform relative to the parent transform.
        public Vec3 localPosition { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        
            #endregion

            #region Rotation
        
        //eulerAngles	The rotation as Euler angles in degrees.
        public Vec3 eulerAngles { get; set; }

        //localEulerAngles	The rotation as Euler angles in degrees relative to the parent transform's rotation.
        public Vec3 localEulerAngles { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        
        //localRotation	The rotation of the transform relative to the transform rotation of the parent.
        public Quat localRotation { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }

        //rotation	A Quaternion that stores the rotation of the Transform in world space.
        public Quat rotation { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }

            #endregion

            #region Scale
        
        //localScale	The scale of the transform relative to the GameObjects parent.
        public Vec3 localScale { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        
        //lossyScale	The global scale of the object (Read Only).
        public Vec3 lossyScale { get { throw new NotImplementedException(); } }
            
            #endregion

        #endregion

        #region Constructors
        #endregion

        #region Methods
        //Public Methods

            #region Hierarchy

        //DetachChildren Unparents all children.
        public void DetachChildren()
        {
            throw new NotImplementedException();
        }

        //Find    Finds a child by n and returns it.
        public MyTransform Find(string n)
        {
            throw new NotImplementedException();
        }

        //GetChild Returns a transform child by index.
        public MyTransform GetChild(int index)
        {
            throw new NotImplementedException();
        }

        //GetSiblingIndex Gets the sibling index.
        public int GetSiblingIndex()
        {
            throw new NotImplementedException();
        }

        //IsChildOf Is this transform a child of parent?
        public bool IsChildOf(MyTransform parent)
        {
            throw new NotImplementedException();
        }

        //SetAsFirstSibling Move the transform to the start of the local transform list.
        public void SetAsFirstSibling()
        {
            throw new NotImplementedException();
        }

        //SetAsLastSibling Move the transform to the end of the local transform list.
        public void SetAsLastSibling()
        {
            throw new NotImplementedException();
        }

        //SetParent Set the parent of the transform.
        public void SetParent(MyTransform p)
        {
            throw new NotImplementedException();
        }

        public void SetParent(MyTransform parent, bool worldPositionStays)
        {
            throw new NotImplementedException();
        }

        //SetSiblingIndex Sets the sibling index.
        public void SetSiblingIndex(int index)
        {
            throw new NotImplementedException();
        }

            #endregion

            #region Position & Rotation

        //GetLocalPositionAndRotation Gets the local space position and rotation of the Transform component.
        public void GetLocalPositionAndRotation(out Vec3 localPosition, out Quat localRotation)
        {
            throw new NotImplementedException();
        }

        //GetPositionAndRotation Gets the world space position and rotation of the Transform component.
        public void GetPositionAndRotation(out Vec3 position, out Quat rotation)
        {
            throw new NotImplementedException();
        }

        //SetPositionAndRotation Sets the world space position and rotation of the Transform component.
        public void SetPositionAndRotation(Vec3 position, Quat rotation)
        {
            throw new NotImplementedException();
        }

        //SetLocalPositionAndRotation Sets the local space position and rotation of the Transform component.
        public void SetLocalPositionAndRotation(Vec3 localPosition, Quat localRotation)
        {
            throw new NotImplementedException();
        }

            #endregion

            #region Rotation

        //LookAt  Rotates the transform so the forward vector points at /target/'s current position.
        public void LookAt(MyTransform target)
        {
            throw new NotImplementedException();
        }

        public void LookAt(MyTransform target, Vec3 worldUp)
        {
            throw new NotImplementedException();
        }

        public void LookAt(Vec3 worldPosition)
        {
            throw new NotImplementedException();
        }

        public void LookAt(Vec3 worldPosition, Vec3 worldUp)
        {
            throw new NotImplementedException();
        }

        //Rotate Use Transform.Rotate to rotate GameObjects in a variety of ways. The rotation is often provided as an Euler angle and not a Quaternion.
        public void Rotate(Vec3 eulers)
        {
            throw new NotImplementedException();
        }

        public void Rotate(Vec3 eulers, Space relativeTo)
        {
            throw new NotImplementedException();
        }

        public void Rotate(float xAngle, float yAngle, float zAngle)
        {
            throw new NotImplementedException();
        }

        public void Rotate(float xAngle, float yAngle, float zAngle, Space relativeTo)
        {
            throw new NotImplementedException();
        }

        public void Rotate(Vec3 axis, float angle)
        {
            throw new NotImplementedException();
        }

        public void Rotate(Vec3 axis, float angle, Space relativeTo)
        {
            throw new NotImplementedException();
        }

        //RotateAround Rotates the transform about axis passing through point in world coordinates by angle degrees.
        public void RotateAround(Vec3 point, Vec3 axis, float angle)
        {
            throw new NotImplementedException();
        }

            #endregion

            #region Translation

        //Translate Moves the transform in the direction and distance of translation.
        public void Translate(Vec3 translation)
        {
            throw new NotImplementedException();
        }

        public void Translate(Vec3 translation, Space relativeTo)
        {
            throw new NotImplementedException();
        }

        public void Translate(float x, float y, float z)
        {
            throw new NotImplementedException();
        }

        public void Translate(float x, float y, float z, Space relativeTo)
        {
            throw new NotImplementedException();
        }

        public void Translate(Vec3 translation, MyTransform relativeTo)
        {
            throw new NotImplementedException();
        }

        public void Translate(float x, float y, float z, MyTransform relativeTo)
        {
            throw new NotImplementedException();
        }

            #endregion

            #region Coordinate Transformations

                #region Transformations

        //TransformDirection Transforms direction from local space to world space.
        public Vec3 TransformDirection(Vec3 direction)
        {
            throw new NotImplementedException();
        }

        public Vec3 TransformDirection(float x, float y, float z)
        {
            throw new NotImplementedException();
        }

        public void TransformDirections(ReadOnlySpan<Vec3> directions, Span<Vec3> transformedDirections)
        {
            throw new NotImplementedException();
        }

        public void TransformDirections(Span<Vec3> directions)
        {
            throw new NotImplementedException();
        }

        //TransformPoint Transforms position from local space to world space.
        public Vec3 TransformPoint(Vec3 position)
        {
            throw new NotImplementedException();
        }

        public Vec3 TransformPoint(float x, float y, float z)
        {
            throw new NotImplementedException();
        }

        public void TransformPoints(ReadOnlySpan<Vec3> positions, Span<Vec3> transformedPositions)
        {
            throw new NotImplementedException();
        }

        public void TransformPoints(Span<Vec3> positions)
        {
            throw new NotImplementedException();
        }

        //TransformVector Transforms vector from local space to world space.
        public Vec3 TransformVector(Vec3 vector)
        {
            throw new NotImplementedException();
        }

        public Vec3 TransformVector(float x, float y, float z)
        {
            throw new NotImplementedException();
        }

        public void TransformVectors(ReadOnlySpan<Vec3> vectors, Span<Vec3> transformedVectors)
        {
            throw new NotImplementedException();
        }

        public void TransformVectors(Span<Vec3> vectors)
        {
            throw new NotImplementedException();
        }

                #endregion

                #region Inverse Transformations

        //InverseTransformDirection Transforms a direction from world space to local space. The opposite of Transform.TransformDirection.
        public Vec3 InverseTransformDirection(Vec3 direction)
        {
            throw new NotImplementedException();
        }

        public Vec3 InverseTransformDirection(float x, float y, float z)
        {
            throw new NotImplementedException();
        }

        public void InverseTransformDirections(ReadOnlySpan<Vec3> directions, Span<Vec3> transformedDirections)
        {
            throw new NotImplementedException();
        }

        public void InverseTransformDirections(Span<Vec3> directions)
        {
            throw new NotImplementedException();
        }

        //InverseTransformPoint Transforms position from world space to local space.
        public Vec3 InverseTransformPoint(Vec3 position)
        {
            throw new NotImplementedException();
        }

        public Vec3 InverseTransformPoint(float x, float y, float z)
        {
            throw new NotImplementedException();
        }

        public void InverseTransformPoints(ReadOnlySpan<Vec3> positions, Span<Vec3> transformedPositions)
        {
            throw new NotImplementedException();
        }

        public void InverseTransformPoints(Span<Vec3> positions)
        {
            throw new NotImplementedException();
        }

        //InverseTransformVector Transforms a vector from world space to local space. The opposite of Transform.TransformVector.
        public Vec3 InverseTransformVector(Vec3 vector)
        {
            throw new NotImplementedException();
        }

        public Vec3 InverseTransformVector(float x, float y, float z)
        {
            throw new NotImplementedException();
        }

        public void InverseTransformVectors(ReadOnlySpan<Vec3> vectors, Span<Vec3> transformedVectors)
        {
            throw new NotImplementedException();
        }

        public void InverseTransformVectors(Span<Vec3> vectors)
        {
            throw new NotImplementedException();
        }

                #endregion

            #endregion

            #region Interfaces & Overrides

        public bool Equals(MyTransform other)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

            #endregion

        #endregion

        #region Operators
        //Operators

        //operator !=	Compares if two objects refer to a different object.
        public static bool operator !=(MyTransform lhs, MyTransform rhs)
        {
            throw new NotImplementedException();
        }

        //operator ==	Compares two object references to see if they refer to the same object.
        public static bool operator ==(MyTransform lhs, MyTransform rhs)
        {
            throw new NotImplementedException();
        }

        public static implicit operator UnityEngine.Transform(MyTransform myTransform)
        {
            throw new NotImplementedException();
        }

        public static implicit operator MyTransform(UnityEngine.Transform unityTransform)
        {
            throw new NotImplementedException();
        }

        #endregion

        //Inherited Members Properties
        //gameObject  The game object this component is attached to. A component is always attached to a game object.
        //tag The tag of this game object.
        //transform The Transform attached to this GameObject.
        //hideFlags Should the object be hidden, saved with the Scene or modifiable by the user?
        //name    The name of the object.

        //Public Methods
        //BroadcastMessage Calls the method named methodName on every MonoBehaviour in this game object or any of its children.
        //CompareTag  Is this game object tagged with tag ?
        //GetComponent Returns the component of Type type if the game object has one attached, null if it doesn't.
        //GetComponentInChildren Returns the component of Type type in the GameObject or any of its children using depth first search.
        //GetComponentInParent Returns the component of Type type in the GameObject or any of its parents.
        //GetComponents	Returns all components of Type type in the GameObject.
        //GetComponentsInChildren	Returns all components of Type type in the GameObject or any of its children using depth first search.Works recursively.
        //GetComponentsInParent Returns all components of Type type in the GameObject or any of its parents.
        //SendMessage	Calls the method named methodName on every MonoBehaviour in this game object.
        //SendMessageUpwards	Calls the method named methodName on every MonoBehaviour in this game object and on every ancestor of the behaviour.
        //TryGetComponent	Gets the component of the specified type, if it exists.
        //GetInstanceID	Returns the instance id of the object.
        //ToString	Returns the name of the object.

        //Static Methods
        //Destroy Removes a GameObject, component or asset.
        //DestroyImmediate    Destroys the object obj immediately.You are strongly recommended to use Destroy instead.
        //DontDestroyOnLoad Do not destroy the target Object when loading a new Scene.
        //FindObjectOfType Returns the first active loaded object of Type type.
        //FindObjectsOfType   Returns a list of all active loaded objects of Type type.
        //Instantiate Clones the object original and returns the clone.
    }
}