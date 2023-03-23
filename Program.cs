// See https://aka.ms/new-console-template for more information
using Lista_de_Tarefas;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Globalization;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using static System.Net.Mime.MediaTypeNames;

 string[] nome = new string[22];
 string[] _descricao = new string[50];
 string[] _vencimento = new string[10];
List<Tarefa> listaTarefas = new List<Tarefa>(); 

Console.WriteLine("*** Lista de Tarefas ***");
main();
Console.WriteLine("Deseja criar uma nova Tarefa? Digite (S) para Sim, (N) para Não");

void main()
{
    criaMenu();
    int numero;
    //tenta converter para inteiro
    bool resultado = Int32.TryParse(Console.ReadLine(), out numero);

    if (resultado)
    {
        processaMenu(numero);
    }
    else
    {
        Console.WriteLine("Digite um número válido!");
        main();
    }
}

void criaMenu()
{
    string menu = ("\n============================================") +
        "\n" +
        "Digite uma das opções abaixo:\n" +
        "\n    0 - Sair do Sistema " +
        "\n    1 - Criar Tarefas" +
        "\n    2 - Consultar Tarefa" +
        "\n    3 - Editar Tarefas" +
        "\n    4 - Excluir Tarefas" +
        "\n    5 - Listar Tarefas" +
        "\n";

    Console.WriteLine(menu);
}

void processaMenu(int valor)
{
    switch (valor)
    {
        case  0:
            Environment.Exit(1);
                
            break;
        case 1:
            CriarTarefa();
            main();
            break;

        case 2:
            ConsultarTarefa();
            main();
            break;
        case 3:
            EditarTarefa();
            main();
            break;
        case 4:
            ExcluirTarefa();
            main();
            break;
        case 5:
            ListarTarefas();
            main();
            break;

        default:
            Console.WriteLine($"Measured value is.");
            break;
    }
}

void ExcluirTarefa(){
    int index = ConsultarTarefa();
    Tarefa tarefa = listaTarefas[index];
    if (tarefa != null)
    {
        Console.WriteLine("Deseja Excluir esta Tarefa?");

        exibeTarefa(tarefa);
        string menu = 
      "\n    Y - Sim " +
      "\n    N - Não" +
      "\n";

        Console.WriteLine(menu);

        var resposta = Console.ReadLine().ToUpper();
        if (resposta != null)
        {
            if (resposta == "Y")
            {
                listaTarefas.Remove(tarefa);
            }            
        }
    }
}

    int ConsultarTarefa()
{
    Console.WriteLine("Busque pelo Título da Tarefa:");
    string titulo = Console.ReadLine();
    int cont = 0;
    foreach (Tarefa t in listaTarefas)
    {        
        if (t.Titulo.Contains(titulo))
        {
            exibeTarefa(t);
            cont++;
            return listaTarefas.IndexOf(t);
           
        }
    }
    if (cont == 0)
    {
        Console.WriteLine("Nenhum tarefa encontrada!");
        return 0;
    }
    return 0;
}


void EditarTarefa()
{
    int index = ConsultarTarefa();
    Tarefa tarefa = listaTarefas[index];
    if (tarefa != null)
    {
        Tarefa tarefaAtualizada = MontaTarefa(1);

        if (!existeTarefa(tarefaAtualizada))
        {
            listaTarefas[index] = tarefaAtualizada;
            Console.WriteLine("Tarefa atualizada com sucesso!");
        }
        else
        {
            Console.WriteLine("Essa tarefa já existe!");

        }
    }
}
void ListarTarefas()
{
    if(listaTarefas.Count > 0)
    {
        foreach (Tarefa tarefa in listaTarefas)
        {
            exibeTarefa(tarefa);
        }
    }
    else
    {
        Console.WriteLine("Nenhuma tarefa encontrada!");
    }
}

void exibeTarefa(Tarefa tarefa)
{
    Console.WriteLine("============================================");
    Console.WriteLine("Título: " + tarefa.Titulo);
    Console.WriteLine("Descrição: " + tarefa.Descricao);
    Console.WriteLine("Vencimento: " + tarefa.Vencimento.ToString().Substring(0, 10));
    Console.WriteLine("============================================");
}

