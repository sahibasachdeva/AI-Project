using System.Web.Mvc;
using TestApplication7.Models;
using TestApplication7.Services.Ics;

namespace TestApplication7.Controllers
{
    public class RateController : Controller
    {
        // GET: Rate
        public ActionResult Index()
        {
            return View(new ViewRatingModel());
        }

        [HttpPost]
        public ActionResult Index(ViewRatingModel viewModel)
        {
            ViewRatingModel rate = new ViewRatingModel();
            {

                var api = new IcsServices();
                decimal result = api.GetEstimatedCharges(viewModel.Ratemodel.Weight, viewModel.Ratemodel.Width, viewModel.Ratemodel.Length, viewModel.Ratemodel.Height, viewModel.Ratemodel.PostalCode);

                rate.EstimatedCost = result;
                rate.Ratemodel = viewModel.Ratemodel;

                if (result == 0)
                    rate.Error = "Something went wrong with this request";

                if (result <= 10)
                    rate.Message = "This is a very inexpensive rate";

                if (result >= 10)
                    rate.Message = "This is an expensive rate";

            }
            return View("Post", rate);

        }
    }

}