using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ZeroDemo.AlarmBox.Boxes;

namespace ZeroDemo.AlarmBox.BoxManagers
{
    [Table("App_BoxManagers")]
    public class BoxManager : FullAuditedEntity<long>
    {
        public long BoxId { get; set; }
        [ForeignKey("BoxId")]
        public Box Box { get; set; }

        [Required]
        [StringLength(BoxManagerConsts.MaxManagerNameLength)]
        public string ManagerName { get; set; }

        [Required]
        [StringLength(BoxManagerConsts.MaxManagerPhoneNumberLength)]
        public string ManagerPhoneNumber { get; set; }

        [Required]
        [StringLength(BoxManagerConsts.MaxManagerEmailLength)]
        public string ManagerEmail { get; set; }

        public int ManagerPort { get; set; }

        public bool IsAlarm { get; set; }

        public BoxManager()
        {
            IsAlarm = false;
        }

        public BoxManager(int managerPort, int boxId, string managerName, string managerPhoneNumber, string managerEmail)
        {
            ManagerPort = managerPort;
            BoxId = boxId;
            ManagerName = managerName;
            ManagerPhoneNumber = managerPhoneNumber;
            ManagerEmail = managerEmail;
        }
    }
}
