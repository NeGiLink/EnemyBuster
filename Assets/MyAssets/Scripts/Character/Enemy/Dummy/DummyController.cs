using UnityEngine;

namespace MyAssets
{
    public class DummyController : CharacterBaseController,IDummySetup
    {
        [SerializeField]
        private DummyStatusProperty property;
        public IBaseStauts BaseStauts => property;

        [SerializeField]
        private Movement movement;
        public IMovement Movement => movement;

        [SerializeField]
        private StepClimberJudgment stepClimberJudgment;
        public IStepClimberJudgment StepClimberJudgment => stepClimberJudgment;

        private SEHandler seHandler;
        public SEHandler SEHandler => seHandler;

        [SerializeField]
        private PlayerRotation rotation;
        public IRotation Rotation => rotation;

        private Timer damageCoolDownTimer;

        public IFieldOfView FieldOfView => null;

        [SerializeField]
        private GuardTrigger guardTrigger;
        public IGuardTrigger GuardTrigger => guardTrigger;

        public IStateMachine StateMachine => null;

        public IEnemyAnimator EnemyAnimator => null;

        public override CharacterType CharaType => CharacterType.Enemy;

        protected override void Awake()
        {
            velocity.DoSetup(this);
            movement.DoSetup(this);
            damageContainer.DoSetup(this);
            damagement.DoSetup(this);

            damageCoolDownTimer = new Timer();
        }

        protected override void Update()
        {
            property.RecoveryHP(1);

            property.DoUpdate(Time.deltaTime);

            velocity.Rigidbody.velocity = Vector3.zero;
            damageCoolDownTimer.Update(Time.deltaTime);
            if (!damageCoolDownTimer.IsEnd()) 
            {
                damageContainer.ClearDamage();
                return; 
            }
            DamageUI();
        }

        private void DamageUI()
        {
            if(damageContainer.AttackType == DamageType.None) { return; }
            damageCoolDownTimer.Start(0.25f);
            GameUIController.Instance.DamageTextCreator.Crate(transform, damageContainer.Data,Color.red);
            damageContainer.ClearDamage();
        }
    }
}
