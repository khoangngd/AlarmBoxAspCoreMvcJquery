using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ZeroDemo.AlarmBox.Boxes;
using ZeroDemo.AlarmBox.Sensors;

namespace ZeroDemo.AlarmBox.SensorConfigs
{
    [Table("App_SensorConfigs")]
    public class SensorConfig : FullAuditedEntity<long>
    {
        public long BoxId { get; set; }
        [ForeignKey("BoxId")]
        public Box Box { get; set; }

        public long SensorId { get; set; }
        [ForeignKey("SensorId")]
        public Sensor Sensor { get; set; }

        public double HighValue { get; set; }

        public double LowValue { get; set; }

        public double TargetValue { get; set; }

        [StringLength(SensorConfigConsts.MaxInrangeColorLength)]
        public string InrangeColor { get; set; }

        [StringLength(SensorConfigConsts.MaxOutrangeColorLength)]
        public string OutrangeColor { get; set; }

        public bool IsAlarm { get; set; }

        public string AlarmMessage { get; set; }

        public int BoxPort { get; set; }

        public SensorConfig()
        {
            IsAlarm = false;
        }
    }
}
