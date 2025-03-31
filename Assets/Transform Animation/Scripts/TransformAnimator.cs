using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Jobs;

namespace Transform_Animator
{
    public class TransformAnimator : MonoBehaviour
    {
        public TransformAnimationRuntime[] tranformAnimationRuntimes;

        private float time = 0;
        private int currentAnim = 0;

        public int CurrentAnim
        {
            get => currentAnim;
            set => currentAnim = value;
        }

        private Vector3 currentLocalPosition, currentEulerRotation, currentLocalScale;
        private Quaternion currentLocalRotation;


        public void Init(int totalClip)
        {
            tranformAnimationRuntimes = new TransformAnimationRuntime[totalClip];
        }

        public void SetTime(float time)
        {
            this.time = time;

            var clip = tranformAnimationRuntimes[currentAnim];

            PositionProgress(clip);
            EulerRotateProgress(clip);
            RotateProgress(clip);
            ScaleProgress(clip);
        }

        void PositionProgress(TransformAnimationRuntime data)
        {
            var totalCurve = data.PositionX.Count;
            for (int i = 0; i < totalCurve; i++)
            {
                var target = data.PositionX[i].target;

                var x = data.PositionX[i].curve.Evaluate(time);
                var y = data.PositionY[i].curve.Evaluate(time);
                var z = data.PositionZ[i].curve.Evaluate(time);

                currentLocalPosition.x = x;
                currentLocalPosition.y = y;
                currentLocalPosition.z = z;

                target.localPosition = currentLocalPosition;
            }
        }

        void EulerRotateProgress(TransformAnimationRuntime data)
        {
            var totalCurve = data.EulerRotationX.Count;
            for (int i = 0; i < totalCurve; i++)
            {
                var target = data.EulerRotationX[i].target;

                var x = data.EulerRotationX[i].curve.Evaluate(time);
                var y = data.EulerRotationY[i].curve.Evaluate(time);
                var z = data.EulerRotationZ[i].curve.Evaluate(time);

                currentEulerRotation.x = x;
                currentEulerRotation.y = y;
                currentEulerRotation.z = z;

                target.localEulerAngles = currentEulerRotation;
            }
        }

        void RotateProgress(TransformAnimationRuntime data)
        {
            var totalCurve = data.RotationX.Count;
            for (int i = 0; i < totalCurve; i++)
            {
                var target = data.RotationX[i].target;

                var x = data.RotationX[i].curve.Evaluate(time);
                var y = data.RotationY[i].curve.Evaluate(time);
                var z = data.RotationZ[i].curve.Evaluate(time);
                var w = data.RotationW[i].curve.Evaluate(time);

                currentLocalRotation.x = x;
                currentLocalRotation.y = y;
                currentLocalRotation.z = z;
                currentLocalRotation.w = w;

                target.localRotation = currentLocalRotation;
            }
        }

        void ScaleProgress(TransformAnimationRuntime data)
        {
            var totalCurve = data.ScaleX.Count;
            for (int i = 0; i < totalCurve; i++)
            {
                var target = data.ScaleX[i].target;

                var x = data.ScaleX[i].curve.Evaluate(time);
                var y = data.ScaleY[i].curve.Evaluate(time);
                var z = data.ScaleZ[i].curve.Evaluate(time);

                currentLocalScale.x = x;
                currentLocalScale.y = y;
                currentLocalScale.z = z;

                target.localScale = currentLocalScale;
            }
        }

        public TransformAnimationRuntime GetCurrentAnimData()
        {
            return tranformAnimationRuntimes[currentAnim];
        }

#if UNITY_EDITOR
        public void Bake()
        {
            foreach (var transformAnimationRuntime in tranformAnimationRuntimes)
            {
                transformAnimationRuntime.InitTransform();
            }
        }
#endif
    }

    [Serializable]
    public class TransformAnimationRuntime
    {
        public float length, speed;
        public bool isLoop;

        public List<TransformAnimationData> PositionX, PositionY, PositionZ;
        public List<TransformAnimationData> EulerRotationX, EulerRotationY, EulerRotationZ;
        public List<TransformAnimationData> RotationX, RotationY, RotationZ, RotationW;
        public List<TransformAnimationData> ScaleX, ScaleY, ScaleZ;

        public List<Transform> TargetPositionAnim, TargetEulerRotationAnim, TargetRotationAnim, TargetScaleAnim;

        public TransformAnimationRuntime()
        {
            length = 0f;
            speed = 1f;
            isLoop = false;

            PositionX = new List<TransformAnimationData>();
            PositionY = new List<TransformAnimationData>();
            PositionZ = new List<TransformAnimationData>();

            EulerRotationX = new List<TransformAnimationData>();
            EulerRotationY = new List<TransformAnimationData>();
            EulerRotationZ = new List<TransformAnimationData>();

            RotationX = new List<TransformAnimationData>();
            RotationY = new List<TransformAnimationData>();
            RotationZ = new List<TransformAnimationData>();
            RotationW = new List<TransformAnimationData>();

            ScaleX = new List<TransformAnimationData>();
            ScaleY = new List<TransformAnimationData>();
            ScaleZ = new List<TransformAnimationData>();

            TargetPositionAnim = new List<Transform>();
            TargetEulerRotationAnim = new List<Transform>();
            TargetRotationAnim = new List<Transform>();
            TargetScaleAnim = new List<Transform>();
        }

        TransformAccessArray ConvertToTransformAccessArray(List<TransformAnimationData> data)
        {
            var array = new Transform[data.Count];
            for (int i = 0; i < data.Count; i++)
            {
                array[i] = data[i].target;
            }

            return new TransformAccessArray(array);
        }

        public void InitTransform()
        {
            foreach (var positionData in PositionX) TargetPositionAnim.Add(positionData.target);
            foreach (var eulerData in EulerRotationX) TargetEulerRotationAnim.Add(eulerData.target);
            foreach (var rotationData in RotationX) TargetRotationAnim.Add(rotationData.target);
            foreach (var scaleData in ScaleX) TargetScaleAnim.Add(scaleData.target);
        }
    }
}