using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;

public class Example : MonoBehaviour
{
    public LeapProvider leapProvider;

    private void OnEnable()
    {
        leapProvider.OnUpdateFrame += OnUpdateFrame;
    }
    private void OnDisable()
    {
        leapProvider.OnUpdateFrame -= OnUpdateFrame;
    }

    void OnUpdateFrame(Frame frame)
    {
        //Use a helpful utility function to get the first hand that matches the Chirality
        Hand _rightHand = frame.GetHand(Chirality.Right);

        //When we have a valid left hand, we can begin searching for more Hand information
        if(_rightHand != null)
        {
            OnUpdateHand(_rightHand);
        }
    }
    void OnUpdateHand(Hand _hand)
    {
        Arm _arm = _hand.Arm;
        Vector3 _elbowPosition = _arm.ElbowPosition; // (左(大)右, 前(大)後, 上(大)下)
        Debug.Log(_elbowPosition);
    }
}

