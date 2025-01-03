using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;

namespace Utils
{
    public static class TimerExtension
    {
        public static event Action OnUpdate;
        
        private static bool _inNextFrame;
        
        private static readonly List<Timer> Timers = new ();
        private static readonly List<IntervalTimer> IntervalTimers = new ();
        private static readonly List<TweenSettings> TweenTimers = new ();
        private static readonly List<Timer> RemoveTimers = new ();
        private static readonly Stack<Action> NextFrameActions = new ();
        private static readonly Stack<Action> NextFrameActionsBuffer = new ();
        
        public static long TotalSeconds(this DateTime dateTime) => (long) (dateTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;

        private struct Timer
        {
            public float Time;
            public Action Callback;
        }
        
        private struct IntervalTimer
        {
            public float Time;
            public float StartTime;
            public Action Callback;
        }
        
        private struct TweenSettings
        {
            public float Time;
            public float Start;
            public float End;
            public float CurrentTime;
            public Action<float> Tween;
        }
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Initialize()
        {
            var playerLoop = PlayerLoop.GetCurrentPlayerLoop();
            var subSub = playerLoop.subSystemList[5].subSystemList;
            PlayerLoopSystem[] systems = new PlayerLoopSystem[subSub.Length + 1];
            for (int i = 0; i < systems.Length; i++)
            {
                if (i != 0)
                {
                    systems[i] = subSub[i-1];
                }
                else
                {
                    systems[i] = new PlayerLoopSystem
                    {
                        type = typeof(TimerExtension)
                    };
                    systems[i].updateDelegate += Update;
                }
            }
            
            playerLoop.subSystemList[5].subSystemList = systems;
            
            PlayerLoop.SetPlayerLoop(playerLoop);
        }

        private static void Update()
        {
            if (!Application.isPlaying)
            {
                PlayerLoop.SetPlayerLoop(PlayerLoop.GetDefaultPlayerLoop());
                Timers.Clear();
                NextFrameActions.Clear();
                NextFrameActionsBuffer.Clear();
                return;
            }

            _inNextFrame = true;
            while (NextFrameActions.TryPop(out Action action))
            {
                try
                {
                    action?.Invoke();
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
            _inNextFrame = false;

            while (NextFrameActionsBuffer.TryPop(out Action action))
            {
                NextFrameActions.Push(action);
            }

            RemoveTimers.Clear();
            
            for (int i = 0; i < Timers.Count; i++)
            {
                var timer = Timers[i];
                timer.Time -= Time.deltaTime;
                if (timer.Time <= 0f)
                {
                    RemoveTimers.Add(timer);
                    try
                    {
                        timer.Callback.Invoke();
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e);
                    }
                    
                }
                Timers[i] = timer;
            }

            for (int i = 0; i < IntervalTimers.Count; i++)
            {
                var timer = IntervalTimers[i];
                timer.Time -= Time.deltaTime;
                if (timer.Time <= 0f)
                {
                    try
                    {
                        timer.Callback.Invoke();
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e);
                    }

                    timer.Time = timer.StartTime;
                }

                IntervalTimers[i] = timer;
            }

            for (int i = 0; i < TweenTimers.Count; i++)
            {
                var timer = TweenTimers[i];
                timer.CurrentTime += Time.deltaTime;
                timer.Tween?.Invoke(Mathf.Lerp(timer.Start, timer.End, timer.CurrentTime/timer.Time));
            }

            Timers.RemoveAll(t => RemoveTimers.Contains(t));
            TweenTimers.RemoveAll(t => t.CurrentTime >= t.Time);
            
            OnUpdate?.Invoke();
        }

        public static void Delay(float seconds, Action callback)
        {
            Timers.Add(new Timer {Callback = callback, Time = seconds});
        }

        public static void Interval(float seconds, Action callback)
        {
            IntervalTimers.Add(new IntervalTimer {StartTime = seconds, Time = seconds, Callback = callback});
        }

        public static void Tween(float time, float start, float end, Action<float> tween)
        {
            TweenTimers.Add(new TweenSettings {Time = time, Start = start, End = end, CurrentTime = 0, Tween = tween});
        }

        public static void NextFrame(Action callback)
        {
            if (_inNextFrame)
            {
                NextFrameActionsBuffer.Push(callback);
                OnUpdate = null;
                return;
            }
            NextFrameActions.Push(callback);
        }
    }
}