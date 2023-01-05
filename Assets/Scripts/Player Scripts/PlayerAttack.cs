using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ComboState {
    NONE,
    PUNCH_1,
    PUNCH_2,
    PUNCH_3,
    KICK_1,
    KICK_2
}
public class PlayerAttack : MonoBehaviour
{
    private CharacterAnimation player_Anim;
    private bool activateTimeToReset;
    private float default_Combo_Timer = 0.4f;
    private float current_Combo_Timer;
    private ComboState current_Combo_State;
    void Awake()
    {
        player_Anim = GetComponentInChildren<CharacterAnimation>();
    }
    // Start is called before the first frame update
    void Start()
    {
        current_Combo_Timer = default_Combo_Timer;
        current_Combo_State = ComboState.NONE;
    }
    // Update is called once per frame
    void Update()
    {
        ComboAttacks();
        ResetComboState();
    }
    void ComboAttacks()
    {
        ExecutePunchCombo();
        ExecuteKickCombo();
    }
    void ResetComboState()
    {
        if(activateTimeToReset)
        {
            current_Combo_Timer -=  Time.deltaTime;
            if(current_Combo_Timer <= 0)
            {
                current_Combo_State = ComboState.NONE;
                activateTimeToReset = false;
                current_Combo_Timer = default_Combo_Timer;
            }
        }
    }
    void ExecutePunchCombo()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
           if(
            current_Combo_State == ComboState.PUNCH_3 ||
            current_Combo_State == ComboState.KICK_1 ||
            current_Combo_State == ComboState.KICK_2
           )
           {
            return;
           }
           // otherwise change combo state
           current_Combo_State ++;
           activateTimeToReset = true;
           current_Combo_Timer = default_Combo_Timer;
           // cycle through various combo animations
           if(current_Combo_State == ComboState.PUNCH_1)
           {
            player_Anim.Punch_1();
           }
           if(current_Combo_State == ComboState.PUNCH_2)
           {
            player_Anim.Punch_2();
           }
           if(current_Combo_State == ComboState.PUNCH_3)
           {
            player_Anim.Punch_3();
           }
        }
    }
    void ExecuteKickCombo()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            // if current state is punch 3 or kick 2 return
            // no combos to perform
           if(
            current_Combo_State == ComboState.KICK_2 ||
            current_Combo_State == ComboState.PUNCH_3
           )
           {
            return;
           }
           // if current combo state is none, punch 1 or punch 2
           // then change state to kick 1 to chain combo
           if(
            current_Combo_State == ComboState.NONE ||
            current_Combo_State == ComboState.PUNCH_1 ||
            current_Combo_State == ComboState.PUNCH_2
           )
           {
            current_Combo_State = ComboState.KICK_1;
           }
           // move from current state kick 1 to kick 2
           else if(current_Combo_State == ComboState.KICK_1)
           {
            current_Combo_State ++;
           }
           activateTimeToReset = true;
           current_Combo_Timer = default_Combo_Timer;
           // perform animations
           if(current_Combo_State == ComboState.KICK_1)
           {
            player_Anim.Kick_1();
           }
           if(current_Combo_State == ComboState.KICK_2)
           {
            player_Anim.Kick_2();
           }
        }
    }
}
