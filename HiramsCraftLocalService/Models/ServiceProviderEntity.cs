using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiramsCraftLocalService.Models
{
    public class ServiceProviderEntity
    {
        public ServiceProviderEntity(ServiceProvider sp)
        {
            this.ServiceProviderId = sp.ServiceProviderId;
            this.Service = sp.Service;
            this.Description = sp.Description;
            this.EstimatedCost = sp.EstimatedCost;
            this.ActualCost = sp.ActualCost;
            this.LeadTime = sp.LeadTime;
            this.HasMinimumOrder = sp.HasMinimumOrder;
            this.MinimumOrder = sp.MinimumOrder;
            this.HasBulkDiscount = sp.HasBulkDiscount;
            this.BulkDiscount = sp.BulkDiscount;
            this.ProviderId = sp.ProviderId;
            this.ProductId = sp.ProductId;

            //set these to null, and just ids
            this.HiramsCraftProductEntity = null;
            this.ProviderEntity = null;
        }

        public ServiceProviderEntity()
        {
            //set these to null, and just ids
            this.HiramsCraftProductEntity = null;
            this.ProviderEntity = null;        
        }

        public int ServiceProviderId { get; set; }
        public string Service { get; set; }
        public string Description { get; set; }
        public decimal EstimatedCost { get; set; }
        public decimal ActualCost { get; set; }
        public System.DateTime LeadTime { get; set; }
        public bool HasMinimumOrder { get; set; }
        public string MinimumOrder { get; set; }
        public bool HasBulkDiscount { get; set; }
        public string BulkDiscount { get; set; }
        public int ProviderId { get; set; }
        public int ProductId { get; set; }

        public virtual HiramsCraftProductEntity HiramsCraftProductEntity { get; set; }
        public virtual ProviderEntity ProviderEntity { get; set; }

    }
}