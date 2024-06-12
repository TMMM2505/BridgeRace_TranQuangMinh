using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : IState
{
    public void OnStart(Bot bot)
    {
        if(bot.getCount() >= bot.getNeedAmount())
        {
            bot.goToFinish();
        }
        else
        {
            bot.FindMinTarget();
        }
        bot.Move();
    }
    public void OnExecute(Bot bot)
    {
        bot.ChangeState(new RunState());
    }
    public void OnExit(Bot bot)
    {

    }
}
