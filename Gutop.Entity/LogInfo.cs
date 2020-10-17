using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Gutop.Entity
{
    /// <summary>
    /// 
    /// <summary>
    [Table("LogInfo")]
    public partial class Log
    {
        /// <summary>
        ///
        /// </summary>
        [Key]
        public Guid Id {set;get;}
        /// <summary>
        ///
        /// </summary>
        [Required]
        [StringLength(192)]
        public String Source {set;get;}
        /// <summary>
        ///
        /// </summary>
        [Required]
        [StringLength(65535)]
        public String Message {set;get;}
    }
}
