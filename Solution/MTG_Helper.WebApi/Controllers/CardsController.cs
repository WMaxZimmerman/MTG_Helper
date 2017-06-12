using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MTG_Helper.DAL.Repositories;

namespace MTG_Helper.WebApi.Controllers
{
    public class CardsController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetCards()
        {
            try
            {
                var cards = CardRepository.GetAllCards();
                return Ok(cards);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return InternalServerError(e);
            }
        }
    }
}