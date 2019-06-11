using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreApi.Domain;
using StoreApi.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StoreApi.Features.Marca
{
    public class InserirEditar
    {
        public class Query : IRequest<Command>
        {
            public int? Id { get; set; }
        }

        public class Command : IRequest
        {
            public int? Id { get; set; }

            public string Descricao { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, Command>
        {
            private readonly StoreContext _storeContext;

            public QueryHandler(StoreContext storeContext)
            {
                _storeContext = storeContext;
            }

            public async Task<Command> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _storeContext
                    .Set<Domain.Marca>()
                    .AsNoTracking()
                    .Where(w => w.Id == request.Id)
                    .Select(s => new Command
                    {
                        Id = s.Id,
                        Descricao = s.Descricao
                    }).FirstOrDefaultAsync();
            }
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
                var marcas = await ObterMarcas(request);

                MapearMarcas(marcas, request);

                await _storeContext.SaveChangesAsync();
            }

            private void MapearMarcas(Domain.Marca marcas, Command request)
            {
                marcas.Id = request.Id.Value;
                marcas.Descricao = request.Descricao;
            }

            private async Task<Domain.Marca> ObterMarcas(Command request)
            {
                if(!request.Id.HasValue)
                {
                    var marca = new Domain.Marca();

                    await _storeContext.AddAsync(marca);

                    return marca;
                }

                return await _storeContext
                    .Set<Domain.Marca>()
                    .FirstOrDefaultAsync(e => e.Id == request.Id);
            }
        }
    }
}
