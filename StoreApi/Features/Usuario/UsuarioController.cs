using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.Features.Usuario
{
    public class UsuarioController : Controller
    {
        private readonly IMediator _mediator;

        public UsuarioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get(InsertEdit.Query query)
        {
            return Json(await _mediator.Send(query));
        }

        [HttpGet]
        public async Task<IActionResult> List(Listar.Query query)
        {
            return Json(await _mediator.Send(query));
        }

        [HttpPost]
        public async Task Insert(InsertEdit.Command command)
        {
            await _mediator.Send(command);
        }

        [HttpPut]
        public async Task Edit([FromBody]InsertEdit.Command command)
        {
            await _mediator.Send(command);
        }

        [HttpDelete]
        public async Task Delete(Excluir.Command command)
        {
            await _mediator.Send(command);
        }
    }
}
