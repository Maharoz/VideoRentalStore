﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class CustomerDtos
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter customer's message")]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribeToNewsLetter { get; set; }

        public byte MembershipTypeId { get; set; }


        [Min18YearsIfAMember]
        public DateTime? Birthdate { get; set; }
    }
}