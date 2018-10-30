using System.ComponentModel.DataAnnotations;
using System;
namespace LoginReg.Models
{
    public class Transaction
    {
        [Key]

        public int TransactionId { get; set; }
        public int UserId { get; set; }

        public DateTime Date {get; set; }

        [Display (Name = "Withdrawl Money: ")]
        public int Withdrawl { get; set; }

        [Display (Name = "Deposit Money: ")]
        public int Deposit { get; set; }
        
    }
}
