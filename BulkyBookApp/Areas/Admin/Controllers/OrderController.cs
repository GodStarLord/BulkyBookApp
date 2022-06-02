using BulkyBook.DataAccess.Repositories.Interfaces;
using BulkyBook.Models;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll(string statusFilter)
        {
            IEnumerable<OrderHeader> orderHeaders = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser");

            switch (statusFilter)
            {
                case "pending":
                    orderHeaders = orderHeaders.Where(x => x.PaymentStatus == Constants.PaymentStatusDelayedPayment);
                    break;
                case "inprocess":
                    orderHeaders = orderHeaders.Where(x => x.OrderStatus == Constants.StatusInProcess);
                    break;
                case "completed":
                    orderHeaders = orderHeaders.Where(x => x.OrderStatus == Constants.StatusShipped);
                    break;
                case "approved":
                    orderHeaders = orderHeaders.Where(x => x.OrderStatus == Constants.StatusApproved);
                    break;
            }

            return Json(new { tableData = orderHeaders });
        }
        #endregion
    }
}
