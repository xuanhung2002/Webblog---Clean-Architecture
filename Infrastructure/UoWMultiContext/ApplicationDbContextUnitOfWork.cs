﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBlog.Application.Abstraction;

namespace WebBlog.Infrastructure.UoWMultiContext
{
    public class ApplicationDbContextUnitOfWork : EFUnitOfWorkDbContext<ApplicationDbContext>, IApplicationDbContextUnitOfWork  
    {
        public ApplicationDbContextUnitOfWork(ApplicationDbContext context) : base(context)
        {
        }
    }
}