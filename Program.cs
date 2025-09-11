using System;
using System.Collections.Generic;
using System.Linq;


class Atributo
{
    public string Nome { get; set; }
    public int Valor { get; set; }
    public int Modificador => (Valor - 10) / 2;
}

class Pericia
{
    public string Nome { get; set; }
    public string AtributoLigado { get; set; }
    public bool Proficiente { get; set; }
    public int ValorTotal(List<Atributo> atributos, int bonusProficiencia)
    {
        var atributo = atributos.First(a => a.Nome == AtributoLigado);
        return atributo.Modificador + (bonusProficiencia);
        //return atributo.Modificador + (Proficiente ? bonusProficiencia : 0);
    }
}

class Raca
{
    public string Nome { get; set; }
    public List<Atributo> Bonus { get; set; }

    
}

class Classe
{
    public string Nome { get; set; }
    public int QuantidadePericias { get; set; }
    public List<Pericia> PericiasDisponiveis { get; set; }
    public int VidaInicial { get; set; }
}

class Personagem
{
    public string Nome { get; set; }
    public Raca Raca { get; set; }
    public Classe Classe { get; set; }
    public int BonusProficiencia { get; set; } = 2;
    public List<Atributo> Atributos { get; set; }
    public List<Pericia> Pericias { get; set; }
}

