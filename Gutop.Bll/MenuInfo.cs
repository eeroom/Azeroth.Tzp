﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gutop.Bll
{
    public class MenuInfo:Bll
    {
        public MenuInfo(Gutop.Model.Entity.DbContext dbcontext):base(dbcontext)
        {

        }
        public int Init()
        {
           var lstMenu= System.Linq.Enumerable.Range(0, 10)
                .Select(x => new Gutop.Model.Entity.MenuInfo()
                {
                    Id = Guid.NewGuid(),
                    Name = "menu" + x.ToString(),
                    Pid = Guid.Empty,
                    Url = "url" + x.ToString()
                }).ToList();
            this.dbcontext.Set<Gutop.Model.Entity.MenuInfo>().AddRange(lstMenu);
            var rt = this.dbcontext.SaveChanges();
            return rt;
        }
    }
}
