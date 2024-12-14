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

        [SerializeField]
        private PlayerRotation rotation;
        public IRotation Rotation => rotation;

        private Timer damageCoolDownTimer;

        public IFieldOfView FieldOfView => null;

        public IStateMachine StateMachine => null;

        public IEnemyAnimator EnemyAnimator => null;

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
                damageContainer.SetAttackerData(0, AttackType.None, null);
                return; 
            }
            DamageUI();
        }

        private void DamageUI()
        {
            if(damageContainer.AttackType == AttackType.None) { return; }
            damageCoolDownTimer.Start(0.25f);
            GameManager.Instance.DamageTextCreator.Crate(transform, damageContainer.Data);
            damageContainer.SetAttackerData(0, AttackType.None, null);
        }
    }
}
