using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ZeroDemo.AlarmBox.BoxManagers;
using ZeroDemo.AlarmBox.SensorConfigs;

namespace ZeroDemo.AlarmBox.Boxes
{
    [Table("App_Boxes")]
    public class Box : FullAuditedEntity<long>
    {
        [Required]
        [StringLength(BoxConsts.MaxBoxNameLength)]
        public string BoxName { get; set; }

        [Required]
        [StringLength(BoxConsts.MaxLocationLength)]
        public string Location { get; set; }

        public int MaxBoxPort { get; set; } = BoxConsts.MaxBoxPortDefault;

        public int MaxBoxManagerPort { get; set; } = BoxConsts.MaxBoxManagerPortDefault;

        public ICollection<BoxManager> BoxManagers { get; set; }

        public ICollection<SensorConfig> SensorConfigs { get; set; }

        //public ICollection<ResultTable> ResultTables { get; set; }

        public Box() { }

        public Box(int id, string boxName, string location)
        {
            Id = id;
            BoxName = boxName;
            Location = location;
        }
    }
}
