using Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BL
{
    public interface RealTimeNotifcationService
    {
        List<TbRealTimeNotifcation> getAll();
        bool Add(TbRealTimeNotifcation client);
        bool Edit(TbRealTimeNotifcation client);
        bool Delete(TbRealTimeNotifcation client);


    }
    public class ClsRealTimeNotifcation : RealTimeNotifcationService
    {
        Al3QaratContext ctx;

        public ClsRealTimeNotifcation(Al3QaratContext context)
        {
            ctx = context;
        }
        public List<TbRealTimeNotifcation> getAll()
        {
            //_4ZsoftwareCompanyTestTaskContext o_4ZsoftwareCompanyTestTaskContext = new _4ZsoftwareCompanyTestTaskContext();
            List<TbRealTimeNotifcation> lstAdvices = ctx.TbRealTimeNotifcations.ToList();

            return lstAdvices;
        }

        public bool Add(TbRealTimeNotifcation item)
        {
            try
            {
                //_4ZsoftwareCompanyTestTaskContext o_4ZsoftwareCompanyTestTaskContext = new _4ZsoftwareCompanyTestTaskContext();
                item.RealTimeNotifcationId = Guid.NewGuid();
                ctx.TbRealTimeNotifcations.Add(item);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }
        public bool Edit(TbRealTimeNotifcation item)
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

        public bool Delete(TbRealTimeNotifcation item)
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
