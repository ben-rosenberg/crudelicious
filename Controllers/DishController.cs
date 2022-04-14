using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using CRUDelicious.Models;

namespace CRUDelicious.Controllers
{

public class DishController : Controller
{
    public DishController(CRUDeliciousContext context) { _db = context; }

    [HttpGet("/dishes/new")]
    public ViewResult New()
    {
        return View("New");
    }

    [HttpPost("/dishes/create")]
    public IActionResult Create(Dish dish)
    {
        if (!ModelState.IsValid) { return View("New"); }

        _db.Dishes.Add(dish);
        _db.SaveChanges();

        return RedirectToAction("Details", new { dishId = dish.DishId });
    }

    [HttpGet("dishes/{dishId}")]
    public IActionResult Details(int dishId)
    {
        Dish thisDish = _db.Dishes
            .FirstOrDefault(dish => dish.DishId == dishId);

        if (thisDish == null) { return RedirectToAction("Index", "Home"); }

        return View("Details", thisDish);
    }

    [HttpPost("dishes/{dishId}/delete")]
    public IActionResult Delete(int dishId)
    {
        Dish thisDish = _db.Dishes
            .FirstOrDefault(dish => dish.DishId == dishId);
        
        if (thisDish != null)
        {
            _db.Dishes.Remove(thisDish);
            _db.SaveChanges();
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpGet("/dishes/{dishId}/edit")]
    public IActionResult Edit(int dishId)
    {
        Dish thisDish = _db.Dishes
            .FirstOrDefault(dish => dish.DishId == dishId);
        
        if (thisDish == null) { return RedirectToAction("Index", "Home"); }

        return View("Edit", thisDish);
    }

    [HttpPost("/dishes/{dishId}/update")]
    public IActionResult Update(int dishId, Dish editedDish)
    {
        if (!ModelState.IsValid)
        {
            // editedDish.DishId = dishId; unnecessary, happens automatically, but good to know
            return View("Edit", editedDish);
        }

        Dish dbDish = _db.Dishes
            .FirstOrDefault(dish => dish.DishId == dishId);
        
        if (dbDish == null) { return RedirectToAction("Index", "Home"); }

        dbDish.Name = editedDish.Name;
        dbDish.Chef = editedDish.Chef;
        dbDish.Calories = editedDish.Calories;
        dbDish.Tastiness = editedDish.Tastiness;
        dbDish.Description = editedDish.Description;
        dbDish.UpdatedAt = DateTime.Now;

        _db.Dishes.Update(dbDish);
        _db.SaveChanges();

        return RedirectToAction("Details", new { dishId = dishId });
    }

    private CRUDeliciousContext _db;
}

}