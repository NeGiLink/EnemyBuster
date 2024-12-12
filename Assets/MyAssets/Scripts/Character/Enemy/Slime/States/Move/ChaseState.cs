using MyAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class ChaseState : SlimeStateBase
    {
        private IMovement movement;
        private Transform thisTransform;
        private FieldOfView fieldOfView;

        private IDamageContainer damageContainer;

        private ISlimeAnimator animator;

        Timer currentSearchinTimer = new Timer();
        GameObject targetObject;
        Vector3 targetLastPoint;

        [SerializeField]
        float moveSpeed = 4;
        [SerializeField]
        float rotationSpeed = 8;
        [SerializeField]
        float moveSpeedChangeRate = 8;

        [SerializeField]
        float searchingTime = 1.0f;

        [SerializeField]
        float minChaseDistance = 2.5f;

        public static readonly string StateKey = "Chase";
        public override string Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(ISlimeSetup enemy)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(MushroomIdleState.StateKey)) { re.Add(new IsNoTargetInViewTransition(enemy, StateChanger, MushroomIdleState.StateKey)); }
            if (StateChanger.IsContain(SlimeDamageState.StateKey)) { re.Add(new IsEnemyDamageTransition(enemy, StateChanger, SlimeDamageState.StateKey)); }
            if (StateChanger.IsContain(ReadySlimeAttackState.StateKey)) { re.Add(new IsReadyAttackTransition(enemy, StateChanger, ReadySlimeAttackState.StateKey)); }
            if (StateChanger.IsContain(SlimeDeathState.StateKey)) { re.Add(new IsDeathTransition(enemy, StateChanger, SlimeDeathState.StateKey)); }
            return re;
        }

        public override void DoSetup(ISlimeSetup enemy)
        {
            base.DoSetup(enemy);
            movement = enemy.Movement;
            thisTransform = enemy.gameObject.transform;
            fieldOfView = enemy.gameObject.GetComponent<FieldOfView>();
            animator = enemy.SlimeAnimator;
            damageContainer = enemy.DamageContainer;
        }

        public override void DoStart()
        {
            if (fieldOfView.TryGetFirstObject(out targetObject))
            {
                targetLastPoint = targetObject.transform.position;
            }
            currentSearchinTimer.End();
        }

        void TargetUpdate()
        {
            //今追っかけてるオブジェクトが見える
            if (fieldOfView.IsInside(targetObject))
            {
                targetLastPoint = targetObject.transform.position;
                currentSearchinTimer.End();
                return;
            }

            //見えないなら

            //新しいオブジェクトが見えたらそちらを追いかけるように切り替える
            if (fieldOfView.TryGetFirstObject(out var obj))
            {
                targetObject = obj;
                targetLastPoint = targetObject.transform.position;
                currentSearchinTimer.End();
                return;
            }

            //新しいオブジェクトもいないなら,一定時間で終了するためタイマースタート
            if (currentSearchinTimer.IsEnd())
            {
                currentSearchinTimer.Start(searchingTime);
            }
        }

        public override void DoFixedUpdate(float time)
        {
            base.DoFixedUpdate(time);
            TargetUpdate();
            currentSearchinTimer.Update(time);

            Vector3 targetVec = targetLastPoint - thisTransform.position;
            targetVec.y = 0.0f;
            float targetDistance = targetVec.magnitude;
            if (targetDistance < minChaseDistance)
            {
                animator.Animator.SetInteger("Move", Define.Zero);
                animator.Animator.SetInteger(animator.AttacksName, Define.Zero);
                movement.Move(0f);
            }
            else
            {
                animator.Animator.SetInteger("Move", 1);
                movement.MoveTo(targetLastPoint, moveSpeed, moveSpeedChangeRate, rotationSpeed, time);
            }
        }

        public override void DoExit()
        {
            base.DoExit();
            animator.Animator.SetInteger("Move", Define.Zero);
            movement.Move(0f);
        }

    }
}

