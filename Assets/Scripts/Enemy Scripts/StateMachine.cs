using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    
    private Dictionary<Type, BaseState> _availableStates;

    public BaseState CurrentState { get; private set; }
    public event Action<BaseState> OnStateChanged;

    public void SetStates(Dictionary<Type, BaseState> states)
    {
        _availableStates = states;
    }


    // Update is called once per frame
    void Update()
    {
        if(CurrentState == null)
        {
            CurrentState = _availableStates.Values.First();

        }

        Type nextState = CurrentState?.Tick(); //?. checks its not null. The tick method returns back the type of the state.
        if(nextState != null && nextState != CurrentState?.GetType())
        {
            SwitchToNewState(nextState);
        }
    }

    private void SwitchToNewState(Type nextState)
    {
        CurrentState = _availableStates[nextState];
        OnStateChanged?.Invoke(CurrentState);
    }
}
