using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    /*
     * スライムのダメージ状態
     */
    [System.Serializable]
    public class SlimeDamageState : SlimeStateBase
    {
        private IBaseStauts             stauts;

        private Transform               thisTransform;

        private IVelocityComponent      velocity;

        private ISlimeAnimator          animator;

        private IDamageContainer        damageContainer;

        private IDamagement             damageMove;

        private FieldOfView             fieldOfView;

        private SEHandler               seHandler;

        private Timer                   damageTimer = new Timer();

        [SerializeField]
        private float                   decreaseForce = 0.9f;

        [SerializeField]
        private float                   damageGravityMultiply = 2.0f;

        [SerializeField]
        private float                   damageIdleCount = 0.5f;

        public static readonly string   StateKey = "Damage";
        public override string          Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(ISlimeSetup actor)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(SlimeIdleState.StateKey)) { re.Add(new IsNotDamageToTransition(actor, damageTimer, StateChanger, SlimeIdleState.StateKey)); }
            if (StateChanger.IsContain(ChaseState.StateKey)) { re.Add(new IsTimerTargetInViewTransition(actor, damageTimer, StateChanger, ChaseState.StateKey)); }
            if (StateChanger.IsContain(SlimeDeathState.StateKey)) { re.Add(new IsDeathTransition(actor, StateChanger, SlimeDeathState.StateKey)); }
            return re;
        }
        public override void DoSetup(ISlimeSetup enemy)
        {
            base.DoSetup(enemy);
            stauts = enemy.BaseStauts;
            thisTransform = enemy.gameObject.transform;
            velocity = enemy.Velocity;
            animator = enemy.SlimeAnimator;
            damageContainer = enemy.DamageContainer;
            damageMove = enemy.Damagement;
            fieldOfView = enemy.gameObject.GetComponent<FieldOfView>();
            seHandler = enemy.SEHandler;
        }

        public override void DoStart()
        {
            base.DoStart();
            FoundTarget();
            seHandler.Play((int)SlimeSETag.Damage);
            velocity.Rigidbody.velocity = Vector3.zero;
            DamageType type = damageContainer.AttackType;
            int damageType = 0;
            damageMove.AddForceMove(thisTransform.position, damageContainer.Attacker.position, damageContainer.KnockBack * 1.0f);
            damageTimer.Start(damageIdleCount);
            animator.Animator.SetInteger("Impact", damageType);
            if(damageContainer.Data > 0)
            {
                float scale = (float)stauts.HP / stauts.MaxHP;
                thisTransform.localScale *= SetScale(scale);
            }
        }

        private float SetScale(float scale)
        {
            float s = 1.0f;
            if(scale < 0.8f&& scale > 0.6f)
            {
                s = 0.8f;
            }
            else if(scale < 0.6f&& scale > 0.4f)
            {
                s = 0.7f;
            }
            else if(scale < 0.4f&& scale > 0.2f)
            {
                s = 0.6f;
            }
            else if(scale < 0.2f)
            {
                s = 0.5f;
            }
            return s;
        }

        private void FoundTarget()
        {
            fieldOfView.SetAllSearch(true);
            Vector3 target = damageContainer.Attacker.position;
            target = new Vector3(target.x, thisTransform.position.y, target.z);
            thisTransform.LookAt(target);
        }

        public override void DoUpdate(float time)
        {
            base.DoUpdate(time);
            damageTimer.Update(time);
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            //AddForceで与えた分を少しずつ減少
            Vector3 v = velocity.Rigidbody.velocity;
            velocity.Rigidbody.velocity = v * decreaseForce;
            velocity.Rigidbody.velocity += Physics.gravity * damageGravityMultiply * time;
        }

        public override void DoExit()
        {
            base.DoExit();
            fieldOfView.SetAllSearch(false);
            damageContainer.ClearDamage();
            animator.Animator.SetInteger("Impact", -1);
            stauts.ClearStoredDamage();
        }
    }
}
