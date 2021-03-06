﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Urms.Model
{
    public class Student:BaseEntity
    {
        
       public Student()
        {
            Results = new HashSet<Result>();
            
        }

        [Required(ErrorMessage = "Öğrenci adı gerekli")]
        [Display(Name = "Öğrenci  Adı")]
        public string StudentName { get; set; }


        [Required(ErrorMessage = "Email gerekli")]
        [EmailAddress(ErrorMessage = "Geçersiz Email girişi"), StringLength(50)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Öğrenci iletişim no gerekli")]
        [Display(Name = "İletişim No")]
        public string ContactNo { get; set; }

        [Display(Name = "Tarih")]
        [Required(ErrorMessage = "Kayıt için tarih gerekli")]
        public DateTime RegDate { get; set; }

        [Display(Name = "Adres")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Öğrenci adresi gerekli")]
       
        public string Address { get; set; }

        [Display(Name = "Bölüm")]
        
        public Guid? DeptId { get; set; }

        [Display(Name = "Kayıt Numarası")]
        //[Required]
        public string RegNo { get; set; }

        
        public virtual Department Department { get; set; }
        public ICollection<Result> Results { get; set; }
    }
}
