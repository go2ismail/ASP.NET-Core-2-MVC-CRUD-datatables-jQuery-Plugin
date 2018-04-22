using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace src.Models
{
    public class Todo
    {

        public int todoId { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Todo Item Name")]
        public string todoName { get; set; }
    }
}
