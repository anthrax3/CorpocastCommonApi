using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CorpocastFAQApi.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CorpocastFAQApi.Controllers
{
    [Route("api/[controller]")]
    public class FrequentlyAskedQuestionController : Controller
    {
        private readonly FrequentlyAskedQuestionContext _context;

        public FrequentlyAskedQuestionController(FrequentlyAskedQuestionContext context)
        {
            _context = context;

            //todo: Remove default values
            if (_context.FrequentlyAskedQuestions.Count() == 0)
            {
                BusinessEntity businessEntity = new BusinessEntity
                {
                    Id = 1,
                    Name = "Lebeau"
                };

                _context.FrequentlyAskedQuestions.Add(new FrequentlyAskedQuestion { ParentBusinessEntity= businessEntity, Answer = "A1", Question = "Q1" });
                _context.FrequentlyAskedQuestions.Add(new FrequentlyAskedQuestion { Answer = "A2", Question = "Q2" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<FrequentlyAskedQuestion> GetAll()
        {
            return _context.FrequentlyAskedQuestions.ToList();
        }

        [HttpGet("{id}", Name = "GetFrequentlyAskedQuestion")]
        public IActionResult GetById(long id)
        {

            var query = from e in _context.FrequentlyAskedQuestions
                        where e.Id == id
                        select new
                        {
                            e.Id,
                            e.Question,
                            e.Answer,
                            e.ParentBusinessEntity
                        };
   

            if (query == null)
            {
                return NotFound();
            }
            return new ObjectResult(query.FirstOrDefault());
        }


    }
}
