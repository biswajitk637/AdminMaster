using AdminMaster.Respository.Interface;
using AdminMaster.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminMaster.Controllers
{
    public class BooksController : Controller
    {
        public GenericInterface<BookWithAuthorViewModel> BookService { get; }
        public IBook BookService2 { get; }

        public BooksController(
            GenericInterface<BookWithAuthorViewModel> _book,
            IBook book
            )
        {
            BookService = _book;
            BookService2 = book;
        }

    

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetBooks()
        {
            var books = BookService.GetData();
            return Json(books);
        } 
        
        [HttpPost]
        public JsonResult DeleteBook(int id)
        {
            var result = BookService2.DeleteBook(id);
            if(result)
            {
                return Json(new { mesage="book deleted succesfully",ok=true });
            }
            return Json(new { mesage = "book not deleted succesfully", ok = false });
        }
    }
}
