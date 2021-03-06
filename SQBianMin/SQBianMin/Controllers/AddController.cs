﻿using SQBianMin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQBianMin.Controllers
{
    public class AddController : BaseController
    {
        // GET: Add
        public ActionResult Index(int id=0)
        {
            BianMinModel model = new BianMinModel();
            if (id > 0)
            {
                model = _bianMinService.GetBianMinModel(id);
            }
            return View(model);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Index(BianMinModel m)
        {

            if (m.Id > 0)//修改
            {
                if (_bianMinService.UpdateBianMinModel(m))
                {
                    return Json(new { code = 0, msg = "修改成功" });
                }
                else
                {
                    return Json(new { code = 1, msg = "修改失败" });
                }

            }
            else//新增
            {
                if (_bianMinService.AddBianMinModel(m))
                {
                    return Json(new { code = 0, msg = "添加成功" });
                }
                else
                {
                    return Json(new { code = 1, msg = "添加失败" });
                }
            }

        }
    }
}