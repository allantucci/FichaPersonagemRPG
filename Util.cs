using System;
using System.Collections.Generic;

static class Util
{
    public static List<int> RolarAtributos()
    {
        Random rnd = new Random();
        List<int> valores = new List<int>();
        for (int i = 0; i < 6; i++)
        {
            int[] dados = new int[4];
            for (int j = 0; j < 4; j++)
                dados[j] = rnd.Next(1, 7);
            Array.Sort(dados);
            valores.Add(dados[1] + dados[2] + dados[3]);
        }
        return valores;
    }

    public static void DistribuirAtributos(Personagem personagem, List<string> nomesAtributos, List<int> valoresRolados)
    {
        foreach (var nome in nomesAtributos)
        {
            int escolhido = -1;
            while (true)
            {
                Console.WriteLine($"\nEscolha o valor para {nome} entre: {string.Join(", ", valoresRolados)}");
                string input = Console.ReadLine();

                if (int.TryParse(input, out escolhido) && valoresRolados.Contains(escolhido))
                {
                    personagem.Atributos.Add(new Atributo { Nome = nome, Valor = escolhido });
                    valoresRolados.Remove(escolhido);
                    break;
                }
                else
                {
                    Console.WriteLine("Valor inválido. Tente novamente.");
                }
            }
        }
    }

    public static void EscolherPericias(Personagem personagem)
    {
        int qtdEscolhas = personagem.Classe.QuantidadePericias;
        personagem.Pericias = new List<Pericia>();

        Console.WriteLine($"Você deve escolher {qtdEscolhas} perícias:");

        while (personagem.Pericias.Count < qtdEscolhas)
        {
            Console.WriteLine("Perícias disponíveis:");
            for (int i = 0; i < personagem.Classe.PericiasDisponiveis.Count; i++)
                Console.WriteLine($"{i + 1} - {personagem.Classe.PericiasDisponiveis[i].Nome}");

            Console.Write("Digite o número da perícia escolhida: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int escolha) &&
                escolha >= 1 && escolha <= personagem.Classe.PericiasDisponiveis.Count)
            {
                var pericia = personagem.Classe.PericiasDisponiveis[escolha - 1];

                // Evita duplicata
                if (!personagem.Pericias.Contains(pericia))
                {
                    pericia.Proficiente = true;
                    personagem.Pericias.Add(pericia);
                    personagem.Classe.PericiasDisponiveis.Remove(pericia);
                    Console.WriteLine($"Você escolheu {pericia.Nome}!");
                }
                else
                {
                    Console.WriteLine("Você já escolheu essa perícia!");
                }
            }
            else
            {
                Console.WriteLine("Escolha inválida. Tente novamente!");
            }
        }

        Console.WriteLine("Todas as perícias foram escolhidas!");
    }


    public static Classe EscolherClasse(List<Classe> classe)
    {
        while (true)
        {
            Console.WriteLine("Escolha a classe:");
            for (int i = 0; i < classe.Count; i++)
                Console.WriteLine($"{i + 1} - {classe[i].Nome}");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int escolha) && escolha >= 1 && escolha < classe.Count)
                {
                    return classe[escolha - 1];
                }
                else
                {
                    Console.WriteLine("Classe inválida!");
                }
            
        }
    }

    public static Raca EscolherRaca(List<Raca> raca)
    {
        while (true)
        {
            Console.WriteLine("Escolha a raça:");
            for (int i = 0; i < raca.Count; i++)
                Console.WriteLine($"{i + 1} - {raca[i].Nome}");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int escolha) && escolha >= 1 && escolha < raca.Count)
            {
                return raca[escolha - 1];
            }
            else
            {
                Console.WriteLine("Raça inválida!");
            }

        }
    }
    public static void AplicarBonusRacial(Personagem personagem)
    {
        foreach (var bonus in personagem.Raca.Bonus)
        {
            var atributo = personagem.Atributos.First(a => a.Nome == bonus.Nome);
            atributo.Valor += bonus.Valor;
        }
    }
}
