using UnityEngine;

namespace MyAssets
{
    public enum CharacterType
    {
        Null,
        Player,
        Enemy
    }
    /*
     * �S�L�����N�^�[�Ɍp������x�[�X�N���X
     */
    public class CharacterBaseController : MonoBehaviour
    {
        [SerializeField]
        protected VelocityComponent     velocity;

        public IVelocityComponent       Velocity => velocity;

        [SerializeField]
        protected DamageContainer       damageContainer;
        public IDamageContainer         DamageContainer => damageContainer;

        [SerializeField]
        protected Damagement            damagement;
        public IDamagement              Damagement => damagement;

        [SerializeField]
        protected GroundCheck           groundCheck;
        public IGroundCheck             GroundCheck => groundCheck;

        public virtual CharacterType    CharaType => CharacterType.Null;

        protected virtual void Awake()
        {
            groundCheck.Setup(this);
        }

        protected virtual void Start()
        {
        }

        protected virtual void Update()
        {
            
        }

        protected virtual void FixedUpdate()
        { 
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
        }

        protected virtual void OnTriggerStay(Collider other)
        {
        }

        protected virtual void OnTriggerExit(Collider other)
        {   
        }
    }
}
