using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    public void OnStart(Bot bot)
    {
        bot.setGoal(Vector3.zero);

    }
    public void OnExecute(Bot bot)
    {
        bot.ChangeAnim("Bidle");
    }
    public void OnExit(Bot bot)
    {

    }
}
