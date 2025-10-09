using BusinessObject;
using BusinessObject.Dtos;
using BusinessObject.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class BatteryTypeService(ApplicationDbContext context, IHttpContextAccessor accessor)
        : IBatteryTypeService
    {
        public async Task<IEnumerable<BatteryTypeResponse>> GetAllAsync()
        {
            return await context.BatteryTypes
                .Select(t => new BatteryTypeResponse
                {
                    BatteryTypeId = t.BatteryTypeId,
                    BatteryTypeName = t.BatteryTypeName
                })
                .ToListAsync();
        }

        public async Task<BatteryTypeResponse?> GetByIdAsync(string id)
        {
            var t = await context.BatteryTypes.FindAsync(id);
            if (t == null) return null;

            return new BatteryTypeResponse
            {
                BatteryTypeId = t.BatteryTypeId,
                BatteryTypeName = t.BatteryTypeName
            };
        }

        public async Task AddAsync(BatteryTypeRequest request)
        {
            var entity = new BatteryType
            {
                BatteryTypeId = Guid.NewGuid().ToString(),
                BatteryTypeName = request.BatteryTypeName
            };

            context.BatteryTypes.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BatteryTypeRequest request)
        {
            var t = await context.BatteryTypes.FindAsync(request.BatteryTypeId);
            if (t == null) throw new Exception("BatteryType not found.");

            t.BatteryTypeName = request.BatteryTypeName;
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var t = await context.BatteryTypes.FindAsync(id);
            if (t == null) throw new Exception("BatteryType not found.");

            context.BatteryTypes.Remove(t);
            await context.SaveChangesAsync();
        }
    }
}
