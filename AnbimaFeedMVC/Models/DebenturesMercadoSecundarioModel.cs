namespace AnbimaFeedMVC.Models
{
    public class DebenturesMercadoSecundarioModel
    {
        public string? Grupo { get; set; }
        public string? Codigo_ativo { get; set; }
        public string? Data_referencia { get; set; }
        public string? Data_vencimento { get; set; }
        public string? Percentual_taxa { get; set; }
        public float? Taxa_compra { get; set; }
        public float? Taxa_venda { get; set; }
        public float? Taxa_indicativa { get; set; }
        public float? Desvio_padrao { get; set; }
        public float? Val_min_intervalo { get; set; }
        public float? Val_max_intervalo { get; set; }
        public decimal? Pu { get; set; }
        public decimal? Percent_pu_par { get; set; }
        public decimal? Duration { get; set; }
        public string? Percent_reune { get; set; }
        public string? Emissor { get; set; }
    }
}
