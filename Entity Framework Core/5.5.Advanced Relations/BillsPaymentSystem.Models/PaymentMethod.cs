namespace BillsPaymentSystem.Models
{
    using Attributes;
    using Enums;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class PaymentMethod
    {
        [Key]
        public int Id { get; set; }

        public PaymentType Type { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        [XorAttribute(nameof(BankAccountId))]
        public int? BankAccountId { get; set; }

        public BankAccount BankAccount { get; set; }

        [XorAttribute(nameof(CreditCardId))]
        public int? CreditCardId { get; set; }

        public CreditCard CreditCard { get; set; }
    }
}
