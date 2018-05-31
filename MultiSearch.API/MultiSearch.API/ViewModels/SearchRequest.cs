using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MultiSearch.API.ViewModels
{
    public class SearchRequest
    {
        [Required]
        [MaxLength(32)]
        public string Query { get; set; }
    }
}