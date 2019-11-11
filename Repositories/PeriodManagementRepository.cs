using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Backend.Model;

namespace Backend.Repositories
{
    public class PeriodManagementRepository
    {
        BinusIAContext db = new BinusIAContext();

        public List<PeriodManagement> GetPeriodManagements()
        {
            var q = db.PeriodManagement.ToList();
            return q;
        }

        public List<PeriodManagement> GetPeriodManagements(int year)
        {
            var q = db.PeriodManagement
                    .Where(x => x.Year == year)
                    .ToList();
            return q;
        }

        public PeriodManagement GetPeriodManagement(Guid id)
        {
            var q = db.PeriodManagement
                    .Where(x => x.PeriodId == id)
                    .FirstOrDefault();
            return q;
        }

        public Tuple<Boolean, String, PeriodManagement> AddPeriodManagement(PeriodManagement periodManagement)
        {
            try
            {
                db.Add(periodManagement);
                db.SaveChanges();

                return Tuple.Create(true, "Data Inserted Successfully", periodManagement);
            }
            catch (Exception ex)
            {
                return Tuple.Create(false, ex.Message, periodManagement);
            }
        }

        public Tuple<Boolean, String, PeriodManagement> UpdatePeriodManagement(PeriodManagement pm)
        {
            try
            {
                var q = db.PeriodManagement.Where(x => x.PeriodId == pm.PeriodId).FirstOrDefault();
                q.Year = pm.Year;
                q.Period = pm.Period;
                q.Description = pm.Description;
                q.StartDate = pm.StartDate;
                q.EndDate = pm.EndDate;
                q.Date = pm.Date;
                q.Active = pm.Active;

                db.SaveChanges();

                return Tuple.Create(true, "Data updated successfully", q);
            }
            catch (Exception ex)
            {
                return Tuple.Create(false, ex.Message, pm);
            }
        }

        public PeriodManagement UpdateActive(Guid periodid, bool status)
        {
            var q = db.PeriodManagement.Where(x => x.PeriodId == periodid).FirstOrDefault();
            q.Active = status;
            db.SaveChanges();
            return q;
        }

        public Tuple<Boolean, String, PeriodManagement> EditTime(Guid id, DateTime start, DateTime end)
        {
            try
            {
                var q = db.PeriodManagement.Where(x => x.PeriodId == id)
                    .FirstOrDefault();   
                q.StartDate = start;
                q.EndDate = end;
                db.SaveChanges();

                return Tuple.Create(true, "Period Management Updated successfully", q);
            }
            catch (Exception ex)
            {
                return Tuple.Create(false, ex.Message, new PeriodManagement());
            }
        }

          public Tuple<Boolean, String> DeletePeriodManagement(Guid id){
            try{
                var q = db.PeriodManagement.Where(x=> x.PeriodId == id).FirstOrDefault();
                db.Remove(q);
                db.SaveChanges();
                
                return Tuple.Create(true, "Data Deleted Successfully");
            }
            catch(Exception ex){
                return Tuple.Create(false, ex.Message);
            }
        }

    }
}
