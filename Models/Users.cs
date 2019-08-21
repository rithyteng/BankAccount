    using System.ComponentModel.DataAnnotations;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;

namespace bankaccount.Models
    {
        public class User
        {
            // auto-implemented properties need to match the columns in your table
            // the [Key] attribute is used to mark the Model property being used for your table's Primary Key
            [Key]
            public int Id { get; set; }
            // MySQL VARCHAR and TEXT types can be represeted by a string
            [Required(ErrorMessage="Please Input Your First Name")]
            public string FirstName { get; set; }
            [Required(ErrorMessage="Please Input Your Last Name")]

            public string LastName { get; set; }
            [Required(ErrorMessage="Please Input Your Email")]
            [EmailAddress(ErrorMessage="Please Input Valid Email")]

            public string Email { get; set; }
            [Required(ErrorMessage="Please Input Your Password")]
            [DataType(DataType.Password)]

            public string Password { get; set; }
            // The MySQL DATETIME type can be represented by a DateTime
            public DateTime CreatedAt {get;set;} = DateTime.Now;
            public DateTime UpdatedAt {get;set;} = DateTime.Now;

            public List<Transaction> mytran {get;set;}

            public int TransId{get;set;}

            [NotMapped]
            [Required(ErrorMessage="Please Input Confirmation Password")]
            [Compare("Password")]
            [DataType(DataType.Password)]
            public string Confirm {get;set;}

        }

        public class Login{

        public string Email {get; set;}
        public string Password { get; set; }
        }
        public class Transaction{
            [Key]
            public int TransId{get;set;}
            

            public int Id{get;set;}

            [Required]
            public decimal Amount{get;set;}

            [Required]
            public DateTime Created_At{get;set;} = DateTime.Now;

            public User myuser{get;set;}
        }
    }
    