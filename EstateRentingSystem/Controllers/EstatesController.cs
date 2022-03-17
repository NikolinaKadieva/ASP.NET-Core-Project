﻿namespace EstateRentingSystem.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using EstateRentingSystem.Data;
    using EstateRentingSystem.Data.Models;
    using EstateRentingSystem.Infrastructure;
    using EstateRentingSystem.Models.Estates;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    public class EstatesController : Controller
    {
        private readonly EstateRentingDbContext data;

        public EstatesController(EstateRentingDbContext data)
            => this.data = data;

        public IActionResult All([FromQuery]AllEstatesQueryModel query)
        {
            var estatesQuery = this.data.Estates.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Type))
            {
                estatesQuery = estatesQuery.Where(e => e.Type == query.Type);
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                estatesQuery = estatesQuery.Where(e =>
                e.Type.ToLower().Contains(query.SearchTerm.ToLower()) || 
                e.Description.ToLower().Contains(query.SearchTerm.ToLower()) ||
                e.TypeOfConstruction.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            estatesQuery = query.Sorting switch
            {
                
                EstateSorting.Year => estatesQuery.OrderByDescending(e => e.YearOfConstruction),
                EstateSorting.Type => estatesQuery.OrderBy(e => e.Type),
                EstateSorting.DateCreated or _ => estatesQuery.OrderByDescending(e => e.Id)
            };

            var totalEstates = estatesQuery.Count();

            var estates = estatesQuery
                .Skip((query.CurrentPage - 1) * AllEstatesQueryModel.EstatesPerPage)
                .Take(AllEstatesQueryModel.EstatesPerPage)
                .Select(e => new EstateListingViewModel
                {
                    Id = e.Id,
                    Type = e.Type,
                    TypeOfConstruction = e.TypeOfConstruction,
                    YearOfConstruction = e.YearOfConstruction,
                    Squaring = e.Squaring,
                    ImageUrl = e.ImageUrl,
                    Category = e.Category.Name
                })
                .ToList();

            var estateTypes = this.data
                .Estates
                .Select(e => e.Type)
                .Distinct()
                .OrderBy(t => t)
                .ToList();

            query.TotalEstates = totalEstates;
            query.Types = estateTypes;
            query.Estates = estates;

            return View(query);
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.UserIsDealer())
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            return View(new AddEstateFormModel
            {
                Categories = this.GetCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddEstateFormModel estate)
        {
            var dealerId = this.data
                .Dealers
                .Where(d => d.UserId == this.User.GetId())
                .Select(d => d.Id)
                .FirstOrDefault();

            if (dealerId == 0)
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            if (!this.data.Categories.Any(e => e.Id == estate.CategoryId))
            {
                this.ModelState.AddModelError(nameof(estate.CategoryId), "Category does not exist.");
            }
            if (!ModelState.IsValid)
            {
                estate.Categories = this.GetCategories();

                return View(estate);
            }

            var estateData = new Estate
            {
                Type = estate.Type,
                TypeOfConstruction = estate.TypeOfConstruction,
                Description = estate.Description,
                YearOfConstruction = estate.YearOfConstruction,
                Squaring = estate.Squaring,
                ImageUrl = estate.ImageUrl,
                CategoryId = estate.CategoryId,
                DealerId = dealerId
            };

            this.data.Estates.Add(estateData);

            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        private bool UserIsDealer()
            => this.data
                .Dealers
                .Any(d => d.UserId == this.User.GetId());
        private IEnumerable<EstateCategoryViewModel> GetCategories()
            => this.data
                .Categories
                .Select(e => new EstateCategoryViewModel
                {
                    Id = e.Id,
                    Name = e.Name
                })
                .ToList();
    }
}
