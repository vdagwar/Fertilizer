﻿using AngularJSAuthentication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace AngularJSAuthentication.API.ControllerV1
{
    [RoutePrefix("api/DailyEssential")]
    public class DailyEssentialController : ApiController
    {
        AuthContext context = new AuthContext();
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        [Route("")]
        [HttpGet]
        public HttpResponseMessage get(string mob)
        {
            try
            {
                var dailyessentials = context.DailyEssentialDb.Where(x => x.CustMobile == mob && x.Deleted == false).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, dailyessentials);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Route("")]
        public HttpResponseMessage get(string mob, string type, string datefrom, string dateto)
        {
            try
            {
                List<myDailydata> mydata = new List<myDailydata>();
                if (datefrom == null)
                {
                    var d = indianTime;
                    var firstDay = new DateTime(d.Year, d.Month, 1);
                    var lastDay = firstDay.AddMonths(1).AddDays(-1);
                    var allDates = new List<DateTime>();
                    for (DateTime date = firstDay; date <= lastDay; date = date.AddDays(1)) allDates.Add(date);

                    foreach (var OD in allDates)
                    {
                        TimeSpan tsmin = new TimeSpan(00, 00, 0);
                        TimeSpan tsmax = new TimeSpan(23, 59, 59);
                        DateTime sd = OD.Date + tsmin;
                        DateTime ed = OD.Date + tsmax;
                        var myDE = context.DailyEssentialDb.Where(x => x.CustMobile == mob && x.EndDate >= ed).ToList();
                        foreach (var item in myDE)
                        {
                            var day = OD.DayOfWeek;
                            var MK = new myDailydata();

                            if (Convert.ToString(day) == "Monday")
                            {
                                if (item.Monday == true)
                                {
                                    var editDE = context.DailyItemCancelDb.Where(x => x.CustMobile == mob && x.ItemId == item.ItemId && x.EditDate >= sd && x.EditDate <= ed).SingleOrDefault();
                                    if (editDE != null)
                                    {
                                        MK.Quantity = editDE.Qty;
                                        MK.Amount = item.UnitPrice * editDE.Qty;
                                    }
                                    else {
                                        MK.Quantity = item.DailyItemQty1;
                                        MK.Amount = item.UnitPrice * item.DailyItemQty1;
                                    }
                                    MK.itemname = item.itemname;
                                    MK.Date = OD;
                                    mydata.Add(MK);
                                }
                            }
                            else if (Convert.ToString(day) == "Tuesday")
                            {
                                if (item.Tuesday == true)
                                {
                                    var editDE = context.DailyItemCancelDb.Where(x => x.CustMobile == mob && x.ItemId == item.ItemId && x.EditDate >= sd && x.EditDate <= ed).SingleOrDefault();
                                    if (editDE != null)
                                    {
                                        MK.Quantity = editDE.Qty;
                                        MK.Amount = item.UnitPrice * editDE.Qty;
                                    }
                                    else {
                                        MK.Quantity = item.DailyItemQty2;
                                        MK.Amount = item.UnitPrice * item.DailyItemQty2;
                                    }
                                    MK.itemname = item.itemname;
                                    MK.Date = OD;
                                    mydata.Add(MK);
                                }
                            }
                            else if (Convert.ToString(day) == "Wednesday")
                            {
                                if (item.Wednesday == true)
                                {
                                    var editDE = context.DailyItemCancelDb.Where(x => x.CustMobile == mob && x.ItemId == item.ItemId && x.EditDate >= sd && x.EditDate <= ed).SingleOrDefault();
                                    if (editDE != null)
                                    {
                                        MK.Quantity = editDE.Qty;
                                        MK.Amount = item.UnitPrice * editDE.Qty;
                                    }
                                    else {
                                        MK.Quantity = item.DailyItemQty3;
                                        MK.Amount = item.UnitPrice * item.DailyItemQty3;
                                    }
                                    MK.itemname = item.itemname;
                                    MK.Date = OD;
                                    mydata.Add(MK);
                                }
                            }
                            else if (Convert.ToString(day) == "Thursday")
                            {
                                if (item.Thursday == true)
                                {
                                    var editDE = context.DailyItemCancelDb.Where(x => x.CustMobile == mob && x.ItemId == item.ItemId && x.EditDate >= sd && x.EditDate <= ed).SingleOrDefault();
                                    if (editDE != null)
                                    {
                                        MK.Quantity = editDE.Qty;
                                        MK.Amount = item.UnitPrice * editDE.Qty;
                                    }
                                    else {
                                        MK.Quantity = item.DailyItemQty4;
                                        MK.Amount = item.UnitPrice * item.DailyItemQty4;
                                    }
                                    MK.itemname = item.itemname;
                                    MK.Date = OD;
                                    mydata.Add(MK);
                                }
                            }
                            else if (Convert.ToString(day) == "Friday")
                            {
                                if (item.Friday == true)
                                {
                                    var editDE = context.DailyItemCancelDb.Where(x => x.CustMobile == mob && x.ItemId == item.ItemId && x.EditDate >= sd && x.EditDate <= ed).SingleOrDefault();
                                    if (editDE != null)
                                    {
                                        MK.Quantity = editDE.Qty;
                                        MK.Amount = item.UnitPrice * editDE.Qty;
                                    }
                                    else {
                                        MK.Quantity = item.DailyItemQty5;
                                        MK.Amount = item.UnitPrice * item.DailyItemQty5;
                                    }
                                    MK.itemname = item.itemname;
                                    MK.Date = OD;
                                    mydata.Add(MK);
                                }
                            }
                            else if (Convert.ToString(day) == "Saturday")
                            {
                                if (item.Saturday == true)
                                {
                                    var editDE = context.DailyItemCancelDb.Where(x => x.CustMobile == mob && x.ItemId == item.ItemId && x.EditDate >= sd && x.EditDate <= ed).SingleOrDefault();
                                    if (editDE != null)
                                    {
                                        MK.Quantity = editDE.Qty;
                                        MK.Amount = item.UnitPrice * editDE.Qty;
                                    }
                                    else {
                                        MK.Quantity = item.DailyItemQty5;
                                        MK.Amount = item.UnitPrice * item.DailyItemQty5;
                                    }
                                    MK.itemname = item.itemname;
                                    MK.Date = OD;
                                    mydata.Add(MK);
                                }
                            }
                            else if (Convert.ToString(day) == "Sunday")
                            {
                                if (item.Sunday == true)
                                {
                                    var editDE = context.DailyItemCancelDb.Where(x => x.CustMobile == mob && x.ItemId == item.ItemId && x.EditDate >= sd && x.EditDate <= ed).SingleOrDefault();
                                    if (editDE != null)
                                    {
                                        MK.Quantity = editDE.Qty;
                                        MK.Amount = item.UnitPrice * editDE.Qty;
                                    }
                                    else {
                                        MK.Quantity = item.DailyItemQty5;
                                        MK.Amount = item.UnitPrice * item.DailyItemQty5;
                                    }
                                    MK.itemname = item.itemname;
                                    MK.Date = OD;
                                    mydata.Add(MK);
                                }
                            }
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, mydata);


                }
                else {

                    var firstDay = Convert.ToDateTime(datefrom.Split(new string[] { "GMT" }, 0)[0]);
                    var lastDay = Convert.ToDateTime(dateto.Split(new string[] { "GMT" }, 0)[0]);
                    var allDates = new List<DateTime>();
                    for (DateTime date = firstDay; date <= lastDay; date = date.AddDays(1)) allDates.Add(date);

                    foreach (var OD in allDates)
                    {
                        TimeSpan tsmin = new TimeSpan(00, 00, 0);
                        TimeSpan tsmax = new TimeSpan(23, 59, 59);
                        DateTime sd = OD.Date + tsmin;
                        DateTime ed = OD.Date + tsmax;
                        var myDE = context.DailyEssentialDb.Where(x => x.CustMobile == mob && x.EndDate >= ed).ToList();
                        foreach (var item in myDE)
                        {
                            var day = OD.DayOfWeek;
                            var MK = new myDailydata();

                            if (Convert.ToString(day) == "Monday")
                            {
                                if (item.Monday == true)
                                {
                                    var editDE = context.DailyItemCancelDb.Where(x => x.CustMobile == mob && x.ItemId == item.ItemId && x.EditDate >= sd && x.EditDate <= ed).SingleOrDefault();
                                    if (editDE != null)
                                    {
                                        MK.Quantity = editDE.Qty;
                                        MK.Amount = item.UnitPrice * editDE.Qty;
                                    }
                                    else {
                                        MK.Quantity = item.DailyItemQty1;
                                        MK.Amount = item.UnitPrice * item.DailyItemQty1;
                                    }
                                    MK.itemname = item.itemname;
                                    MK.Date = OD;
                                    mydata.Add(MK);
                                }
                            }
                            else if (Convert.ToString(day) == "Tuesday")
                            {
                                if (item.Tuesday == true)
                                {
                                    var editDE = context.DailyItemCancelDb.Where(x => x.CustMobile == mob && x.ItemId == item.ItemId && x.EditDate >= sd && x.EditDate <= ed).SingleOrDefault();
                                    if (editDE != null)
                                    {
                                        MK.Quantity = editDE.Qty;
                                        MK.Amount = item.UnitPrice * editDE.Qty;
                                    }
                                    else {
                                        MK.Quantity = item.DailyItemQty2;
                                        MK.Amount = item.UnitPrice * item.DailyItemQty2;
                                    }
                                    MK.itemname = item.itemname;
                                    MK.Date = OD;
                                    mydata.Add(MK);
                                }
                            }
                            else if (Convert.ToString(day) == "Wednesday")
                            {
                                if (item.Wednesday == true)
                                {
                                    var editDE = context.DailyItemCancelDb.Where(x => x.CustMobile == mob && x.ItemId == item.ItemId && x.EditDate >= sd && x.EditDate <= ed).SingleOrDefault();
                                    if (editDE != null)
                                    {
                                        MK.Quantity = editDE.Qty;
                                        MK.Amount = item.UnitPrice * editDE.Qty;
                                    }
                                    else {
                                        MK.Quantity = item.DailyItemQty3;
                                        MK.Amount = item.UnitPrice * item.DailyItemQty3;
                                    }
                                    MK.itemname = item.itemname;
                                    MK.Date = OD;
                                    mydata.Add(MK);
                                }
                            }
                            else if (Convert.ToString(day) == "Thursday")
                            {
                                if (item.Thursday == true)
                                {
                                    var editDE = context.DailyItemCancelDb.Where(x => x.CustMobile == mob && x.ItemId == item.ItemId && x.EditDate >= sd && x.EditDate <= ed).SingleOrDefault();
                                    if (editDE != null)
                                    {
                                        MK.Quantity = editDE.Qty;
                                        MK.Amount = item.UnitPrice * editDE.Qty;
                                    }
                                    else {
                                        MK.Quantity = item.DailyItemQty4;
                                        MK.Amount = item.UnitPrice * item.DailyItemQty4;
                                    }
                                    MK.itemname = item.itemname;
                                    MK.Date = OD;
                                    mydata.Add(MK);
                                }
                            }
                            else if (Convert.ToString(day) == "Friday")
                            {
                                if (item.Friday == true)
                                {
                                    var editDE = context.DailyItemCancelDb.Where(x => x.CustMobile == mob && x.ItemId == item.ItemId && x.EditDate >= sd && x.EditDate <= ed).SingleOrDefault();
                                    if (editDE != null)
                                    {
                                        MK.Quantity = editDE.Qty;
                                        MK.Amount = item.UnitPrice * editDE.Qty;
                                    }
                                    else {
                                        MK.Quantity = item.DailyItemQty5;
                                        MK.Amount = item.UnitPrice * item.DailyItemQty5;
                                    }
                                    MK.itemname = item.itemname;
                                    MK.Date = OD;
                                    mydata.Add(MK);
                                }
                            }
                            else if (Convert.ToString(day) == "Saturday")
                            {
                                if (item.Saturday == true)
                                {
                                    var editDE = context.DailyItemCancelDb.Where(x => x.CustMobile == mob && x.ItemId == item.ItemId && x.EditDate >= sd && x.EditDate <= ed).SingleOrDefault();
                                    if (editDE != null)
                                    {
                                        MK.Quantity = editDE.Qty;
                                        MK.Amount = item.UnitPrice * editDE.Qty;
                                    }
                                    else {
                                        MK.Quantity = item.DailyItemQty5;
                                        MK.Amount = item.UnitPrice * item.DailyItemQty5;
                                    }
                                    MK.itemname = item.itemname;
                                    MK.Date = OD;
                                    mydata.Add(MK);
                                }
                            }
                            else if (Convert.ToString(day) == "Sunday")
                            {
                                if (item.Sunday == true)
                                {
                                    var editDE = context.DailyItemCancelDb.Where(x => x.CustMobile == mob && x.ItemId == item.ItemId && x.EditDate >= sd && x.EditDate <= ed).SingleOrDefault();
                                    if (editDE != null)
                                    {
                                        MK.Quantity = editDE.Qty;
                                        MK.Amount = item.UnitPrice * editDE.Qty;
                                    }
                                    else {
                                        MK.Quantity = item.DailyItemQty5;
                                        MK.Amount = item.UnitPrice * item.DailyItemQty5;
                                    }
                                    MK.itemname = item.itemname;
                                    MK.Date = OD;
                                    mydata.Add(MK);
                                }
                            }
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, mydata);


                }
               // return null;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, false);
            }

        }

        [Route("")]
        [AcceptVerbs("Post")]
        public HttpResponseMessage Post(DailyEssential item)
        {
            try
            {
                if (item != null)
                {
                    var d = context.AddDailyItem(item);
                    if (d == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, false);
                    }
                    else {
                        return Request.CreateResponse(HttpStatusCode.OK, d);
                    }
                }
                else {
                    return Request.CreateResponse(HttpStatusCode.OK, false);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        [Route("")]
        [AcceptVerbs("Put")]
        public HttpResponseMessage Put(DailyItemEdit item, string datefrom, string dateto)
        {
            try
            {
                var firstDay = Convert.ToDateTime(datefrom.Split(new string[] { "GMT" }, 0)[0]);
                var lastDay = Convert.ToDateTime(dateto.Split(new string[] { "GMT" }, 0)[0]);
                var allDates = new List<DateTime>();
                for (DateTime date = firstDay; date <= lastDay; date = date.AddDays(1)) allDates.Add(date);
                DailyEssential dailyitems = context.DailyEssentialDb.Where(x => x.CustMobile == item.CustMobile && x.ItemId == item.ItemId && x.EndDate > lastDay).SingleOrDefault();
                var d = indianTime.Date;
                
                if (dailyitems != null && d.AddDays(1) < dailyitems.EndDate.Date)
                {
                    foreach (var Edate in allDates)
                    {
                        TimeSpan tsmin = new TimeSpan(00, 00, 0);
                        TimeSpan tsmax = new TimeSpan(23, 59, 59);
                        DateTime sd = Edate.Date + tsmin;
                        DateTime ed = Edate.Date + tsmax;
                        var day = Edate.DayOfWeek;
                        DailyItemEdit DE = context.DailyItemCancelDb.Where(x => x.CustMobile == item.CustMobile && x.ItemId == item.ItemId && x.EditDate < ed && x.EditDate > sd).SingleOrDefault();
                        if (DE == null)
                        {
                            if (Convert.ToString(day) == "Monday")
                            {
                                if (dailyitems.Monday == true)
                                {
                                    item.CreatedDate = indianTime;
                                    item.UpdatedDate = indianTime;
                                    item.EditDate = Edate;
                                    context.DailyItemCancelDb.Add(item);
                                    int id = context.SaveChanges();
                                }
                            }
                            else if (Convert.ToString(day) == "Tuesday")
                            {
                                if (dailyitems.Tuesday == true)
                                {
                                    item.CreatedDate = indianTime;
                                    item.UpdatedDate = indianTime;
                                    item.EditDate = Edate;
                                    context.DailyItemCancelDb.Add(item);
                                    int id = context.SaveChanges();
                                }
                            }
                            else if (Convert.ToString(day) == "Wednesday")
                            {
                                if (dailyitems.Wednesday == true)
                                {
                                    item.CreatedDate = indianTime;
                                    item.UpdatedDate = indianTime;
                                    item.EditDate = Edate;
                                    context.DailyItemCancelDb.Add(item);
                                    int id = context.SaveChanges();
                                }
                            }
                            else if (Convert.ToString(day) == "Thursday")
                            {
                                if (dailyitems.Thursday == true)
                                {
                                    item.CreatedDate = indianTime;
                                    item.UpdatedDate = indianTime;
                                    item.EditDate = Edate;
                                    context.DailyItemCancelDb.Add(item);
                                    int id = context.SaveChanges();
                                }
                            }
                            else if (Convert.ToString(day) == "Friday")
                            {
                                if (dailyitems.Friday == true)
                                {
                                    item.CreatedDate = indianTime;
                                    item.UpdatedDate = indianTime;
                                    item.EditDate = Edate;
                                    context.DailyItemCancelDb.Add(item);
                                    int id = context.SaveChanges();
                                }
                            }
                            else if (Convert.ToString(day) == "Saturday")
                            {
                                if (dailyitems.Saturday == true)
                                {
                                    item.CreatedDate = indianTime;
                                    item.UpdatedDate = indianTime;
                                    item.EditDate = Edate;
                                    context.DailyItemCancelDb.Add(item);
                                    int id = context.SaveChanges();
                                }
                            }
                            else if (Convert.ToString(day) == "Sunday")
                            {
                                if (dailyitems.Sunday == true)
                                {
                                    item.CreatedDate = indianTime;
                                    item.UpdatedDate = indianTime;
                                    item.EditDate = Edate;
                                    context.DailyItemCancelDb.Add(item);
                                    int id = context.SaveChanges();
                                }
                            }
                        }
                        else {
                            ///////////////// else update
                            if (Convert.ToString(day) == "Monday")
                            {
                                if (dailyitems.Monday == true)
                                {
                                    DE.UpdatedDate = indianTime;
                                    DE.Qty = item.Qty;
                                    context.DailyItemCancelDb.Attach(DE);
                                    context.Entry(DE).State =EntityState.Modified;
                                    context.SaveChanges();
                                }
                            }
                            else if (Convert.ToString(day) == "Tuesday")
                            {
                                if (dailyitems.Tuesday == true)
                                {
                                    DE.UpdatedDate = indianTime;
                                    DE.Qty = item.Qty;
                                    context.DailyItemCancelDb.Attach(DE);
                                    context.Entry(DE).State = EntityState.Modified;
                                    context.SaveChanges();
                                }
                            }
                            else if (Convert.ToString(day) == "Wednesday")
                            {
                                if (dailyitems.Wednesday == true)
                                {
                                    DE.UpdatedDate = indianTime;
                                    DE.Qty = item.Qty;
                                    context.DailyItemCancelDb.Attach(DE);
                                    context.Entry(DE).State = EntityState.Modified;
                                    context.SaveChanges();
                                }
                            }
                            else if (Convert.ToString(day) == "Thursday")
                            {
                                if (dailyitems.Thursday == true)
                                {
                                    DE.UpdatedDate = indianTime;
                                    DE.Qty = item.Qty;
                                    context.DailyItemCancelDb.Attach(DE);
                                    context.Entry(DE).State = EntityState.Modified;
                                    context.SaveChanges();
                                }
                            }
                            else if (Convert.ToString(day) == "Friday")
                            {
                                if (dailyitems.Friday == true)
                                {
                                    DE.UpdatedDate = indianTime;
                                    DE.Qty = item.Qty;
                                    context.DailyItemCancelDb.Attach(DE);
                                    context.Entry(DE).State = EntityState.Modified;
                                    context.SaveChanges();
                                }
                            }
                            else if (Convert.ToString(day) == "Saturday")
                            {
                                if (dailyitems.Saturday == true)
                                {
                                    DE.UpdatedDate = indianTime;
                                    DE.Qty = item.Qty;
                                    context.DailyItemCancelDb.Attach(DE);
                                    context.Entry(DE).State = EntityState.Modified;
                                    context.SaveChanges();
                                }
                            }
                            else if (Convert.ToString(day) == "Sunday")
                            {
                                if (dailyitems.Sunday == true)
                                {
                                    DE.UpdatedDate = indianTime;
                                    DE.Qty = item.Qty;
                                    context.DailyItemCancelDb.Attach(DE);
                                    context.Entry(DE).State = EntityState.Modified;
                                    context.SaveChanges();
                                }
                            }

                        }
                        
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, item);
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, false);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, false);
            }

        }

        //[Route("")]
        //[AcceptVerbs("Put")]
        //public HttpResponseMessage Put(DailyItemEdit item ,string datefrom, string dateto)
        //{
        //    try
        //    {
        //        if (item != null)
        //        {

        //            var d = context.EditItem(item);
        //            if (d == null)
        //            {
        //                return Request.CreateResponse(HttpStatusCode.OK, false);
        //            }
        //            else {
        //                return Request.CreateResponse(HttpStatusCode.OK, d);
        //            }
        //        }
        //        else {
        //            return Request.CreateResponse(HttpStatusCode.OK, false);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }

        //}

        public class myDailydata
        {

            public string itemname { get; set; }
            public int Quantity { get; set; }
            public DateTime Date { get; set; }
            public double Amount { get; set; }

        }
    }
}
