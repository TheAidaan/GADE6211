using System;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public event EventHandler<OnObstaclePasedEventArgs> OnObstaclePased;
    public class OnObstaclePasedEventArgs : EventArgs
    {
        public int obstaclesPassed;
    }

    int _obstaclesPassed = 0;

    public void ObstaclePassed()
    {
        OnObstaclePased?.Invoke(this, new OnObstaclePasedEventArgs { obstaclesPassed = _obstaclesPassed });
    }

}
