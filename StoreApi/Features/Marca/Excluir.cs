using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreApi.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StoreApi.Features.Marca
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
                var marca = await _storeContext
                    .Set<Domain.Marca>()
                    .FirstOrDefaultAsync(p => p.Id == request.Id);

                _storeContext.Remove(marca);

                await _storeContext.SaveChangesAsync();
            }
        }
    }
}
