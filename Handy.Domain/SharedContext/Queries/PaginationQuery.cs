using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Handy.Domain.SharedContext.Queries
{
    public class PaginationQuery
    {
        [Required]
        public int Limit { get; set; } = 10;
        
        [Required]
        public int Offset { get; set; } = 0;
        
        public List<string> Filter { get; set; } = new List<string>();
    }
}