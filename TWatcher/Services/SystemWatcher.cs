using NvAPIWrapper.GPU;

using System.Management;

using TWatcher.Models;

namespace TWatcher.Services
{
    public class SystemWatcher
    {
        private readonly System.Timers.Timer Timer;
        private readonly ManagementObjectSearcher Searcher;
        private readonly PhysicalGPU Gpu;
        public delegate void Action(Temperatures temperatures);
        public event Action Updated;

        private readonly Temperatures Temperatures;
        public SystemWatcher()
        {
            Timer = new System.Timers.Timer(1000);
            Searcher = new ManagementObjectSearcher(@"root\WMI", "SELECT * FROM MSAcpi_ThermalZoneTemperature");
            Gpu = PhysicalGPU.GetPhysicalGPUs().FirstOrDefault();

            Temperatures = new Temperatures();

            Timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            var obj = Searcher.Get();

            foreach (var item in obj)
            {
                Double temperature = Convert.ToDouble(item["CurrentTemperature"].ToString());
                temperature = (temperature - 2732) / 10.0;

                Temperatures.CPU = temperature;
            }

            try
            {
                var sensor = Gpu.ThermalInformation.ThermalSensors.FirstOrDefault();
                Temperatures.GPU = sensor.CurrentTemperature;
            }
            catch (Exception)
            {
                
            }

            Updated?.Invoke(Temperatures);
        }

        public void Start() => Timer.Start();
        public void Stop() => Timer.Stop();
    }
}
