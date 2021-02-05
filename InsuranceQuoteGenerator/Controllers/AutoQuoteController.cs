using InsuranceQuoteGenerator.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace InsuranceQuoteGenerator.Controllers
{
    public class AutoQuoteController : Controller
    {
        private readonly InsuranceQuotesEntities db = new InsuranceQuotesEntities();

        public InsuranceQuotesEntities Db => db;

        // GET: AutoQuote
        public ActionResult Index()
        {
            return View(db.AutoQuotes.ToList());
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
