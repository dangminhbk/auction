using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;
using WebShop.Authorization.Users;

namespace WebShop.Domain.Seller
{
    public class Seller : FullAuditedAggregateRoot<long>
    {
        [ForeignKey(nameof(Seller.User))]
        public long UserId { get; set; }
        public User User { get; set; }
        //public virtual ICollection<Product.Product> Products { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public PaymentRegisterStatus PaymentRegisterStatus { get; set; } = PaymentRegisterStatus.UnRegistered;

        [ForeignKey(nameof(Seller.SellerLogo))]
        public long? SellerLogoId { get; set; }
        public virtual SellerLogo SellerLogo { get; set; }
        [ForeignKey(nameof(Seller.SellerCover))]
        public long? SellerCoverId { get; set; }
        public virtual SellerCover SellerCover { get; set; }

        public SellerPaymentOption SellerPaymentOption { get; set; }
    }

    public class SellerLogo : FullAuditedEntity<long>
    {
        [ForeignKey(nameof(Image))]
        public long ImageId { get; set; }
        public Image.Image Image { get; set; }
    }

    public class SellerCover : FullAuditedEntity<long>
    {
        [ForeignKey(nameof(Image))]
        public long ImageId { get; set; }
        public Image.Image Image { get; set; }
    }

    public class SellerPaymentOption : FullAuditedEntity<long>, IExtendableObject
    {
        public PaymentOption PaymentOption { get; set; } = PaymentOption.Unset;
        [Column("Payload")]
        public string ExtensionData { get; set; }
    }

    public enum PaymentOption
    {
        Unset,
        BankAccount,
        Cash
    }

    public enum PaymentRegisterStatus
    {
        UnRegistered,
        Registered,
    }
}
