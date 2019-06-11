using MediatR;
using StoreApi.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using StoreApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace StoreApi.Features.Marca
{
    public class Listar
    {
        public class Query : IRequest<Dto[]>
        {

        }

        public class Dto
        {
            public int Id { get; set; }

            public string Descricao { get; set; }

        }

        public class QueryHandler : IRequestHandler<Query, Dto[]>
        {
            private readonly StoreContext _storeContext;

            public QueryHandler(StoreContext storeContext)
            {
                _storeContext = storeContext;
            }

            public async Task<Dto[]> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _storeContext
                    .Set<Domain.Marca>()
                    .AsNoTracking()
                    .Select(s => new Dto
                    {
                        Id = s.Id,
                        Descricao = s.Descricao

                    }).ToArrayAsync();
            }
        }
    }
}
