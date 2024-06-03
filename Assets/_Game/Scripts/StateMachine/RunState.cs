using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : IState
{
    public void OnStart(Bot bot)
    {
        if(bot.getCount() >= 15)
        {
            bot.goToFinish();
        }
        else
        {
            bot.FindMinTarget();
        }
    }
    public void OnExecute(Bot bot)
    {
        bot.Move();
    }
    public void OnExit(Bot bot)
    {

    }
}
