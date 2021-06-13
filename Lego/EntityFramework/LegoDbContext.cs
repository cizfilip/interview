using Lego.Entities;
using System;
using System.Collections.Generic;

namespace Lego.EntityFramework
{
    // EF Db context class in reality
    public class LegoDbContext : IDisposable
    {
        public IEnumerable<EmailTemplate> EmailTemplates { get; set; }

        public void Dispose()
        {
            // Noop in this fake
        }
        
    }
}
