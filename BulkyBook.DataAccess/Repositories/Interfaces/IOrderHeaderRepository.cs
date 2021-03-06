using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repositories.Interfaces
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        void Update(OrderHeader orderHeader);
        void UpdateStatus(int Id, string orderStatus, string? paymentStatus = null);
        void UpdateStripePaymentId(int Id, string sessionId, string paymentInentId);
    }
}
