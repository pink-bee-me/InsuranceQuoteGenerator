using InsuranceQuoteGenerator.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace InsuranceQuoteGenerator.Controllers
{
    public class AutoQuotesController : Controller
    {
        //gain access to the Insurance Quote Entities(Insurees , AutoQuotes)

        private readonly InsuranceModelEntities db = new InsuranceModelEntities();

        // ActionResult Index() returns the InsureeController Index View (/Views/Insuree/Index.cshtml)//
        // This Index View Displays a list of all the current Insurees stored in the Insurance Database//
        //(you can see this by looking at the parameters that are passed to the view)//

        public ActionResult Index()
        {
            return View(db.AutoQuotes.ToList());
        }

        // GET: AutoQuote/Create
        public ActionResult Create()

        {




            return View();
        }

        // POST: AutoQuote/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AutoQuoteId,InsureeId,BaseRate,AgeUnder18,AgeBtw19and25,Age26andUp,AutoYearBefore2000,AutoYearAfter2015,IsPorsche,IsCarrera,SubTotalBeforeDuiCalc,DuiRateUp25Percent,SubTotalAfterDuiCalc,SpeedingTickets,SubTotalBeforeCoverageCalc,FullCoverageRateUp50Percent,SubTotalAfterCoverageCalc,MonthlyQuoteRate,YearlyQuoteRate")] AutoQuote autoQuote)
        {
            if (ModelState.IsValid)
            {
                db.AutoQuotes.Add(autoQuote);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(autoQuote);
        }



        [HttpPost]
        public decimal FigureAutoQuote(Insuree insuree)
        {
            //create a new autoQuote instance
            var autoQuote = new AutoQuote(insuree);


            // figure BaseRate for AutoQuote Insurance Rate Calculation
            double baseRate = 50.00;
            autoQuote.BaseRate = Convert.ToDecimal(baseRate);


            //figure Insurance Rate Calc values that are based on Age of Insuree
            var age = DateTime.Now.Year - insuree.DateOfBirth.Year;

            if (insuree.DateOfBirth.Month > DateTime.Now.Month
            || insuree.DateOfBirth.Month == DateTime.Now.Month
            && insuree.DateOfBirth.Day > DateTime.Now.Day)
                age--;

            var insureeAge = Convert.ToInt32(age);
            double under18 = (insureeAge < 18) ? 100.00 : 0.00;
            double btw19and25 = ((insureeAge > 18) && (age <= 25)) ? 50.00 : 0.00;
            double over25 = (insureeAge > 25) ? 25.00 : 0.00;

            autoQuote.AgeUnder18 = Convert.ToDecimal(under18);
            autoQuote.AgeBtw19and25 = Convert.ToDecimal(btw19and25);
            autoQuote.Age26andUp = Convert.ToDecimal(over25);


            //figure Insurance Rate Calc Values that are based on the year of the vehicle
            double autoYearPrior2000 = (insuree.CarYear < 2000) ? 25.00 : 0.00;
            double autoYearAfter2015 = (insuree.CarYear > 2015) ? 25.00 : 0.00;

            autoQuote.AutoYearBefore2000 = Convert.ToDecimal(autoYearPrior2000);
            autoQuote.AutoYearAfter2015 = Convert.ToDecimal(autoYearAfter2015);


            //figure Insurance Rate Calc Values based on if the vehicle is a Porsche Carerra
            double yesIsPorsche = (insuree.CarMake == "Porsche") ? 25.00 : 0.00;
            double yesIsCarrera = (insuree.CarModel == "Carrera") ? 25.00 : 0.00;

            autoQuote.IsPorsche = Convert.ToDecimal(yesIsPorsche);
            autoQuote.IsCarrera = Convert.ToDecimal(yesIsCarrera);


            //Calculate Subtotal to check for accuracy before DUI calculation
            double subtotalBeforeDUI = baseRate + under18 + btw19and25 + over25 +
                                       autoYearPrior2000 + autoYearAfter2015 + yesIsPorsche +
                                       yesIsCarrera;
            autoQuote.SubTotalBeforeDuiCalc = Convert.ToDecimal(subtotalBeforeDUI);


            //figure Insurance Rate Calc based on if the Insuree has a DUI on their record
            int yesDUI = (insuree.DUI == true) ? 1 : 0;
            double duiRate = (yesDUI == 1) ? (subtotalBeforeDUI * .025) : 0.00;

            var DuiRate = Convert.ToDecimal(duiRate);
            autoQuote.DuiRateUp25Percent = DuiRate; // value that will be placed in Quote DUIRateUP25Percent


            //figure Insurance Rate Calc after DUI Rate is computed and added to running sum of rates 

            autoQuote.SubTotalAfterDuiCalc = Convert.ToDecimal(subtotalBeforeDUI + duiRate);


            //figure Insurance Rate Calc for the number of speeding tickets the Insuree has on record
            int speedingTickets = insuree.SpeedingTickets;
            double speedingTicketsRate = speedingTickets * 10.00;

            autoQuote.SpeedingTicketsRate = Convert.ToDecimal(speedingTicketsRate);


            //figure subtotal of all figured rates after speeding tickets calculation(before Coverage Rate calc)
            autoQuote.SubTotalBeforeCoverageCalc = autoQuote.SubTotalAfterDuiCalc + autoQuote.SpeedingTicketsRate;


            //figure Insurance Rate Calc based on whether the Insuree needs full coverage insurance or not
            int coverageType = (insuree.CoverageType == true) ? 1 : 0;
            double coverageTypeRate = (coverageType == 1) ? (Convert.ToDouble(autoQuote.SubTotalBeforeCoverageCalc) * 0.50) : 0.00;// calculating the rate of increase if FullCoverage is true

            autoQuote.SubTotalAfterCoverageCalc = autoQuote.SubTotalBeforeCoverageCalc + (Convert.ToDecimal(coverageTypeRate));


            //figure Insurance Rate per Month
            autoQuote.MonthlyRate = autoQuote.SubTotalAfterCoverageCalc;

            //figure Insurance Rate per Year
            autoQuote.YearlyRate = autoQuote.SubTotalAfterCoverageCalc * 12;


            //insureeVM.QuoteMonthly = insuree.QuoteMonthly;
            //insureeVM.QuoteYearly = insuree.QuoteYearly;
            //autoQuoteVM.InsureeId = autoQuote.InsureeId;
            //autoQuoteVM.BaseRate = autoQuote.BaseRate;
            //autoQuoteVM.AgeUnder18 = autoQuote.AgeUnder18;
            //autoQuoteVM.AgeBtw19and25 = autoQuote.AgeBtw19and25;
            //autoQuoteVM.Age26andUp = autoQuote.Age26andUp;
            //autoQuoteVM.AutoYearBefore2000 = autoQuote.AutoYearBefore2000;
            //autoQuoteVM.AutoYearAfter2015 = autoQuote.AutoYearAfter2015;
            //autoQuoteVM.IsPorsche = autoQuote.IsPorsche;
            //autoQuoteVM.IsCarrera = autoQuote.IsCarrera;
            //autoQuoteVM.SubTotalBeforeDuiCalc = autoQuote.SubTotalBeforeDuiCalc;
            //autoQuoteVM.DuiRateUp25Percent = autoQuote.DuiRateUp25Percent;
            //autoQuoteVM.SubTotalAfterDuiCalc = autoQuote.SubTotalAfterDuiCalc;
            //autoQuoteVM.SpeedingTickets = autoQuote.SpeedingTickets;
            //autoQuoteVM.SubTotalBeforeCoverageCalc = autoQuote.SubTotalBeforeCoverageCalc;
            //autoQuoteVM.FullCoverageRateUp50Percent = autoQuote.FullCoverageRateUp50Percent;
            //autoQuoteVM.SubTotalAfterCoverageCalc = autoQuote.SubTotalAfterCoverageCalc;
            //autoQuoteVM.QuoteMonthly = autoQuote.QuoteMonthly;
            //autoQuoteVM.QuoteYearly = autoQuote.QuoteYearly;
        }
    }
}





























































{
    public class AutoQuoteController : Controller
{
    private readonly InsuranceModelEntities db = new InsuranceModelEntities();

    // GET: AutoQuote
    public ActionResult Index()
    {
        return View(db.AutoQuotes.ToList());
    }

    public ActionResult CalculateAutoQuote(Insuree insuree)
    {


        return View(insuree);


    }

    // GET: AutoQuote/Details/5
    public ActionResult Details(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        AutoQuote autoQuote = db.AutoQuotes.Find(id);
        if (autoQuote == null)
        {
            return HttpNotFound();
        }
        return View(autoQuote);
    }

    // GET: AutoQuote/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: AutoQuote/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "AutoQuoteId,InsureeId,BaseRate,AgeUnder18,AgeBtw19and25,Age26andUp,AutoYearBefore2000,AutoYearAfter2015,IsPorsche,IsCarrera,SubTotalBeforeDuiCalc,DuiRateUp25Percent,SubTotalAfterDuiCalc,SpeedingTickets,SubTotalBeforeCoverageCalc,FullCoverageRateUp50Percent,SubTotalAfterCoverageCalc,MonthlyQuoteRate,YearlyQuoteRate")] AutoQuote autoQuote)
    {
        if (ModelState.IsValid)
        {
            db.AutoQuotes.Add(autoQuote);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(autoQuote);
    }

    // GET: AutoQuote/Edit/5
    public ActionResult Edit(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        AutoQuote autoQuote = db.AutoQuotes.Find(id);
        if (autoQuote == null)
        {
            return HttpNotFound();
        }
        return View(autoQuote);
    }

    // POST: AutoQuote/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "AutoQuoteId,InsureeId,BaseRate,AgeUnder18,AgeBtw19and25,Age26andUp,AutoYearBefore2000,AutoYearAfter2015,IsPorsche,IsCarrera,SubTotalBeforeDuiCalc,DuiRateUp25Percent,SubTotalAfterDuiCalc,SpeedingTickets,SubTotalBeforeCoverageCalc,FullCoverageRateUp50Percent,SubTotalAfterCoverageCalc,MonthlyQuoteRate,YearlyQuoteRate")] AutoQuote autoQuote)
    {
        if (ModelState.IsValid)
        {
            db.Entry(autoQuote).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(autoQuote);
    }

    // GET: AutoQuote/Delete/5
    public ActionResult Delete(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        AutoQuote autoQuote = db.AutoQuotes.Find(id);
        if (autoQuote == null)
        {
            return HttpNotFound();
        }
        return View(autoQuote);
    }

    // POST: AutoQuote/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        AutoQuote autoQuote = db.AutoQuotes.Find(id);
        db.AutoQuotes.Remove(autoQuote);
        db.SaveChanges();
        return RedirectToAction("Index");
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            db.Dispose();
        }
        base.Dispose(disposing);
    }
}
}
