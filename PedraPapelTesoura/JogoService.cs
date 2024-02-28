using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("PedraPapeTesouraTest")]
namespace PedraPapelTesoura
{
    public class JogoService
    {
        /// <summary>
        /// Método que avalia uma rodada do jogo.
        /// </summary>
        /// <param name="forma1">A <see cref="Forma">Forma</see> escolhida pelo(a) primeiro(a) jogador(a).</param>
        /// <param name="forma2">A <see cref="Forma">Forma</see> escolhida pelo(a) segundo(a) jogador(a)</param>
        /// <returns>A <see cref="Forma">Forma</see> vencedora, ou <c>null</c> em caso de empate.</returns>
        public Forma? Jogar(Forma forma1, Forma forma2)
        {
            if (forma1 == forma2)
                return default;

            if (forma1.GanhaDe(forma2))
                return forma1;
            else if (forma2.GanhaDe(forma1))
                return forma2;

            throw new InvalidOperationException($"Combinação de Formas inválidas: {forma1} x {forma2}");
        }

        public string? Jogar(string forma1, string forma2)
        {
            var forma1Convertida = ConverterParaForma(forma1);
            if (forma1Convertida == default)
                throw new InvalidOperationException($"A forma '{forma1}' não é válida");

            var forma2Convertida = ConverterParaForma(forma2);
            if (forma2Convertida == default)
                throw new InvalidOperationException($"A forma '{forma2}' não é válida.");

            return Jogar(forma1Convertida!.Value, forma2Convertida!.Value)
                ?.ToString();
        }

        private Forma? ConverterParaForma(string descricao)
        {
            if (Enum.TryParse(descricao, out Forma forma))
                return forma;
            else
                return default;
        }
    }

    public static class Extensions
    {
        public static bool GanhaDe(this Forma forma1, Forma forma2)
        {
            IDictionary<Forma, (Forma a, Forma b)> ganhaDe = new Dictionary<Forma, (Forma a, Forma b) >() 
            { 
                { Forma.Pedra, (Forma.Tesoura, Forma.Lagarto) },
                { Forma.Tesoura, (Forma.Papel, Forma.Lagarto) },
                { Forma.Papel, (Forma.Pedra, Forma.Spock) },
                { Forma.Spock, (Forma.Tesoura, Forma.Pedra) },
                { Forma.Lagarto, (Forma.Spock, Forma.Papel) },
            };
            if (ganhaDe.TryGetValue(forma1, out var value)) return value.a == forma2 || value.b == forma2;
            return false;
        }
    }
}
