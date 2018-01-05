/*
 
   Copyright 2018 Christian Chicoine

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CorpocastCommonModels.Models;

namespace CorpocastCommonApi.Controllers
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
