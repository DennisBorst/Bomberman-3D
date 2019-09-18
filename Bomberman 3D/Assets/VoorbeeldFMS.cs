using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VaalsFSM
{

    public class FSMVaal : MonoBehaviour
    {
        public FSM fsm;
        public State startState;
        // Start is called before the first frame update
        void Start()
        {
            fsm = new FSM(StateEnum.Idle, new IdleState(StateEnum.Idle));
        }

        // Update is called once per frame
        void Update()
        {
            fsm.OnUpdate();
        }
    }

    public enum StateEnum { Idle, Attack, Hide }

    public class FSM
    {
        private State currentState;
        public Dictionary<StateEnum, State> states = new Dictionary<StateEnum, State>();

        public FSM(StateEnum startState, params State[] statesList)
        {
            foreach (State state in statesList)
            {
                state.Init(this);
                states.Add(state.id, state);
            }

            SwitchState(startState);
        }

        public void SwitchState(StateEnum newState)
        {
            currentState?.OnExit();
            currentState = states[newState];
            currentState?.OnEnter();
        }

        public void OnUpdate()
        {
            currentState?.OnUpdate();
        }
    }

    public abstract class State
    {
        public FSM fsm;
        public StateEnum id;
        public void Init(FSM owner)
        {
            fsm = owner;
        }
        public abstract void OnEnter();
        public abstract void OnUpdate();
        public abstract void OnExit();
    }

    public class IdleState : State
    {
        public IdleState(StateEnum id)
        {
            this.id = id;
        }
        public override void OnEnter()
        {
            throw new System.NotImplementedException();
        }

        public override void OnExit()
        {
            throw new System.NotImplementedException();
        }

        public override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }
    }

    public class AttackState : State
    {
        public AttackState(StateEnum id)
        {
            this.id = id;
        }
        public override void OnEnter()
        {
            throw new System.NotImplementedException();
        }

        public override void OnExit()
        {
            throw new System.NotImplementedException();
        }

        public override void OnUpdate()
        {
            fsm.SwitchState(StateEnum.Hide);
        }
    }
}