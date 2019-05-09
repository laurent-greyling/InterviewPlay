using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace InterviewPlay.Controllers
{
    [Route("api/RespondentId")]
    public class RespondentIdController : Controller
    {
        [Route("GenerateId", Name = "respondentid")]
        public Guid GenerateId()
        {
            return Guid.NewGuid();
        }
    }
}