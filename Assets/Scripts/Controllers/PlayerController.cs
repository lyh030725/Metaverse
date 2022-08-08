using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerController : BaseController
{
    

    int _mask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);

    PlayerStat _stat;

    bool _stopSkill = false;

    public override void Init()
    {
       
        _stat = gameObject.GetComponent<PlayerStat>();
        Managers.Input.MouseAction -= OnMouseEvent;
        Managers.Input.MouseAction += OnMouseEvent;
        if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
    }

    protected override void OnUpdateDie()
    {
        // 아무것도 못함
    }

    protected override void OnUpdateMoving()
    {
        //몬스터가 내 사정거리 안에 있으면 공격
        if(_lockTarget != null)
        {
            float distance = (_destPos - transform.position).magnitude;
            if (distance < 1)
            {
                State = Define.State.Skill;
                return;
            }
        }
        


        //이동
        Vector3 dir = _destPos - transform.position;



        if (dir.magnitude < 0.1f)
        {
            State = Define.State.Idle;
        }
        else
        {


            float moveDist = _stat.MoveSpeed * Time.deltaTime;
            moveDist = Mathf.Clamp(moveDist, 0, dir.magnitude);

            //if (moveDist >= dir.magnitude)
            //    moveDist = dir.magnitude;
            Debug.DrawRay(transform.position + Vector3.up * 0.5f, dir.normalized,Color.green);
            if(Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1, LayerMask.GetMask("Block")))
            {
                if(Input.GetMouseButton(0) == false)
                    State = Define.State.Idle;
                return;
            }


            NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
            _destPos.y = transform.position.y;
            nma.Move(dir.normalized * moveDist);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);


            //wait_run_ratio = Mathf.Lerp(wait_run_ratio, 1, 10.0f * Time.deltaTime);
            
            //anim.SetFloat("wait_run_ratio", wait_run_ratio);
            //anim.Play("WAIT_RUN");

        }


    }

    protected override void OnUpdateIdle()
    {
        //wait_run_ratio = Mathf.Lerp(wait_run_ratio, 0, 10.0f * Time.deltaTime);
        
        //anim.SetFloat("wait_run_ratio", wait_run_ratio);
        //anim.Play("WAIT_RUN");
    }

    protected override void OnUpdateSkill()
    {
        if(_lockTarget != null) {
            Vector3 dir = _lockTarget.transform.position - transform.position;
            Quaternion qua = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, qua, 20 * Time.deltaTime);
        }

        
    }

    void OnHitEvent()
    {
        if (_lockTarget != null)
        {
            Stat targetStat = _lockTarget.GetComponent<Stat>();
            PlayerStat myStat = gameObject.GetComponent<PlayerStat>();
            int damage = Mathf.Max(0, myStat.Attack - targetStat.Defense);
            targetStat.Hp -= damage;
        }

        if (_stopSkill)
            State = Define.State.Idle;
        else
            State = Define.State.Skill;
         
    }

    ////void OnKeyBoard()
    //{
    //    if (Input.GetKey(KeyCode.W))
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
    //        transform.position += Vector3.forward * Time.deltaTime * _speed;

    //        //transform.Translate(Vector3.forward * Time.deltaTime * _speed;)
    //    }
    //    if (Input.GetKey(KeyCode.S))
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
    //        transform.position += Vector3.back * Time.deltaTime * _speed;
    //    }
    //    if (Input.GetKey(KeyCode.A))
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
    //        transform.position += Vector3.left * Time.deltaTime * _speed;
    //    }
    //    if (Input.GetKey(KeyCode.D))
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
    //        transform.position += Vector3.right * Time.deltaTime * _speed;
    //    }

    //    _moveToDest = false;
    //}

    void OnMouseEvent(Define.MouseEvent evt) {

        switch (State)
        {
            case Define.State.Idle:
                OnMouseEvent_IdleRun(evt);
                break;
            case Define.State.Moving:
                OnMouseEvent_IdleRun(evt);
                break;
            case Define.State.Skill:
                {
                    if (evt == Define.MouseEvent.PointerUP)
                        _stopSkill = true;
                    break;
                }
               
        }
        

    }

    void OnMouseEvent_IdleRun(Define.MouseEvent evt)
    {

        if (State == Define.State.Die)
            return;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool raycastiHit = Physics.Raycast(ray, out hit, 100.0f, _mask);

        switch (evt)
        {

            case Define.MouseEvent.PointerDown:
                {
                    if (raycastiHit)
                    {
                        State = Define.State.Moving;
                        _destPos = hit.point;
                        _stopSkill = false;

                        if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
                            _lockTarget = hit.collider.gameObject;
                        else
                            _lockTarget = null;

                    }
                    break;
                }
            case Define.MouseEvent.Press:
                {
                    if (_lockTarget == null && raycastiHit)
                        _destPos = hit.point;
                    break;
                }
            case Define.MouseEvent.PointerUP:
                {
                    _stopSkill = true;
                    break;
                }
        }
    }

}
