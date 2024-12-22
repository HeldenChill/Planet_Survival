using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Utilities.Core.Character.LogicSystem
{
    using System;
    public class LogicEvent
    {
        /// <summary>
        /// Set <c>Velocity_x</c> for character in a period of time.
        /// </summary>
        public event Action<float> _SetVelocityX;
        public event Action<float, float> _SetVelocityXTime;
        public event Action<float, int> _SetVelocityXFrame;
        /// <summary>
        /// Set <c>Velocity_y</c> for character in a period of time.
        /// </summary>
        public event Action<float> _SetVelocityY;
        public event Action<float, float> _SetVelocityYTime;
        public event Action<float, int> _SetVelocityYFrame;
        /// <summary>
        /// Set <c>Velocity</c> for character in a period of time.
        /// </summary>
        public event Action<Vector3> _SetVelocity;
        public event Action<Vector3> _SetLocalVelocityXZ;
        public event Action<Vector3, ForceMode> _AddForce;
        public event Action<Vector2, float> _SetVelocityTime;
        public event Action<Vector2, int> _SetVelocityFrame;
        /// <summary>
        /// Set <c>GravityScale = 0</c> in character RigidBody.
        /// </summary>
        public event Action _DisableGravity;
        /// <summary>
        /// Set <c>GravityScale = originalGravityScale</c> in character RigidBody.
        /// </summary>
        public event Action _EnableGravity;
        /// <summary>
        /// Set <c>Character.Transform.Position = value</c>.
        /// </summary>
        public event Action<Vector3> _SetTransformPosition;
        /// <summary>
        /// Set <c>Character.Transform.Position = Character.Transform.Position + value</c>.
        /// </summary>
        public event Action<Vector3> _AddTransformPosition;

        //AnimModule.ActivateAfterImageEffect(0.02f, dashTime);
        public event Action<Type, float, float> _UseAfterEffect;
        public event Action<Type> _ShowAnimation;
        public event Action<Type> _HideAnimation;

        public event Action<Type, string> _PlayAnimation; //AnimModule.Activate(string)
        public event Action<Type, string> _ExitAnimation; //AnimModule.Deactivate(string)
        public event Action<Type, string, float> _SetAnimFloat; //AnimModule.UpdateFloatParameter(float)
        public event Action<Type, string> _SetAnimTrigger;//player.AnimModule.Trigger(nameTrigger);
        public event Action<Type, string, bool> _SetAnimBool;


        public event Action<bool, float> _IgnoreCollision;

        /// <summary>
        /// Set <c>Rotation</c> of character.
        /// </summary>
        public event Action<Quaternion> _SetSkinRotation;
        public event Action<Quaternion> _SetSkinLocalRotation;
        public event Action _OnDie;

        public void SetVelocityX(float speed)
        {
            _SetVelocityX?.Invoke(speed);
        }
        public void SetVelocityX(float speed, float time = -1f)
        {
            WarningInformation(_SetVelocityXTime, "SetVelocityX");
            _SetVelocityXTime?.Invoke(speed, time);
        }
        public void SetVelocityX(float speed, int frame)
        {
            WarningInformation(_SetVelocityXTime, "SetVelocityXFrame");
            _SetVelocityXFrame?.Invoke(speed, frame);
        }
        public void SetVelocityY(float speed)
        {
            _SetVelocityY?.Invoke(speed);
        }
        public void SetVelocityY(float speed, float time = -1f)
        {
            WarningInformation(_SetVelocityYTime, "SetVelocityY");
            _SetVelocityYTime?.Invoke(speed, time);
        }
        public void SetVelocityY(float speed, int frame)
        {
            WarningInformation(_SetVelocityYTime, "SetVelocityYFrame");
            _SetVelocityYFrame?.Invoke(speed, frame);
        }
        public void SetVelocity(Vector3 speed, float time = -1f)
        {
            _SetVelocityTime?.Invoke(speed, time);
        }
        public void SetVelocity(Vector3 speed, int frame)
        {
            _SetVelocityFrame?.Invoke(speed, frame);
        }
        public void SetVelocity(Vector3 velocity)
        {
            _SetVelocity?.Invoke(velocity);
        }
        public void SetLocalVelocityXZ(Vector3 velocity)
        {
            _SetLocalVelocityXZ?.Invoke(velocity);
        }
        public void AddForce(Vector3 force, ForceMode mode = ForceMode.Impulse)
        {
            _AddForce?.Invoke(force, mode);
        }

        public void DisableGravity()
        {
            _DisableGravity?.Invoke();
        }
        public void EnableGravity()
        {
            _EnableGravity?.Invoke();
        }
        public void UseAfterEffect(Type type, float timeBetweenImage, float time)
        {
            _UseAfterEffect?.Invoke(type, timeBetweenImage, time);
        }
        public void ShowAnimation(Type type)
        {
            _ShowAnimation?.Invoke(type);
        }
        public void HideAnimation(Type type)
        {
            _HideAnimation?.Invoke(type);
        }

        public void PlayAnimation(Type type, string name)
        {
            _PlayAnimation?.Invoke(type, name);
        }
        public void ExitAnimation(Type type, string name)
        {
            _ExitAnimation?.Invoke(type, name);
        }

        public void SetAnimFloat(Type type, string name, float value)
        {
            _SetAnimFloat?.Invoke(type, name, value);
        }

        public void SetAnimTrigger(Type type, string name)
        {
            _SetAnimTrigger(type, name);
        }
        public void SetAnimBool(Type type, string name, bool value)
        {
            _SetAnimBool(type, name, value);
        }

        public void SetTransformPosition(Vector3 pos)
        {
            _SetTransformPosition?.Invoke(pos);
        }

        public void AddTransformPosition(Vector3 pos)
        {
            _AddTransformPosition?.Invoke(pos);
        }

        public void OnDie()
        {
            _OnDie?.Invoke();
        }
        //public void InflictDamage(Type type, float damage, int frame = 1)
        //{
        //    if(frame <= 1)
        //    {
        //        _InflictDamage?.Invoke(type, damage);
        //    }
        //    else
        //    {
        //        _InflictDamageInFrames.Invoke(type, damage, frame);
        //    }
        //}

        //public void InflictEffect(Type type, CombatEffect effect)
        //{
        //    _InflictEffect?.Invoke(type, effect);                     
        //}

        //public void InflictEffectInFrame(Type type, CombatEffect[] effects, int frame)
        //{
        //    _InflictEffectInFrames?.Invoke(type, effects, frame);
        //}

        public void IgnoreCollision(bool value, float time = -1f)
        {
            _IgnoreCollision?.Invoke(value, time);
        }
        public void SetSkinRotation(Quaternion rotation)
        {
            _SetSkinRotation?.Invoke(rotation);
        }
        public void SetSkinLocalRotation(Quaternion rotation)
        {
            _SetSkinLocalRotation?.Invoke(rotation);
        }
        private void WarningInformation(Delegate action, string name)
        {
            if (action == null)
            {
                Debug.LogWarning(name + " not implement" + "DynamicLogicSystem");
            }
        }
    }
}