using Microsoft.AspNetCore.Mvc;
using GS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace GS.Controllers
{
    public class RevenueController : Controller
    {
        private readonly DACSDbContext _context;

        public RevenueController(DACSDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetBillDetails(string month)
        {
            if (string.IsNullOrEmpty(month))
            {
                return BadRequest("Month is required");
            }

            // Format the month to match the expected format in parsing.
            string formattedMonth = month.Replace("THÁNG ", "").Trim().ToUpperInvariant();


            DateTime monthYear;
            if (!DateTime.TryParseExact($"{formattedMonth}", "M yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out monthYear))
            {
                return BadRequest("Invalid month format. Please use 'MM yyyy'.");
            }

            var billDetails = await _context.Bills
                .Include(b => b.ApplicationUser)
                .Where(b => b.DateOfPayment.HasValue && b.DateOfPayment.Value.Month == monthYear.Month && b.DateOfPayment.Value.Year == 2024)
                .ToListAsync();

            if (billDetails == null || billDetails.Count == 0)
            {
                return Content("No bill details found for the selected month.");
            }

            return PartialView("_BillDetailsPartial", billDetails);
        }


        public IActionResult RevenueComparison()
        {
            var billData = _context.Bills.ToList();

            // Group and aggregate bills by month and year
            var revenueByMonth = billData.GroupBy(bill => new { bill.DateOfPayment.Value.Year, bill.DateOfPayment.Value.Month })
                                         .Select(group => new
                                         {
                                             MonthYear = new DateTime(group.Key.Year, group.Key.Month, 1).ToString("MMMM yyyy"),
                                             TotalRevenue = group.Sum(bill => (bill.TotalMoney ?? 0) - bill.TotalDiscount)
                                         }).ToList();

            // Prepare data for view model
            var labels = revenueByMonth.Select(x => x.MonthYear).ToList();
            var revenues = revenueByMonth.Select(x => x.TotalRevenue).ToList();
            var monthlyRevenueDetails = revenueByMonth.ToDictionary(
                x => x.MonthYear,
                x => x.TotalRevenue);

            var viewModel = new RevenueComparisonViewModel
            {
                Labels = labels,
                Revenues = revenues,
                MonthlyRevenueDetails = monthlyRevenueDetails
            };

            return View(viewModel);
        }

    }
}

