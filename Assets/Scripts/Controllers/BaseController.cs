using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    [SerializeField]
    protected Vector3 _destPos;
    [SerializeField]
    protected Define.State _state = Define.State.Idle;
    [SerializeField]
    protected GameObject _lockTarget;

    public abstract void Init();

    private void Start()
    {
        Init();
    }

    public virtual Define.State State
    {
        get { return _state; }
        set
        {
            _state = value;
            Animator anim = GetComponent<Animator>();
            switch (_state)
            {
                case Define.State.Die:
                    break;
                case Define.State.Skill:
                    anim.CrossFade("ATTACK", 0.1f, -1, 0);
                    break;
                case Define.State.Idle:
                    anim.CrossFade("WAIT", 0.1f);
                    break;
                case Define.State.Moving:
                    anim.CrossFade("RUN", 0.1f);
                    break;
            }
        }
    }

    void Update()
    {


        switch (State)
        {
            case Define.State.Die:
                OnUpdateDie();
                break;
            case Define.State.Moving:
                OnUpdateMoving();
                break;
            case Define.State.Idle:
                OnUpdateIdle();
                break;
            case Define.State.Skill:
                OnUpdateSkill();
                break;

        }



    }

    protected virtual void OnUpdateDie() { }
    protected virtual void OnUpdateMoving() { }
    protected virtual void OnUpdateIdle() { }
    protected virtual void OnUpdateSkill() { }
}
