/**********************************************************                                                                                
CoxlinCore - Copyright (c) 2023 Lindsay Cox / MIT License                                                                                                         
**********************************************************/
namespace CoxlinCore
{
    public class Timer
    {
        public float ElapsedTime { set; get; }
        public bool IsRunning { private set; get; }

        public Timer()
        {
            IsRunning = false;
            ElapsedTime = 0.0f;
        }

        public void Start()
        {
            IsRunning = true;
        }

        public void Stop()
        {
            IsRunning = false;
        }

        public void Reset()
        {
            ElapsedTime = 0.0f;
        }

        public void Restart()
        {
            IsRunning = false;
            ElapsedTime = 0.0f;
            IsRunning = true;
        }

        public void Update(float dt)
        {
            if (IsRunning)
            {
                ElapsedTime += dt;
            }
        }
    }
}