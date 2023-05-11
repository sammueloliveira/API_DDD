using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Notifies
    {
        public Notifies()
        {
            Notificacoes = new List<Notifies>();
        }
        [NotMapped]
        public string NomePropriedade { get; set; }
        [NotMapped]
        public string mensagem { get; set; }
        [NotMapped]
        public List<Notifies> Notificacoes { get; set; }

        public bool ValidarPropriedadeString(string valor, string NomePropriedade)
        {
            if(string.IsNullOrWhiteSpace(valor) || string.IsNullOrWhiteSpace(NomePropriedade))
            {
                Notificacoes.Add(new Notifies { mensagem = "Campo obrigatorio", NomePropriedade = NomePropriedade });
                return false;
            }
            return true;
        }
        public bool ValidarPropriedadeInt(int valor, string NomePropriedade)
        {
            if (valor < 1 || string.IsNullOrWhiteSpace(NomePropriedade))
            {
                Notificacoes.Add(new Notifies { mensagem = "Valor deve ser maior que 0", NomePropriedade = NomePropriedade });
                return false;
            }
            return true;
        }
        public bool ValidarPropriedadeDecimal(decimal valor, string NomePropriedade)
        {
            if (valor < 1 || string.IsNullOrWhiteSpace(NomePropriedade))
            {
                Notificacoes.Add(new Notifies { mensagem = "Valor deve ser maior que 0", NomePropriedade = NomePropriedade });
                return false;
            }
            return true;
        }

    }
}
