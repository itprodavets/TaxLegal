using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReferenceBook.Application.Dto.Languages;
using ReferenceBook.Application.Queries.Interfaces;

namespace ReferenceBook.Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        private readonly ILanguageQueries _languageQueries;

        public LanguagesController(ILanguageQueries languageQueries)
        {
            _languageQueries = languageQueries ?? throw new ArgumentNullException(nameof(languageQueries));
        }

        [HttpGet("list")]
        [ProducesResponseType(typeof(IEnumerable<LanguageDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<LanguageDto>>> GetLanguagesAsync()
        {
            var countries = await _languageQueries.GetLanguagesAsync();
            return Ok(countries);
        }
    }
}