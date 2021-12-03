using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoesStore.Data;

namespace ShoesStore.Models
{
    public class LoadUpdateModel
    {
        private readonly ApplicationDbContext _db;

        public LoadUpdateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public LoadUpdateModel()
        {
            foreach (var publisher in _db.Publishers)
            {
                Publishers.Add(new SelectListItem() {Value = publisher.Id.ToString(), Text = publisher.Name});           
            }

            foreach (var category in _db.Categories)
            {
                Categories.Add(new SelectListItem() {Value = category.Id.ToString(), Text = category.Name, });
            }
        }
        public Shoe Shoe { get; set; }

        public List<SelectListItem> Publishers { get; set; } = new List<SelectListItem>();

        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
    }
}
