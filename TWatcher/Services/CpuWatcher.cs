using NvAPIWrapper.GPU;

using System.Management;

namespace TWatcher.Services
{
    internal class CpuWatcher
    {
        public double CurrentValue { get; set; }
        public string InstanceName { get; set; }
        public static List<CpuWatcher> Temperatures
        {
            get
            {
                List<CpuWatcher> result = new();
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"root\WMI", "SELECT * FROM MSAcpi_ThermalZoneTemperature");
                foreach (ManagementObject obj in searcher.Get())
                {
                    Double temperature = Convert.ToDouble(obj["CurrentTemperature"].ToString());
                    temperature = (temperature - 2732) / 10.0;
                    result.Add(new CpuWatcher { CurrentValue = temperature, InstanceName = obj["InstanceName"].ToString() });
                }

                PhysicalGPU[] gpus = PhysicalGPU.GetPhysicalGPUs();
                foreach (PhysicalGPU gpu in gpus)
                {
                    try
                    {
                        foreach (GPUThermalSensor sensor in gpu.ThermalInformation.ThermalSensors)
                        {
                            Console.WriteLine(sensor.CurrentTemperature);
                        }
                    }
                    catch (Exception)
                    {
                        continue;
                    }
       
                }

                return result;
            }
        }
    }
}
