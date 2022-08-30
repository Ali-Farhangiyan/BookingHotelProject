﻿using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ContextInterfaces
{
    public interface IIdentityDatabaseContext
    {
        DbSet<User> Users { get; set; }
    }
}