using RSG;
using UnityEngine;
using UnityEngine.Events;

namespace Common
{
    public class Timer : FloatValueProvider
    {
        [SerializeField] private float fasterTimersMultiplier = 3;
        [SerializeField] private bool m_Multiplier = true;
        [SerializeField] private float startTime = float.NegativeInfinity;
        [SerializeField] private float finishTime = float.PositiveInfinity;

        [SerializeField] private float pausedTime;
        [SerializeField] private bool paused;

        private float CurrentTime => paused ? pausedTime : TimeManager.Time();

        public override float CalculateValue => (finishTime - CurrentTime) * fasterTimersMultiplier;

        public float TimeSpent => (CurrentTime - startTime) * fasterTimersMultiplier;

        public UnityEvent onTimeout;
        public bool once;
        public bool spent;

        Promise<bool> timeout;

        public void Pause()
        {
            pausedTime = CurrentTime;
            paused = true;
        }

        public void Unpause()
        {
            var delta = TimeManager.Time() - pausedTime;
            startTime += delta;
            finishTime += delta;
            paused = false;
        }

        public void Awake()
        {
            if (m_Multiplier)
            {
                fasterTimersMultiplier = 3;
            }
            else
            {
                fasterTimersMultiplier = 1;
            }
        }

        public void StartFrom(float duration)
        {
            startTime = CurrentTime;
            finishTime = CurrentTime + duration / fasterTimersMultiplier;
            if (timeout != null)
            {
                timeout.Resolve(false);
            }
            timeout = new Promise<bool>();

            DebugManager.LogFormat($"StartFrom {this.ExtToString()}");
        }

        public IPromise<bool> RunFrom(float duration)
        {
            StartFrom(duration);
            return timeout;
        }

        public void Timeout()
        {
            if (timeout == null)
            {
                return;
            }
            if (once && spent)
            {
                return;
            }

            timeout.Resolve(true);
            timeout = null;

            onTimeout.Invoke();

            spent = true;

            DebugManager.LogFormat($"Timeout {this.ExtToString()}");
        }

        public void Stop()
        {
            if (timeout != null)
            {
                timeout.Resolve(false);
                timeout = null;
            }
            startTime = finishTime = float.NegativeInfinity;
            DebugManager.LogFormat($"Stop {this.ExtToString()}");
        }

        protected override void Update()
        {
            if (startTime == float.NegativeInfinity || timeout == null)
                return;
            base.Update();

            if (Value < 0)
            {
                Timeout();
            }
        }
    }
}