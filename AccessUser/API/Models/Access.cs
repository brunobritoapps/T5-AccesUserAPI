using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Access
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccessId { get; set; }

        public string Ip { get; set; }

        public string Page { get; set; }

        public string Browser { get; set; }

        public string Parameters { get; set; }

        public string Date { get; set; }
    }
}
