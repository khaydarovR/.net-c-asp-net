﻿using SCP.DAL;
using SCP.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCP.Application.Services
{
    public class RLogService
    {
        private readonly AppDbContext db;

        public RLogService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task Push(string text, Guid recordId)
        {
            var model = new ActivityLog
            {
                At = DateTime.UtcNow,
                RecordId = recordId,
                LogText = text,
            };

            db.Add(model);

            await db.SaveChangesAsync();
        }

        public async Task Push(string text, string recordId)
        {
            var model = new ActivityLog
            {
                At = DateTime.UtcNow,
                RecordId = Guid.Parse(recordId),
                LogText = text,
            };

            db.Add(model);

            await db.SaveChangesAsync();
        }
    }
}