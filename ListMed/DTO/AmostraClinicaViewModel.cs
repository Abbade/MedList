using ListMed.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ListMed.DTO
{
    public class AmostraClinicaViewModel
    {
        [Required(ErrorMessage = "Informe o nome!")]
        [MaxLength(180, ErrorMessage = "Digite no máximo 180 caracteres!")]
        public string Nome { get; set; }

        [MaxLength(80, ErrorMessage = "Digite no máximo 80 caracteres!")]
        public string Site { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        [Range(0.0, Double.PositiveInfinity)]
        public decimal? PrecoConsulta { get; set; }

        [Range(0.0, Double.PositiveInfinity)]
        public decimal? PrecoExame { get; set; }

        public TimeSpan? HoraAbertura { get; set; }

        public TimeSpan? HoraFechamento { get; set; }

        [Required(ErrorMessage = "Informe o estado!")]
        public int IdEstado { get; set; }

        [Required(ErrorMessage = "Informe a cidade!")]
        public int IdCidade { get; set; }

        [Required(ErrorMessage = "Informe o bairro!")]
        public int IdBairro { get; set; }

        public  List<TelefonesClinica> TelefonesClinicas { get; set; }

    }
}