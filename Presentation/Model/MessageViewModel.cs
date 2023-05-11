﻿using Domain.Entities;

namespace Presentation.Model
{
    public class MessageViewModel
    {
       
        public int Id { get; set; }
        
      
        public string Titulo { get; set; }
        
        public bool Ativo { get; set; }
        
        public DateTime DataCadastro { get; set; }
        
        public DateTime DataAlteracao { get; set; }
        public string UserId { get; set; }
        
        
    }
}
