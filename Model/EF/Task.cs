namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Task")]
    public partial class Task
    {
        public long Id { get; set; }

        public DateTime? PlanedStartDate { get; set; }

        public DateTime? PlanedEnDate { get; set; }

        public DateTime? ActualStartDate { get; set; }

        public DateTime? ActualEndDate { get; set; }

        public long? AssigneeID { get; set; }

        public long? ReporterID { get; set; }

        public short? PercentDone { get; set; }

        public short? Status { get; set; }

        [Column(TypeName = "ntext")]
        public string Attachment { get; set; }
    }
}
