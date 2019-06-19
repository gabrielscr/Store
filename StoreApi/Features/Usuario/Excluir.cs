using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreApi.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StoreApi.Features.Usuario
{
    public class Excluir
    {
            public class Command : IRequest
            {
                public int Id { get; set; }
            }

            public class CommandHandler : AsyncRequestHandler<Command>
            {
                private readonly StoreContext _storeContext;

            public CommandHandler(StoreContext storeContext)
            {
                _storeContext = storeContext;
            }

            protected override async Task Handle(Command request, CancellationToken cancellationToken)
                {
                    var person = await GetUsuario(request);

                    if (person is null)
                        return;

                _storeContext.Remove(person);

                    await _storeContext.SaveChangesAsync();
                }

                private async Task<Domain.Usuario> GetUsuario(Command request)
                {
                    return await _storeContext
                        .Set<Domain.Usuario>()
                        .Where(e => e.Id == request.Id)
                        .FirstOrDefaultAsync();
                }
            }
        
    }
}
