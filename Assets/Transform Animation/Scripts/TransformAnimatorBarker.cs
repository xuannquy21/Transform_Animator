using System;
using UnityEditor;
using UnityEngine;

namespace Transform_Animator
{
    public class TransformAnimatorBarker : MonoBehaviour
    {
        [SerializeField] private AnimationClip[] clips;

#if UNITY_EDITOR
        [ContextMenu("Bake")]
        public void Bake()
        {
            if (!TryGetComponent<TransformAnimator>(out var tfController))
            {
                tfController = gameObject.AddComponent<TransformAnimator>();
            }
                
            tfController.Init(clips.Length);

            for (int i = 0; i < clips.Length; i++)
            {
                var controller = new TransformAnimationRuntime();
                controller = new TransformAnimationRuntime();
                controller.length = clips[i].length;

                foreach (var binding in AnimationUtility.GetCurveBindings(clips[i]))
                {
                    var curve = AnimationUtility.GetEditorCurve(clips[i], binding);
                    var path = binding.path;
                    var property = binding.propertyName;

                    Debug.Log(path + " " + property);

                    switch (property)
                    {
                        case "m_LocalPosition.x":
                            controller.PositionX.Add(new TransformAnimationData()
                            {
                                path = path,
                                target = transform.Find(path),
                                curve = curve
                            });
                            break;
                        case "m_LocalPosition.y":
                            controller.PositionY.Add(new TransformAnimationData()
                            {
                                path = path,
                                target = transform.Find(path),
                                curve = curve
                            });
                            break;
                        case "m_LocalPosition.z":
                            controller.PositionZ.Add(new TransformAnimationData()
                            {
                                path = path,
                                target = transform.Find(path),
                                curve = curve
                            });
                            break;

                        case "localEulerAnglesRaw.x":
                            controller.EulerRotationX.Add(new TransformAnimationData()
                            {
                                path = path,
                                target = transform.Find(path),
                                curve = curve
                            });
                            break;
                        case "localEulerAnglesRaw.y":
                            controller.EulerRotationY.Add(new TransformAnimationData()
                            {
                                path = path,
                                target = transform.Find(path),
                                curve = curve
                            });
                            break;
                        case "localEulerAnglesRaw.z":
                            controller.EulerRotationZ.Add(new TransformAnimationData()
                            {
                                path = path,
                                target = transform.Find(path),
                                curve = curve
                            });
                            break;

                        case "m_LocalRotation.x":
                            controller.RotationX.Add(new TransformAnimationData()
                            {
                                path = path,
                                target = transform.Find(path),
                                curve = curve
                            });
                            break;
                        case "m_LocalRotation.y":
                            controller.RotationY.Add(new TransformAnimationData()
                            {
                                path = path,
                                target = transform.Find(path),
                                curve = curve
                            });
                            break;
                        case "m_LocalRotation.z":
                            controller.RotationZ.Add(new TransformAnimationData()
                            {
                                path = path,
                                target = transform.Find(path),
                                curve = curve
                            });
                            break;
                        case "m_LocalRotation.w":
                            controller.RotationW.Add(new TransformAnimationData()
                            {
                                path = path,
                                target = transform.Find(path),
                                curve = curve
                            });
                            break;
                        case "m_LocalScale.x":
                            controller.ScaleX.Add(new TransformAnimationData()
                            {
                                path = path,
                                target = transform.Find(path),
                                curve = curve
                            });
                            break;
                        case "m_LocalScale.y":
                            controller.ScaleY.Add(new TransformAnimationData()
                            {
                                path = path,
                                target = transform.Find(path),
                                curve = curve
                            });
                            break;
                        case "m_LocalScale.z":
                            controller.ScaleZ.Add(new TransformAnimationData()
                            {
                                path = path,
                                target = transform.Find(path),
                                curve = curve
                            });
                            break;
                    }
                }

                tfController.tranformAnimationRuntimes[i] = controller;
            }
            
            tfController.Bake();
        }

#endif
    }

    [Serializable]
    public class TransformAnimationData
    {
        public string path;
        public Transform target;
        public AnimationCurve curve;
    }
}