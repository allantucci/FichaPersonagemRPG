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
}
