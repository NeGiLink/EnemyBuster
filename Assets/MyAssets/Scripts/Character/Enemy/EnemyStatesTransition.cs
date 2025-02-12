using UnityEngine;

namespace MyAssets
{
    /// <summary>
    /// 敵の状態変更クラスの一覧
    /// 敵全体のものや個別のものをまとめています
    /// </summary>

    public class IsPatrolTransition : CharacterStateTransitionBase
    {
        private readonly Timer timer;

        public IsPatrolTransition(IEnemySetup enemy,Timer _timer, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            timer = _timer;
        }
        public override bool IsTransition() => timer.IsEnd();
    }

    public class IsNotPatrolTransition : CharacterStateTransitionBase
    {
        private readonly PatrplPointContainer container;

        public IsNotPatrolTransition(IEnemySetup enemy,IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            container = enemy.gameObject.GetComponent<PatrplPointContainer>();
        }
        public override bool IsTransition() => container.IsStop;
    }

    public class IsEnemyDamageTransition : CharacterStateTransitionBase
    {

        private readonly IBaseStauts baseStauts;

        public IsEnemyDamageTransition(IEnemySetup chara, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            baseStauts = chara.BaseStauts;
        }
        public override bool IsTransition() => baseStauts.MaxStoredDamage <= baseStauts.StoredDamage;
    }

    public class IsNotDamageToTransition : CharacterStateTransitionBase
    {

        private readonly Timer damageTimer;

        public IsNotDamageToTransition(IEnemySetup chara, Timer t, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            damageTimer = t;
        }
        public override bool IsTransition() => damageTimer.IsEnd();
    }

    public class IsReadyAttackTransition : CharacterStateTransitionBase
    {

        private readonly ISlimeAnimator animator;

        public IsReadyAttackTransition(ISlimeSetup chara, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            animator = chara.SlimeAnimator;
        }
        public override bool IsTransition() => animator.Animator.GetInteger(animator.AttackAnimationID) == 0;
    }
    public class IsAttackTransition : CharacterStateTransitionBase
    {

        private readonly Timer readyTimer;

        public IsAttackTransition(ISlimeSetup chara, Timer t, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            readyTimer = t;
        }
        public override bool IsTransition() =>readyTimer.IsEnd();
    }

    public class IsNotSlimeAttackTransition : CharacterStateTransitionBase
    {

        private readonly ISlimeAnimator animator;

        private readonly string[] attackMotionNames = new string[]
        {
            "Attack"
        };

        public IsNotSlimeAttackTransition(ISlimeSetup chara, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            animator = chara.SlimeAnimator;
        }

        private bool AttackMotionEndChecker()
        {
            AnimatorStateInfo animInfo = animator.Animator.GetCurrentAnimatorStateInfo(0);
            for (int i = 0; i < attackMotionNames.Length; i++)
            {
                if (animInfo.IsName(attackMotionNames[i]) && animInfo.normalizedTime >= 1.0f)
                {
                    return true;
                }
            }
            return false;
        }
        public override bool IsTransition() => AttackMotionEndChecker();
    }

    public class IsMushroomAttackTransition : CharacterStateTransitionBase
    {

        private readonly IMushroomAnimator animator;

        public IsMushroomAttackTransition(IMushroomSetup chara, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            animator = chara.MushroomAnimator;
        }
        public override bool IsTransition() => animator.Animator.GetInteger("Attack") == 0;
    }
    public class IsNotMushroomAttackTransition : CharacterStateTransitionBase
    {

        private readonly IMushroomAnimator animator;

        private readonly string[] attackMotionNames = new string[]
        {
            "Attack"
        };

        public IsNotMushroomAttackTransition(IMushroomSetup chara, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            animator = chara.MushroomAnimator;
        }

        private bool AttackMotionEndChecker()
        {
            AnimatorStateInfo animInfo = animator.Animator.GetCurrentAnimatorStateInfo(0);
            for (int i = 0; i < attackMotionNames.Length; i++)
            {
                if (animInfo.IsName(attackMotionNames[i]) && animInfo.normalizedTime >= 1.0f)
                {
                    return true;
                }
            }
            return false;
        }
        public override bool IsTransition() => AttackMotionEndChecker();
    }

    public class IsNotBullTankAttackTransition : CharacterStateTransitionBase
    {

        private readonly IBullTankAnimator  animator;

        private readonly string             motionName;

        public IsNotBullTankAttackTransition(IBullTankSetup chara,string name, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            animator = chara.BullTankAnimator;
            motionName = name;
        }

        private bool AttackMotionEndChecker()
        {
            AnimatorStateInfo animInfo = animator.Animator.GetCurrentAnimatorStateInfo(0);
            if (animInfo.IsName(motionName) && animInfo.normalizedTime >= 1.0f)
            {
                return true;
            }
            return false;
        }
        public override bool IsTransition() => AttackMotionEndChecker();
    }

    public class IsNotGolemAttackTransition : CharacterStateTransitionBase
    {

        private readonly IGolemAnimator animator;

        private readonly string         motionName;

        public IsNotGolemAttackTransition(IGolemSetup actor, string name, IStateChanger<string> stateChanger, string changeKey)
            : base(stateChanger, changeKey)
        {
            animator = actor.GolemAnimator;
            motionName = name;
        }

        private bool AttackMotionEndChecker()
        {
            AnimatorStateInfo animInfo = animator.Animator.GetCurrentAnimatorStateInfo(0);
            if (animInfo.IsName(motionName) && animInfo.normalizedTime >= 1.0f)
            {
                return true;
            }
            return false;
        }
        public override bool IsTransition() => AttackMotionEndChecker();
    }
}
