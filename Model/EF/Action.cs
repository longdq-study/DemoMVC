namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Action")]
    public partial class Action
    {
        public long ID { get; set; }

        [StringLength(200)]
        public string ActionName { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        [Required]
        [StringLength(250)]
        public string ControllerID { get; set; }
    }
}