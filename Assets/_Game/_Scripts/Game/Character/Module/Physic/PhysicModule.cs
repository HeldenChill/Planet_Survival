using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Core.Character.PhysicSystem;
using Utilities.Timer;

namespace _Game.Character
{
    public class PhysicModule : AbstractPhysicModule
    {
        protected override void OnInit()
        {

        }
        public override void IgnoreCollision(bool value, float time)
        {
            
        }

        public override void SetActiveRBStimulate(bool val)
        {
            
        }

        public override void SetGravityScale(float val)
        {
           
        }

        public override void SetOriginalGravityScale()
        {
            
        }

        public override void SetVelocity(Vector2 vel)
        {
            rb.velocity = vel;
        }

        public override void SetVelocity(Vector2 vel, float time)
        {           
            rb.velocity = vel;
            TimerManager.Ins.WaitForTime(time, () => rb.velocity = Vector2.zero);
        }

        public override void SetVelocity(Vector2 vel, int frame)
        {
            rb.velocity = vel;
            TimerManager.Ins.WaitForFrame(frame, () => rb.velocity = Vector2.zero);
        }

        public override void SetVelocityX(float velX)
        {
            rb.velocity = new Vector2 (velX, rb.velocity.y);
        }

        public override void SetVelocityX(float velX, float time)
        {
            rb.velocity = new Vector2(velX, rb.velocity.y);
            TimerManager.Ins.WaitForTime(time, () => rb.velocity = new Vector2(0, rb.velocity.y));
        }

        public override void SetVelocityX(float velX, int frame)
        {
            rb.velocity = new Vector2(velX, rb.velocity.y);
            TimerManager.Ins.WaitForFrame(frame, () => rb.velocity = new Vector2(0, rb.velocity.y));
        }

        public override void SetVelocityY(float velY)
        {
            rb.velocity = new Vector2(rb.velocity.x, velY);
        }

        public override void SetVelocityY(float velY, float time)
        {
            rb.velocity = new Vector2(rb.velocity.x, velY);
            TimerManager.Ins.WaitForTime(time, () => rb.velocity = new Vector2(rb.velocity.x, 0));
        }

        public override void SetVelocityY(float velY, int frame)
        {
            rb.velocity = new Vector2(rb.velocity.x, velY);
            TimerManager.Ins.WaitForFrame(frame, () => rb.velocity = new Vector2(rb.velocity.x, 0));
        }

        public override void UpdateData()
        {
            Data.CharacterParameterData.RbVelocity = rb.velocity;
        }

        public override void UpdateEvent(int type)
        {
           
        }
       
    }
}