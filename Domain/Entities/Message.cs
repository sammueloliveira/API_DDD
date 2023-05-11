using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("TB_MESSAGE")]
     public class Message : Notifies
    {
        [Column("MSG_ID")]
        public int Id { get; set; }
        [Column("MSG_TITULO")]
        [MaxLength(255)]
        public string Titulo { get; set; }
        [Column("MSG_ATIVO")]
        public bool Ativo { get; set; }
        [Column("MSG_DATA_CADASTRO")]
        public DateTime DataCadastro { get; set; }
        [Column("MSG_DATA_DE_ALTERACAO")]
        public DateTime DataAlteracao { get; set; }
        public string UserId { get; set; }

        [ForeignKey("UserId")]
      
        public ApplicationUser ApplicationUser { get; set; }

    }
}
