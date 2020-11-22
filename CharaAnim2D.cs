using UnityEngine;

namespace CoxlinCore
{
    public abstract class CharaAnim2D : MonoBehaviour
    {
        [SerializeField]
        protected Animator _anim;

        public void SetMoveAnim(Vector2 moveDir)
        {
            if (moveDir == Vector2.up)
            {
                _anim.SetState("MoveUp");
            }
            else if (moveDir == Vector2.down)
            {
                _anim.SetState("MoveDown");
            }
            else if (moveDir == Vector2.left)
            {
                _anim.SetState("MoveLeft");
            }
            else if (moveDir == Vector2.right)
            {
                _anim.SetState("MoveRight");
            }
        }

        public void SetIdleAnim(Vector2 moveDir)
        {
            if (moveDir == Vector2.up)
            {
                _anim.SetState("IdleUp");
            }
            else if (moveDir == Vector2.down)
            {
                _anim.SetState("IdleDown");
            }
            else if (moveDir == Vector2.left)
            {
                _anim.SetState("IdleLeft");
            }
            else if (moveDir == Vector2.right)
            {
                _anim.SetState("IdleRight");
            }
        }

        public abstract void Attack(Vector2 moveDir);

        public abstract void Die(Vector2 moveDir);
    }
}
