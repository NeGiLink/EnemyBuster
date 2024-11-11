using MyAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets
{
    [System.Serializable]
    public class ChaseState : SlimeStateBase
    {
        private ICharacterMovement movement;
        private Transform thisTransform;
        private FieldOfView fieldOfView;

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

        public static readonly string StateKey = "Chase";
        public override string Key => StateKey;

        public override List<ICharacterStateTransition<string>> CreateTransitionList(ISlimeSetup enemy)
        {
            List<ICharacterStateTransition<string>> re = new List<ICharacterStateTransition<string>>();
            if (StateChanger.IsContain(SlimeIdleState.StateKey)) { re.Add(new IsNoTargetInViewTransition(enemy, StateChanger, SlimeIdleState.StateKey)); }
            return re;
        }

        public override void DoSetup(ISlimeSetup enemy)
        {
            base.DoSetup(enemy);
            movement = enemy.Movement;
            thisTransform = enemy.gameObject.transform;
            fieldOfView = enemy.gameObject.GetComponent<FieldOfView>();
            animator = enemy.SlimeAnimator;
        }

        public override void DoStart()
        {
            animator.Animator.SetInteger("Move", 1);

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
            const float minChaseDistance = 1.0f;
            if (targetDistance < minChaseDistance)
            {
                movement.Move(0f);
            }
            else
            {
                movement.MoveTo(targetLastPoint, moveSpeed, moveSpeedChangeRate, rotationSpeed, time);
            }
        }

        public override void DoExit()
        {
            base.DoExit();
            animator.Animator.SetInteger("Move", 0);
        }
    }
}

