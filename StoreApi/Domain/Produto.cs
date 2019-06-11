namespace StoreApi.Domain

{
    public class Produto
    {
        public int Id { get; set; }

        public string Descricao { get; set; }

        public double Valor { get; set; }

        public bool Ativo { get; set; }

        public UnidadeMedidaEnum UnidadeMedida { get; set; }

        public int MarcaId { get; set; }

        public Marca Marca { get; set; }
    }
}
