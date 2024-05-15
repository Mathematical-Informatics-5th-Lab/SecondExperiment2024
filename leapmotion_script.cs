using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        Finger _thumb = _hand.GetThumb();
        Finger _index = _hand.GetIndex();
        Finger _middle = _hand.GetMiddle();
        Finger _ring = _hand.GetRing();
        Finger _pinky = _hand.GetPinky();

        // Get the extended state of each finger
        bool thumbIsExtended = _thumb.IsExtended;
        bool indexIsExtended = _index.IsExtended;
        bool middleIsExtended = _middle.IsExtended;
        bool ringIsExtended = _ring.IsExtended;
        bool pinkyIsExtended = _pinky.IsExtended;

        List<bool> isExtended = new List<bool> { thumbIsExtended, indexIsExtended, middleIsExtended, ringIsExtended, pinkyIsExtended };

        // Count the number of extended fingers
        int extendedCount = isExtended.Count(extended => extended);

        // Get the palm position
        Vector3 _palmPosition = _hand.PalmPosition;
        
        // Combine the extended states and palm position into one log message
        string logMessage = $"Finger extended states: {string.Join(", ", isExtended)}; Number of extended fingers: {extendedCount}; Palm position: {_palmPosition}";

        Debug.Log(logMessage);
    }
}
