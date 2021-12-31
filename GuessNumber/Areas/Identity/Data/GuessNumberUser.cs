using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using GuessNumber.Models;
using Microsoft.AspNetCore.Identity;

namespace GuessNumber.Areas.Identity.Data
{
    public class GuessNumberUser : IdentityUser
    {   
        [PersonalData]
        [Column(TypeName ="nvarchar(100)")]
        public string? FirstName { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string? LastName { get; set; }

        //[NotMapped]
        //public MatchRequest MatchRequest { get; set; }
        //[NotMapped]
        //public MatchResponse MatchResponse { get; set; }

        public GuessNumberUser()
        {
        }
    }
}