class Program
{
    static void Main()
    {
        // 1 - Lista de raças
        List<Raca> racas = new List<Raca>()
        {
            new Raca { Nome = "Humano", Bonus = new List<Atributo>
                {
                    new Atributo { Nome = "Força", Valor = 1 },
                    new Atributo { Nome = "Destreza", Valor = 1 },
                    new Atributo { Nome = "Constituição", Valor = 1 },
                    new Atributo { Nome = "Inteligência", Valor = 1 },
                    new Atributo { Nome = "Sabedoria", Valor = 1 },
                    new Atributo { Nome = "Carisma", Valor = 1 }
                }
            },
            new Raca { Nome = "Elfo", Bonus = new List<Atributo>
                {
                    new Atributo { Nome = "Destreza", Valor = 2 }
                }
            }
        };

        // 2 - Lista de classes
        List<Classe> classes = new List<Classe>()
        {
            new Classe { Nome = "Guerreiro", QuantidadePericias = 2,
                PericiasDisponiveis = new List<Pericia>
                {
                    new Pericia { Nome = "Atletismo", AtributoLigado = "Força" },
                    new Pericia { Nome = "Intimidação", AtributoLigado = "Carisma" },
                    new Pericia { Nome = "Sobrevivência", AtributoLigado = "Sabedoria" },
                    new Pericia { Nome = "História", AtributoLigado = "Inteligência" }
                },
                VidaInicial = 10
            },
            new Classe { Nome = "Ladino", QuantidadePericias = 4,
                PericiasDisponiveis = new List<Pericia>
                {
                    new Pericia { Nome = "Furtividade", AtributoLigado = "Destreza" },
                    new Pericia { Nome = "Acrobacia", AtributoLigado = "Destreza" },
                    new Pericia { Nome = "Prestidigitação", AtributoLigado = "Destreza" },
                    new Pericia { Nome = "Persuasão", AtributoLigado = "Carisma" },
                    new Pericia { Nome = "Enganação", AtributoLigado = "Carisma" }
                },
                VidaInicial = 8
            }
        };

        // 3 - Lista geral de perícias do jogo
        List<Pericia> todasPericias = new List<Pericia>()
        {
            new Pericia { Nome = "Acrobacia", AtributoLigado = "Destreza" },
            new Pericia { Nome = "Arcanismo", AtributoLigado = "Inteligência" },
            new Pericia { Nome = "Atletismo", AtributoLigado = "Força" },
            new Pericia { Nome = "Atuação", AtributoLigado = "Carisma" },
            new Pericia { Nome = "Enganação", AtributoLigado = "Carisma" },
            new Pericia { Nome = "História", AtributoLigado = "Inteligência" },
            new Pericia { Nome = "Intimidação", AtributoLigado = "Carisma" },
            new Pericia { Nome = "Intuição", AtributoLigado = "Sabedoria" },
            new Pericia { Nome = "Investigação", AtributoLigado = "Inteligência" },
            new Pericia { Nome = "Medicina", AtributoLigado = "Sabedoria" },
            new Pericia { Nome = "Natureza", AtributoLigado = "Inteligência" },
            new Pericia { Nome = "Percepção", AtributoLigado = "Sabedoria" },
            new Pericia { Nome = "Persuasão", AtributoLigado = "Carisma" },
            new Pericia { Nome = "Prestidigitação", AtributoLigado = "Destreza" },
            new Pericia { Nome = "Religião", AtributoLigado = "Inteligência" },
            new Pericia { Nome = "Sobrevivência", AtributoLigado = "Sabedoria" },
            new Pericia { Nome = "Furtividade", AtributoLigado = "Destreza" }
        };

        // 4 - Criar personagem
        Personagem personagem = new Personagem();

        Console.Write("Digite o nome do personagem: ");
        personagem.Nome = Console.ReadLine();

        // Escolha da raça
        Console.WriteLine("\nEscolha a raça:");
        for (int i = 0; i < racas.Count; i++)
            Console.WriteLine($"{i + 1} - {racas[i].Nome}");
        int escolhaRaca = int.Parse(Console.ReadLine()) - 1;
        personagem.Raca = racas[escolhaRaca];

        // Escolha da classe
        /*Console.WriteLine("\nEscolha a classe:");
        for (int i = 0; i < classes.Count; i++)
            Console.WriteLine($"{i + 1} - {classes[i].Nome}");
        int escolhaClasse = int.Parse(Console.ReadLine()) - 1;*/

        personagem.Classe = Util.EscolherClasse(classes);

        // 5 - Rolagem de atributos (4d6 descartando o menor)
        List<int> valoresRolados = Util.RolarAtributos();
        Console.WriteLine("\nValores rolados: " + string.Join(", ", valoresRolados));

        // Inicializar atributos do personagem
        personagem.Atributos = new List<Atributo>();

        List<string> nomesAtributos = new List<string>
        { "Força", "Destreza", "Constituição", "Inteligência", "Sabedoria", "Carisma" };

        // 6 - Distribuição manual de atributos


        Util.DistribuirAtributos(personagem, nomesAtributos, valoresRolados);

        // 7 - Aplicar bônus racial
        foreach (var bonus in personagem.Raca.Bonus)
        {
            var atributo = personagem.Atributos.First(a => a.Nome == bonus.Nome);
            atributo.Valor += bonus.Valor;
        }

        // 8 - Escolha de perícias da classe
        personagem.Pericias = new List<Pericia>();
        Console.WriteLine($"\nEscolha {personagem.Classe.QuantidadePericias} perícias da lista abaixo:");

        for (int i = 0; i < personagem.Classe.PericiasDisponiveis.Count; i++)
            Console.WriteLine($"{i + 1} - {personagem.Classe.PericiasDisponiveis[i].Nome}");

        for (int i = 0; i < personagem.Classe.QuantidadePericias; i++)
        {
            int escolha = int.Parse(Console.ReadLine()) - 1;
            var pericia = personagem.Classe.PericiasDisponiveis[escolha];
            pericia.Proficiente = true;
            personagem.Pericias.Add(pericia);
        }

        // 9 - Imprimir ficha
        Console.WriteLine($"\n--- FICHA DE {personagem.Nome} ---");
        Console.WriteLine($"Vida total: {personagem.Classe.VidaInicial + personagem.Atributos.First(a => a.Nome == "Constituição").Modificador}");
        Console.WriteLine($"Raça: {personagem.Raca.Nome}");
        Console.WriteLine($"Classe: {personagem.Classe.Nome}");
        Console.WriteLine($"Bônus de Proficiência: {personagem.BonusProficiencia}");

        Console.WriteLine("\nAtributos:");
        foreach (var atributo in personagem.Atributos)
            Console.WriteLine($"{atributo.Nome}: {atributo.Valor} (Mod: {atributo.Modificador})");

        Console.WriteLine("\nPerícias do personagem:");
        foreach (var pericia in personagem.Pericias)
        {
            int valor = pericia.ValorTotal(personagem.Atributos, personagem.BonusProficiencia);
            Console.WriteLine($"{pericia.Nome}: {valor} (Proficiente)");
        }

        Console.WriteLine("\nDemais perícias do jogo:");
        foreach (var pericia in todasPericias)
        {
            bool v = personagem.Pericias.Any(p => p.Nome == pericia.Nome);
            bool proficiente = v;
            int valor = pericia.ValorTotal(personagem.Atributos, proficiente ? personagem.BonusProficiencia : 0);
            Console.WriteLine($"{pericia.Nome}: {valor} {(proficiente ? "(Proficiente)" : "")}");
        }
    }
   

}


