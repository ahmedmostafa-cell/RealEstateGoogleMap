using Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BL
{
    public interface SalesRepPointService
    {
        List<TbSalesRepPoint> getAll();
        bool Add(TbSalesRepPoint client);
        bool Edit(TbSalesRepPoint client);
        bool Delete(TbSalesRepPoint client);


    }
    public class ClsSalesRepPoint : SalesRepPointService
    {
        Al3QaratContext ctx;

        public ClsSalesRepPoint(Al3QaratContext context)
        {
            ctx = context;
        }
        public List<TbSalesRepPoint> getAll()
        {
            //_4ZsoftwareCompanyTestTaskContext o_4ZsoftwareCompanyTestTaskContext = new _4ZsoftwareCompanyTestTaskContext();
            List<TbSalesRepPoint> lstSalesRepPoints = ctx.TbSalesRepPoints.ToList();

            return lstSalesRepPoints;
        }

        public bool Add(TbSalesRepPoint item)
        {
            try
            {
                //_4ZsoftwareCompanyTestTaskContext o_4ZsoftwareCompanyTestTaskContext = new _4ZsoftwareCompanyTestTaskContext();
                item.CurrentState = 1;
                ctx.TbSalesRepPoints.Add(item);
                ctx.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }
        public bool Edit(TbSalesRepPoint item)
        {
            try
            {
                //_4ZsoftwareCompanyTestTaskContext o_4ZsoftwareCompanyTestTaskContext = new _4ZsoftwareCompanyTestTaskContext();

                ctx.Entry(item).State = EntityState.Modified;
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }

        public bool Delete(TbSalesRepPoint item)
        {
            try
            {
                //_4ZsoftwareCompanyTestTaskContext o_4ZsoftwareCompanyTestTaskContext = new _4ZsoftwareCompanyTestTaskContext();

                ctx.Entry(item).State = EntityState.Deleted;
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }
    }
}
