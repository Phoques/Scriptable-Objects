using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Timer
{
public class TimerScript : MonoBehaviour
{
        
        public BaseTimer baseTimer;
        public Text timerText;

        private void Start()
        {
            baseTimer.baseTime = 0;
        }

        private void Update()
        {
            TimeSpan timespan = TimeSpan.FromSeconds(baseTimer.baseTime);
            baseTimer.baseTime += Time.deltaTime;
            timerText.text = baseTimer.baseTime.ToString();
            timerText.text = timespan.ToString("m':'ss");
        }

    }

}
