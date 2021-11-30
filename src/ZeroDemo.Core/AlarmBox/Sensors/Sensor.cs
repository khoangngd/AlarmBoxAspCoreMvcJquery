using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ZeroDemo.AlarmBox.SensorConfigs;

namespace ZeroDemo.AlarmBox.Sensors
{
    [Table("App_Sensors")]
    public class Sensor : FullAuditedEntity<long>
    {
        [Required]
        [StringLength(SensorConsts.MaxSensorNameLength)]
        public string SensorName { get; set; }

        public double HighValueDefault { get; set; }

        public double LowValueDefault { get; set; }

        public double TargetValueDefault { get; set; }

        public ICollection<SensorConfig> SensorConfigs { get; set; }

        //public ICollection<ResultTable> ResultTables { get; set; }

        public Sensor() { }

        public Sensor(string sensorName)
        {
            SensorName = sensorName;
        }
    }
}
