﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : Action
{
    private int jump_frame = 0;
    
    public override void SetUp(AbstractFighter _actor)
    {
        sprite_name = "land";
        jump_frame = 5;
        length = 6;
        base.SetUp(_actor);
        Debug.Log("JumpAction Created");
    }

    public override void Update()
    {
        base.Update();
        if (current_frame == jump_frame)
        {
            if (Input.GetButton("Jump"))
                actor._ySpeed = actor.jump_height;
            else
                actor._ySpeed = actor.short_hop_height;
            if (Mathf.Abs(actor._xSpeed) > actor.aerial_transition_speed)
            {
                if (actor._xSpeed < 0) //negative speed
                    actor._xSpeed = -actor.aerial_transition_speed;
                else
                    actor._xSpeed = actor.aerial_transition_speed;
            }
            actor.GetComponent<SpriteLoader>().ChangeSprite("jump");
        }
    }

    public override void stateTransitions()
    {
        base.stateTransitions();
        //if (Input.GetButton("Shield"))
        //actor.doAction("AirDodge");
        //if (Input.GetButton("Attack")) //&& actor.CheckSmash("Up")
        //actor.doAction("UpSmash")
        //if (Input.GetButton("Special")) //&& actor.CheckSmash("Up")
        //actor.doAction("UpSpecial")
        if (current_frame < jump_frame)
            StateTransitions.JumpState(actor);
        if (current_frame > last_frame)
            actor.doAction("Fall");

    }
}