bool validaData(string dataStr)
{
    DateTime valor;
    if (DateTime.TryParseExact(dataStr, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out valor))
    {
        if (valor > DateTime.Now)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    else
    {
        return false;
    }
}

Tarefa MontaTarefa(int tipoAcao)
{
    string acao = "";
    if(tipoAcao== 0)
    {
        acao = "Digite";
    }
    else 
    {
        acao = "Edite";
    }

    Tarefa tarefa = new Tarefa();
    Console.WriteLine(acao+" o Título da Tarefa:");
    tarefa.Titulo = Console.ReadLine();

    Console.WriteLine(acao +" a descrição da Tarefa:");
    tarefa.Descricao = Console.ReadLine();

    Console.WriteLine(acao + " a data da Tarefa:");
    string dataStr = Console.ReadLine();
    Console.WriteLine(acao + " o horário da Tarefa:");
    string horaStr = Console.ReadLine();

    while (!validaData(dataStr + " " + horaStr))
    {

        Console.WriteLine("Data e horário inválidos!");
        Console.WriteLine(acao + " a data da tarefa!");
        dataStr = Console.ReadLine();
        Console.WriteLine(acao + " o horário da Tarefa:");
        horaStr = Console.ReadLine();

    }

    tarefa.Vencimento = Convert.ToDateTime(dataStr);

    return tarefa;
}
void CriarTarefa()
{
    Tarefa tarefa = MontaTarefa(0);

    if (!existeTarefa(tarefa))
    {
        listaTarefas.Add(tarefa);
        Console.WriteLine("Tarefa criada com sucesso!");
    }
    else
    {
        Console.WriteLine("Essa tarefa já existe!");

    }
}

bool existeTarefa(Tarefa tarefa)
{
    bool existe = false;    
    // Consultar se existe essa tarefa 
    foreach (Tarefa t in listaTarefas)
    {
        if (t.Titulo == tarefa.Titulo &&
            t.Descricao == tarefa.Descricao &&
            t.Vencimento == tarefa.Vencimento)
        {
            existe = true;
            return existe;
        }
        else
        {
            existe = false;
            return existe;
        }
    }
    return existe;
}

bool processaDecisao(string decisao)
{
    if (decisao.Equals("S", StringComparison.InvariantCultureIgnoreCase))
    {
        return true;        
    }
    else if (decisao.Equals("N", StringComparison.InvariantCultureIgnoreCase))
    {
        return false;
    }
    else
    {
        Console.WriteLine("Digite um valor válido!");
        return false;
    }
}


string formataNome(string nomeProd)
{
    //verifica o tamanho e corta o final do array
    if (nomeProd.Length > 21)
    {
        for (int i = 21; i < nomeProd.Length; i++)
        {
            nomeProd = nomeProd.Remove(i, nomeProd.Length - 22);
        }
    }

    //preencho array nome
    Console.WriteLine(nomeProd);
    for (int i = 0; i < 22; i++)
    {
        nome[i] = " ";
    }
    //cria um array para receber o nome do produto
    char[] nomeArray;
    //transforma o nomeProd em array e atribui ao nomeArray
    nomeArray = nomeProd.ToCharArray(0, nomeProd.Length);

    Console.WriteLine(String.Join("", nomeArray));
    //cria o loop partindo do final do resto ate o fim
    for (int i = 0; i < nomeArray.Length; i++)
    {
        nome[i] = nomeArray[i].ToString();
    }
    string result = String.Join("", nome);
    return result;
}

//formata preço do produto para criação do arquivo para a balança
string formataDescricao(string descricao)
{
    Console.WriteLine(descricao);
    //verifica o tamanho e corta o final do array
    if (descricao.Length > 50)
    {
        for (int i = 50; i < descricao.Length; i++)
        {
            descricao = descricao.Remove(i, descricao.Length - 51);
        }
    }

    //preencho array nome
    Console.WriteLine(descricao);
    for (int i = 0; i < 50; i++)
    {
        _descricao[i] = " ";
    }
    //cria um array para receber o nome do produto
    char[] nomeArray;
    //transforma o nomeProd em array e atribui ao nomeArray
    nomeArray = descricao.ToCharArray(0, descricao.Length);

    Console.WriteLine(String.Join("", nomeArray));
    //cria o loop partindo do final do resto ate o fim
    for (int i = 0; i < nomeArray.Length; i++)
    {
        _descricao[i] = nomeArray[i].ToString();
    }
    string result = String.Join("", _descricao);
    return result;
}

//formata preço do produto para criação do arquivo para a balança
string formataVencimento(string data)
{
    data = data.Substring(0, 10);
    //preencho array 
    Console.WriteLine(data);
    for (int i = 0; i < 10; i++)
    {
        _vencimento[i] = " ";
    }
    //cria um array para receber o nome do produto
    char[] nomeArray;
    //transforma o nomeProd em array e atribui ao nomeArray
    nomeArray = data.ToCharArray(0, data.Length);

    Console.WriteLine(String.Join("", nomeArray));
    //cria o loop partindo do final do resto ate o fim
    for (int i = 0; i < nomeArray.Length; i++)
    {
        _vencimento[i] = nomeArray[i].ToString();
    }
    string result = String.Join("", _vencimento);
    return result;
}




class Tarefa
{
    public int Numero { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public DateTime Vencimento { get; set; }

}
