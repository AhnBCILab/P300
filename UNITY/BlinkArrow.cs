﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class BlinkArrow : MonoBehaviour
{

    public Image UpArrow; //image to toggle
    public Image DownArrow; //image to toggle
    public Image LeftArrow; //image to toggle
    public Image RightArrow; //image to toggle  
    public int num_of_blink_arrow = 2;
    public float current_time = 0.0f;

    public float interval = 0.1f;
    public float startDelay = 0.1f;
    public float timebetweenarrows = 0.1f;

    public int blinkcnt = 0;
    public int BlinkCount = 40;

    bool isBlinking = false;
    bool arrow0 = true, arrow1 = true, arrow2 = true, arrow3 = true;
    public int up = 0, down = 0, left = 0, right = 0;
    int count = 0;
    int buttonIndexNum = 0;
    int targetChange = 0;
    int rndnum = 0;
    public int[] ranArr = { 0, 1, 2, 3 };
    public string order = "";
    public int value = 4;

    string ipUIVAServer = "localhost";
    public string buttons;

    bool blinkstate = true;
    UIVA_Client theClient = null;

    Image pubimg;

    void Start()
    {
        theClient = new UIVA_Client(ipUIVAServer);
        UpArrow.enabled = true;
        DownArrow.enabled = true;
        LeftArrow.enabled = true;
        RightArrow.enabled = true;
    }
    private void Update()
    {
        current_time += Time.deltaTime;
        if (targetChange == 4 && current_time < 10.0f)
        {
            if (current_time > 5.0f) theClient.Disconnect();
        }
        //Restart blinking
        if (current_time > 10.0f && blinkstate == true)
        {
            blinkstate = false;
            arrow0 = true;
            arrow1 = true;
            arrow2 = true;
            arrow3 = true;
            blinkcnt = 0;
            Blink();
        }
        else if (current_time > 30.0f)
        {
            current_time = 0.0f;
            blinkstate = true;
        }
    }
    public void Blink()
    {
        if (blinkcnt < BlinkCount)
        {
            rndnum = UnityEngine.Random.Range(0, 4);
            if (rndnum == 0 && arrow0 == true)
            {
                arrow0 = false;
                buttonIndexNum = 0;
                theClient.Press_O(buttonIndexNum);
                pubimg = UpArrow;

                if (isBlinking)
                    return;
                if (pubimg != null)
                {
                    isBlinking = true;
                    InvokeRepeating("ToggleState", startDelay, interval);
                }
            }
            else if (rndnum == 0 && arrow0 == false)
            {
                Blink();
            }
            else if (rndnum == 1 && arrow1 == true)
            {
                arrow1 = false;
                buttonIndexNum = 1;
                theClient.Press_O(buttonIndexNum);
                pubimg = DownArrow;

                if (isBlinking)
                    return;

                if (pubimg != null)
                {
                    isBlinking = true;
                    InvokeRepeating("ToggleState", startDelay, interval);
                }
            }
            else if (rndnum == 1 && arrow1 == false)
            {
                Blink();
            }
            else if (rndnum == 2 && arrow2 == true)
            {
                arrow2 = false;
                buttonIndexNum = 2;
                theClient.Press_O(buttonIndexNum);
                pubimg = LeftArrow;

                if (isBlinking)
                    return;

                if (pubimg != null)
                {
                    isBlinking = true;
                    InvokeRepeating("ToggleState", startDelay, interval);
                }
            }
            else if (rndnum == 2 && arrow2 == false)
            {
                Blink();
            }
            else if (rndnum == 3 && arrow3 == true)
            {
                arrow3 = false;
                buttonIndexNum = 3;
                theClient.Press_O(buttonIndexNum);
                pubimg = RightArrow;

                if (isBlinking)
                    return;

                if (pubimg != null)
                {
                    isBlinking = true;
                    InvokeRepeating("ToggleState", startDelay, interval);
                }

            }
            else if (rndnum == 3 && arrow3 == false)
            {
                Blink();
            }

            if (arrow0 == false && arrow1 == false && arrow2 == false && arrow3 == false)
            {
                arrow0 = true;
                arrow1 = true;
                arrow2 = true;
                arrow3 = true;
            }
        }
        else
        {
            theClient.GetDirectionData(out buttons);
            switch (buttons)
            {
                case "Zero":
                    value = 0;
                    break;
                case "One":
                    value = 1;
                    break;
                case "Two":
                    value = 2;
                    break;
                case "Three":
                    value = 3;
                    break;
                default:
                    break;
            }
        }
    }
    public void ToggleState()
    {
        pubimg.enabled = !pubimg.enabled;
        count++;
        if (count == num_of_blink_arrow)
        {
            CancelInvoke();
            blinkcnt++;

            if (rndnum == 0) up++;
            else if (rndnum == 1) down++;
            else if (rndnum == 2) left++;
            else right++;

            count = 0;
            isBlinking = false;
            Invoke("Blink", timebetweenarrows);
        }
    }

}