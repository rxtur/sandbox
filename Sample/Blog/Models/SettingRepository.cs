using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace BlogiFire.Models
{
    public class SettingRepository : ISettingsRepository
    {
        BlogContext db;
        public SettingRepository()
        {
            db = new BlogContext();
        }
        public async Task Add(Setting item)
        {
            db.Settings.Add(item);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var item = await db.Settings.FirstOrDefaultAsync(i => i.Id == id);
            db.Settings.Remove(item);
            await db.SaveChangesAsync();
        }

        public async Task<List<Setting>> Find(Expression<Func<Setting, bool>> predicate)
        {
            return await db.Settings.Where(predicate).ToListAsync();
        }

        public async Task Update(Setting item)
        {
            var dbItem = db.Settings.SingleOrDefault(i => i.Id == item.Id);
            dbItem.SettingValue = item.SettingValue;
            await db.SaveChangesAsync();
        }
    }
}