using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : BaseController
{
    Stat _stat;
    public override void Init()
    {
        _stat = gameObject.GetComponent<PlayerStat>();

        if(gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
        
    }

    protected override void OnUpdateDie()
    {
        base.OnUpdateDie();
        
    }

    protected override void OnUpdateIdle()
    {
        base.OnUpdateDie();
    }

    protected override void OnUpdateSkill()
    {
        base.OnUpdateDie();
    }

    protected override void OnUpdateMoving()
    {
        base.OnUpdateDie();
    }

    void OnHitEvent()
    {

    }
}
